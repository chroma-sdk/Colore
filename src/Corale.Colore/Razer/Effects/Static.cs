// <copyright file="Static.cs" company="Corale">
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

    using Corale.Colore.Core;

    using JetBrains.Annotations;

    /// <summary>
    /// Describes the static effect for system devices.
    /// </summary>
    [Obsolete("Use custom effects instead.")]
    [StructLayout(LayoutKind.Sequential)]
    public struct Static
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
        /// Effect color.
        /// </summary>
        [PublicAPI]
        public readonly Color Color;

        /// <summary>
        /// Initializes a new instance of the <see cref="Static" /> struct.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public Static(Color color, int parameter = 0)
        {
            Color = color;
            Parameter = parameter;
            Size = Marshal.SizeOf(typeof(Static));
        }
    }
}
