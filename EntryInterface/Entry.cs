using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    interface Entry
    {
        DateTime getTime();

        string getName();

        string getType();

        int getSize();

        object getContent();

        string createEntry(string _name,string type);

        void reNameEntry(string newName, int _index);

        void removeEntry(int workDir, string name, inode n);

        bool write(string content); 
    }
}
