using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AstrologyCalculator.TimespanUnits
{
    public class RelayCommand : ICommand
    {
        private Action ExecutionMethod;
        private Func<bool> CanExecuteMethod;

        public RelayCommand(Action executionMethod, Func<bool> canExecuteMethod)
        {
            ExecutionMethod = executionMethod;
            CanExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod.Invoke();
        }

        public void Execute(object parameter)
        {
            ExecutionMethod.Invoke();
        }
    }
}
