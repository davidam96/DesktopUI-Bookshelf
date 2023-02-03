using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfLibrosCMVVM2.ViewModel
{
    public class Command : ICommand
    {
        readonly Action<object> accionAEjecutar;
        public Command(Action<object> execute) : this(execute, null)
        {
        }
        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            accionAEjecutar = execute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            accionAEjecutar(parameter);
        }
    }
}
