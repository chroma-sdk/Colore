using System;
using Corale.Colore.Annotations;
using Corale.Colore.Razer.Keypad;
using Corale.Colore.Razer.Keypad.Effects;

namespace Corale.Colore.Core
{
    /// <summary>
    /// Interface for keypad functions.
    /// </summary>
    public partial interface IKeypad : IDevice
    {
        /// <summary>
        /// Sets a <see cref="Breathing" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        [PublicAPI]
        void Set(Breathing effect);

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetCustom(Custom).", false)]
        [PublicAPI]
        void Set(Custom effect);

        /// <summary>
        /// Sets a <see cref="Reactive" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Reactive" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Reactive).", false)]
        [PublicAPI]
        void Set(Reactive effect);

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        [PublicAPI]
        void Set(Static effect);

        /// <summary>
        /// Sets a <see cref="Wave" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        [PublicAPI]
        void Set(Wave effect);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        [PublicAPI]
        void Set(Effect effect);
    }
}