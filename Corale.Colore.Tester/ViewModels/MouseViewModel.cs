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
    using Classes;
    using Razer.Mouse;
    using Razer.Mouse.Effects;
    using Duration = Razer.Mouse.Effects.Duration;

    public class MouseViewModel : INotifyPropertyChanged
    {
        private Led _selectedLed;

        private GridLed _selectedGridLed;

        private Duration _selectedReactiveDuration;

        private Direction _selectedWaveDirection;

        public MouseViewModel()
        {
            SelectedLed = Led.All;
            SelectedGridLed = GridLed.Logo;
            SelectedReactiveDuration = Duration.Long;
            SelectedWaveDirection = Direction.FrontToBack;
            ColorOne.Color = Core.Color.Red;
            ColorTwo.Color = Core.Color.Blue;
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
                return _selectedLed;
            }

            set
            {
                _selectedLed = value;
                OnPropertyChanged(nameof(SelectedLed));
            }
        }

        public GridLed SelectedGridLed
        {
            get
            {
                return _selectedGridLed;
            }

            set
            {
                _selectedGridLed = value;
                OnPropertyChanged(nameof(SelectedGridLed));
            }
        }

        public Duration SelectedReactiveDuration
        {
            get
            {
                return _selectedReactiveDuration;
            }

            set
            {
                _selectedReactiveDuration = value;
                OnPropertyChanged(nameof(SelectedReactiveDuration));
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
                OnPropertyChanged(nameof(SelectedWaveDirection));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Mouse.Instance.SetAll(ColorOne.Color));

        public ICommand BreathingOneColorCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(ColorOne.Color, SelectedLed));

        public ICommand BreathingTwoColorCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(ColorOne.Color, ColorTwo.Color));

        public ICommand BreathingRandomColorCommand
            => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(SelectedLed));

        public ICommand ReactiveCommand => new DelegateCommand(SetReactiveEffect);

        public ICommand WaveCommand => new DelegateCommand(SetWaveEffect);

        public ICommand StaticCommand => new DelegateCommand(() => Core.Mouse.Instance.SetStatic(new Static(SelectedLed, ColorOne.Color)));

        public ICommand GridLedCommand
            => new DelegateCommand(SetGridLedEffect);

        public ICommand LedCommand => new DelegateCommand(SetLedEffect);

        public ICommand BlinkingCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBlinking(ColorOne.Color, SelectedLed));

        public ICommand ClearCommand => new DelegateCommand(() => Core.Mouse.Instance.Clear());

        public IEnumerable<Led> LedValues => Enum.GetValues(typeof(Led)).Cast<Led>();

        public IEnumerable<GridLed> GridLedValues => Enum.GetValues(typeof(GridLed)).Cast<GridLed>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        public string Connected => "Connected: " + Core.Mouse.Instance.Connected.ToString();

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetReactiveEffect()
        {
            try
            {
                Core.Mouse.Instance.SetReactive(SelectedReactiveDuration, ColorOne.Color, SelectedLed);
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
                Core.Mouse.Instance.SetWave(SelectedWaveDirection);
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
                Core.Mouse.Instance[SelectedGridLed] = ColorOne.Color;
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
                Core.Mouse.Instance[SelectedLed] = ColorOne.Color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
