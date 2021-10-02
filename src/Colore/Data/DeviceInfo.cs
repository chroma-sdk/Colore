// ---------------------------------------------------------------------------------------
// <copyright file="DeviceInfo.cs" company="Corale">
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

namespace Colore.Data
{
    using System;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Contains information about a device.
    /// </summary>
    public struct DeviceInfo : IEquatable<DeviceInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceInfo" /> struct.
        /// </summary>
        /// <param name="baseInfo">Instance of <see cref="SdkDeviceInfo" /> to copy base data from.</param>
        /// <param name="deviceId">The ID of the device.</param>
        /// <param name="metadata">Instance of <see cref="Devices.Metadata" /> to copy metadata from.</param>
        internal DeviceInfo(SdkDeviceInfo baseInfo, Guid deviceId, Devices.Metadata metadata)
        {
            Id = deviceId;
            Type = baseInfo.Type;
            Connected = baseInfo.Connected;
            Name = metadata.Name;
            Description = metadata.Description;
        }

        /// <summary>
        /// Gets the unique ID of the device.
        /// </summary>
        [PublicAPI]
        public Guid Id { get; }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        [PublicAPI]
        public DeviceType Type { get; }

        /// <summary>
        /// Gets a value indicating whether the device is currently connected.
        /// </summary>
        [PublicAPI]
        public bool Connected { get; }

        /// <summary>
        /// Gets the device name.
        /// </summary>
        [PublicAPI]
        public string Name { get; }

        /// <summary>
        /// Gets a description of the device.
        /// </summary>
        [PublicAPI]
        public string Description { get; }

        /// <summary>
        /// Compares an instance of <see cref="DeviceInfo" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="DeviceInfo" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(DeviceInfo left, object right) => left.Equals(right);

        /// <summary>
        /// Compares an instance of <see cref="DeviceInfo" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="DeviceInfo" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(DeviceInfo left, object right) => !left.Equals(right);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(DeviceInfo other)
        {
            return Id.Equals(other.Id) && Type == other.Type && Connected == other.Connected &&
                   Name == other.Name && Description == other.Description;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return !(obj is null) && obj is DeviceInfo info && Equals(info);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)Type;
                hashCode = (hashCode * 397) ^ Connected.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode(StringComparison.InvariantCulture) ?? 0);
                hashCode = (hashCode * 397) ^ (Description?.GetHashCode(StringComparison.InvariantCulture) ?? 0);
                return hashCode;
            }
        }
    }
}
