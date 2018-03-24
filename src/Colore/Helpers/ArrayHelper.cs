// ---------------------------------------------------------------------------------------
// <copyright file="ArrayHelper.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Helpers
{
    using Colore.Data;

    /// <summary>
    /// Contains helper methods for working with arrays.
    /// </summary>
    internal static class ArrayHelper
    {
        /// <summary>
        /// Copies a single-dimensional <see cref="Color" /> array to a multi-dimensional one.
        /// </summary>
        /// <param name="source">Source array to copy from.</param>
        /// <param name="destination">Destination array to copy into.</param>
        /// <remarks>
        /// <paramref name="destination" /> array must be large enough to contain the values in the <paramref name="source" /> array.
        /// </remarks>
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

        internal static void CopyToMultidimensional(this Color[] source, Color[,] destination)
        {
            var rows = destination.GetLength(0);
            var cols = destination.GetLength(1);

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                    destination[row, col] = source[row * cols + col];
            }
        }

#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    }
}
