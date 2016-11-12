using System;
using System.Collections.Generic;


namespace FileSystem
{
    [Serializable]
    class inodeTable
    {
        private List<string> name;      //文件名
        private List<int> id;       //inode编号

        private void isNull()
        {
            if (name == null && id == null)
            {
                name = new List<string>();
                id = new List<int>();
            }
        }
        public string reNameInode(string newName, int _index)
        {
            string temp = name[_index];
            name[_index] = newName;
            return temp;
        }

        public int reNameInode(string n_name, string o_name)
        {
            int temp = name.IndexOf(o_name);
            name[temp] = n_name;
            return temp;
        }

        public int findInode(string _name)      //根据文件名查找节点
        {
            for(int i=0;i<name.Count;i++)
            {
                if(name[i].Equals(_name))
                {
                    return id[i];
                }
            }
            return -1;
        }

        public string findInode(int index)      //根据inode编号查找节点
        {
            isNull();
            if (index >= id.Count)
            {
                return null;
            }
            return name[index];
        }

        private Boolean hasSameName(string _name)       //查找文件是否重名
        {
            if (name.Contains(_name))
            {
                return true;
            }
            return false;
        }

        public int reDirectInode(string _name)
        {
            int index = name.IndexOf(_name);
            return id[index];
        }

        public string createInode(string _name,int index)
        {
            isNull();
            for (int i = 0; ; i++)
            {
                if(i==0)
                {
                    if(hasSameName(_name))
                    {
                        continue;
                    }
                    break;
                }
                else
                {
                    if(hasSameName(_name+i))
                    {
                        continue;
                    }
                    _name = _name + i;
                    break;
                }
                
            }
            name.Add(_name);
            id.Add(index);
            return _name;
        }

        public int removeInode(string _name)
        { 
            int index=name.IndexOf(_name);
            int _id = id[index];
            name.RemoveAt(index);
            id.RemoveAt(index);
            return _id;
        }

        
    }
}
