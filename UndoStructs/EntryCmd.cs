using FileSystem.EntryInterface;
using System.Collections.Generic;


namespace FileSystem.UndoStructs
{
    class EntryCmd:AbstractUndoableCmd
    {
        protected List<Directory> entries;
        private int index = 0;
        private static EntryCmd entryCmd;
        private EntryCmd()
        {
            entries = new List<Directory>();
        }

        public static EntryCmd getInstance()
        {
            if (entryCmd == null)
            {
                entryCmd = new EntryCmd();
            }
            return entryCmd;
        }

        public Directory getEntry()
        {
            return entries[index];
        }

        public new Directory undo()
        {
            if (index == entries.Count)
            {
                canUndo = false;
                return entries[index-1];
            }
            canRedo = true;
            index++;
            return entries[index];
        }


        public new Directory redo()
        {
            if (index == 0)
            {
                canRedo = false;
                return entries[0];
            }

            canUndo = true;
            index--;
            return entries[index];
        }

        public override void die()
        {
            entries.Clear();
        }


        public void newOpe(Directory entry)
        {
            entries.Insert(0, (Directory)entry.Clone());
        }
    }
}
