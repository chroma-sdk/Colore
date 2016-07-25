// <copyright file="Breathing.cs" company="Corale">
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
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Describes a breathing effect for a system device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Breathing
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
        /// The type of breathing effect.
        /// </summary>
        [PublicAPI]
        public readonly BreathingType Type;

        /// <summary>
        /// Primary color to breathe with.
        /// </summary>
        [PublicAPI]
        public readonly Color First;

        /// <summary>
        /// Secondary color to breathe with.
        /// </summary>
        [PublicAPI]
        public readonly Color Second;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="type">The type of breathing effect to create.</param>
        /// <param name="first">Primary effect color.</param>
        /// <param name="second">Secondary effect color.</param>
        /// <param name="parameter">Additional effect parameter to set.</param>
        public Breathing(BreathingType type, Color first, Color second, int parameter = 0)
        {
            Type = type;
            First = first;
            Second = second;
            Parameter = parameter;
            Size = Marshal.SizeOf(typeof(Breathing));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="color">Color to breathe with.</param>
        /// <param name="parameter">Additional effect parameter to set.</param>
        public Breathing(Color color, int parameter = 0)
            : this(BreathingType.One, color, Color.Black, parameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="first">Primary effect color.</param>
        /// <param name="second">Secondary effect color.</param>
        /// <param name="parameter">Additional effect parameter to set.</param>
        public Breathing(Color first, Color second, int parameter = 0)
            : this(BreathingType.Two, first, second, parameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="parameter">Additional effect parameter to set.</param>
        public Breathing(int parameter = 0)
            : this(BreathingType.Random, Color.Black, Color.Black, parameter)
        {
        }
    }
}
