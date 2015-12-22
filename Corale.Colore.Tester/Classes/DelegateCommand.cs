
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

#pragma warning disable CS0067
        public event EventHandler CanExecuteChanged;
#pragma warning restore CS0067

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
