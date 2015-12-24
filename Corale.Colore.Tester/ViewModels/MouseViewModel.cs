// ---------------------------------------------------------------------------------------
// <copyright file="MouseViewModel.cs" company="Corale">
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
    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;
    using Corale.Colore.Tester.Classes;
    using Duration = Corale.Colore.Razer.Mouse.Effects.Duration;

    public class MouseViewModel : INotifyPropertyChanged
    {
        private Led _selectedLed;

        private GridLed _selectedGridLed;

        private Duration _selectedReactiveDuration;

        private Direction _selectedWaveDirection;

        public MouseViewModel()
        {
            this.SelectedLed = Led.All;
            this.SelectedGridLed = GridLed.Logo;
            this.SelectedReactiveDuration = Duration.Long;
            this.SelectedWaveDirection = Direction.FrontToBack;
            this.ColorOne.Color = Core.Color.Red;
            this.ColorTwo.Color = Core.Color.Blue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int Col { get; set; }

        public int Row { get; set; }

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public SolidColorBrush ColorTwo { get; set; } = new SolidColorBrush();

        public Led Leds { get; set; }

        public Led SelectedLed
        {
            get
            {
                return this._selectedLed;
            }

            set
            {
                this._selectedLed = value;
                this.OnPropertyChanged(nameof(this.SelectedLed));
            }
        }

        public GridLed SelectedGridLed
        {
            get
            {
                return this._selectedGridLed;
            }

            set
            {
                this._selectedGridLed = value;
                this.OnPropertyChanged(nameof(this.SelectedGridLed));
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
                return this._selectedWaveDirection;
            }

            set
            {
                this._selectedWaveDirection = value;
                this.OnPropertyChanged(nameof(this.SelectedWaveDirection));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Mouse.Instance.SetAll(this.ColorOne.Color));

        public ICommand BreathingOneColorCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(this.ColorOne.Color, SelectedLed));

        public ICommand BreathingTwoColorCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(this.ColorOne.Color, this.ColorTwo.Color));

        public ICommand BreathingRandomColorCommand
            => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(SelectedLed));

        public ICommand ReactiveCommand => new DelegateCommand(SetReactiveEffect);

        public ICommand WaveCommand => new DelegateCommand(SetWaveEffect);

        public ICommand StaticCommand => new DelegateCommand(() => Core.Mouse.Instance.SetStatic(new Static(this.SelectedLed, this.ColorOne.Color)));

        public ICommand GridLedCommand
            => new DelegateCommand(SetGridLedEffect);

        public ICommand LedCommand => new DelegateCommand(SetLedEffect);

        public ICommand BlinkingCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBlinking(this.ColorOne.Color, this.SelectedLed));

        public ICommand ClearCommand => new DelegateCommand(() => Core.Mouse.Instance.Clear());

        public IEnumerable<Razer.Mouse.Led> LedValues => Enum.GetValues(typeof(Razer.Mouse.Led)).Cast<Razer.Mouse.Led>();

        public IEnumerable<Razer.Mouse.GridLed> GridLedValues => Enum.GetValues(typeof(Razer.Mouse.GridLed)).Cast<Razer.Mouse.GridLed>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetReactiveEffect()
        {
            try
            {
                Core.Mouse.Instance.SetReactive(this.SelectedReactiveDuration, this.ColorOne.Color, this.SelectedLed);
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
                Core.Mouse.Instance.SetWave(this.SelectedWaveDirection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetGridLedEffect()
        {
            try
            {
                Core.Mouse.Instance[SelectedGridLed] = this.ColorOne.Color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetLedEffect()
        {
            try
            {
                Core.Mouse.Instance[this.SelectedLed] = this.ColorOne.Color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
