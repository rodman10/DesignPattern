using System.Collections.Generic;


namespace FileSystem.UndoStructs
{
    class CompoundCmd:UndoableCmd
    {
        protected List<UndoableCmd> opes;
        protected int index=0;
        protected bool canUndo = false;
        protected bool canRedo = false;

        public CompoundCmd()
        {
            opes = new List<UndoableCmd>();
        }

        public bool CanUndo()
        {
            return canUndo;
        }

        public bool CanRedo()
        {
            return canRedo;
        }

        public virtual void undo()

        {
            while (true)
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
            
        }
        
        public virtual void redo()
        {
            while (true)
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
        
        public void die()
        {
            opes.Clear();
            canRedo = canRedo = false;

        }

        public void newOpe(UndoableCmd cmd)
        {
            if (index > 0)
            {
                opes.RemoveRange(0, index);
            }
            index = 0;
            canUndo = true;
            opes.Insert(0, cmd);
            
        }
    }
}
