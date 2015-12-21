using Corale.Colore.Core;
using Corale.Colore.Razer;

namespace Corale.Colore.Tester.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Media;
    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;
    using Corale.Colore.Tester.Classes;

    public class MouseViewModel : INotifyPropertyChanged
    {
        private Led selectedLed;
        private Duration selectedReactiveDuration;
        private Direction selectedWaveDirection;

        public MouseViewModel()
        {
            this.SelectedLed = Led.All;
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
                return this.selectedLed;
            }

            set
            {
                this.selectedLed = value;
                this.OnPropertyChanged(nameof(this.SelectedLed));
            }
        }

        public Duration SelectedReactiveDuration
        {
            get
            {
                return this.selectedReactiveDuration;
            }

            set
            {
                this.selectedReactiveDuration = value;
                this.OnPropertyChanged(nameof(this.SelectedReactiveDuration));
            }
        }

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

        public ICommand AllCommand => new DelegateCommand(() => Core.Mouse.Instance.SetAll(this.ColorOne.Color));

        public ICommand BreathingCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBreathing(this.ColorOne.Color, this.ColorTwo.Color));

        public ICommand ReactiveCommand => new DelegateCommand(() => Core.Mouse.Instance.SetReactive(this.SelectedReactiveDuration, this.ColorOne.Color, this.SelectedLed));

        public ICommand WaveCommand => new DelegateCommand(() => Core.Mouse.Instance.SetWave(this.SelectedWaveDirection));

        public ICommand StaticCommand => new DelegateCommand(() => Core.Mouse.Instance.SetStatic(new Static(this.SelectedLed, this.ColorOne.Color)));

        public ICommand IndexerCommand
            => new DelegateCommand(() => Core.Mouse.Instance[this.Row, this.Col] = this.ColorOne.Color);

        public ICommand KeyCommand => new DelegateCommand(() => Core.Mouse.Instance[this.SelectedLed] = this.ColorOne.Color);

        public ICommand BlinkingCommand => new DelegateCommand(() => Core.Mouse.Instance.SetBlinking(this.ColorOne.Color, this.SelectedLed));

        public IEnumerable<Razer.Mouse.Led> LedValues => Enum.GetValues(typeof(Razer.Mouse.Led)).Cast<Razer.Mouse.Led>();

        public IEnumerable<Direction> WaveDirectionValues => Enum.GetValues(typeof(Direction)).Cast<Direction>();

        public IEnumerable<Duration> ReactiveDurationValues => Enum.GetValues(typeof(Duration)).Cast<Duration>();

        [Annotations.NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
