using Corale.Colore.Razer.Mouse;

namespace Corale.Colore.Tester
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using Annotations;
    using Classes;
    using Corale.Colore.Core;
    using Corale.Colore.Razer.Keyboard.Effects;
    using Key = Razer.Keyboard.Key;
    using Static = Razer.Keyboard.Effects.Static;

    public class KeyboardViewModel : INotifyPropertyChanged
    {

        private Key _selectedKey;
        private Duration _selectedReactiveDuration;
        private Direction _selectedWaveDirection;

        public KeyboardViewModel()
        {
            this.SelectedKey = Key.A;
            this.SelectedReactiveDuration = Duration.Long;
            this.SelectedWaveDirection = Direction.LeftToRight;
            ColorOne.Color = Core.Color.Red;
            ColorTwo.Color = Core.Color.Blue;
        }

        public int Col { get; set; }

        public int Row { get; set; }

        public SolidColorBrush ColorOne { get; set; } = new SolidColorBrush();

        public SolidColorBrush ColorTwo { get; set; } = new SolidColorBrush();

        public event PropertyChangedEventHandler PropertyChanged;

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

        public ICommand AllCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetAll(ColorOne.Color));

        public ICommand BreathingCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetBreathing(ColorOne.Color, ColorTwo.Color));

        public ICommand ReactiveCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetReactive(ColorOne.Color, this.SelectedReactiveDuration));

        public ICommand WaveCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetWave(this.SelectedWaveDirection));

        public ICommand StaticCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetStatic(new Static(ColorOne.Color)));

        public ICommand IndexerCommand
            => new DelegateCommand(() => Core.Keyboard.Instance[this.Row, this.Col] = ColorOne.Color);

        public ICommand KeyCommand => new DelegateCommand(() => Core.Keyboard.Instance[SelectedKey] = ColorOne.Color);

        public IEnumerable<Razer.Keyboard.Key> KeyValues => Enum.GetValues(typeof(Razer.Keyboard.Key)).Cast<Razer.Keyboard.Key>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
