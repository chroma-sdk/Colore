// ---------------------------------------------------------------------------------------
// <copyright file="Custom.cs" company="Corale">
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

namespace Corale.Colore.Razer.Keyboard.Effects
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Describes a custom grid effect for every key.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Custom : IEquatable<Custom>, IEquatable<Color[][]>
    {
        /// <summary>
        /// Color definitions for each key on the keyboard.
        /// </summary>
        /// <remarks>
        /// The array is 2-dimensional, with the first dimension
        /// specifying the row for the key, and the second the column.
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxRows)]
        private readonly Row[] _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public Custom(Color[][] colors)
        {
            var rows = colors.GetLength(0);

            if (rows != Constants.MaxRows)
            {
                throw new ArgumentException(
                    $"Colors array has incorrect number of rows, should be {Constants.MaxRows}, received {rows}.",
                    nameof(colors));
            }

            _rows = new Row[Constants.MaxRows];

            for (uint row = 0; row < Constants.MaxRows; row++)
            {
                var inRow = colors[row];
                _rows[row] = new Row(inRow);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Custom" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public Custom(Color color)
        {
            _rows = new Row[Constants.MaxRows];

            for (var row = 0; row < Constants.MaxRows; row++)
                _rows[row] = new Row(color);
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
                if (row < 0 || row >= Constants.MaxRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                return _rows[row][column];
            }

            set
            {
                if (row < 0 || row >= Constants.MaxRows)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(row),
                        row,
                        "Attempted to access a row that does not exist.");
                }

                _rows[row][column] = value;
            }
        }

        /// <summary>
        /// Gets or sets the color for a specific key in the custom grid.
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
                var row = (int)key >> 8;
                var column = (int)key & 0xFF;
                return this[row, column];
            }

            set
            {
                var row = (int)key >> 8;
                var column = (int)key & 0xFF;
                this[row, column] = value;
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
        public static Custom Create()
        {
            return new Custom(Color.Black);
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
            return _rows?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
        /// </summary>
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

            var arr = obj as Color[][];
            return arr != null && Equals(arr);
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
            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var col = 0; col < Constants.MaxColumns; col++)
                {
                    if (this[row, col] != other[row, col])
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Indicates whether the current object is equal to an instance of
        /// a 2-dimensional array of <see cref="Color" />.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the <paramref name="other" /> object has the same
        /// number of rows and columns, and contain matching colors; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">
        /// A 2-dimensional array of <see cref="Color" /> to compare with this object.
        /// </param>
        public bool Equals(Color[][] other)
        {
            if (other == null || other.GetLength(0) != Constants.MaxRows)
                return false;

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                if (other[row].Length != Constants.MaxColumns)
                    return false;

                for (var col = 0; col < Constants.MaxColumns; col++)
                {
                    if (this[row, col] != other[row][col])
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
            for (var row = 0; row < Constants.MaxRows; row++)
                _rows[row].Set(color);
        }

        /// <summary>
        /// Container struct holding color definitions for a single row in the custom grid.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct Row
        {
            /// <summary>
            /// Color definitions for the columns of this row.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.MaxColumns)]
            private readonly uint[] _columns;

            /// <summary>
            /// Initializes a new instance of the <see cref="Row" /> struct.
            /// </summary>
            /// <param name="colors">Colors for this row.</param>
            internal Row(IList<Color> colors)
            {
                if (colors.Count != Constants.MaxColumns)
                {
                    throw new ArgumentException(
                        "Incorrect color count, expected " + Constants.MaxColumns + " but received " + colors.Count,
                        nameof(colors));
                }

                _columns = new uint[Constants.MaxColumns];

                for (var col = 0; col < Constants.MaxColumns; col++)
                    _columns[col] = colors[col];
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Row" /> struct
            /// setting each column to a specific color.
            /// </summary>
            /// <param name="color">The <see cref="Color" /> to set each column to.</param>
            internal Row(Color color)
            {
                _columns = new uint[Constants.MaxColumns];

                for (var col = 0; col < Constants.MaxColumns; col++)
                    _columns[col] = color;
            }

            /// <summary>
            /// Gets or sets a column's <see cref="Color" />.
            /// </summary>
            /// <param name="column">The column index to access (zero-index).</param>
            /// <returns>The <see cref="Color" /> at the specified column index.</returns>
            internal Color this[int column]
            {
                get
                {
                    if (column < 0 || column >= Constants.MaxColumns)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(column),
                            column,
                            "Attempted to access a column that does not exist.");
                    }

                    return _columns[column];
                }

                set
                {
                    if (column < 0 || column >= Constants.MaxColumns)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(column),
                            column,
                            "Attempted to access a column that does not exist.");
                    }

                    _columns[column] = value;
                }
            }

            /// <summary>
            /// Converts an instance of the <see cref="Row" /> struct to an array of unsigned integers.
            /// </summary>
            /// <param name="row">The <see cref="Row" /> object to convert.</param>
            /// <returns>An array of unsigned integeres representing the colors of the row.</returns>
            public static implicit operator uint[](Row row)
            {
                return row._columns;
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
                return _columns?.GetHashCode() ?? 0;
            }

            /// <summary>
            /// Sets the entire row to a specific <see cref="Color" />.
            /// </summary>
            /// <param name="color">The <see cref="Color" /> to apply.</param>
            public void Set(Color color)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    _columns[column] = color;
            }
        }
    }
}
