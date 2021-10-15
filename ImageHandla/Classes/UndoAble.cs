using MangaCleaner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MangaCleaner.Classes
{
    class UndoAble : IUndoAble
    {
        List<ObjectBackup> ObjectChanges = new List<ObjectBackup>();

        public UndoAble(Action<Freezable> setter,Freezable changedObject, Action<Freezable> change)
        {
            var objectBackup = new ObjectBackup(setter, change, changedObject.Clone());
            ObjectChanges.Add(objectBackup);
        }

        public UndoAble(List<ObjectBackup> backups)
        {
            ObjectChanges = backups;
        }

        public void Redo()
        {
            foreach (var backup in ObjectChanges)
            {
                var clone = backup.Backup.Clone();
                backup.Change(clone);
                backup.Setter(clone);
            }
        }

        public void Undo()
        {
            foreach (var backup in ObjectChanges)
            {
                backup.Setter(backup.Backup);
            }
        }
    }

    record ObjectBackup(Action<Freezable> Setter, Action<Freezable> Change, Freezable Backup);
   
}
