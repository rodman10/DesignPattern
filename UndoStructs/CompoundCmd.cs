using System.Collections.Generic;


namespace FileSystem.UndoStructs
{
    class CompoundCmd:AbstractUndoableCmd
    {
        protected List<UndoableCmd> opes;
        protected int index=0;

        public CompoundCmd()
        {
            opes = new List<UndoableCmd>();
        }

        public override void undo()
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
        
        public override void redo()
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
        
        public override void die()
        {
            opes.Clear();
            canRedo = canRedo = false;

        }

        public override void newOpe(UndoableCmd cmd)
        {
            if (index > 0)
            {
                opes.RemoveRange(0, index);
            }
            index = 0;
            canUndo = true;
            canRedo = false;
            opes.Insert(0, cmd);
            
        }
    }
}
