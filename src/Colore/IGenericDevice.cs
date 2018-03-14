// ---------------------------------------------------------------------------------------
// <copyright file="IGenericDevice.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

    using Colore.Effects.Generic;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for generic devices.
    /// </summary>
    public interface IGenericDevice : IDevice
    {
        /// <summary>
        /// Gets the <see cref="Guid" /> of this device.
        /// </summary>
        [PublicAPI]
        Guid DeviceId { get; }

        /// <summary>
        /// Sets a parameter-less effect on this device.
        /// </summary>
        /// <param name="effect">Effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetEffectAsync(Effect effect);

        /// <summary>
        /// Sets an effect on this device, taking a parameter.
        /// </summary>
        /// <typeparam name="T">The type of effect data to set.</typeparam>
        /// <param name="effect">Effect to set.</param>
        /// <param name="data">Effect-specific parameter to use.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        [PublicAPI]
        Task<Guid> SetEffectAsync<T>(Effect effect, T data)
            where T : struct;
    }
}
