using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    abstract class Cmd:UndoableCmd
    {
        protected bool canUndo = true;
        protected bool canRedo = false;
        protected inode node;
        protected List<dataBlock> blocks;

        public Cmd(int node_index)
        {
            inode node = MemoryInterface.getInstance().getInodeByIndex(node_index);
            this.node = (inode)node.Clone();
            this.blocks = new List<dataBlock>();
            for (int i = 0; i < 13; i++)
            {
                if (node.getBlock(i) == 0)
                {
                    break;
                }
                blocks.Add((dataBlock)MemoryInterface.getInstance().getDataBlockByIndex(node.getBlock(i)).Clone());
            }
        }

        public virtual void undo()
        {
            canRedo = true;
        }
     
        public bool CanUndo()
        {
            return canUndo;
        }

        public virtual void redo()
        {

            canUndo = true;
        }

        public bool CanRedo()
        {
            return canRedo;
        }
        public void die()
        {

        }
        public void newOpe(UndoableCmd cmd)
        {

        }
    }
}
