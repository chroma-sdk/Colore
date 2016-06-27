// ---------------------------------------------------------------------------------------
// <copyright file="MousepadViewModel.cs" company="Corale">
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Classes;
    using Razer.Mousepad.Effects;
    using Wpf;

    public class MousepadViewModel : INotifyPropertyChanged
    {
        private Direction _selectedWaveDirection;

        public MousepadViewModel()
        {
            SelectedWaveDirection = Direction.LeftToRight;
            ColorOne.Color = Core.Color.Red.ToWpfColor();
            ColorTwo.Color = Core.Color.Blue.ToWpfColor();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; set; }

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public SolidColorBrush ColorTwo { get; set; } = new SolidColorBrush();

        public Direction SelectedWaveDirection
        {
            get
            {
                return _selectedWaveDirection;
            }

            set
            {
                _selectedWaveDirection = value;
                OnPropertyChanged(nameof(SelectedWaveDirection));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Mousepad.Instance.SetAll(ColorOne.Color.ToColoreColor()));

        public ICommand BreathingCommand
            => new DelegateCommand(() => Core.Mousepad.Instance.SetBreathing(ColorOne.Color.ToColoreColor(), ColorTwo.Color.ToColoreColor()));

        public ICommand WaveCommand
            => new DelegateCommand(SetWaveEffect);

        public ICommand StaticCommand
            => new DelegateCommand(() => Core.Mousepad.Instance.SetStatic(ColorOne.Color.ToColoreColor()));

        public ICommand IndexerCommand
            => new DelegateCommand(SetIndexerEffect);

        public ICommand ClearCommand => new DelegateCommand(() => Core.Mousepad.Instance.Clear());

        public IEnumerable<Razer.Mouse.Led> LedValues
            => Enum.GetValues(typeof(Razer.Mouse.Led)).Cast<Razer.Mouse.Led>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetWaveEffect()
        {
            try
            {
                Core.Mousepad.Instance.SetWave(SelectedWaveDirection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetIndexerEffect()
        {
            try
            {
                Core.Mousepad.Instance[Index] = ColorOne.Color.ToColoreColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
