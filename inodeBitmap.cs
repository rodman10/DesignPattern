using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    [Serializable]
    class inodeBitmap
    {
        Boolean[] isUse=new Boolean[1024];      //表示该inode是否已被使用

        public List<int> findUnuse(int num)     //寻找未使用的inode，返回指定数目的结果
        {
            int count = 0;
            List<int> unuse = new List<int>();
            for (int i = 0; i < 1024; i++)
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

        public void release(int index)      //释放inode
        {
            isUse[index] = false;
        }
    }
}
