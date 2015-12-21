
namespace Corale.Colore.Tester.Classes
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        public DelegateCommand(Action action)
        {
            this.CommandAction = action;
        }

        public event EventHandler CanExecuteChanged;

        public Action CommandAction { get; }

        public void Execute(object parameter)
        {
            this.CommandAction();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
