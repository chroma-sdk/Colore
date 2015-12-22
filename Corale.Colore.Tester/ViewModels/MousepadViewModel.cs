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
    using System.Windows.Input;
    using System.Windows.Media;
    using Corale.Colore.Razer.Mousepad.Effects;
    using Corale.Colore.Tester.Classes;

    public class MousepadViewModel : INotifyPropertyChanged
    {
        private Direction selectedWaveDirection;

        public MousepadViewModel()
        {
            this.SelectedWaveDirection = Direction.LeftToRight;
            this.ColorOne.Color = Core.Color.Red;
            this.ColorTwo.Color = Core.Color.Blue;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        public int Index { get; set; }

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public SolidColorBrush ColorTwo { get; set; } = new SolidColorBrush();

        public Direction SelectedWaveDirection
        {
            get
            {
                return this.selectedWaveDirection;
            }

            set
            {
                this.selectedWaveDirection = value;
                this.OnPropertyChanged(nameof(this.SelectedWaveDirection));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Mousepad.Instance.SetAll(this.ColorOne.Color));

        public ICommand BreathingCommand
            => new DelegateCommand(() => Core.Mousepad.Instance.SetBreathing(this.ColorOne.Color, this.ColorTwo.Color));

        public ICommand WaveCommand
            => new DelegateCommand(() => Core.Mousepad.Instance.SetWave(this.SelectedWaveDirection));

        public ICommand StaticCommand
            => new DelegateCommand(() => Core.Mousepad.Instance.SetStatic(this.ColorOne.Color));

        public ICommand IndexerCommand
            => new DelegateCommand(() => Core.Mousepad.Instance[this.Index] = this.ColorOne.Color);

        public ICommand ClearCommand => new DelegateCommand(() => Core.Mousepad.Instance.Clear());

        public IEnumerable<Razer.Mouse.Led> LedValues
            => Enum.GetValues(typeof(Razer.Mouse.Led)).Cast<Razer.Mouse.Led>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
