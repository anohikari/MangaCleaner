using System;
using System.Collections.Generic;
using System.Text;

namespace MangaCleaner.Interfaces
{
    interface IUndoAble
    {
        void Undo();
        void Redo();
    }
}
