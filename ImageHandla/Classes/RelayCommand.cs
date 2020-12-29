using System;
using System.Windows.Input;

namespace MangaCleaner
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Action CommandAction;

        public RelayCommand(Action commandAction)
        {
            CommandAction = commandAction;
        }

        public void Execute(object parameter) => CommandAction();
        
        public bool CanExecute(object parameter) => true;
    }
}
