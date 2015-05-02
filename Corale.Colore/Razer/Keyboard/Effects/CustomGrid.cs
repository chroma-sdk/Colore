// ---------------------------------------------------------------------------------------
// <copyright file="CustomGrid.cs" company="Corale">
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
    public struct CustomGrid : IEquatable<CustomGrid>, IEquatable<Color[][]>
    {
        /// <summary>
        /// Color definitions for each key on the keyboard.
        /// </summary>
        /// <remarks>
        /// The array is 2-dimensional, with the first dimension
        /// specifying the row for the key, and the second the column.
        /// </remarks>
        //[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NestedColorArrayMarshaler))]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Constants.MaxRows)]
        private readonly Row[] Rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrid" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public CustomGrid(Color[][] colors)
        {
            var rows = (Size)colors.GetLength(0);

            if (rows != Constants.MaxRows)
            {
                throw new ArgumentException(
                    "Colors array has incorrect number of rows, should be " + Constants.MaxRows + ", received " + rows,
                    "colors");
            }

            Rows = new Row[Constants.MaxRows];

            for (Size row = 0; row < (int)Constants.MaxRows; row++)
            {
                var inRow = colors[row];
                Rows[row] = new Row(inRow);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrid" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public CustomGrid(Color color)
        {
            Rows = new Row[Constants.MaxRows];

            for (var row = 0; row < Constants.MaxRows; row++)
                Rows[row] = new Row(color);
        }

        /// <summary>
        /// Gets or sets cells in the custom grid.
        /// </summary>
        /// <param name="row">Row to access, zero indexed.</param>
        /// <param name="column">Column to access, zero indexed.</param>
        [PublicAPI]
        public Color this[int row, int column]
        {
            get
            {
                if (row < 0 || row >= Constants.MaxRows)
                    throw new ArgumentOutOfRangeException("row", row, "Attempted to access a row that does not exist.");

                if (column < 0 || column >= Constants.MaxColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        "column",
                        column,
                        "Attempted to access a column that does not exist.");
                }

                return Rows[row][column];
            }

            set
            {
                if (row < 0 || row >= Constants.MaxRows)
                    throw new ArgumentOutOfRangeException("row", row, "Attempted to access a row that does not exist.");

                if (column < 0 || column >= Constants.MaxColumns)
                {
                    throw new ArgumentOutOfRangeException(
                        "column",
                        column,
                        "Attempted to access a column that does not exist.");
                }

                Rows[row][column] = value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="CustomGrid" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomGrid" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(CustomGrid left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="CustomGrid" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="CustomGrid" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(CustomGrid left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="CustomGrid" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="CustomGrid" />
        /// filled with the color black.</returns>
        public static CustomGrid Create()
        {
            return new CustomGrid(Color.Black);
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
            return Rows == null ? 0 : Rows.GetHashCode();
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
        /// </summary>
        public void Clear()
        {
            for (var row = 0; row < (int)Constants.MaxRows; row++)
            {
                var rowArr = Rows[row];
                for (var col = 0; col < (int)Constants.MaxColumns; col++)
                    rowArr[col] = Color.Black;
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
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            if (obj is CustomGrid)
                return Equals((CustomGrid)obj);

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
        /// <param name="other">A <see cref="CustomGrid" /> to compare with this object.</param>
        public bool Equals(CustomGrid other)
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
        /// Container struct holding color definitions for a single row in the custom grid.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct Row
        {
            /// <summary>
            /// Color definitions for the columns of this row.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)Constants.MaxColumns)]
            internal readonly uint[] Columns;

            /// <summary>
            /// Initializes a new instance of the <see cref="Row" /> struct.
            /// </summary>
            /// <param name="colors">Colors for this row.</param>
            internal Row(IReadOnlyList<Color> colors)
            {
                if (colors.Count != (int)Constants.MaxColumns)
                {
                    throw new ArgumentException(
                        "Incorrect color count, expected " + Constants.MaxColumns + " but received " + colors.Count,
                        "colors");
                }

                Columns = new uint[Constants.MaxColumns];

                for (var col = 0; col < (int)Constants.MaxColumns; col++)
                    Columns[col] = colors[col];
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="Row" /> struct
            /// setting each column to a specific color.
            /// </summary>
            /// <param name="color">The <see cref="Color" /> to set each column to.</param>
            internal Row(Color color)
            {
                Columns = new uint[Constants.MaxColumns];

                for (var col = 0; col < (int)Constants.MaxColumns; col++)
                    Columns[col] = color;
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
                            "column",
                            column,
                            "Attempted to access a column that does not exist.");
                    }

                    return Columns[column];
                }

                set
                {
                    if (column < 0 || column >= Constants.MaxColumns)
                    {
                        throw new ArgumentOutOfRangeException(
                            "column",
                            column,
                            "Attempted to access a column that does not exist.");
                    }

                    Columns[column] = value;
                }
            }

            /// <summary>
            /// Converts an instance of the <see cref="Row" /> struct to an array of unsigned integers.
            /// </summary>
            /// <param name="row">The <see cref="Row" /> object to convert.</param>
            /// <returns>An array of unsigned integeres representing the colors of the row.</returns>
            public static implicit operator uint[](Row row)
            {
                return row.Columns;
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
                return Columns == null ? 0 : Columns.GetHashCode();
            }
        }
    }
}
