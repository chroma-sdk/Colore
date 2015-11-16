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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Razer.Mouse.Effects
{
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Core;

    /// <summary>
    /// Describes the breathing effect type.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Breathing
    {
        /// <summary>
        /// The LED on which to apply the effect.
        /// </summary>
        [UsedImplicitly]
        public readonly Led Led;

        /// <summary>
        /// The type of breathing effect.
        /// </summary>
        [UsedImplicitly]
        public readonly BreathingType Type;

        /// <summary>
        /// Initial effect color.
        /// </summary>
        [UsedImplicitly]
        public readonly Color First;

        /// <summary>
        /// Second color to breathe to.
        /// </summary>
        [UsedImplicitly]
        public readonly Color Second;

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct.
        /// </summary>
        /// <param name="led">The LED on which to apply the effect.</param>
        /// <param name="type">The type of breathing effect to create.</param>
        /// <param name="first">The initial <see cref="Color" /> to use.</param>
        /// <param name="second">The second color.</param>
        public Breathing(Led led, BreathingType type, Color first, Color second)
        {
            Led = led;
            Type = type;
            First = first;
            Second = second;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making a specified LED breathe randomly.
        /// </summary>
        /// <param name="led">The LED on which to apply the effect.</param>
        public Breathing(Led led)
            : this(led, BreathingType.Random, Color.Black, Color.Black)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making a specified LED breathe to a single color.
        /// </summary>
        /// <param name="led">The LED on which to apply the effect.</param>
        /// <param name="first">The color to breathe to.</param>
        public Breathing(Led led, Color first)
            : this(led, BreathingType.One, first, Color.Black)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making a specified LED breathe between two colors.
        /// </summary>
        /// <param name="led">The LED on which to apply the effect.</param>
        /// <param name="first">Initial color.</param>
        /// <param name="second">Second color.</param>
        public Breathing(Led led, Color first, Color second)
            : this(led, BreathingType.Two, first, second)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making every LED on the mouse follow the specified pattern.
        /// </summary>
        /// <param name="type">The type of effect to create.</param>
        /// <param name="first">Initial color.</param>
        /// <param name="second">Second color.</param>
        public Breathing(BreathingType type, Color first, Color second)
            : this(Led.All, type, first, second)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making every LED breathe to a single color.
        /// </summary>
        /// <param name="first">The <see cref="Color" /> to breathe to.</param>
        public Breathing(Color first)
            : this(BreathingType.One, first, Color.Black)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Breathing" /> struct for
        /// making every LED breathe between two colors.
        /// </summary>
        /// <param name="first">Initial color.</param>
        /// <param name="second">Second color.</param>
        public Breathing(Color first, Color second)
            : this(BreathingType.Two, first, second)
        {
        }
    }
}
