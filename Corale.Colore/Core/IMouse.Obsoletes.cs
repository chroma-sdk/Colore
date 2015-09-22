using System;
using Corale.Colore.Annotations;
using Corale.Colore.Razer.Mouse;
using Corale.Colore.Razer.Mouse.Effects;

namespace Corale.Colore.Core
{
    /// <summary>
    /// Interface for mouse functionality.
    /// </summary>
    public partial interface IMouse : IDevice
    {
        /// <summary>
        /// Sets the color of a specific LED on the mouse.
        /// </summary>
        /// <param name="led">Which LED to modify.</param>
        /// <param name="color">Color to set.</param>
        [Obsolete("Set is deprecated, please use SetLed(Led, Color).", false)]
        [PublicAPI]
        void Set(Led led, Color color);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        [PublicAPI]
        void Set(Effect effect);

        /// <summary>
        /// Sets a breathing effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> effect.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        [PublicAPI]
        void Set(Breathing effect);

        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> effect.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        [PublicAPI]
        void Set(Static effect);

        /// <summary>
        /// Starts a blinking effect on the specified LED.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Blinking" /> effect.</param>
        [Obsolete("Set is deprecated, please use SetBlinking(Blinking).", false)]
        void Set(Blinking effect);

        /// <summary>
        /// Sets a reactive effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Reactive).", false)]
        void Set(Reactive effect);

        /// <summary>
        /// Sets a spectrum cycling effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        [Obsolete("Set is deprecated, please use SetSpectrumCycling(SpectrumCycling).", false)]
        void Set(SpectrumCycling effect);

        /// <summary>
        /// Sets a wave effect on the mouse.
        /// </summary>
        /// <param name="effect">Effect options struct.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        void Set(Wave effect);
    }
}