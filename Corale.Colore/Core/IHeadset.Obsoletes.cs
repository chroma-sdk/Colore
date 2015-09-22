using System;
using Corale.Colore.Razer.Headset.Effects;

namespace Corale.Colore.Core
{
    /// <summary>
    /// Interface for headset functionality.
    /// </summary>
    public partial interface IHeadset : IDevice
    {
        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="Effect.SpectrumCycling" /> effect.
        /// </summary>
        /// <param name="effect">The type of effect to set.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        void Set(Effect effect);

        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Static" /> struct
        /// describing the effect.
        /// </param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        void Set(Static effect);

        /// <summary>
        /// Sets a new breathing effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Breathing" /> struct
        /// describing the effect.
        /// </param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        void Set(Breathing effect);
    }
}