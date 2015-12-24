// ---------------------------------------------------------------------------------------
// <copyright file="MiscViewModel.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Tester.ViewModels
{
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Windows.Media;
    using Corale.Colore.Annotations;
    using Corale.Colore.Tester.Classes;
    using Color = Corale.Colore.Core.Color;

    public class MiscViewModel : INotifyPropertyChanged
    {
        public MiscViewModel()
        {
            ColorOne.Color = Core.Color.Red;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public string QueryGuid { get; set; }

        public ICommand AllCommand => new DelegateCommand(() => Core.Chroma.Instance.SetAll(ColorOne.Color));

        public ICommand InitializeCommand => new DelegateCommand(() => Core.Chroma.Instance.Initialize());

        public ICommand UninitializeCommand => new DelegateCommand(() => Core.Chroma.Instance.Uninitialize());

        public ICommand ClearCommand => new DelegateCommand(() => Core.Chroma.Instance.SetAll(Color.Black));

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
