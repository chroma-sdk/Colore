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
    public struct CustomGrid
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
        /// Gets or sets cells in the custom grid.
        /// </summary>
        /// <param name="row">Row to access, zero indexed.</param>
        /// <param name="column">Column to access, zero indexed.</param>
        [PublicAPI]
        public Color this[int row, int column]
        {
            get
            {
                if (row >= Constants.MaxRows)
                    throw new ArgumentOutOfRangeException("row", row, "Attempted to access a row that does not exist.");

                if (column >= Constants.MaxColumns)
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
                if (row >= Constants.MaxRows)
                    throw new ArgumentOutOfRangeException("row", row, "Attempted to access a row that does not exist.");

                if (column >= Constants.MaxColumns)
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
        }
    }
}
