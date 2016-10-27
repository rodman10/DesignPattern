using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem
{
    [Serializable]
    class dataBlock
    {
        private inodeTable nodeTable = new inodeTable();        //表示文件夹中每个文件对应的inode编号
        private string content;     //文件内容
        private blockBitmap bmap;       //存储块的位图，该项只出现在第一块存储块(超级块)中
        private inodeBitmap imap;       //存储inode的位图，该项只出现在第一块存储块(超级块)中
        private inode[] node;       //存储所有的inode信息，该项只出现在第一块存储块(超级块)中

        public void reNameInode(string newName,int _index)
        {
            nodeTable.reNameInode(newName, _index);
        }

        public int findInode(string name)
        {
            return nodeTable.findInode(name);
        }

        public string findInode(int index)
        {
            return nodeTable.findInode(index);
        }

        public string createInode(string name,int index)
        {
            return nodeTable.createInode(name,index);
        }

        public int reDirectInode(string _name)
        {
            return nodeTable.reDirectInode(_name);
        }

        public int removeInode(string name)
        {
            return nodeTable.removeInode(name);
        }

        public void setNode(inode[] _node)
        {
            node = _node;
        }

        public inode[] getNode()
        {
            return node;
        }

        public void setImap(inodeBitmap _imap)
        {
            imap = _imap;
        }

        public inodeBitmap getImap()
        {
            return imap;
        }

        public void setBmap(blockBitmap _bmap)
        {
            bmap = _bmap;
        }

        public blockBitmap getBmap()
        {
            return bmap;
        }

        public void setContent(string content)
        {
            this.content = content;
        }

        public string getContent()
        {
            return content;
        }
    }
}
