// ---------------------------------------------------------------------------------------
// <copyright file="IMousepad.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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
    using Colore.Effects.Mousepad;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for mouse pad functionality.
    /// </summary>
    [PublicAPI]
    public interface IMousepad : IDevice
    {
        /// <summary>
        /// Gets or sets a specific LED on the mouse pad.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="Color" /> at the <paramref name="index"/>.</returns>
        Color this[int index] { get; set; }

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="StaticMousepadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetStatic(StaticMousepadEffect effect);

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetStatic(Color color);

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="StaticMousepadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetStaticAsync(StaticMousepadEffect effect);

        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetStaticAsync(Color color);

        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomMousepadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetCustom(CustomMousepadEffect effect);

        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomMousepadEffect" /> struct.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetCustomAsync(CustomMousepadEffect effect);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="MousepadEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetEffect(MousepadEffectType effectType);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="MousepadEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetEffectAsync(MousepadEffectType effectType);
    }
}
