// ---------------------------------------------------------------------------------------
// <copyright file="StaticHeadsetEffect.cs" company="Corale">
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
#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Colore.Effects.Headset
{
    using System;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Static color effect.
    /// </summary>
    [JsonConverter(typeof(HeadsetStaticConverter))]
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable CA1716 // Identifiers should not match keywords
    public struct StaticHeadsetEffect : IEquatable<StaticHeadsetEffect>
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        /// <summary>
        /// The <see cref="Color" /> of the effect.
        /// </summary>
        [PublicAPI]
        public readonly Color Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticHeadsetEffect" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public StaticHeadsetEffect(Color color)
        {
            Color = color;
        }

        /// <summary>
        /// Checks an instance of <see cref="StaticHeadsetEffect" /> for equality with another <see cref="StaticHeadsetEffect" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(StaticHeadsetEffect left, StaticHeadsetEffect right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks an instance of <see cref="StaticHeadsetEffect" /> for inequality with another <see cref="StaticHeadsetEffect" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(StaticHeadsetEffect left, StaticHeadsetEffect right)
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
        public bool Equals(StaticHeadsetEffect other)
        {
            return Color.Equals(other.Color);
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

            return obj is StaticHeadsetEffect effect && Equals(effect);
        }

        /// <inheritdoc />
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }
    }
}
