using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CloudMusic.UWP.ViewModels.Base
{
    public class Command:ICommand
    {
        private Predicate<object> _canExecute;
        private Action<object> _execute;

        public Command(Predicate<object> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
