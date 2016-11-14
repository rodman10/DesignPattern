using System.Collections.Generic;


namespace FileSystem.UndoStructs
{
    class CompoundCmd:AbstractUndoableCmd<UndoableCmd>
    {
        protected int index=0;

        public CompoundCmd()
        {
            list = new List<UndoableCmd>();
        }

        public override void undo()
        {
            while (true)
            {
                if (index == list.Count)
                {
                    canUndo = false;
                    return;
                }
                if (list[index].CanUndo())
                {
                    canRedo = true;
                    list[index].undo();
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
                if (list[index - 1].CanRedo())
                {
                    canUndo = true;
                    index--;
                    list[index].redo();
                }
            }
        }

        public override void newOpe(UndoableCmd cmd)
        {
            if (index > 0)
            {
                list.RemoveRange(0, index);
            }
            index = 0;
            canUndo = true;
            canRedo = false;
            list.Insert(0, cmd);
            
        }
    }
}
