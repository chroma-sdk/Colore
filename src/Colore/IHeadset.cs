// ---------------------------------------------------------------------------------------
// <copyright file="IHeadset.cs" company="Corale">
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
    using Colore.Effects.Headset;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for headset functionality.
    /// </summary>
    [PublicAPI]
    public interface IHeadset : IDevice
    {
        /// <summary>
        /// Gets or sets the color of a specific LED on the headset.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="Color" /> at the <paramref name="index" />.</returns>
        Color this[int index] { get; set; }

        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="HeadsetEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">The type of effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetEffect(HeadsetEffectType effectType);

        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="HeadsetEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">The type of effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetEffectAsync(HeadsetEffectType effectType);

        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="StaticHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetStatic(StaticHeadsetEffect effect);

        /// <summary>
        /// Sets a new <see cref="StaticHeadsetEffect" /> effect on
        /// the headset using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> of the effect.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetStatic(Color color);

        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="StaticHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetStaticAsync(StaticHeadsetEffect effect);

        /// <summary>
        /// Sets a new <see cref="StaticHeadsetEffect" /> effect on
        /// the headset using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> of the effect.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetStaticAsync(Color color);

        /// <summary>
        /// Sets a new <see cref="CustomHeadsetEffect" /> effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="CustomHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetCustom(CustomHeadsetEffect effect);

        /// <summary>
        /// Sets a new <see cref="CustomHeadsetEffect" /> effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="CustomHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetCustomAsync(CustomHeadsetEffect effect);
    }
}
