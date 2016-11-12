using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class AddCmd:Cmd
    {
        public AddCmd(int node_index):base(node_index)
        {
            
        }

        public override void redo()
        {
            base.redo();
            MemoryInterface.getInstance().setInodeByIndex(node.id, node);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().setDataBlockByIndex(node.getBlockPtr().ToList<int>(), blocks);

        }

        public override void undo()
        {
            base.undo();
            MemoryInterface.getInstance().releaseInode(node.id);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().releaseBlock(node.getBlockPtr().ToList<int>());
        }
    }
}
