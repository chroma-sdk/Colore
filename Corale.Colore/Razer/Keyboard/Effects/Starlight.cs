// ---------------------------------------------------------------------------------------
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
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Razer.Keyboard.Effects
{
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Describes the starlight effect.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Starlight
    {
        /// <summary>
        /// Effect type.
        /// </summary>
        [PublicAPI]
        public readonly StarlightType Type;

        /// <summary>
        /// The first color to use.
        /// </summary>
        [PublicAPI]
        public readonly Color FirstColor;

        /// <summary>
        /// The second color to use.
        /// </summary>
        [PublicAPI]
        public readonly Color SecondColor;

        /// <summary>
        /// Duration of the effect.
        /// </summary>
        [PublicAPI]
        public readonly Duration Duration;

        /// <summary>
        /// Initializes a new instance of the <see cref="Starlight" /> struct.
        /// </summary>
        /// <param name="type">Effect type.</param>
        /// <param name="firstColor">First color to use.</param>
        /// <param name="secondColor">Second color to use.</param>
        /// <param name="duration">Duration of the effect.</param>
        public Starlight(StarlightType type, Color firstColor, Color secondColor, Duration duration)
        {
            Type = type;
            FirstColor = firstColor;
            SecondColor = secondColor;
            Duration = duration;
        }
    }
}
