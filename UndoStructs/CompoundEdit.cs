using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class CompoundEdit
    {
        private List<UndoableEdit> edits;
        private int index=0;
        private bool canUndo = false;
        private bool canRedo = false;

        public CompoundEdit()
        {
            edits = new List<UndoableEdit>();
        }

        public void undo()
        {
            if ( index == edits.Count-1)
            {
                canUndo = false;
                return;
            }
            if (edits[index].CanUndo())
            {
                canRedo = true;
                edits[index].undo();
                index++;
            }
        }
        public bool CanUndo()
        {
            return canUndo;
        }
        public void redo()
        {
            if (index == 0)
            {
                canRedo = false;
                return;
            }
            if (edits[index-1].CanRedo())
            {
                canUndo = true;
                index--;
                edits[index].redo();
            }
        }
        public bool CanRedo()
        {
            return canRedo;
        }
        public void die()
        {

        }
        public void addEdit(inode node)
        {
            if (index > 0)
            {
                edits.RemoveRange(0, index);
            }
            index = 0;
            canUndo = true;
            edits.Insert(0,new DeleteCmd(node));
        }
    }
}
