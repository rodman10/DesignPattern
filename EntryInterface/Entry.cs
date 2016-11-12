using System;


namespace FileSystem.EntryInterface
{
    [Serializable]
    abstract class Entry:ICloneable
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

        public virtual object Clone()
        {
            return null;
        }

        public abstract string getType();

        public abstract int getSize();

        public abstract object getContent();

        public virtual string createEntry(string _name,string type)
        {
            return null;
        }

        public virtual void reNameEntry(string newName, int _index) { }

        public virtual void removeEntry(int selectedItem , string name)
        {

        }

        public virtual bool write(string content)
        {
            return true;
        }
    }
}
