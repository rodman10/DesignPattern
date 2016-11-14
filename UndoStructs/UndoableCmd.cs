using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystem.UndoStructs
{
    interface UndoableCmd
    {
        void undo();
        bool CanUndo();
        void redo();
        bool CanRedo();
        void die();
        void newOpe(UndoableCmd cmd);

    }
}
