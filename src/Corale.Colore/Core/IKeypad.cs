// ---------------------------------------------------------------------------------------
// <copyright file="IKeypad.cs" company="Corale">
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
    using Corale.Colore.Razer.Keypad;
    using Corale.Colore.Razer.Keypad.Effects;

    using JetBrains.Annotations;

    /// <summary>
    /// Interface for keypad functions.
    /// </summary>
    public interface IKeypad : IDevice
    {
        /// <summary>
        /// Gets or sets a color at the specified position in the keypad's
        /// grid layout.
        /// </summary>
        /// <param name="row">The row to access (between <c>0</c> and <see cref="Constants.MaxRows" />, exclusive upper-bound).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="Constants.MaxColumns" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        Color this[int row, int column] { get; set; }

        /// <summary>
        /// Returns whether a key has had a custom color set.
        /// </summary>
        /// <param name="row">The row to query.</param>
        /// <param name="column">The column to query.</param>
        /// <returns><c>true</c> if the position has a color set that is not black, otherwise <c>false</c>.</returns>
        [PublicAPI]
        bool IsSet(int row, int column);

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [PublicAPI]
        void SetCustom(Custom effect);

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        [PublicAPI]
        void SetStatic(Color color);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetEffect(Effect effect);
    }
}
