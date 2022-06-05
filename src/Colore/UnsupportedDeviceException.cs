// ---------------------------------------------------------------------------------------
// <copyright file="UnsupportedDeviceException.cs" company="Corale">
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
    using System.Globalization;
    using System.Runtime.Serialization;

    using Colore.Implementations;

    using JetBrains.Annotations;

#pragma warning disable CA1032 // Implement standard exception constructors
    /// <inheritdoc />
    /// <summary>
    /// Thrown when an invalid <see cref="Guid" /> is passed to the
    /// constructor of <see cref="GenericDeviceImplementation" />.
    /// </summary>
    [Serializable]
    public sealed class UnsupportedDeviceException : ColoreException
    {
        /// <summary>
        /// Template for exception message.
        /// </summary>
        private const string MessageTemplate = "Attempted to initialize an unsupported device with ID: {0}";

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedDeviceException" /> class.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device.</param>
        /// <param name="innerException">Inner exception object.</param>
        public UnsupportedDeviceException(Guid deviceId, Exception? innerException = null)
            : base(string.Format(CultureInfo.InvariantCulture, MessageTemplate, deviceId), innerException)
        {
            DeviceId = deviceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedDeviceException" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="info" /> parameter is <see langword="null" />.
        /// </exception>
        /// <exception cref="SerializationException">
        /// The class name is <see langword="null" /> or <see cref="Exception.HResult" /> is zero (0).
        /// </exception>
        private UnsupportedDeviceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            DeviceId = new Guid(info.GetString($"{nameof(UnsupportedDeviceException)}.{nameof(DeviceId)}"));
        }

        /// <summary>
        /// Gets the <see cref="Guid" /> of the device.
        /// </summary>
        [PublicAPI]
        public Guid DeviceId { get; }

        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue($"{nameof(UnsupportedDeviceException)}.{nameof(DeviceId)}", DeviceId.ToString());
        }
    }
}
