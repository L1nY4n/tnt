using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TNT
{
    class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged =(sender,e)=>{ };
        private readonly Action _action;
        public RelayCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
