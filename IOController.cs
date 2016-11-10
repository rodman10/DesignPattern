using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FileSystem.EntryInterface;

namespace FileSystem
{
    public class IOController
    {
        private int workDir=0;      //当前工作目录
        private int selectedFile = 0;       //选中的文件或文件夹
        private Entry entry;
        private ListView listView;
        private List<int> lastDir = new List<int>();        //记录上一个访问的文件
        private MemoryInterface memory = new MemoryInterface();


        public IOController(ListView listView) 
        {
            this.listView = listView;
            memory.loadMemory();    
            listItems(workDir);     
        }


        public void back()
        {
            int node_table_block = memory.getInodeByIndex(workDir).getBlock(0);     //inode_table所在块号
            int parent = memory.getDataBlockByIndex(node_table_block).findInode("..");
            if ( parent == -1)
            {
                return;
            }
            lastDir.Insert(0, workDir);
            workDir = parent;
            listItems(workDir);
        }

        public void forward()
        {
            if (lastDir.Count == 0)
            {
                return;
            }
            workDir = lastDir[0];
            lastDir.RemoveAt(0);
            listItems(workDir);
        }

        private void listItems(int _index)          //列出文件夹下的文件
        {
            listView.Items.Clear();
            
            for (int i = 0; ; i++)
            {
                string _name = memory.getDataBlockByIndex(memory.getInodeByIndex(_index).getBlock(0)).findInode(i);
                if (_name == null)
                {
                    break;
                }

                if( _name.Equals("..") || _name.Equals("."))
                {
                    continue;
                }
                int size = -1;
                inode _node = memory.getInodeByIndex(memory.getInodeIndexByName(_index, _name));
                string type = _node.getType();
                DateTime time = _node.getTime();
                if(type.Equals("文件"))
                {
                    size=calculate(_node, _name);
                }
                setViewItem(_name, type,size,time);
            }
        }

        private int calculate(inode _node,string _name)     //计算文件大小
        {
            int[] ptr = _node.getBlockPtr();
            string content = "";
            for (int i = 0; i < 13; i++)        //遍历该文件的所有块，计算大小
            {
                if (memory.getDataBlockByIndex(ptr[i]) != null && memory.getDataBlockByIndex(ptr[i]).data != null && !memory.getDataBlockByIndex(ptr[i]).data.Equals(""))
                {
                    content += memory.getDataBlockByIndex(ptr[i]).data;
                    continue;
                }
                break;
            }
            byte[] sarr = Encoding.Default.GetBytes(content);
            return sarr.Length;
        }

        private void setViewItem(string _name, string type,int size,DateTime _time)     //显示文件属性
        {
            ListViewItem item=new ListViewItem();
            item.Text=_name;
            item.SubItems.Add(type);
            if (type.Equals("文件夹"))
            {
                item.SubItems.Add("");
                item.SubItems.Add(_time.ToString());
                item.ImageIndex = 0;
            }
            else
            {
                item.SubItems.Add(size.ToString()+"B");
                item.SubItems.Add(_time.ToString());
                item.ImageIndex = 1;
            }
            listView.Items.Add(item);  
        }

        public Boolean createEntry(string type,string _name)        //创建新的文件
        {
            if (type.Equals("文件夹"))
            {
                entry = new Directory(ref memory);
            }
            else
            {
                entry = new File(ref memory);
            }
            string name = entry.createEntry(workDir, _name);
            if (name == null)
            {
                return false;
            }

            if (_name == null)
            {
                setViewItem(name,type,0,DateTime.Now);
            }
            
            return true;
        }

        public void reNameEntry(string newName,int _index)       //重命名文件
        {
            entry = new Directory(ref memory);
            entry.reNameEntry(workDir, newName, _index);
        }

        public void removeEntry(string name,inode n,int index)
        {
            listView.Items.RemoveAt(index);
            entry = new Directory(ref memory);
            entry.removeEntry(workDir, name, n, index);
        }

        public Boolean reDirectCatalog(string name)     //切换文件目录或打开文件
        {
            entry = new Directory(ref memory);           
            if (entry.openEntry(name,ref selectedFile,ref workDir)==null)
            {
                return true;        //是文件直接打开
            }
            listItems(workDir);     //是文件夹进入目录
            return false;
        }

        public string readFile()
        {
            entry = new File(ref memory);
            return entry.openEntry(null, ref selectedFile, ref workDir);
           
        }

        public Boolean saveFile(string content)
        {
            entry = new File(ref memory);
            
            if(entry.write(content , ref selectedFile,ref workDir) == false)
            {
                return false;
            }
            listItems(workDir);
            return true;
        }

    }
}
