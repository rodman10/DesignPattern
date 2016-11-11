using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    class File:Entry
    {
        public int size{ get; set; }
        public string name { get; }
        public int node { get; }
        public DateTime time { get; }
        public string content { get; set; }

        public File(inode _node , string name)
        {
            string type = _node.getType();
            DateTime time = _node.getTime();
            this.size = calculate(_node);
            this.name = name;
            this.node = _node.id;
            this.time = _node.getTime();
            int num = _node.getBlockNum();     //获取磁盘块数目
            string content = "";
            for (int i = 0; i < num; i++)       //遍历磁盘块
            {
                content += MemoryInterface.getInstance().getDataBlockByIndex(_node.getBlock(i)).data;
            }
            this.content=content;
        }

        public DateTime getTime()
        {
            return time;
        }

        public string getName()
        {
            return name;
        }

        public string getType()
        {
            return "文件";
        }

        public int getSize()
        {
            return size;
        }

        public object getContent()
        {
            return content;
        }

        private int calculate(inode _node)     //计算文件大小
        {
            int[] ptr = _node.getBlockPtr();
            string content = "";
            for (int i = 0; i < 13; i++)        //遍历该文件的所有块，计算大小
            {
                if (MemoryInterface.getInstance().getDataBlockByIndex(ptr[i]) != null && MemoryInterface.getInstance().getDataBlockByIndex(ptr[i]).data != null && !MemoryInterface.getInstance().getDataBlockByIndex(ptr[i]).data.Equals(""))
                {
                    content += MemoryInterface.getInstance().getDataBlockByIndex(ptr[i]).data;
                    continue;
                }
                break;
            }
            byte[] sarr = Encoding.Default.GetBytes(content);
            return sarr.Length;
        }

        public string createEntry( string _name,string type )
        {
            return null;
        }

        public void reNameEntry(string newName, int _index)
        {
            
        }

        public void removeEntry(int workDir,string name, inode n)
        {
            inode _node = null;
            if (n == null)
            {
                _node = MemoryInterface.getInstance().getInodeByIndex(workDir);

            }
            else
            {
                _node = n;
            }
            int id = MemoryInterface.getInstance().getDataBlockByIndex(_node.getBlock(0)).removeInode(name);      //找到删除文件的inode
            _node = MemoryInterface.getInstance().getInodeByIndex(id);
         
            List<int> b = _node.getBlockPtr().ToList<int>();        //获得该节点占用的全部块
            MemoryInterface.getInstance().releaseBlock(b);        //释放块位图
            MemoryInterface.getInstance().releaseInode(id);     //释放节点位图
            MemoryInterface.getInstance().write();
        }

    
    

        public bool write(string content)
        {
            this.content = content;
            int num = MemoryInterface.getInstance().getInodeByIndex(node).getBlockNum();       //获取文件已有磁盘块数目
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
                mem = MemoryInterface.getInstance().getRequireBlocks(n + 1 - num);
            }
            else
            {
                mem = MemoryInterface.getInstance().getRequireBlocks(n - num);
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
                if (MemoryInterface.getInstance().getInodeByIndex(node).getBlock(i) == 0)
                {
                    MemoryInterface.getInstance().getInodeByIndex(node).setBlock(mem[0], i);      //为文件分配新的磁盘块
                    MemoryInterface.getInstance().getDataBlockByIndex(mem[0]).data = con;      //存储信息
                    mem.RemoveAt(0);
                }
                else
                {
                    MemoryInterface.getInstance().getDataBlockByIndex(MemoryInterface.getInstance().getInodeByIndex(node).getBlock(i)).data = con;
                }
            }

            MemoryInterface.getInstance().getInodeByIndex(node).setTime(DateTime.Now);
            MemoryInterface.getInstance().write();
            this.size = calculate(MemoryInterface.getInstance().getInodeByIndex(node));
            return true;
        }
    }
}
