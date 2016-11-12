using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class DeleteCmd:Cmd
    {
        public DeleteCmd(inode node) : base(node)
        {

        }

        public override void undo()
        {
            MemoryInterface.getInstance().setInodeByIndex(node.id, node);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().setDataBlockByIndex(node.getBlockPtr().ToList<int>(),blocks);
                   
        }
       
        public override void redo()
        {
            MemoryInterface.getInstance().releaseInode(node.id);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().releaseBlock(node.getBlockPtr().ToList<int>());
        }
     
    
    }
}
