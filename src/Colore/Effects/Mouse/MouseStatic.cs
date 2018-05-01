// ---------------------------------------------------------------------------------------
// <copyright file="MouseStatic.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Effects.Mouse
{
    using System;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Describes the static effect type.
    /// </summary>
    [JsonConverter(typeof(MouseStaticConverter))]
    [StructLayout(LayoutKind.Sequential)]
#pragma warning disable CA1716 // Identifiers should not match keywords
    public struct MouseStatic : IEquatable<MouseStatic>
#pragma warning restore CA1716 // Identifiers should not match keywords
    {
        /// <summary>
        /// The LED on which to apply the color.
        /// </summary>
        [UsedImplicitly]
        public readonly Led Led;

        /// <summary>
        /// The color to apply.
        /// </summary>
        [UsedImplicitly]
        public readonly Color Color;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseStatic" /> struct,
        /// with a color to set for every LED.
        /// </summary>
        /// <param name="color">The colo to set for every LED.</param>
        public MouseStatic(Color color)
            : this(Led.All, color)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MouseStatic" /> struct.
        /// </summary>
        /// <param name="led">The <see cref="Led" /> on which to apply the color.</param>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public MouseStatic(Led led, Color color)
        {
            Led = led;
            Color = color;
        }

        /// <summary>
        /// Compares an instance of <see cref="MouseStatic" /> for equality with another <see cref="MouseStatic" /> struct.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(MouseStatic left, MouseStatic right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="MouseStatic" /> for inequality with another <see cref="MouseStatic" /> struct.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(MouseStatic left, MouseStatic right)
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
        public bool Equals(MouseStatic other)
        {
            return Led == other.Led && Color.Equals(other.Color);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="other">The object to compare with the current instance. </param>
        /// <returns>
        /// <c>true</c> if <paramref name="other" /> and this instance are the same type and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            return other is MouseStatic effect && Equals(effect);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Led * 397) ^ Color.GetHashCode();
            }
        }
    }
}
