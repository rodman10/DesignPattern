using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    abstract class AbstractUndoableCmd:UndoableCmd
    {
        protected bool canUndo = true;
        protected bool canRedo = false;
        public bool CanUndo()
        {
            return canUndo;
        }
        public bool CanRedo()
        {
            return canRedo;
        }
        public virtual void undo() { }

        
        public virtual void redo() { }

        
        public virtual void die() { }
        public virtual void newOpe(UndoableCmd cmd) { }
    }
}
