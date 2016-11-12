using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    [Serializable]
    class blockBitmap
    {
        public bool[] isUse;       //表示该块是否已被使用
        
        public blockBitmap(int num)
        {
            isUse = new bool[num];
        }

        public List<int> findUnuse(int num)     //寻找未使用的块，返回指定数目的结果
        {
            if (num == 0)
            {
                return null;
            }
            int count = 0;
            List<int> unuse = new List<int>();
            for (int i = 0; i< 4096; i++)
            {
                if (isUse[i].Equals(false))
                {
                    unuse.Add(i);
                    isUse[i] = true;
                    count++;
                    if (count == num)
                    {
                        return unuse;
                    }
                }
            }
            return null;
        }

        public void release(List<int> index)        //释放块
        {
            for (int i = 0; i<index.Count; i++)
            { 
                if (index[i] != 0)
                {
                    isUse[index[i]] = false;
                }
            }
        }

        public void obtain(List<int> index)        //持有块
        {
            for (int i = 0; i < index.Count; i++)
            {
                if (index[i] != 0)
                {
                    isUse[index[i]] = true;
                }
            }
        }

    }
}
