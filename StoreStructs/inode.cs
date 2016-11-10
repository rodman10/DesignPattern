using System;
using System.Collections.Generic;


namespace FileSystem
{
    [Serializable]
    public class inode
    {
        private int id;     //用于标识inode的唯一编号
        private string type;        //文件还是文件夹
        private DateTime time;      //最后修改时间
        private int[] datablock = new int[13];      //存储位置

        public void init(int id, List<int> block,string type , DateTime time)
        {
            this.id = id;
            this.type = type;
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
