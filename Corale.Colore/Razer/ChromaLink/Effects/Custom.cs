// ---------------------------------------------------------------------------------------
// <copyright file="Custom.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Razer.ChromaLink.Effects
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Custom effect.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Custom : IEquatable<Custom>, IEquatable<Color[]>
    {
        /// <summary>
        /// Color definitions for each element in Chroma Link.
        /// </summary>
        /// <remarks>
        /// The array is 1-dimensional
        /// according to: Constants.MaxLEDs
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxLEDs)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public Custom(Color[] colors)
        {
            if (colors.Length != Constants.MaxLEDs)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect number of elements, should be {Constants.MaxLEDs}, actual is {colors.Length}.",
                    nameof(colors));
            }

            _colors = new Color[Constants.MaxLEDs];

            for (var index = 0; index < Constants.MaxLEDs; index++)
            {
                this[index] = colors[index];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an invalid size.</exception>
        public Custom(IList<Color> colors)
        {
            if (colors.Count != Constants.MaxLEDs)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect size, should be {Constants.MaxLEDs}, actual is {colors.Count}.",
                    nameof(colors));
            }

            _colors = new Color[Constants.MaxLEDs];

            for (var index = 0; index < Constants.MaxLEDs; index++)
                this[index] = colors[index];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public Custom(Color color)
        {
            _colors = new Color[Constants.MaxLEDs];

            for (var index = 0; index < Constants.MaxLEDs; index++)
                this[index] = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct
        /// with color values copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The struct to copy data from.</param>
        public Custom(Custom other)
            : this(other._colors)
        {
        }

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
                if (index < 0 || index >= Constants.MaxLEDs)
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
                if (index < 0 || index >= Constants.MaxLEDs)
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
        /// Compares an instance of <see cref="Custom" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="Custom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(Custom left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="Custom" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="Custom" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(Custom left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="Custom" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="Custom" />
        /// filled with the color black.</returns>
        [PublicAPI]
        public static Custom Create()
        {
            return new Custom(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public Custom Clone()
        {
            return new Custom(this);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
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
            for (var index = 0; index < Constants.MaxLEDs; index++)
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
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (obj is Custom)
                return Equals((Custom)obj);

            var array = obj as Color[];

            return array != null && Equals(array);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">A <see cref="Custom" /> to compare with this object.</param>
        public bool Equals(Custom other)
        {
            for (var index = 0; index < Constants.MaxLEDs; index++)
            {
                if (this[index] != other[index])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to an instance of
        /// an array of <see cref="Color" />.
        /// </summary>
        /// <param name="other">An array of <see cref="Color" /> to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="other" /> object has the same
        /// number of elements, and contain matching colors; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Color[] other)
        {
            if (other == null || other.Length != Constants.MaxLEDs)
                return false;

            for (var index = 0; index < Constants.MaxLEDs; index++)
            {
                if (other[index] != this[index])
                    return false;
            }

            return true;
        }
    }
}
