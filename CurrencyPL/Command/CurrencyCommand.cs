using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CurrencyPL.Command
{
    public class CurrencyCommand : ICommand
    {
        public Action WithoutParameterEvent { get; set; }
        public Action<string> OneParemterEvent { get; set; }

        public event EventHandler CanExecuteChanged;

        private bool _IsExecuting;
        public bool IsExecuting
        {
            get { return _IsExecuting; }
            set
            {
                _IsExecuting = value;
                CanExecuteChanged?.Invoke(this, new EventArgs());

            }
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object parameter)
        {
            IsExecuting = true;
            await ExecuteAsync(parameter);
            IsExecuting = false;
        }
        private async Task ExecuteAsync(object parameter)
        {
            if (WithoutParameterEvent != null)
            {
                await Task.Run(() => WithoutParameterEvent());
            }
            if (OneParemterEvent != null)
            {
                await Task.Run(() => OneParemterEvent(parameter?.ToString()));
            }
        }
    }
}
