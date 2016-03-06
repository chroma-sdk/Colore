// ---------------------------------------------------------------------------------------
// <copyright file="IDevice.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
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
    using System.Collections.Generic;

    using Corale.Colore.Annotations;

    /// <summary>
    /// Interface for functionality common with all devices.
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// Gets a value indicating whether this device
        /// is connected or not.
        /// </summary>
        [PublicAPI]
        bool Connected { get; }

        /// <summary>
        /// Gets a list of connected devices for this type
        /// is connected or not.
        /// </summary>
        [PublicAPI]
        List<Guid> ConnectedDevices { get; }

        /// <summary>
        /// Gets the ID of the currently active effect.
        /// </summary>
        [PublicAPI]
        Guid CurrentEffectId { get; }

        /// <summary>
        /// Clears the current effect on the device.
        /// </summary>
        [PublicAPI]
        void Clear();

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        [PublicAPI]
        void SetAll(Color color);

        /// <summary>
        /// Updates the device to use the effect pointed to by the specified GUID.
        /// </summary>
        /// <param name="guid">GUID to set.</param>
        [PublicAPI]
        void SetGuid(Guid guid);
    }
}
