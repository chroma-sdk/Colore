// ---------------------------------------------------------------------------------------
// <copyright file="UnsupportedDeviceException.cs" company="Corale">
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

namespace Corale.Colore.Razer
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Permissions;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Thrown when an invalid <see cref="Guid" /> is passed to the
    /// constructor of <see cref="GenericDevice" />.
    /// </summary>
    [Serializable]
    public sealed class UnsupportedDeviceException : ColoreException
    {
        /// <summary>
        /// Template for exception message.
        /// </summary>
        private const string MessageTemplate = "Attempted to initialize an unsupported device with ID: {0}";

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedDeviceException" /> class.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device.</param>
        /// <param name="innerException">Inner exception object.</param>
        internal UnsupportedDeviceException(Guid deviceId, Exception innerException = null)
            : base(string.Format(CultureInfo.InvariantCulture, MessageTemplate, deviceId), innerException)
        {
            DeviceId = deviceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedDeviceException" /> class
        /// from serialization data.
        /// </summary>
        /// <param name="info">Serialization info object.</param>
        /// <param name="context">Streaming context.</param>
        private UnsupportedDeviceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            DeviceId = (Guid)info.GetValue("DeviceId", typeof(Guid));
        }

        /// <summary>
        /// Gets the <see cref="Guid" /> of the device.
        /// </summary>
        [PublicAPI]
        public Guid DeviceId { get; }

        /// <summary>
        /// Adds object data to serialization object.
        /// </summary>
        /// <param name="info">Serialization info object.</param>
        /// <param name="context">Streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("DeviceId", DeviceId);
        }
    }
}
