using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.EntryInterface
{
    interface Entry
    {


        string createEntry(int workDir,string _name);

        void reNameEntry(int workDir, string newName, int _index);

        void removeEntry(int workDir, string name, inode n, int index);

        string openEntry(string name, ref int selectedFile,ref int workDir);

        bool write(string content,ref int selectedFile,ref int workDir); 
    }
}
