using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ChaarrRescueMission.ViewModel.Base
{
    class NoParameterCommand : ICommand
    {
        private Action _executeDelegate = null;
        private Func<bool> _canExecuteDelegate = null;

        public NoParameterCommand(Action execute)
        {
            _executeDelegate = execute;
            _canExecuteDelegate = () => { return true; };
        }

        public NoParameterCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Execute function is null");

            _executeDelegate = execute;
            _canExecuteDelegate = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameters)
        {
            return _canExecuteDelegate();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameters)
        {
            if (_executeDelegate != null)
            {
                _executeDelegate();
            }
        }
    }
}
