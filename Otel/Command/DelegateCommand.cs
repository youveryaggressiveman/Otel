using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Otel.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteFunc)
        {
            execute = executeAction;
            canExecute = canExecuteFunc;
        }

        public DelegateCommand(Action<object> executeAction) : this(executeAction, null) { }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }

            return true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
