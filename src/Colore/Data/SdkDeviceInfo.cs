// ---------------------------------------------------------------------------------------
// <copyright file="SdkDeviceInfo.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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
#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Colore.Data
{
    using System;
    using System.Runtime.InteropServices;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Information about a device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SdkDeviceInfo : IEquatable<SdkDeviceInfo>
    {
        /// <summary>
        /// The type of device this is.
        /// </summary>
        [PublicAPI]
        public readonly DeviceType Type;

        /// <summary>
        /// Whether this device is connected.
        /// </summary>
        [PublicAPI]
        public readonly bool Connected;

        /// <summary>
        /// Initializes a new instance of the <see cref="SdkDeviceInfo" /> struct.
        /// </summary>
        /// <param name="type">The type of device.</param>
        /// <param name="connected">Whether the device is currently connected.</param>
        public SdkDeviceInfo(DeviceType type, bool connected)
        {
            Type = type;
            Connected = connected;
        }

        /// <summary>
        /// Checks an instance of <see cref="SdkDeviceInfo" /> for equality with another <see cref="SdkDeviceInfo" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(SdkDeviceInfo left, SdkDeviceInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks an instance of <see cref="SdkDeviceInfo" /> for inequality with another <see cref="SdkDeviceInfo" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(SdkDeviceInfo left, SdkDeviceInfo right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(SdkDeviceInfo other)
        {
            return Type == other.Type && Connected == other.Connected;
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            return obj is SdkDeviceInfo info && Equals(info);
        }

        /// <inheritdoc />
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Type * 397) ^ Connected.GetHashCode();
            }
        }
    }
}
