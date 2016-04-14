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
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;

    using SystemColor = System.Drawing.Color;
    using WpfColor = System.Windows.Media.Color;

    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = sizeof(uint))]
    public partial struct Color : IEquatable<Color>, IEquatable<uint>, IEquatable<SystemColor>, IEquatable<WpfColor>
    {
        /// <summary>
        /// Internal color value.
        /// </summary>
        /// <remarks>
        /// Format: <c>0xAABBGGRR</c>.
        /// </remarks>
        private readonly uint _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using an integer
        /// color value in the format <c>0xAABBGGRR</c>. Where the alpha component ranges
        /// from <c>0x00</c> (fully transparent) to <c>0xFF</c> (fully opaque).
        /// </summary>
        /// <param name="value">Value to create the color from.</param>
        [PublicAPI]
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
        [PublicAPI]
        public Color(byte red, byte green, byte blue, byte alpha = 255)
            : this(red + ((uint)green << 8) + ((uint)blue << 16) + ((uint)alpha << 24))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="float" /> (<c>float</c>) values for the
        /// R, G, B, and A (optional) channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0f</c> to <c>1.0f</c>, inclusive).</param>
        /// <param name="alpha">The alpha component (<c>0.0f</c> to <c>1.0f</c>, inclusive, <c>0.0f</c> = fully opaque).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0f</c> and <c>1.0f</c> (inclusive).
        /// </remarks>
        [PublicAPI]
        public Color(float red, float green, float blue, float alpha = 1.0f)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255), (byte)(alpha * 255))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color" /> struct using
        /// three <see cref="double" /> values for the R, G, B, and A (optional) channels.
        /// </summary>
        /// <param name="red">The red component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="green">The green component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="blue">The blue component (<c>0.0</c> to <c>1.0</c>, inclusive).</param>
        /// <param name="alpha">The alpha component (<c>0.0</c> to <c>1.0</c>, inclusive, <c>0.0</c> = fully opaque).</param>
        /// <remarks>
        /// Each parameter value must be between <c>0.0</c> and <c>1.0</c> (inclusive).
        /// </remarks>
        [PublicAPI]
        public Color(double red, double green, double blue, double alpha = 1.0)
            : this((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255), (byte)(alpha * 255))
        {
        }

        /// <summary>
        /// Gets the alpha component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte A => (byte)((_value >> 24) & 0xFF);

        /// <summary>
        /// Gets the blue component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte B => (byte)((_value >> 16) & 0xFF);

        /// <summary>
        /// Gets the green component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte G => (byte)((_value >> 8) & 0xFF);

        /// <summary>
        /// Gets the red component of the color as a byte.
        /// </summary>
        [PublicAPI]
        public byte R => (byte)(_value & 0xFF);

        /// <summary>
        /// Gets the unsigned integer representing
        /// the color. On the form <c>0xAABBGGRR</c>.
        /// </summary>
        [PublicAPI]
        public uint Value => _value;

        /// <summary>
        /// Converts a <see cref="Color" /> struct to a <see cref="uint" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to convert.</param>
        /// <returns>A <see cref="uint" /> representing the value of the <paramref name="color" /> argument.</returns>
        /// <remarks>The returned <see cref="uint" /> has a format of <c>0xAABBGGRR</c>.</remarks>
        public static implicit operator uint(Color color)
        {
            return color._value;
        }

        /// <summary>
        /// Converts a <c>uint</c> <paramref name="value" /> in the format of <c>0xAABBGGRR</c>
        /// to a new instance of the <see cref="Color" /> struct.
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
        public static implicit operator SystemColor(Color color)
        {
            return SystemColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts a <see cref="System.Drawing.Color" /> struct to a <see cref="Color" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="System.Drawing.Color" /> to convert.</param>
        /// <returns>
        /// An instance of <see cref="Color" /> representing the value of the <paramref name="color" /> argument.
        /// </returns>
        public static implicit operator Color(SystemColor color)
        {
            return FromSystemColor(color);
        }

        /// <summary>
        /// Converts a <see cref="Color" /> struct to a <see cref="System.Windows.Media.Color" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to convert.</param>
        /// <returns>
        /// An instance of <see cref="System.Windows.Media.Color" /> representing the
        /// value of the <paramref name="color" /> argument.
        /// </returns>
        public static implicit operator WpfColor(Color color)
        {
            return WpfColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Converts a <see cref="System.Windows.Media.Color" /> struct to a <see cref="Color" /> struct.
        /// </summary>
        /// <param name="color">The <see cref="System.Windows.Media.Color" /> to convert.</param>
        /// <returns>
        /// An instance of <see cref="Color" /> representing the value of the <paramref name="color" /> argument.
        /// </returns>
        public static implicit operator Color(WpfColor color)
        {
            return FromWpfColor(color);
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
        /// Creates a new <see cref="Color" /> from the source
        /// <see cref="System.Drawing.Color" />.
        /// </summary>
        /// <param name="source">The source object to construct color from.</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        public static Color FromSystemColor(SystemColor source)
        {
            return new Color(source.R, source.G, source.B, source.A);
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> from the source
        /// <see cref="System.Windows.Media.Color" />.
        /// </summary>
        /// <param name="source">The source object to construct color from.</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        public static Color FromWpfColor(WpfColor source)
        {
            return new Color(source.R, source.G, source.B, source.A);
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> from the HSV values
        /// </summary>
        /// <param name="hue">The <c>HUE</c> component (<c>0.0</c> to <c>360.0</c>, inclusive).</param>
        /// <param name="saturation">The <c>SATURATION</c> component in percentage (<c>0.0</c> to <c>100.0</c>, inclusive).</param>
        /// <param name="value">The <c>VALUE</c> component in percentage (<c>0.0</c> to <c>100.0</c>, inclusive).</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        public static Color FromHsv(double hue, double saturation, double value)
        {
            double h = hue;
            saturation = saturation / 100;
            value = value / 100;
            while (h < 0)
            {
                h += 360;
            }

            while (h >= 360)
            {
                h -= 360;
            }

            double r, g, b;
            if (value <= 0)
            {
                r = g = b = 0;
            }
            else if (saturation <= 0)
            {
                r = g = b = value;
            }
            else
            {
                double hf = h / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = value * (1 - saturation);
                double qv = value * (1 - (saturation * f));
                double tv = value * (1 - (saturation * (1 - f)));
                switch (i)
                {
                    // Red is the dominant color
                    case 0:
                        r = value;
                        g = tv;
                        b = pv;
                        break;

                    // Green is the dominant color
                    case 1:
                        r = qv;
                        g = value;
                        b = pv;
                        break;
                    case 2:
                        r = pv;
                        g = value;
                        b = tv;
                        break;

                    // Blue is the dominant color
                    case 3:
                        r = pv;
                        g = qv;
                        b = value;
                        break;
                    case 4:
                        r = tv;
                        g = pv;
                        b = value;
                        break;

                    // Red is the dominant color
                    case 5:
                        r = value;
                        g = pv;
                        b = qv;
                        break;

                    // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
                    case 6:
                        r = value;
                        g = tv;
                        b = pv;
                        break;
                    case -1:
                        r = value;
                        g = pv;
                        b = qv;
                        break;

                    // The color is not defined, we should throw an error.
                    default:
                        // LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        r = g = b = value; // Just pretend its black/white
                        break;
                }
            }

            return new Color(r, g, b);
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> from an ARGB integer value
        /// in the format of <c>0xAARRGGBB</c> where the alpha component
        /// ranges from <c>0x00</c> (fully transparent) to <c>0xFF</c> (fully opaque).
        /// </summary>
        /// <param name="value">The ARGB value to convert from.</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        public static Color FromArgb(uint value)
        {
            var abgr = (value & 0xFF00FF00) | ((value & 0xFF0000) >> 16) | ((value & 0xFF) << 16);
            return new Color(abgr);
        }

        /// <summary>
        /// Creates a new <see cref="Color" /> from an RGB integer value
        /// in the format of <c>0xRRGGBB</c>.
        /// </summary>
        /// <param name="value">The RGB value to convert from.</param>
        /// <returns>A new instance of the <see cref="Color" /> struct.</returns>
        [PublicAPI]
        public static Color FromRgb(uint value)
        {
            var abgr = 0xFF000000 | ((value & 0xFF0000) >> 16) | (value & 0xFF00) | ((value & 0xFF) << 16);
            return new Color(abgr);
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

            if (other is SystemColor)
                return Equals((SystemColor)other);

            if (other is WpfColor)
                return Equals((WpfColor)other);

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
            return _value.Equals(other._value);
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
        public bool Equals(SystemColor other)
        {
            return R == other.R && G == other.G && B == other.B && A == other.A;
        }

        /// <summary>
        /// Indicates whether the current object is equal to an instance of a
        /// <see cref="System.Windows.Media.Color" /> struct.
        /// </summary>
        /// <param name="other">An instance of <see cref="System.Windows.Media.Color" /> to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(WpfColor other)
        {
            return R == other.R && G == other.G && B == other.B & A == other.A;
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
