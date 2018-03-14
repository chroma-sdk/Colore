// ---------------------------------------------------------------------------------------
// <copyright file="PositionData.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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
    using System.Collections.Generic;

    /// <summary>
    /// Contains methods to determine if a grid positions is safe.
    /// </summary>
    internal static class PositionData
    {
        /// <summary>
        /// Set of positions that are unsafe to use.
        /// </summary>
        internal static readonly HashSet<int> UnsafePositions = new HashSet<int>
        {
            0x0000,
            0x0002,
            0x0012,
            0x0013,
            0x0014,
            0x0015,
            0x020E,
            0x030D,
            0x030E,
            0x030F,
            0x0310,
            0x0311,
            0x0315,
            0x0401,
            0x0402,
            0x040D,
            0x040E,
            0x040F,
            0x0411,
            0x0504,
            0x0505,
            0x0506,
            0x0508,
            0x0509,
            0x050A,
            0x0512,
            0x0515
        };
    }
}
