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
        [PublicAPI]
        public readonly Color[][] Colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomGrid" /> struct.
        /// </summary>
        /// <param name="colors">The colors to use.</param>
        /// <exception cref="ArgumentException">Thrown if the colors array supplied is of an incorrect size.</exception>
        public CustomGrid(Color[][] colors)
        {
            var rows = colors.GetLength(0);

            if (rows > Constants.MaxRows)
            {
                throw new ArgumentException(
                    "Colors array has too many rows, max count is " + Constants.MaxRows + ", received " + rows,
                    "colors");
            }

            Colors = new Color[rows][];

            for (var row = 0; row < rows; row++)
            {
                var inRow = colors[row];

                if (inRow.Length > Constants.MaxRows)
                {
                    throw new ArgumentException(
                        "Row " + row + " of the colors array has too many columns, max count is " + Constants.MaxRows
                        + ", received " + inRow.Length,
                        "colors");
                }

                Colors[row] = new Color[inRow.Length];
                inRow.CopyTo(Colors[row], 0);
            }
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
        /// </summary>
        public void Clear()
        {
            var rows = Colors.GetLength(0);
            for (var row = 0; row < rows; row++)
            {
                var rowArr = Colors[row];
                for (var col = 0; col < rowArr.Length; col++)
                    rowArr[col] = Color.Black;
            }
        }
    }
}
