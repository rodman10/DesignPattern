using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    [Serializable]
    public class inode
    {
        private int id;     //用于标识inode的唯一编号
        private string type;        //文件还是文件夹
        private DateTime time;      //最后修改时间
        private int parent;     //父级目录节点编号
        private int[] datablock = new int[13];      //存储位置

        public void init(int id, List<int> block,string type,int _parent,DateTime time)
        {
            this.id = id;
            this.type = type;
            parent = _parent;
            this.time = time;
            for (int i = 0; i < block.Count; i++)
            {
                datablock[i] = block[i];
            }
        }

        public void setTime(DateTime _time)
        {
            this.time= _time;
        }

        public DateTime getTime()
        {
            return time;
        }

        public int getParent()
        {
            return parent;
        }

        public void setBlock(int realIndex, int virIndex)
        {
            datablock[virIndex] = realIndex;
        }

        public int getBlockNum()
        {
            int i;
            for ( i= 0; i < 13; i++)
            {
                if (datablock[i] == 0)
                {
                    break;
                }
            }
            return i;
        }

        public string getType()
        {
            return type;
        }

        public int getBlock(int index)
        {
            return datablock[index];
        }

        public int[] getBlockPtr()
        {
            return datablock;
        }
    }
}
