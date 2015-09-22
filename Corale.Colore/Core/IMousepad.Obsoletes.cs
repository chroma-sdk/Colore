using System;
using Corale.Colore.Annotations;
using Corale.Colore.Razer.Mousepad.Effects;

namespace Corale.Colore.Core
{
    /// <summary>
    /// Interface for mouse pad functionality.
    /// </summary>
    public partial interface IMousepad : IDevice
    {
        /// <summary>
        /// Sets a breathing effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        [PublicAPI]
        void Set(Breathing effect);

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        [PublicAPI]
        void Set(Static effect);

        /// <summary>
        /// Sets a wave effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        [PublicAPI]
        void Set(Wave effect);

        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetCustom(Custom).", false)]
        [PublicAPI]
        void Set(Custom effect);

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