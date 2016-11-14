using FileSystem.EntryInterface;
using System.Collections.Generic;


namespace FileSystem.UndoStructs
{
    class EntryCmd:AbstractUndoableCmd<Directory>
    {
        private int index = 0;
        private static EntryCmd entryCmd;

        public Directory tempDir
        {
            get
            {
                if (index == list.Count)
                {
                    return list[index - 1];
                }
                if (index == 0)
                {
                    return list[0];
                }
                return list[index];
            }
        }

        private EntryCmd()
        {
            list = new List<Directory>();
        }

        public static EntryCmd getInstance()
        {
            if (entryCmd == null)
            {
                entryCmd = new EntryCmd();
            }
            return entryCmd;
        }

        public override void undo()
        {
            if (index == list.Count)
            {
                canUndo = false;
                return;
            }
            canRedo = true;
            index++;
        }


        public override void redo()
        {
            if (index == 0)
            {
                canRedo = false;
                return;
            }
            canUndo = true;
            index--;
        }

        public override void die()
        {
            list.Clear();
        }

        public void newOpe(Directory entry)
        {
            if (index > 0)
            {
                list.RemoveRange(0, index);
            }
            index = 0;
            canUndo = true;
            canRedo = false;
            list.Insert(0, (Directory)entry.Clone());
        }
    }
}
