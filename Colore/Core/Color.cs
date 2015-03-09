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

namespace Colore.Core
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint))]
    public struct Color : IEquatable<Color>, IEquatable<uint>
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
        /// Format: <c>0x00BBGGRR</c>.
        /// </remarks>
        private readonly uint _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using an integer
        /// color value in the format <c>0x00BBGGRR</c>.
        /// </summary>
        /// <param name="value">Value to create the color from.</param>
        /// <remarks>
        /// The left-padding zeroes can be omitted,
        /// leading to a format similar to that used on the web.
        /// </remarks>
        public Color(uint value)
        {
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using three
        /// distinct R, G, and B byte values.
        /// </summary>
        /// <param name="red">The red component.</param>
        /// <param name="green">The green component.</param>
        /// <param name="blue">The blue component.</param>
        public Color(byte red, byte green, byte blue)
        {
            _value = red + ((uint)green << 8) + ((uint)blue << 16);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="System.Single" /> (<c>float</c>) values for the
        /// R, G, and B channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0f</c> and <c>1.0f</c> (inclusive).
        /// </remarks>
        public Color(float red, float green, float blue)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="System.Double" /> values for the R, G, and B channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0</c> and <c>1.0</c> (inclusive).
        /// </remarks>
        public Color(double red, double green, double blue)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255))
        {
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
        /// Gets the unique hash code for this <see cref="Color" />.
        /// </summary>
        /// <returns>A unique has code.</returns>
        public override int GetHashCode()
        {
            return (int)_value;
        }
    }
}
