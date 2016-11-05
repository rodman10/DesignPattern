using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace FileSystem
{
    public class MemoryManage
    {
        private int workDir=0;      //当前工作目录
        private int selectedFile = 0;       //选中的文件或文件夹
        private inode[] node = new inode[1024];         //第一个块中inode信息
        private dataBlock[] datablock = new dataBlock[4096];        //模拟磁盘块的数组
        private inodeBitmap nodeMap = new inodeBitmap();        //inode的位图
        private blockBitmap blockMap = new blockBitmap();       //磁盘块的位图
        private ListView listView;
        private List<int> lastDir = new List<int>();        //记录上一个访问的文件
      
        private void initDir( int index , int parent , int current )
        {
            datablock[index].createInode("..", parent);
            datablock[index].createInode(".", current);
        }

        private void initBoot( Stream stream , BinaryFormatter formatter)
        {
            inode root = new inode();
            List<int> b = new List<int>();
            b.Add(1);
            root.init(0, b, "文件夹" , DateTime.Now);
            node[0] = root;
            dataBlock data = new dataBlock();
            blockMap.findUnuse(2);
            List<int> nodes = nodeMap.findUnuse(1);
            data.setBmap(blockMap);
            data.setImap(nodeMap);
            data.setNode(node);
            datablock[0] = data;
            datablock[1] = new dataBlock();
            initDir( 1 , -1, nodes[0]);
            formatter.Serialize(stream, datablock);   
        }


        public MemoryManage(ListView listView) 
        {
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            this.listView = listView;
            if( 0 == stream.Length)
            {
                initBoot(stream, formatter);
            }
            stream.Position = 0;
            datablock = (dataBlock[])formatter.Deserialize(stream);
            node = datablock[0].getNode();
            blockMap = datablock[0].getBmap();
            nodeMap = datablock[0].getImap();
            stream.Close();
            listItems(workDir);
            
          
        }


        public void back()
        {
            int node_table_block = node[workDir].getBlock(0);     //inode_table所在块号
            int parent = datablock[node_table_block].findInode("..");
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
            inode _node = node[_index];
            for (int i = 0; ; i++)
            {
                string _name = datablock[_node.getBlock(0)].findInode(i);
                if (_name == null)
                {
                    break;
                }

                if( _name.Equals("..") || _name.Equals("."))
                {
                    continue;
                }
                int size = -1;
                string type = node[datablock[_node.getBlock(0)].findInode(_name)].getType();
                DateTime time = node[datablock[_node.getBlock(0)].findInode(_name)].getTime();
                if(type.Equals("文件"))
                {
                    size=calculate(_node, _name);
                }
                setViewItem(_name, type,size,time);
            }
        }

        private int calculate(inode _node,string _name)     //计算文件大小
        {
            int[] ptr = node[datablock[_node.getBlock(0)].findInode(_name)].getBlockPtr();
            string content = "";
            for (int i = 0; i < 13; i++)        //遍历该文件的所有块，计算大小
            {
                if (datablock[ptr[i]] != null && datablock[ptr[i]].getContent() != null && !datablock[ptr[i]].getContent().Equals(""))
                {
                    content += datablock[ptr[i]].getContent();
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
            int parent = -1;
            inode _node = null;
            if (_name != null)      //点击文件夹创建
            {
                parent = datablock[node[workDir].getBlock(0)].findInode(_name);     //选中文件夹为父目录
            }
            else        //直接新建
            {
                parent = workDir;       //当前目录为父目录
            }

            _node=node[parent];
            List<int> nodeLoc=nodeMap.findUnuse(1);        //找到未使用的inode节点
            List<int> blockLoc=blockMap.findUnuse(1);       //找到空闲磁盘块


            if (nodeLoc == null || blockLoc == null)        //inodeMap或blockMap用尽  
            {
                return false;
            }


            if (datablock[blockLoc[0]] == null)     //磁盘块未初始化
            {
                datablock[blockLoc[0]] = new dataBlock();
                if (type == "文件夹")
                {
                    initDir( blockLoc[0] , parent , nodeLoc[0]);
                }
            }
            if (node[nodeLoc[0]] == null)       //inode为初始化
            {
                node[nodeLoc[0]] = new inode();
            }
            string name = "新建" + type;
            name = datablock[_node.getBlock(0)].createInode(name, nodeLoc[0]);
            if (_name == null)
            {
                setViewItem(name,type,0,DateTime.Now);
            }
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            node[nodeLoc[0]].init(nodeLoc[0], blockLoc, type , DateTime.Now);
            formatter.Serialize(stream, datablock);
            stream.Close();
            return true;
        }

        public void reNameFile(string newName,int _index)       //重命名文件
        {
            inode _node = node[workDir];
            datablock[_node.getBlock(0)].reNameInode(newName, _index);      //在父目录的inodetable中进行修改
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, datablock);
            stream.Close();
        }

        public void removeFile(string name,inode n,int index)
        {
            inode _node=null;
            if(n==null)
            {
                _node= node[workDir];
                listView.Items.RemoveAt(index);
            }
            else
            {
                _node=n;
            }
            int id=datablock[_node.getBlock(0)].removeInode(name);      //找到删除文件的inode
            _node=node[id];
            for(int i=0;;i++)       //释放inodetable中信息
            {
                string _name=datablock[_node.getBlock(0)].findInode(0);
                if(_name!=null)
                {
                    removeFile(_name,node[id],-1);
                }
                else
                {
                    break;
                }
            }
            List<int> b = _node.getBlockPtr().ToList<int>();        //获得该节点占用的全部块
            blockMap.release(b);        //释放块位图
            nodeMap.release(id);     //释放节点位图
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, datablock);
            stream.Close();

        }

        public Boolean reDirectCatalog(string name)     //切换文件目录或打开文件
        {
            inode _node = node[workDir];
            int temp = workDir;
            workDir = datablock[_node.getBlock(0)].reDirectInode(name);     //找到所选项的inode
            if (node[workDir].getType().Equals("文件"))
            {
                selectedFile = workDir;
                workDir = temp;
                return true;        //是文件直接打开
            }
            listItems(workDir);     //是文件夹进入目录
            return false;
        }

        public string readFile()
        {
            int num = node[selectedFile].getBlockNum();     //获取磁盘块数目
            string content = "";
            for (int i = 0; i < num; i++)       //遍历磁盘块
            {
                content += datablock[node[selectedFile].getBlock(i)].getContent();
            }
            return content;
        }

        public Boolean saveFile(string content)
        {
            int num=node[selectedFile].getBlockNum();       //获取文件也有磁盘块数目
            byte[] buffer=Encoding.Default.GetBytes(content);
            int n=buffer.Length/100;        //计算所需磁盘块
            int offset=buffer.Length%100;
            if(n>13)
            {
                return false;
            }
            List<int> mem;
            if(offset>0)
            {
                mem=blockMap.findUnuse(n+1-num);
            }
            else
            {
                mem=blockMap.findUnuse(n-num);
            }
            if(mem==null&&n - num > 0)      //需要新块但找不到空的磁盘块
            {
                return false;
            }
            for(int i=0;i<=n;i++)
            {
                string con;
                if(i<n)
                {
                    con=Encoding.Default.GetString(buffer, 100 * i, 100);
                }
                else
                {
                    con=Encoding.Default.GetString(buffer, 100 * i, offset );
                }
                if(node[selectedFile].getBlock(i)==0)
                {
                    if(datablock[mem[0]]==null)
                    {
                        datablock[mem[0]]=new dataBlock();
                    }
                    node[selectedFile].setBlock(mem[0],i);      //为文件分配新的磁盘块
                    datablock[mem[0]].setContent(con);      //存储信息
                    mem.RemoveAt(0);
                }
                else
                {
                    datablock[node[selectedFile].getBlock(i)].setContent(con);
                }
            }
            node[selectedFile].setTime(DateTime.Now);
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, datablock);
            stream.Close();
            listItems(workDir);
            return true;
        }

    }
}
