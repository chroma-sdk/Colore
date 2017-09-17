// ---------------------------------------------------------------------------------------
// <copyright file="IMousepad.cs" company="Corale">
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
    using Corale.Colore.Razer.Mousepad.Effects;

    /// <summary>
    /// Interface for mouse pad functionality.
    /// </summary>
    public interface IMousepad : IDevice
    {
        /// <summary>
        /// Gets or sets a specific LED on the mouse pad.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="Color" /> at the <paramref name="index"/>.</returns>
        [PublicAPI]
        Color this[int index] { get; set; }

        /// <summary>
        /// Sets a breathing effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetBreathing(Breathing effect);

        /// <summary>
        /// Sets a breathing effect on the mouse pad.
        /// </summary>
        /// <param name="first">First color to breathe into.</param>
        /// <param name="second">Second color to breathe into.</param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetBreathing(Color first, Color second);

        /// <summary>
        /// Sets an effect on the mouse pad that causes
        /// it to breathe between random colors.
        /// </summary>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetBreathing();

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        [PublicAPI]
        void SetStatic(Color color);

        /// <summary>
        /// Sets a wave effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetWave(Wave effect);

        /// <summary>
        /// Sets a wave effect on the mouse pad.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        [Obsolete("Use custom effects instead.")]
        [PublicAPI]
        void SetWave(Direction direction);

        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [PublicAPI]
        void SetCustom(Custom effect);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetEffect(Effect effect);
    }
}
