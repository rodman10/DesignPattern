using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    abstract class Cmd:UndoableEdit
    {
        protected bool canUndo = false;
        protected bool canRedo = false;
        protected inode node;
        protected List<dataBlock> blocks;

        public Cmd(inode node)
        {
            this.node = node;
            this.blocks = new List<dataBlock>();
            for (int i = 0; i < 13; i++)
            {
                if (node.getBlock(i) == 0)
                {
                    break;
                }
                blocks.Add(MemoryInterface.getInstance().getDataBlockByIndex(node.getBlock(i)));
            }
        }

        public abstract void undo();
     
        public bool CanUndo()
        {
            return canUndo;
        }
        public abstract void redo();

        public bool CanRedo()
        {
            return canRedo;
        }
        public void die()
        {

        }
        public void addEdit()
        {

        }
    }
}
