// ---------------------------------------------------------------------------------------
// <copyright file="IMouse.cs" company="Corale">
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
    using Colore.Effects.Mouse;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for mouse functionality.
    /// </summary>
    public interface IMouse : IDevice
    {
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific position
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="row">The row to query, between <c>0</c> and <see cref="MouseConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">The column to query, between <c>0</c> and <see cref="MouseConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        Color this[int row, int column] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specified <see cref="GridLed" />
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="led">The <see cref="GridLed" /> to query.</param>
        /// <returns>The <see cref="Color" /> currently set for the specified <see cref="GridLed" />.</returns>
        [PublicAPI]
        Color this[GridLed led] { get; set; }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="MouseEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetEffectAsync(MouseEffectType effectType);

        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="StaticMouseEffect" /> effect.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetStaticAsync(StaticMouseEffect effect);

        /// <summary>
        /// Sets a static effect on the mouse.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetStaticAsync(Color color, Led led = Led.All);

        /// <summary>
        /// Sets a custom grid effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomMouseEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetGridAsync(CustomMouseEffect effect);
    }
}
