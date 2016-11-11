using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    abstract class Entry
    {
        public string name { set; get; }
        public int node { get; set; }
        public DateTime time { get; set; }

        public DateTime getTime()
        {
            return time;
        }

        public string getName()
        {
            return name;
        }

        public abstract string getType();

        public abstract int getSize();

        public abstract object getContent();

        public string createEntry(string _name,string type)
        {
            return null;
        }

        public void reNameEntry(string newName, int _index) { }

        public void removeEntry(int selectedItem , string name, inode n)
        {

        }

        public bool write(string content)
        {
            return true;
        }
    }
}
