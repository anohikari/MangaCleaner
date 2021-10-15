using System.Collections.Generic;
using MangaCleaner.Interfaces;

namespace MangaCleaner
{
    class UndoRedoStack
    {
        private Stack<IUndoAble> UndoStack = new Stack<IUndoAble>();
        private Stack<IUndoAble> RedoStack = new Stack<IUndoAble>();

        public void Push(IUndoAble operation)
        {
            UndoStack.Push(operation);
        }

        public void Undo()
        {
            if(UndoStack.Count <= 0)
                return;
            var Operation = UndoStack.Pop();
            Operation.Undo();
            RedoStack.Push(Operation);
        }

        public void Redo()
        {
            if (RedoStack.Count <= 0)
                return;
            var Operation = RedoStack.Pop();
            Operation.Redo();
            UndoStack.Push(Operation);
        }

        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
        }

    }
}
