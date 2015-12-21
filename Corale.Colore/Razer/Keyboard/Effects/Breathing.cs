// ---------------------------------------------------------------------------------------
// <copyright file="Breathing.cs" company="Corale">
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
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Describes the breathing effect.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Breathing
    {
        /// <summary>
        /// The type of breathing effect.
        /// </summary>
        [PublicAPI]
        public readonly BreathingType Type;

        /// <summary>
        /// First color.
        /// </summary>
        [PublicAPI]
        public readonly Color First;

        /// <summary>
        /// Second color.
        /// </summary>
        [PublicAPI]
        public readonly Color Second;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="type">The type of breathing effect.</param>
        /// <param name="first">Initial color.</param>
        /// <param name="second">Second color.</param>
        public Breathing(BreathingType type, Color first, Color second)
        {
            Type = type;
            First = first;
            Second = second;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct with
        /// two colors to breathe between.
        /// </summary>
        /// <param name="first">Initial color.</param>
        /// <param name="second">Second color.</param>
        public Breathing(Color first, Color second)
            : this(BreathingType.Two, first, second)
        {
        }
    }
}
