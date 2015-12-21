using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Corale.Colore.Tester.Annotations;
using Corale.Colore.Core;

namespace Corale.Colore.Tester
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Razer.Keyboard.Key Keys { get; set; }

        public IEnumerable<Razer.Keyboard.Key> KeyValues => Enum.GetValues(typeof (Razer.Keyboard.Key)).Cast<Razer.Keyboard.Key>();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
