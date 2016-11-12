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
            if (index == opes.Count)
            {
                canUndo = false;
                return;
            }
            if (opes[index].CanUndo())
            {
                canRedo = true;
                opes[index].undo();
                index++;
            }
        }

        public override void redo()
        {
            if (index == 0)
            {
                canRedo = false;
                return;
            }
            if (opes[index - 1].CanRedo())
            {
                canUndo = true;
                index--;
                opes[index].redo();
            }
        }
     
        
    }
}
