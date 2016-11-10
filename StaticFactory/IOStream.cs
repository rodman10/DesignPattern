using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem
{
    class IOStream
    {
        private static Stream ioStream = null;
        public static Stream getInstance()
        {
            if( null == ioStream)
            {
                ioStream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            }
            ioStream.Position = 0;
            return ioStream;
        }
    }
}
