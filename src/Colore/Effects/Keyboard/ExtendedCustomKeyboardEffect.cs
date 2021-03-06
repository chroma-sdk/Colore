﻿// ---------------------------------------------------------------------------------------
// <copyright file="ExtendedCustomKeyboardEffect.cs" company="Corale">
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

namespace Colore.Effects.Keyboard
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Helpers;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Describes a custom grid effect for extended keyboards.
    /// </summary>
    [JsonConverter(typeof(KeyboardExtendedCustomConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct ExtendedCustomKeyboardEffect : IEquatable<ExtendedCustomKeyboardEffect>
    {
        /// <summary>
        /// Color definitions for each key on the keyboard.
        /// </summary>
        /// <remarks>
        /// The array is 1-dimensional, but will be passed to code expecting
        /// a 2-dimensional array. Access to this array is done using indices
        /// according to: <c>column + row * KeyboardConstants.MaxExtendedColumns</c>.
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = KeyboardConstants.MaxExtendedKeys)]
        private readonly Color[] _colors;

        /// <summary>
        /// Color definitions for the key translation mode.
        /// </summary>
        /// <remarks>Colors set in here will, if flagged with <c>0x01000000</c>,
        /// automatically be translated to the proper keyboard location
        /// depending on the user's keyboard configuration.</remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = KeyboardConstants.MaxKeys)]
        private readonly Color[] _keys;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedCustomKeyboardEffect" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public ExtendedCustomKeyboardEffect(Color color)
        {
            _colors = new Color[KeyboardConstants.MaxExtendedKeys];
            _keys = new Color[KeyboardConstants.MaxKeys];

            for (var index = 0; index < KeyboardConstants.MaxExtendedKeys; index++)
            {
                this[index] = color;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedCustomKeyboardEffect" /> struct
        /// with the colors copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The <see cref="ExtendedCustomKeyboardEffect" /> struct to copy data from.</param>
        public ExtendedCustomKeyboardEffect(ExtendedCustomKeyboardEffect other)
        {
            _colors = new Color[KeyboardConstants.MaxExtendedKeys];
            _keys = new Color[KeyboardConstants.MaxKeys];

            for (var index = 0; index < KeyboardConstants.MaxExtendedKeys; index++)
            {
                _colors[index] = other._colors[index];
            }

            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                _keys[index] = other._keys[index];
            }
        }

        /// <summary>
        /// Gets or sets cells in the custom grid.
        /// </summary>
        /// <param name="row">Row to access, zero indexed.</param>
        /// <param name="column">Column to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="row" /> and/or <paramref name="column" /> are out of range.
        /// </exception>
        [PublicAPI]
        public Color this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= KeyboardConstants.MaxExtendedRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                if (column < 0 || column >= KeyboardConstants.MaxExtendedColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "Attempted to access a column that does not exist.");
                }

                var index = column + row * KeyboardConstants.MaxExtendedColumns;
                return _colors[index];
            }

            set
            {
                if (row < 0 || row >= KeyboardConstants.MaxExtendedRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                if (column < 0 || column >= KeyboardConstants.MaxExtendedColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(column),
                        column,
                        "Attempted to access a column that does not exist.");
                }

                var index = column + row * KeyboardConstants.MaxExtendedColumns;
                _colors[index] = value;

                if (index < KeyboardConstants.MaxKeys)
                {
                    _keys[index] = _keys[index] & 0xFFFFFF;
                }
            }
        }

        /// <summary>
        /// Gets or sets a position in the custom grid.
        /// </summary>
        /// <param name="index">The index to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="index" /> is out of range.
        /// </exception>
        [PublicAPI]
        public Color this[int index]
        {
            get
            {
                if (index < 0 || index >= KeyboardConstants.MaxExtendedKeys)
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
                if (index < 0 || index >= KeyboardConstants.MaxExtendedKeys)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        index,
                        "Attempted to access an index that does not exist.");
                }

                _colors[index] = value;

                if (index < KeyboardConstants.MaxKeys)
                {
                    _keys[index] = _keys[index] & 0xFFFFFF;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color for a specific key in the custom grid.
        /// The SDK will handle translation of location data to access the
        /// correct key depending on user configuration.
        /// </summary>
        /// <param name="key">The <see cref="Key" /> to access.</param>
        /// <returns>The <see cref="Color" /> for the specified key.</returns>
        /// <remarks>
        /// Please note that the position of a key accessed in this way is not
        /// guaranteed to be correct, as different layouts on different keyboards
        /// can place these keys in other locations.
        /// </remarks>
        [PublicAPI]
        public Color this[Key key]
        {
            get
            {
                var index = (int)key;
                index = (index >> 8) * KeyboardConstants.MaxColumns + (index & 0xFF);
                return _keys[index] & 0xFFFFFF;
            }

            set
            {
                var index = (int)key;
                index = (index >> 8) * KeyboardConstants.MaxColumns + (index & 0xFF);
                _keys[index] = KeyboardConstants.KeyFlag | value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="ExtendedCustomKeyboardEffect" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="ExtendedCustomKeyboardEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(ExtendedCustomKeyboardEffect left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="ExtendedCustomKeyboardEffect" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="ExtendedCustomKeyboardEffect" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(ExtendedCustomKeyboardEffect left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="ExtendedCustomKeyboardEffect" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="ExtendedCustomKeyboardEffect" />
        /// filled with the color black.</returns>
        public static ExtendedCustomKeyboardEffect Create()
        {
            return new ExtendedCustomKeyboardEffect(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public ExtendedCustomKeyboardEffect Clone()
        {
            return new ExtendedCustomKeyboardEffect(this);
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
        /// </summary>
        public void Clear()
        {
            Set(Color.Black);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((_colors?.GetHashCode() ?? 0) * 397) ^ (_keys?.GetHashCode() ?? 0);
            }
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

            return obj is ExtendedCustomKeyboardEffect custom && Equals(custom);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">A <see cref="ExtendedCustomKeyboardEffect" /> to compare with this object.</param>
        public bool Equals(ExtendedCustomKeyboardEffect other)
        {
            for (var index = 0; index < KeyboardConstants.MaxExtendedKeys; index++)
            {
                if (_colors[index] != other._colors[index])
                {
                    return false;
                }
            }

            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                if (_keys[index] != other._keys[index])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Sets the entire grid to a specific <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        [PublicAPI]
        public void Set(Color color)
        {
            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                _keys[index] = color | KeyboardConstants.KeyFlag;
            }

            for (var index = 0; index < KeyboardConstants.MaxExtendedKeys; index++)
            {
                this[index] = color;
            }
        }

        /// <summary>
        /// Retrieves the internal backing arrays as multi-dimensional <see cref="Color" /> arrays.
        /// </summary>
        /// <returns>A <see cref="ValueTuple{T1,T2}" /> containing the two arrays.</returns>
        internal (Color[,] Colors, Color[,] Keys) ToMultiArrays()
        {
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
            var colors = new Color[KeyboardConstants.MaxExtendedRows, KeyboardConstants.MaxExtendedColumns];
            var keys = new Color[KeyboardConstants.MaxRows, KeyboardConstants.MaxColumns];
#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional

            _colors.CopyToMultidimensional(colors);
            _keys.CopyToMultidimensional(keys);

            return (colors, keys);
        }
    }
}
