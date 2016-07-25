// <copyright file="Starlight.cs" company="Corale">
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
    /// Describes the starlight effect for system devices.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Starlight
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
        /// The starlight effect type.
        /// </summary>
        [PublicAPI]
        public readonly StarlightType Type;

        /// <summary>
        /// First color of the effect.
        /// </summary>
        [PublicAPI]
        public readonly Color First;

        /// <summary>
        /// Second color of the effect.
        /// </summary>
        [PublicAPI]
        public readonly Color Second;

        /// <summary>
        /// Effect duration.
        /// </summary>
        [PublicAPI]
        public readonly Duration Duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Starlight" /> struct.
        /// </summary>
        /// <param name="type">The type of starlight effect to make.</param>
        /// <param name="duration">Duration of the effect.</param>
        /// <param name="first">First color of the effect.</param>
        /// <param name="second">Second color of the effect.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public Starlight(StarlightType type, Duration duration, Color first, Color second, int parameter = 0)
        {
            Type = type;
            Duration = duration;
            First = first;
            Second = second;
            Parameter = parameter;
            Size = Marshal.SizeOf(typeof(Starlight));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Starlight" /> struct.
        /// </summary>
        /// <param name="duration">Effect duration.</param>
        /// <param name="first">First color of the effect.</param>
        /// <param name="second">Second color of the effect.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public Starlight(Duration duration, Color first, Color second, int parameter = 0)
            : this(StarlightType.Two, duration, first, second, parameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Starlight" /> struct.
        /// </summary>
        /// <param name="duration">Effect duration.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public Starlight(Duration duration, int parameter = 0)
            : this(StarlightType.Random, duration, Color.Black, Color.Black, parameter)
        {
        }
    }
}
