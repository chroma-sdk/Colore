// ---------------------------------------------------------------------------------------
// <copyright file="UnsupportedDeviceException.cs" company="Corale">
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

namespace Corale.Colore.Razer
{
    using System;
    using System.Globalization;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Thrown when an invalid <see cref="T:System.Guid" /> is passed to the
    /// constructor of <see cref="T:Corale.Colore.Core.GenericDevice" />.
    /// </summary>
    public sealed class UnsupportedDeviceException : ColoreException
    {
        /// <summary>
        /// Template for exception message.
        /// </summary>
        private const string MessageTemplate = "Attempted to initialize an unsupported device with ID: {0}";

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Corale.Colore.Razer.UnsupportedDeviceException" /> class.
        /// </summary>
        /// <param name="deviceId">The <see cref="T:System.Guid" /> of the device.</param>
        /// <param name="innerException">Inner exception object.</param>
        public UnsupportedDeviceException(Guid deviceId, Exception innerException = null)
            : base(string.Format(CultureInfo.InvariantCulture, MessageTemplate, deviceId), innerException)
        {
            DeviceId = deviceId;
        }

        /// <summary>
        /// Gets the <see cref="Guid" /> of the device.
        /// </summary>
        [PublicAPI]
        public Guid DeviceId { get; }
    }
}
