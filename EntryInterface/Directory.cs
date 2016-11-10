using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    class Directory:Entry
    {

        private MemoryInterface memory = new MemoryInterface();

        public Directory(ref MemoryInterface memory)
        {
            this.memory = memory;
        }

        private void initDir(int index, int parent, int current)
        {
            memory.addNewInodeTableItem(index, "..", parent);
            memory.addNewInodeTableItem(index, ".", current);
        }

        public string createEntry(int workDir,string _name)
        {
            int parent = -1;

            if (_name != null)      //点击文件夹创建
            {
                parent = memory.getInodeIndexByName(workDir, _name);     //选中文件夹为父目录
            }
            else        //直接新建
            {
                parent = workDir;       //当前目录为父目录
            }

            inode _node = memory.getInodeByIndex(parent);
            List<int> nodeLoc = memory.getRequireInodes(1);        //找到未使用的inode节点
            List<int> blockLoc = memory.getRequireBlocks(1);       //找到空闲磁盘块


            if (nodeLoc == null || blockLoc == null)        //inodeMap或blockMap用尽  
            {
                return null;
            }

            initDir(blockLoc[0], parent, nodeLoc[0]);
           

            string name = "新建文件夹";
            name= memory.addNewInodeTableItem(_node.getBlock(0), name, nodeLoc[0]);
            memory.getInodeByIndex(nodeLoc[0]).init(nodeLoc[0], blockLoc, "文件夹", DateTime.Now);
            memory.write();
            return name;
        }

        public void reNameEntry(int workDir,string newName,int _index)
        {
            inode _node = memory.getInodeByIndex(workDir);
            memory.getDataBlockByIndex(_node.getBlock(0)).reNameInode(newName, _index);      //在父目录的inodetable中进行修改
            memory.write();
        }

        public void removeEntry(int workDir,string name, inode n, int index)
        {
            inode _node = null;
            if (n == null)
            {
                _node = memory.getInodeByIndex(workDir);
                
            }
            else
            {
                _node = n;
            }
            int id = memory.getDataBlockByIndex(_node.getBlock(0)).removeInode(name);      //找到删除文件的inode
            _node = memory.getInodeByIndex(id);
            for (int i = 0; ; i++)       //释放inodetable中信息
            {
                string _name = memory.getDataBlockByIndex(_node.getBlock(0)).findInode(0);
                if (_name != null)
                {
                    removeEntry(workDir,_name, memory.getInodeByIndex(id), -1);
                }
                else
                {
                    break;
                }
            }
            List<int> b = _node.getBlockPtr().ToList<int>();        //获得该节点占用的全部块
            memory.releaseBlock(b);        //释放块位图
            memory.releaseInode(id);     //释放节点位图
            memory.write();
        }

        public string openEntry(string name, ref int selectedFile,ref int workDir)
        {
            inode _node = memory.getInodeByIndex(workDir);
            int temp = workDir;
            workDir = memory.getDataBlockByIndex(_node.getBlock(0)).reDirectInode(name);     //找到所选项的inode
            if (memory.getInodeByIndex(workDir).getType().Equals("文件"))
            {
                selectedFile = workDir;
                workDir = temp;
                return null;        //是文件直接打开
            }
            return "0";
        }

        public bool write(string content, ref int selectedFile, ref int workDir)
        {
            return true;
        }
    }
}
