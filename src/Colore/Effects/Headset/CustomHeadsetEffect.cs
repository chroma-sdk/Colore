// ---------------------------------------------------------------------------------------
// <copyright file="CustomHeadsetEffect.cs" company="Corale">
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

namespace Colore.Effects.Headset
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Custom effect for headsets.
    /// </summary>
    [JsonConverter(typeof(HeadsetCustomConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct CustomHeadsetEffect : IEquatable<CustomHeadsetEffect>
    {
        /// <summary>
        /// Colors for the LEDs.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = HeadsetConstants.MaxLeds)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeadsetEffect" /> struct with
        /// a default color to apply to every LED.
        /// </summary>
        /// <param name="color">The color to set every LED to initially.</param>
        public CustomHeadsetEffect(Color color)
        {
            _colors = new Color[HeadsetConstants.MaxLeds];

            for (var i = 0; i < _colors.Length; i++)
                _colors[i] = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeadsetEffect" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="colors" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the colors list supplied is of an incorrect size.</exception>
        public CustomHeadsetEffect(IList<Color> colors)
        {
            if (colors is null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            if (colors.Count != HeadsetConstants.MaxLeds)
            {
                throw new ArgumentException(
                    "Invalid length of color list, expected " + HeadsetConstants.MaxLeds + " but received " + colors.Count,
                    nameof(colors));
            }

            _colors = new Color[HeadsetConstants.MaxLeds];

            for (var i = 0; i < _colors.Length; i++)
                _colors[i] = colors[i];
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeadsetEffect" /> struct
        /// with the colors copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The struct to copy data from.</param>
        public CustomHeadsetEffect(CustomHeadsetEffect other)
            : this(other._colors)
        {
        }

        /// <summary>
        /// Gets the internal <see cref="Color" /> backing array.
        /// </summary>
        internal Color[] Colors => _colors;

        /// <summary>
        /// Gets or sets LEDs in the custom array.
        /// </summary>
        /// <param name="led">Index of the LED to access.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the specified LED index is outside the range of available LED indices.
        /// </exception>
        [PublicAPI]
        public Color this[int led]
        {
            get
            {
                if (led < 0 || led >= HeadsetConstants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(led),
                        led,
                        "Attempted to access an LED that was out of range.");
                }

                return _colors[led];
            }

            set
            {
                if (led < 0 || led >= HeadsetConstants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(led),
                        led,
                        "Attempted to access an LED that was out of range.");
                }

                _colors[led] = value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="CustomHeadsetEffect" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomHeadsetEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(CustomHeadsetEffect left, object? right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="CustomHeadsetEffect" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomHeadsetEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(CustomHeadsetEffect left, object? right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Create a new empty <see cref="CustomHeadsetEffect" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="CustomHeadsetEffect" /> filled with the color black.</returns>
        [PublicAPI]
        public static CustomHeadsetEffect Create()
        {
            return new CustomHeadsetEffect(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public CustomHeadsetEffect Clone()
        {
            return new CustomHeadsetEffect(this);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return _colors?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Sets all the LED indices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set the LEDs to.</param>
        [PublicAPI]
        public void Set(Color color)
        {
            for (var i = 0; i < HeadsetConstants.MaxLeds; i++)
                _colors[i] = color;
        }

        /// <summary>
        /// Clears the colors in this <see cref="CustomHeadsetEffect" /> struct (sets to <see cref="Color.Black" />).
        /// </summary>
        [PublicAPI]
        public void Clear()
        {
            Set(Color.Black);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance are the same type
        /// and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is CustomHeadsetEffect custom && Equals(custom);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(CustomHeadsetEffect other)
        {
            for (var i = 0; i < HeadsetConstants.MaxLeds; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
