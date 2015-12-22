// ---------------------------------------------------------------------------------------
// <copyright file="KeypadViewModel.cs" company="Corale">
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
    using System.Configuration;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Corale.Colore.Razer.Keypad.Effects;
    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Tester.Classes;
    using Duration = Corale.Colore.Razer.Keypad.Effects.Duration;
    using Key = Corale.Colore.Razer.Keyboard.Key;

    public class KeypadViewModel : INotifyPropertyChanged
    {
        private Key _selectedKey;
        private Duration _selectedReactiveDuration;
        private Direction _selectedWaveDirection;

        public KeypadViewModel()
        {
            this.SelectedKey = Key.A;
            this.SelectedReactiveDuration = Duration.Long;
            this.SelectedWaveDirection = Direction.LeftToRight;
            ColorOne.Color = Core.Color.Red;
            ColorTwo.Color = Core.Color.Blue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Col { get; set; }

        public int Row { get; set; }

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public SolidColorBrush ColorTwo { get; set; } = new SolidColorBrush();

        public Led Keys { get; set; }

        public Key SelectedKey
        {
            get
            {
                return this._selectedKey;
            }

            set
            {
                this._selectedKey = value;
                this.OnPropertyChanged(nameof(this.SelectedKey));
            }
        }

        public Duration SelectedReactiveDuration
        {
            get
            {
                return this._selectedReactiveDuration;
            }

            set
            {
                this._selectedReactiveDuration = value;
                this.OnPropertyChanged(nameof(this.SelectedReactiveDuration));
            }
        }

        public Direction SelectedWaveDirection
        {
            get
            {
                return _selectedWaveDirection;
            }

            set
            {
                _selectedWaveDirection = value;
                this.OnPropertyChanged(nameof(SelectedWaveDirection));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Keypad.Instance.SetAll(ColorOne.Color));

        public ICommand BreathingCommand
            => new DelegateCommand(() => Core.Keypad.Instance.SetBreathing(ColorOne.Color, ColorTwo.Color));

        public ICommand ReactiveCommand
            =>
                new DelegateCommand(SetReactiveEffect);

        public ICommand WaveCommand
            => new DelegateCommand(SetWaveEffect);

        public ICommand StaticCommand
            => new DelegateCommand(() => Core.Keypad.Instance.SetStatic(new Static(ColorOne.Color)));

        public ICommand IndexerCommand
            => new DelegateCommand(SetIndexerEffect);

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        public ICommand ClearCommand => new DelegateCommand(() => Core.Keypad.Instance.Clear());

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetReactiveEffect()
        {
            try
            {
                Core.Keypad.Instance.SetReactive(this.SelectedReactiveDuration, ColorOne.Color);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetWaveEffect()
        {
            try
            {
                Core.Keypad.Instance.SetWave(this.SelectedWaveDirection);
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
                Core.Keypad.Instance[this.Row, this.Col] = ColorOne.Color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
