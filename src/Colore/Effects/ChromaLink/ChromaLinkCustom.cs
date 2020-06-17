// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLinkCustom.cs" company="Corale">
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

namespace Colore.Effects.ChromaLink
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Custom effect.
    /// </summary>
    [JsonConverter(typeof(ChromaLinkCustomConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct ChromaLinkCustom : IEquatable<ChromaLinkCustom>
    {
        /// <summary>
        /// Color definitions for each element in Chroma Link.
        /// </summary>
        /// <remarks>
        /// The array is 1-dimensional
        /// according to: <see cref="ChromaLinkConstants.MaxLeds" />.
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ChromaLinkConstants.MaxLeds)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaLinkCustom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="colors" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public ChromaLinkCustom(Color[] colors)
        {
            if (colors is null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            if (colors.Length != ChromaLinkConstants.MaxLeds)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect number of elements, should be {ChromaLinkConstants.MaxLeds}, actual is {colors.Length}.",
                    nameof(colors));
            }

            _colors = new Color[ChromaLinkConstants.MaxLeds];

            for (var index = 0; index < ChromaLinkConstants.MaxLeds; index++)
            {
                this[index] = colors[index];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaLinkCustom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="colors" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an invalid size.</exception>
        public ChromaLinkCustom(IList<Color> colors)
        {
            if (colors is null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            if (colors.Count != ChromaLinkConstants.MaxLeds)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect size, should be {ChromaLinkConstants.MaxLeds}, actual is {colors.Count}.",
                    nameof(colors));
            }

            _colors = new Color[ChromaLinkConstants.MaxLeds];

            for (var index = 0; index < ChromaLinkConstants.MaxLeds; index++)
                this[index] = colors[index];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaLinkCustom" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public ChromaLinkCustom(Color color)
        {
            _colors = new Color[ChromaLinkConstants.MaxLeds];

            for (var index = 0; index < ChromaLinkConstants.MaxLeds; index++)
                this[index] = color;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaLinkCustom" /> struct
        /// with color values copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The struct to copy data from.</param>
        public ChromaLinkCustom(ChromaLinkCustom other)
            : this(other._colors)
        {
        }

        /// <summary>
        /// Gets the backing array for the <see cref="Color" /> data.
        /// </summary>
        internal Color[] Colors => _colors;

        /// <summary>
        /// Gets or sets a position in the custom array.
        /// </summary>
        /// <param name="index">The index to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        public Color this[int index]
        {
            get
            {
                if (index < 0 || index >= ChromaLinkConstants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        index,
                        "Attempted to access an index that does not exist.");
                }

                return _colors[index];
            }

            set
            {
                if (index < 0 || index >= ChromaLinkConstants.MaxLeds)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        index,
                        "Attempted to access an index that does not exist.");
                }

                _colors[index] = value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="ChromaLinkCustom" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="ChromaLinkCustom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(ChromaLinkCustom left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="ChromaLinkCustom" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="ChromaLinkCustom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(ChromaLinkCustom left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="ChromaLinkCustom" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="ChromaLinkCustom" />
        /// filled with the color black.</returns>
        [PublicAPI]
        public static ChromaLinkCustom Create()
        {
            return new ChromaLinkCustom(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public ChromaLinkCustom Clone()
        {
            return new ChromaLinkCustom(this);
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
        /// Sets all LED indices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public void Set(Color color)
        {
            for (var index = 0; index < ChromaLinkConstants.MaxLeds; index++)
                this[index] = color;
        }

        /// <summary>
        /// Clears the colors from the array, setting them to <see cref="Color.Black" />.
        /// </summary>
        [PublicAPI]
        public void Clear()
        {
            Set(Color.Black);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance are of compatible types
        /// and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            return obj is ChromaLinkCustom custom && Equals(custom);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">A <see cref="ChromaLinkCustom" /> to compare with this object.</param>
        public bool Equals(ChromaLinkCustom other)
        {
            for (var index = 0; index < ChromaLinkConstants.MaxLeds; index++)
            {
                if (this[index] != other[index])
                    return false;
            }

            return true;
        }
    }
}
