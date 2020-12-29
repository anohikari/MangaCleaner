using System.Collections.Generic;
using MangaCleaner.Interfaces;

namespace MangaCleaner
{
    class UndoRedoStack
    {
        private Stack<UndoRedoOperation> UndoStack;
        private Stack<UndoRedoOperation> RedoStack;

        public void Push(UndoRedoOperation operation)
        {
            UndoStack.Push(operation);
        }

        public void Undo()
        {
            if(UndoStack.Count <= 0) { return; }
            var Operation = UndoStack.Pop();
            Operation.Undo();
            RedoStack.Push(Operation);
        }

        public void Redo()
        {
            if (RedoStack.Count <= 0) { return; }
            var Operation = RedoStack.Pop();
            Operation.Redo();
            UndoStack.Push(Operation);
        }

    }
}
