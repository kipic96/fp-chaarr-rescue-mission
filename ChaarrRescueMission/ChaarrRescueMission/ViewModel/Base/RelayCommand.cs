using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ChaarrRescueMission.ViewModel.Base
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeDelegate;
        private Predicate<object> _canExecuteDelegate;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute function is null");

            _executeDelegate = execute;
            _canExecuteDelegate = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecuteDelegate == null ? true : _canExecuteDelegate(parameters);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            _executeDelegate(parameters);
        }
    }
}
