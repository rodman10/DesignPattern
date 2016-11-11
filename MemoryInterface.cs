using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSystem
{
    class MemoryInterface
    {

        private static MemoryInterface memory;

        
        protected dataBlock[] datablock = new dataBlock[4096];        //模拟磁盘块的数组

        private MemoryInterface()
        {
            if (0 == IOStream.getInstance().Length)
            {
                boot();
                
            }

            datablock = (dataBlock[])IOFormatter.getInstance().Deserialize(IOStream.getInstance());

        }

        public static MemoryInterface getInstance()
        {
            if (memory == null)
            {
                memory = new MemoryInterface();
            }
            return memory;
        }

        private void initBoot()
        {
            superBlock superblock = (superBlock)datablock[0].data;

            inode[] node = new inode[superblock.nodeNum];
            blockBitmap blockMap = new blockBitmap(superblock.blockNum);
            inodeBitmap nodeMap = new inodeBitmap(superblock.nodeNum);

            datablock[1] = new dataBlock();
            datablock[1].data = nodeMap;

            datablock[2] = new dataBlock();
            datablock[2].data = blockMap;

            datablock[3] = new dataBlock();
            datablock[3].data = node;
        }

        private void boot()
        {
            Stream stream = new FileStream("block.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
            
            if (0 == stream.Length)
            {
                superBlock superblock = new superBlock(1024, 4096);
                dataBlock block = new dataBlock();
                block.data = superblock;
                datablock[0] = block;
                initBoot();
                ((blockBitmap)datablock[2].data).findUnuse(1);
            }

            inode root = new inode();
            List<int> b = new List<int>();
            b.Add(4);
            root.init(0, b, "文件夹", DateTime.Now);
            ((inode[])datablock[3].data)[0] = root;
            dataBlock data = new dataBlock();
            ((blockBitmap)datablock[2].data).findUnuse(4);
            List<int> nodes = ((inodeBitmap)datablock[1].data).findUnuse(1);



            datablock[4] = new dataBlock();
            datablock[4].createInode("..", -1);
            datablock[4].createInode(".", nodes[0]);
            IOFormatter.getInstance().Serialize(IOStream.getInstance(), datablock);
        }

        public void write()
        {
            IOFormatter.getInstance().Serialize(IOStream.getInstance(), datablock);

        }

       public void cleanBlock( List<int> blocks)
        {
            foreach(int index in blocks)
            {
                datablock[index].data = null;
            }
            
        }
        
        public void releaseInode(int index)
        {
            List<int> id = new List<int>();
            id.Add(index);
            ((inodeBitmap)datablock[1].data).release(id);     //释放节点位图

        }

        public void releaseBlock( List<int> index)
        {
            ((blockBitmap)datablock[2].data).release(index);
        }

        public inode getInodeByIndex( int node_index)
        {
            return ((inode[])datablock[3].data)[node_index];
        }

        public dataBlock getDataBlockByIndex( int block_index)
        {
            return datablock[block_index];
        }

        public List<int> getRequireInodes( int num)
        {
            List<int> nodes= ((inodeBitmap)datablock[1].data).findUnuse(num);
            for (int i = 0; i < nodes.Count; i++)
            {
                if (((inode[])datablock[3].data)[nodes[i]] == null)
                {
                    ((inode[])datablock[3].data)[nodes[i]] = new inode();
                }
            }
            return nodes;
        }

        public List<int> getRequireBlocks( int num)
        {
            List<int> blocks= ((blockBitmap)datablock[2].data).findUnuse(num);
            if (blocks == null)
            {
                return null;
            }
            for(int i = 0; i < blocks.Count; i++)
            {
                if( datablock[blocks[i]] == null)
                {
                    datablock[blocks[i]] = new dataBlock();
                }
            }
            return blocks;

        }

        public string addNewInodeTableItem( int blockIndex , string name , int inodeIndex)
        {
            return datablock[blockIndex].createInode(name, inodeIndex);
        }

        public int getInodeIndexByName( int workDir , string _name )
        {
            return datablock[getInodeByIndex(workDir).getBlock(0)].findInode(_name);     //选中文件夹为父目录
        }

    }
}
