// ---------------------------------------------------------------------------------------
// <copyright file="CustomKeypadEffect.cs" company="Corale">
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

namespace Colore.Effects.Keypad
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text.Json.Serialization;

    using Colore.Data;
    using Colore.Helpers;
    using Colore.Serialization;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Custom effect.
    /// </summary>
    [JsonConverter(typeof(KeypadCustomConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct CustomKeypadEffect : IEquatable<CustomKeypadEffect>
    {
        /// <summary>
        /// Color definitions for each key on the keypad.
        /// </summary>
        /// <remarks>
        /// The array is 1-dimensional, but will be passed to code expecting
        /// a 2-dimensional array. Access to this array is done using indices
        /// according to: <c>column + row * KeypadConstants.MaxColumns</c>.
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = KeypadConstants.MaxKeys)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeypadEffect" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="colors" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public CustomKeypadEffect(Color[][] colors)
        {
            if (colors is null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            var rows = colors.GetLength(0);

            if (rows != KeypadConstants.MaxRows)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect number of rows, should be {KeypadConstants.MaxRows}, received {rows}.",
                    nameof(colors));
            }

            _colors = new Color[KeypadConstants.MaxKeys];

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                if (colors[row].Length != KeypadConstants.MaxColumns)
                {
                    throw new ArgumentException(
                        $"Colors array has incorrect number of columns, should be {KeypadConstants.MaxColumns}, received {colors[row].Length} for row {row}.",
                        nameof(colors));
                }

                for (var column = 0; column < KeypadConstants.MaxColumns; column++)
                    this[row, column] = colors[row][column];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeypadEffect" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="colors" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an invalid size.</exception>
        public CustomKeypadEffect(IList<Color> colors)
        {
            if (colors is null)
            {
                throw new ArgumentNullException(nameof(colors));
            }

            if (colors.Count != KeypadConstants.MaxKeys)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect size, should be {KeypadConstants.MaxKeys}, actual is {colors.Count}.",
                    nameof(colors));
            }

            _colors = new Color[KeypadConstants.MaxKeys];

            for (var index = 0; index < KeypadConstants.MaxKeys; index++)
                this[index] = colors[index];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeypadEffect" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public CustomKeypadEffect(Color color)
        {
            _colors = new Color[KeypadConstants.MaxKeys];

            for (var index = 0; index < KeypadConstants.MaxKeys; index++)
                this[index] = color;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomKeypadEffect" /> struct
        /// with color values copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The struct to copy data from.</param>
        public CustomKeypadEffect(CustomKeypadEffect other)
            : this(other._colors)
        {
        }

        /// <summary>
        /// Gets or sets cells in the custom grid.
        /// </summary>
        /// <param name="row">Row to access, zero indexed.</param>
        /// <param name="column">Column to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        public Color this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= KeypadConstants.MaxRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                if (column < 0 || column >= KeypadConstants.MaxColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "Attempted to access a column that does not exist.");
                }

#pragma warning disable SA1407 // Arithmetic expressions must declare precedence
                return _colors[column + row * KeypadConstants.MaxColumns];
#pragma warning restore SA1407 // Arithmetic expressions must declare precedence
            }

            set
            {
                if (row < 0 || row >= KeypadConstants.MaxRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                if (column < 0 || column >= KeypadConstants.MaxColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "Attempted to access a column that does not exist.");
                }

#pragma warning disable SA1407 // Arithmetic expressions must declare precedence
                _colors[column + row * KeypadConstants.MaxColumns] = value;
#pragma warning restore SA1407 // Arithmetic expressions must declare precedence
            }
        }

        /// <summary>
        /// Gets or sets a position in the custom grid.
        /// </summary>
        /// <param name="index">The index to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        public Color this[int index]
        {
            get
            {
                if (index < 0 || index >= KeypadConstants.MaxKeys)
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
                if (index < 0 || index >= KeypadConstants.MaxKeys)
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
        /// Compares an instance of <see cref="CustomKeypadEffect" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomKeypadEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(CustomKeypadEffect left, object? right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="CustomKeypadEffect" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomKeypadEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(CustomKeypadEffect left, object? right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="CustomKeypadEffect" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="CustomKeypadEffect" />
        /// filled with the color black.</returns>
        [PublicAPI]
        public static CustomKeypadEffect Create()
        {
            return new CustomKeypadEffect(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public CustomKeypadEffect Clone()
        {
            return new CustomKeypadEffect(this);
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
            for (var index = 0; index < KeypadConstants.MaxKeys; index++)
                this[index] = color;
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
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
        public override bool Equals(object? obj) => obj is CustomKeypadEffect custom && Equals(custom);

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">A <see cref="CustomKeypadEffect" /> to compare with this object.</param>
        public bool Equals(CustomKeypadEffect other)
        {
            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                for (var col = 0; col < KeypadConstants.MaxColumns; col++)
                {
                    if (this[row, col] != other[row, col])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Retrieves the internal backing array as a multi-dimensional <see cref="Color" /> array.
        /// </summary>
        /// <returns>
        /// An instance of <c><see cref="Color" />[<see cref="KeypadConstants.MaxRows" />, <see cref="KeypadConstants.MaxColumns" />]</c>.
        /// </returns>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

        internal Color[,] ToMultiArray()
        {
            var destination = new Color[KeypadConstants.MaxRows, KeypadConstants.MaxColumns];
            _colors.CopyToMultidimensional(destination);
            return destination;
        }

#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    }
}
