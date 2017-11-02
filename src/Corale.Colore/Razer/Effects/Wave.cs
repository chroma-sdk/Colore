// <copyright file="Wave.cs" company="Corale">
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

namespace Corale.Colore.Razer.Effects
{
    using System;
    using System.Runtime.InteropServices;

    using JetBrains.Annotations;

    /// <summary>
    /// Describes the wave effect for a system device.
    /// </summary>
    [Obsolete("Use custom effects instead.")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Wave
    {
        /// <summary>
        /// The size of the struct.
        /// </summary>
        [PublicAPI]
        public readonly int Size;

        /// <summary>
        /// Additional effect parameter.
        /// </summary>
        [PublicAPI]
        public readonly int Parameter;

        /// <summary>
        /// Direction of the wave.
        /// </summary>
        [PublicAPI]
        public readonly Direction Direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Wave" /> struct.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public Wave(Direction direction, int parameter = 0)
        {
            Direction = direction;
            Parameter = parameter;
            Size = Marshal.SizeOf(typeof(Wave));
        }
    }
}
