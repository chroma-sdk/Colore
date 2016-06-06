// ---------------------------------------------------------------------------------------
// <copyright file="KeyboardViewModel.cs" company="Corale">
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
    using Razer.Keyboard.Effects;
    using Razer.Mouse;
    using Duration = Razer.Keyboard.Effects.Duration;
    using Key = Razer.Keyboard.Key;

    public class KeyboardViewModel : INotifyPropertyChanged
    {
        private Key _selectedKey;
        private Duration _selectedReactiveDuration;
        private Direction _selectedWaveDirection;

        private StarlightType _selectedStarlightType;

        private Duration _selectedStarlightDuration;

        public KeyboardViewModel()
        {
            SelectedKey = Key.A;
            SelectedReactiveDuration = Duration.Long;
            SelectedWaveDirection = Direction.LeftToRight;
            SelectedStarlightType = StarlightType.Two;
            SelectedStarlightDuration = Duration.Long;
            ColorOne.Color = (Corale.Colore.Wpf.Color)Core.Color.Red;
            ColorTwo.Color = (Corale.Colore.Wpf.Color)Core.Color.Blue;
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
                return _selectedKey;
            }

            set
            {
                _selectedKey = value;
                OnPropertyChanged(nameof(SelectedKey));
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

        public StarlightType SelectedStarlightType
        {
            get
            {
                return _selectedStarlightType;
            }

            set
            {
                _selectedStarlightType = value;
                OnPropertyChanged(nameof(SelectedStarlightType));
            }
        }

        public Duration SelectedStarlightDuration
        {
            get
            {
                return _selectedStarlightDuration;
            }

            set
            {
                _selectedStarlightDuration = value;
                OnPropertyChanged(nameof(SelectedStarlightDuration));
            }
        }

        public ICommand AllCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetAll((Corale.Colore.Wpf.Color)ColorOne.Color));

        public ICommand BreathingCommand
            => new DelegateCommand(() => Core.Keyboard.Instance.SetBreathing((Corale.Colore.Wpf.Color)ColorOne.Color, (Corale.Colore.Wpf.Color)ColorTwo.Color));

        public ICommand ReactiveCommand
            =>
                new DelegateCommand(SetReactiveEffect);

        public ICommand WaveCommand
            => new DelegateCommand(SetWaveEffect);

        public ICommand StarlightCommand => new DelegateCommand(SetStarlightEffect);

        public ICommand StaticCommand
            => new DelegateCommand(() => Core.Keyboard.Instance.SetStatic(new Static((Corale.Colore.Wpf.Color)ColorOne.Color)));

        public ICommand IndexerCommand
            => new DelegateCommand(SetIndexerEffect);

        public ICommand KeyCommand => new DelegateCommand(SetKeyColor);

        public ICommand ClearCommand => new DelegateCommand(() => Core.Keyboard.Instance.Clear());

        public IEnumerable<Key> KeyValues
            => Enum.GetValues(typeof(Key)).Cast<Key>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<StarlightType> StarlightTypeValues
            => Enum.GetValues(typeof(StarlightType)).Cast<StarlightType>();

        public IEnumerable<Duration> StarlightDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetReactiveEffect()
        {
            try
            {
                Core.Keyboard.Instance.SetReactive((Corale.Colore.Wpf.Color)ColorOne.Color, SelectedReactiveDuration);
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
                Core.Keyboard.Instance.SetWave(SelectedWaveDirection);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetStarlightEffect()
        {
            try
            {
                Core.Keyboard.Instance.SetStarlight(
                    new Starlight(SelectedStarlightType, (Corale.Colore.Wpf.Color)ColorOne.Color, (Corale.Colore.Wpf.Color)ColorTwo.Color, SelectedStarlightDuration));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SetKeyColor()
        {
            try
            {
                Core.Keyboard.Instance[SelectedKey] = (Corale.Colore.Wpf.Color)ColorOne.Color;
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
                Core.Keyboard.Instance[Row, Col] = (Corale.Colore.Wpf.Color)ColorOne.Color;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
