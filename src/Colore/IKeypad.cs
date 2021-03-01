// ---------------------------------------------------------------------------------------
// <copyright file="IKeypad.cs" company="Corale">
//     Copyright © 2015-2021 by Adam Hellberg and Brandon Scott.
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

namespace Colore
{
    using System;
    using System.Threading.Tasks;

    using Colore.Data;
    using Colore.Effects.Keypad;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for keypad functions.
    /// </summary>
    public interface IKeypad : IDevice
    {
        /// <summary>
        /// Gets or sets a color at the specified position in the keypad's grid layout.
        /// </summary>
        /// <param name="row">The row to access (between <c>0</c> and <see cref="KeypadConstants.MaxRows" />, exclusive upper-bound).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="KeypadConstants.MaxColumns" />, exclusive upper-bound).</param>
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
        /// Sets a <see cref="CustomKeypadEffect" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomKeypadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetCustomAsync(CustomKeypadEffect effect);

        /// <summary>
        /// Sets a <see cref="StaticKeypadEffect" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="StaticKeypadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetStaticAsync(StaticKeypadEffect effect);

        /// <summary>
        /// Sets a <see cref="StaticKeypadEffect" /> effect on the keypad.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetStaticAsync(Color color);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="KeypadEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetEffectAsync(KeypadEffectType effectType);
    }
}
