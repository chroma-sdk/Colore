// ---------------------------------------------------------------------------------------
// <copyright file="Color.cs" company="Corale">
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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint))]
    public struct Color : IEquatable<Color>, IEquatable<uint>, IEquatable<System.Drawing.Color>
    {
        /// <summary>
        /// Black color.
        /// </summary>
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// (Dark) blue color.
        /// </summary>
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>
        /// (Neon/bright) green color.
        /// </summary>
        public static readonly Color Green = new Color(0, 255, 0);

        /// <summary>
        /// Hot pink color.
        /// </summary>
        public static readonly Color HotPink = new Color(255, 105, 180);

        /// <summary>
        /// Orange color.
        /// </summary>
        public static readonly Color Orange = new Color(0xFFA500);

        /// <summary>
        /// Pink color.
        /// </summary>
        public static readonly Color Pink = new Color(255, 0, 255);

        /// <summary>
        /// Purple color.
        /// </summary>
        public static readonly Color Purple = new Color(0x800080);

        /// <summary>
        /// Red color.
        /// </summary>
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>
        /// White color.
        /// </summary>
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>
        /// Yellow color.
        /// </summary>
        public static readonly Color Yellow = new Color(255, 255, 0);

        /// <summary>
        /// Internal color value.
        /// </summary>
        /// <remarks>
        /// Format: <c>0xAABBGGRR</c>.
        /// </remarks>
        private readonly uint _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using an integer
        /// color value in the format <c>0xAABBGGRR</c>.
        /// </summary>
        /// <param name="value">Value to create the color from.</param>
        public Color(uint value)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using three
        /// distinct R, G, B, and A (optional) byte values. An alpha value of <c>0</c>
        /// is treated as fully opaque.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        /// <param name="alpha">The alpha component (<c>0</c> = fully opaque).</param>
        public Color(byte red, byte green, byte blue, byte alpha = 0)
            : this(red + ((uint)green << 8) + ((uint)blue << 16) + ((uint)alpha << 24))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="System.Single" /> (<c>float</c>) values for the
        /// R, G, B, and A (optional) channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="alpha">The alpha component (<c>0.0f</c> to <c>1.0f</c>, inclusive, <c>0.0f</c> = fully opaque).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0f</c> and <c>1.0f</c> (inclusive).
        /// </remarks>
        public Color(float red, float green, float blue, float alpha = 0.0f)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255), (byte)(alpha * 255))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="System.Double" /> values for the R, G, B, and A (optional) channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="alpha">The alpha component (<c>0.0</c> to <c>1.0</c>, inclusive, <c>0.0</c> = fully opaque).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0</c> and <c>1.0</c> (inclusive).
        /// </remarks>
        public Color(double red, double green, double blue, double alpha = 0.0)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255), (byte)(alpha * 255))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// a <see cref="System.Drawing.Color" /> struct as the source.
        /// </summary>
        /// <param name="source">An instance of the <see cref="System.Drawing.Color" /> struct.</param>
        public Color(System.Drawing.Color source)
            : this(source.R, source.G, source.B, source.A)
        {
        }

        /// <summary>
        /// Gets the alpha component of the color as a byte.
        /// </summary>
        public byte A
        {
            get
            {
                return (byte)((_value >> 24) & 0xFF);
            }
        }

        /// <summary>
        /// Gets the blue component of the color as a byte.
        /// </summary>
        public byte B
        {
            get
            {
                return (byte)((_value >> 16) & 0xFF);
            }
        }

        /// <summary>
        /// Gets the green component of the color as a byte.
        /// </summary>
        public byte G
        {
            get
            {
                return (byte)((_value >> 8) & 0xFF);
            }
        }

        /// <summary>
        /// Gets the red component of the color as a byte.
        /// </summary>
        public byte R
        {
            get
            {
                return (byte)(_value & 0xFF);
            }
        }

        /// <summary>
        /// Gets the unsigned integer representing
        /// the color. On the form <c>0x00BBGGRR</c>.
        /// </summary>
        public uint Value
        {
            get
            {
                return _value;
            }
        }

        /// <summary>
        /// Converts a <see cref="Color" /> struct to a <see cref="uint" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to convert.</param>
        /// <returns>A <see cref="uint" /> representing the value of the <paramref name="color" /> argument.</returns>
        /// <remarks>The returned <see cref="uint" /> has a format of <c>0x00BBGGRR</c>.</remarks>
        public static implicit operator uint(Color color)
        {
            return color._value;
        }

        /// <summary>
        /// Converts <paramref name="value" /> to an instance of the <see cref="Color" /> struct.
        /// </summary>
        /// <param name="value">The <see cref="uint" /> to convert, on the form <c>0x00BBGGRR</c>.</param>
        /// <returns>An instance of <see cref="Color" /> representing the color value of <paramref name="value" />.</returns>
        public static implicit operator Color(uint value)
        {
            return new Color(value);
        }

        /// <summary>
        /// Converts a <see cref="Color" /> struct to a <see cref="System.Drawing.Color" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to convert.</param>
        /// <returns>
        /// An instance of <see cref="System.Drawing.Color" /> representing the
        /// value of the <paramref name="color" /> argument.
        /// </returns>
        /// <remarks>
        /// This is an explicit cast since casting a <see cref="System.Drawing.Color" /> struct to <see cref="Color" />
        /// is explicit.
        /// </remarks>
        public static explicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Color" /> struct to a <see cref="Color" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="System.Drawing.Color" /> to convert.</param>
        /// <returns>
        /// An instance of <see cref="Color" /> representing the value of the <paramref name="color" /> argument.
        /// </returns>
        /// <remarks>
        /// This is an explicit cast since the alpha component of the <see cref="System.Drawing.Color" />
        /// struct is discarded.
        /// </remarks>
        public static explicit operator Color(System.Drawing.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Checks <paramref name="left" /> and <paramref name="right" /> for equality.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Color" /> struct.</param>
        /// <param name="right">Right operand, an <see cref="object" />.</param>
        /// <returns><c>true</c> if the two instances are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Color left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks <paramref name="left" /> and <paramref name="right" /> for inequality.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Color" /> struct.</param>
        /// <param name="right">Right operand, an <see cref="object" />.</param>
        /// <returns><c>true</c> if the two instances are not equal, <c>false</c> otherwise.</returns>
        public static bool operator !=(Color left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to an <see cref="object" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="object" /> to check equality against.</param>
        /// <returns><c>true</c> if the two are equal, <c>false</c> otherwise.</returns>
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (other is Color)
                return Equals((Color)other);

            if (other is uint)
                return Equals((uint)other);

            if (other is System.Drawing.Color)
                return Equals((System.Drawing.Color)other);

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to a <see cref="Color" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="Color" /> to check equality against.</param>
        /// <returns><c>true</c> of the two are equal, false otherwise.</returns>
        public bool Equals(Color other)
        {
            return _value.Equals(other);
        }

        /// <summary>
        /// Returns a value indicating whether this instance of <see cref="Color" />
        /// is equal to a <see cref="uint" /> <paramref name="other" />.
        /// </summary>
        /// <param name="other">The <see cref="uint" /> to check equality against.</param>
        /// <returns><c>true</c> if the two are equal, <c>false</c> otherwise.</returns>
        public bool Equals(uint other)
        {
            return _value.Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to an instance of a
        /// <see cref="System.Drawing.Color" /> struct.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">An instance of <see cref="System.Drawing.Color" /> to compare with this object.</param>
        public bool Equals(System.Drawing.Color other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Gets the unique hash code for this <see cref="Color" />.
        /// </summary>
        /// <returns>A unique has code.</returns>
        public override int GetHashCode()
        {
            return (int)_value;
        }
    }
}
