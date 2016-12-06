using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class UndoManager:CompoundCmd
    {
        private static UndoManager manager;
        private UndoManager():base()
        {

        }

        public static UndoManager getInstance()
        {
            if (manager == null)
            {
                manager = new UndoManager();
            }
            return manager;
        }

        public override void undo()
        {
            if (list[index].CanUndo())
            {
                canRedo = true;
                list[index].undo();
                index++;
            }
            if (index == list.Count)
            {
                canUndo = false;
                return;
            }
        }

        public override void redo()
        {
            if (list[index - 1].CanRedo())
            {
                canUndo = true;
                index--;
                list[index].redo();
            }
            if (index == 0)
            {
                canRedo = false;
                return;
            }
        }
             
    }
}
