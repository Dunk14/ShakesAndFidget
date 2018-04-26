using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShakesAndFidgetLibrary.Utils
{


    public class DelegateCommand : ICommand
    {
        public delegate void SimpleEventHandler();
        private SimpleEventHandler handler;
        private bool isEnabled = true;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(SimpleEventHandler handler)
        {
            this.handler = handler;
        }

        private void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        bool ICommand.CanExecute(object arg)
        {
            return IsEnabled;
        }

        void ICommand.Execute(object arg)
        {
            this.handler();
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }

            set
            {
                this.isEnabled = value;
                OnCanExecuteChanged();
            }
        }
    }
}
