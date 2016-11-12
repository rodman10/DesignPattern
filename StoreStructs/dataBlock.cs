using System;


namespace FileSystem
{
    [Serializable]
    class dataBlock
    {
        public object data { set; get; }

        public void reNameInode(string newName,int _index)
        {
            ((inodeTable)data).reNameInode(newName, _index);
        }

        public int findInode(string name)
        {
            return ((inodeTable)data).findInode(name);
        }

        public string findInode(int index)
        {
            return ((inodeTable)data).findInode(index);
        }

        public string createInode(string name,int index)
        {
            if(((inodeTable)data) == null)
            {
                data = new inodeTable();

            }
            return ((inodeTable)data).createInode(name,index);
        }

        public int reDirectInode(string _name)
        {
            return ((inodeTable)data).reDirectInode(_name);
        }

        public int removeInode(string name)
        {
            return ((inodeTable)data).removeInode(name);
        }
    
        public void reNameInode(string n_name,string o_name)
        {
            ((inodeTable)data).reNameInode(n_name, o_name);
        }
    }
}
