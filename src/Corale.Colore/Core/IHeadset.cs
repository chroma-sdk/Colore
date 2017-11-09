// ---------------------------------------------------------------------------------------
// <copyright file="IHeadset.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Core
{
    using System;

    using Corale.Colore.Annotations;
    using Corale.Colore.Razer.Headset.Effects;

    /// <summary>
    /// Interface for headset functionality.
    /// </summary>
    public interface IHeadset : IDevice
    {
        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="Effect.SpectrumCycling" /> effect.
        /// </summary>
        /// <param name="effect">The type of effect to set.</param>
        [PublicAPI]
        void SetEffect(Effect effect);

        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Static" /> struct
        /// describing the effect.
        /// </param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a new <see cref="Static" /> effect on
        /// the headset using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> of the effect.</param>
        [PublicAPI]
        void SetStatic(Color color);

        /// <summary>
        /// Sets a new breathing effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Breathing" /> struct
        /// describing the effect.
        /// </param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetBreathing(Breathing effect);

        /// <summary>
        /// Sets a new <see cref="Breathing" /> effect on the headset
        /// using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color"/> of the effect.</param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetBreathing(Color color);
    }
}
