// ---------------------------------------------------------------------------------------
// <copyright file="Color.cs" company="Corale">
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
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    [DebuggerDisplay("({R}, {G}, {B})", Name = nameof(Color))]
    [PublicAPI]
    [JsonConverter(typeof(ColorConverter))]
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint))]
    public partial struct Color : IEquatable<Color>, IEquatable<uint>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using an integer
        /// color value in the format <c>0xKKBBGGRR</c>.
        /// </summary>
        /// <param name="value">Value to create the color from.</param>
        [PublicAPI]
        public Color(uint value)
        {
            Value = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using three
        /// distinct R, G, and B byte values.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        [PublicAPI]
        public Color(byte red, byte green, byte blue)
            : this(red + ((uint)green << 8) + ((uint)blue << 16))
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="float" /> values for the R, G, and B channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0f</c> and <c>1.0f</c> (inclusive).
        /// </remarks>
        [PublicAPI]
        public Color(float red, float green, float blue)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255))
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="double" /> values for the R, G, and B channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0</c> and <c>1.0</c> (inclusive).
        /// </remarks>
        [PublicAPI]
        public Color(double red, double green, double blue)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255))
        {
        }

        /// <summary>
        /// Gets the blue component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte B => (byte)((Value >> 16) & 0xFF);

        /// <summary>
        /// Gets the green component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte G => (byte)((Value >> 8) & 0xFF);

        /// <summary>
        /// Gets the red component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte R => (byte)(Value & 0xFF);

        /// <summary>
        /// Gets the unsigned integer representing
        /// the color. On the form <c>0xKKBBGGRR</c>.
        /// </summary>
        [PublicAPI]
        public uint Value { get; }

        /// <summary>
        /// Gets a version of this color with the key bit set.
        /// </summary>
        public Color AsKeyColor => Value | 0x1000000;

        /// <summary>
        /// Gets a version of this color without the key bit set.
        /// </summary>
        public Color AsRegularColor => Value & 0xFFFFFF;

        /// <summary>
        /// Converts a <see cref="Color" /> struct to a <see cref="uint" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to convert.</param>
        /// <returns>A <see cref="uint" /> representing the value of the <paramref name="color" /> argument.</returns>
        /// <remarks>The returned <see cref="uint" /> has a format of <c>0xAABBGGRR</c>.</remarks>
        [Pure]
        public static implicit operator uint(Color color)
        {
            return color.Value;
        }

        /// <summary>
        /// Converts a <c>uint</c> <paramref name="value" /> in the format of <c>0xAABBGGRR</c>
        /// to a new instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert, on the form <c>0x00BBGGRR</c>.</param>
        /// <returns>An instance of <see cref="Color" /> representing the color value of <paramref name="value" />.</returns>
        [Pure]
        public static implicit operator Color(uint value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Checks <paramref name="left" /> and <paramref name="right" /> for equality.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Color" /> struct.</param>
        /// <param name="right">Right operand, an <see cref="object" />.</param>
        /// <returns><c>true</c> if the two instances are equal, <c>false</c> otherwise.</returns>
        [Pure]
        public static bool operator ==(Color left, object? right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks <paramref name="left" /> and <paramref name="right" /> for inequality.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Color" /> struct.</param>
        /// <param name="right">Right operand, an <see cref="object" />.</param>
        /// <returns><c>true</c> if the two instances are not equal, <c>false</c> otherwise.</returns>
        [Pure]
        public static bool operator !=(Color left, object? right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> from an RGB integer value
        /// in the format of <c>0xRRGGBB</c>.
        /// </summary>
        /// <param name="value">The RGB value to convert from.</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        [Pure]
        public static Color FromRgb(uint value)
        {
            return new Color(((value & 0xFF0000) >> 16) | (value & 0xFF00) | ((value & 0xFF) << 16));
        }

        /// <summary>
        /// Converts an unsigned integer value to a <see cref="Color" /> object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns><see cref="Color" /> representation of the value.</returns>
        /// <remarks>This is an alias for <see cref="FromRgb" />.</remarks>
        /// <seealso cref="FromRgb" />
        [Pure]
        public static Color ToColor(uint value) => value;

        /// <summary>
        /// Converts this instance of <see cref="Color" /> to an unsigned integer value.
        /// </summary>
        /// <returns>The unsigned integer value of this <see cref="Color" />.</returns>
        [Pure]
        public uint ToUInt32() => this;

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to an <see cref="object" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="object" /> to check equality against.</param>
        /// <returns><c>true</c> if the two are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object? other)
        {
            if (other is null)
            {
                return false;
            }

            switch (other)
            {
                case Color color:
                    return Equals(color);

                case uint value:
                    return Equals(value);
            }

            return false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to a <see cref="Color" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="Color" /> to check equality against.</param>
        /// <returns><c>true</c> of the two are equal, false otherwise.</returns>
        /// <remarks>
        /// Only compares the lower 32 bits of each color value, in order to
        /// be able to compare a keymode color to a non-keymode color.
        /// </remarks>
        [Pure]
        public bool Equals(Color other)
        {
            return Value.Equals(other.Value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to a <see cref="uint" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="uint" /> to check equality against.</param>
        /// <returns><c>true</c> if the two are equal, <c>false</c> otherwise.</returns>
        /// <remarks>
        /// Only compares the lower 32 bits of each value, in order to
        /// be able to compare a keymode color to a non-keymode color.
        /// </remarks>
        [Pure]
        public bool Equals(uint other)
        {
            return Value.Equals(other);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns a string representation of this color's red, green, and blue values.
        /// </summary>
        /// <returns>A <see cref="string" /> containing a string representation of this color.</returns>
        [Pure]
        public override string ToString() => $"{R}, {G}, {B} (0x{Value:X7})";

        /// <summary>
        /// Gets the unique hash code for this <see cref="Color" />.
        /// </summary>
        /// <returns>A unique has code.</returns>
        [Pure]
        public override int GetHashCode()
        {
            return (int)Value;
        }
    }
}
