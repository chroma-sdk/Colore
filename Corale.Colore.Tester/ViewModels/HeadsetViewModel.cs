// ---------------------------------------------------------------------------------------
// <copyright file="HeadsetViewModel.cs" company="Corale">
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

    public class HeadsetViewModel : INotifyPropertyChanged
    {
        public HeadsetViewModel()
        {
            ColorOne.Color = Core.Color.Red;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public ICommand AllCommand => new DelegateCommand(() => Core.Headset.Instance.SetAll(ColorOne.Color));

        public ICommand BreathingCommand => new DelegateCommand(() => Core.Headset.Instance.SetBreathing(ColorOne.Color));

        public ICommand StaticCommand => new DelegateCommand(() => Core.Headset.Instance.SetStatic(ColorOne.Color));

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}