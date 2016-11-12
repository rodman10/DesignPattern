using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileSystem
{
    [Serializable]
    class dataBlock:ICloneable
    {
        public object data { set; get; }


        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }

        public string reNameInode(string newName,int _index)
        {
            return ((inodeTable)data).reNameInode(newName, _index);
        }

        public int reNameInode(string n_name, string o_name)
        {
            return ((inodeTable)data).reNameInode(n_name, o_name);
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
    }
}
