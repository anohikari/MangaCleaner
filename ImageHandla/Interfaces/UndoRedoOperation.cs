using System;
using System.Collections.Generic;
using System.Text;

namespace MangaCleaner.Interfaces
{
    interface UndoRedoOperation
    {
        void Undo();
        void Redo();
    }
}
