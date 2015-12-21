namespace Corale.Colore.Tester
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using Annotations;
    using Classes;
    using Corale.Colore.Core;
    using Corale.Colore.Razer.Keyboard.Effects;
    using Key = Razer.Keyboard.Key;
    using Static = Razer.Keyboard.Effects.Static;

    public class ViewModel : INotifyPropertyChanged
    {
        private Key _selectedKey;
        private Duration _selectedReactiveDuration;
        private Direction _selectedWaveDirection;

        public ViewModel()
        {
            this.SelectedKey = Key.A;
            this.SelectedReactiveDuration = Duration.Long;
            this.SelectedWaveDirection = Direction.LeftToRight;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Key Keys { get; set; }

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

        public ICommand AllCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetAll(Color.Red));

        public ICommand BreathingCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetBreathing(Color.Red, Color.Blue));

        public ICommand ReactiveCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetReactive(Color.Red, this.SelectedReactiveDuration));

        public ICommand WaveCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetWave(this.SelectedWaveDirection));

        public ICommand StaticCommand => new DelegateCommand(() => Core.Keyboard.Instance.SetStatic(new Static(Color.Red)));

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
