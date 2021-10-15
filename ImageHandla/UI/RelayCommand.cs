using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MangaCleaner.UI
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

    public class AsyncCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        Func<Task> CommandTask; 

        public AsyncCommand(Func<Task> commandTask)
        {
            CommandTask = commandTask;
        }

        public void Execute(object parameter) => FireAndForget();

        private async void FireAndForget()
        {
            await CommandTask().ConfigureAwait(true);
        }

        public bool CanExecute(object parameter) => true;
    }
}
