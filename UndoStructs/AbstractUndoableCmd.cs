using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    abstract class AbstractUndoableCmd<T>:UndoableCmd
    {
        protected List<T> list;
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
        public abstract void undo();


        public abstract void redo();
        
        public virtual void die()
        {
            list.Clear();
            canRedo = canRedo = false;
        }

        public virtual void newOpe(UndoableCmd cmd) { }
    }
}
