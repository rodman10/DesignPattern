using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class EditCmd:Cmd
    {
        private string o_name;
        private string n_name;
        public EditCmd(inode p_node,string o_name,string n_name) : base(p_node)
        {
            this.o_name = o_name;
            this.n_name = n_name;
        }
        public override void undo()
        {
            MemoryInterface.getInstance().getDataBlockByIndex(node.getBlock(0)).reNameInode(n_name,o_name);
        }

        public override void redo()
        {
            MemoryInterface.getInstance().getDataBlockByIndex(node.getBlock(0)).reNameInode(o_name, n_name);

        }

    }
}
