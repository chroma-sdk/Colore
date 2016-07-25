// ---------------------------------------------------------------------------------------
// <copyright file="IGenericDevice.cs" company="Corale">
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
    using Corale.Colore.Razer;
    using Corale.Colore.Razer.Effects;

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
        [PublicAPI]
        void SetEffect(Effect effect);

        /// <summary>
        /// Sets an effect on this device, taking a parameter.
        /// </summary>
        /// <param name="effect">Effect to set.</param>
        /// <param name="param">Effect-specific parameter to use.</param>
        [PublicAPI]
        void SetEffect(Effect effect, IntPtr param);

        /// <summary>
        /// Sets a blinking effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetBlinking(Blinking effect);

        /// <summary>
        /// Sets a breathing effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetBreathing(Breathing effect);

        /// <summary>
        /// Sets a custom effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetCustom(Custom effect);

        /// <summary>
        /// Sets a reactive effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetReactive(Reactive effect);

        /// <summary>
        /// Sets a spectrum cycling effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetSpectrumCycling(SpectrumCycling effect);

        /// <summary>
        /// Sets a starlight effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetStarlight(Starlight effect);

        /// <summary>
        /// Sets a static effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a wave effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetWave(Wave effect);
    }
}
