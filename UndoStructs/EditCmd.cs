using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    class EditCmd:Cmd
    {
        private List<dataBlock> new_data;

        public EditCmd(int node_index) : base(node_index)
        {
            new_data = new List<dataBlock>();
            for (int i = 0; i < 13; i++)
            {
                if (node.getBlock(i) == 0)
                {
                    break;
                }
                new_data.Add(MemoryInterface.getInstance().getDataBlockByIndex(node.getBlock(i)));
            }
        }
        public override void undo()
        {
            base.undo();
            MemoryInterface.getInstance().setInodeByIndex(node.id, node);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().setDataBlockByIndex(node.getBlockPtr().ToList<int>(),blocks);

        }

        public override void redo()
        {
            base.redo();
            MemoryInterface.getInstance().setInodeByIndex(node.id, node);
            List<int> temp = new List<int>();
            MemoryInterface.getInstance().setDataBlockByIndex(node.getBlockPtr().ToList<int>(), new_data);

        }

    }
}
