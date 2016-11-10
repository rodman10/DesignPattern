using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    [Serializable]
    class superBlock
    {

        public superBlock( int nodeNum , int blockNum)
        {
            this.nodeNum = nodeNum;
            this.blockNum = blockNum;
        }
        public int nodeNum
        {
            get;
        }
        public int blockNum
        {
            get;
        }

    }
}
