using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    class File:Entry
    {

        private MemoryInterface memory = new MemoryInterface();

        public File(ref MemoryInterface memory)
        {
            this.memory = memory;
        }

        public string createEntry(int workDir, string _name)
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

         

            string name = "新建文件";
            name = memory.addNewInodeTableItem(_node.getBlock(0), name, nodeLoc[0]);
            memory.getInodeByIndex(nodeLoc[0]).init(nodeLoc[0], blockLoc, "文件", DateTime.Now);
            memory.write();
            return name;
        }

        public void reNameEntry(int workDir, string newName, int _index)
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
                    removeEntry(workDir, _name, memory.getInodeByIndex(id), -1);
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

        public string openEntry(string name, ref int selectedFile, ref int workDir)
        {
            int num = memory.getInodeByIndex(selectedFile).getBlockNum();     //获取磁盘块数目
            string content = "";
            for (int i = 0; i < num; i++)       //遍历磁盘块
            {
                content += memory.getDataBlockByIndex(memory.getInodeByIndex(selectedFile).getBlock(i)).data;
            }
            return content;
        }

        public bool write(string content, ref int selectedFile, ref int workDir)
        {
            int num = memory.getInodeByIndex(selectedFile).getBlockNum();       //获取文件已有磁盘块数目
            byte[] buffer = Encoding.Default.GetBytes(content);
            int n = buffer.Length / 100;        //计算所需磁盘块
            int offset = buffer.Length % 100;
            if (n > 13)
            {
                return false;
            }
            List<int> mem;
            if (offset > 0)
            {
                mem = memory.getRequireBlocks(n + 1 - num);
            }
            else
            {
                mem = memory.getRequireBlocks(n - num);
            }
            if (mem == null && n - num > 0)      //需要新块但找不到空的磁盘块
            {
                return false;
            }
            for (int i = 0; i <= n; i++)
            {
                string con;
                if (i < n)
                {
                    con = Encoding.Default.GetString(buffer, 100 * i, 100);
                }
                else
                {
                    con = Encoding.Default.GetString(buffer, 100 * i, offset);
                }
                if (memory.getInodeByIndex(selectedFile).getBlock(i) == 0)
                {
                    memory.getInodeByIndex(selectedFile).setBlock(mem[0], i);      //为文件分配新的磁盘块
                    memory.getDataBlockByIndex(mem[0]).data = con;      //存储信息
                    mem.RemoveAt(0);
                }
                else
                {
                    memory.getDataBlockByIndex(memory.getInodeByIndex(selectedFile).getBlock(i)).data = con;
                }
            }
            memory.getInodeByIndex(selectedFile).setTime(DateTime.Now);
            memory.write();
            
            return true;
        }
    }
}
