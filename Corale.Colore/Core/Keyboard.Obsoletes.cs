// ---------------------------------------------------------------------------------------
// <copyright file="Keyboard.Obsoletes.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System;
    using System.Collections.Generic;

    using Corale.Colore.Razer.Keyboard;
    using Corale.Colore.Razer.Keyboard.Effects;

    /// <summary>
    /// Class for interacting with a Chroma keyboard.
    /// </summary>
    public sealed partial class Keyboard
    {
        /// <summary>
        /// Sets a breathing effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        public void Set(Breathing effect)
        {
            SetBreathing(effect);
        }

        /// <summary>
        /// Sets the color of all keys on the keyboard.
        /// </summary>
        /// <param name="color">Color to set.</param>
        [Obsolete("Set is deprecated, please use SetAll(Color).", false)]
        public void Set(Color color)
        {
            SetAll(color);
        }

        /// <summary>
        /// Sets a breathing effect on the keyboard, fading between the
        /// two specified colors.
        /// </summary>
        /// <param name="first">Color to start from.</param>
        /// <param name="second">Color to reach, before going back to <paramref name="first" />.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Color, Color).", false)]
        public void Set(Color first, Color second)
        {
            SetBreathing(new Breathing(first, second));
        }

        /// <summary>
        /// Sets a reactive effect on the keyboard with the specified
        /// color and duration.
        /// </summary>
        /// <param name="color">Color to emit on key press.</param>
        /// <param name="duration">How long to illuminate the key after being pressed.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Color, Duration).", false)]
        public void Set(Color color, Duration duration)
        {
            SetReactive(color, duration);
        }

        /// <summary>
        /// Sets a custom grid effect on the keyboard using
        /// a two dimensional array of color values.
        /// </summary>
        /// <param name="colors">The grid of colors to use.</param>
        /// <remarks>
        /// The passed in arrays cannot have more than <see cref="Constants.MaxRows" /> rows and
        /// not more than <see cref="Constants.MaxColumns" /> columns in any row.
        /// <para />
        /// This will overwrite the internal <see cref="Custom" />
        /// struct in the <see cref="Keyboard" /> class.
        /// </remarks>
        [Obsolete("Set is deprecated, please use SetGrid(Color[][]).", false)]
        public void Set(Color[][] colors)
        {
            SetGrid(colors);
        }

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="Custom" />
        /// struct in the <see cref="Keyboard" /> class.
        /// </remarks>
        [Obsolete("Set is deprecated, please use SetCustom(Custom).", false)]
        public void Set(Custom effect)
        {
           SetCustom(effect);
        }

        /// <summary>
        /// Sets a wave effect on the keyboard in the specified direction.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        [Obsolete("Set is deprecated, please use SetWave(Direction).", false)]
        public void Set(Direction direction)
        {
            SetWave(direction);
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> and <see cref="Effect.SpectrumCycling" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        public void Set(Effect effect)
        {
            SetEffect(effect);
        }

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 0 and <see cref="Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to set, between 0 and <see cref="Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <exception cref="ArgumentException">Thrown if the row or column parameters are outside the valid ranges.</exception>
        [Obsolete("Set is deprecated, please use SetPosition(Size, Size, Color, bool).", false)]
        public void Set(Size row, Size column, Color color, bool clear = false)
        {
            SetPosition(row, column, color, clear);
        }

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If true, the keyboard will first be cleared before setting the key.</param>
        [Obsolete("Set is deprecated, please use SetKey(Key, Color, bool).", false)]
        public void Set(Key key, Color color, bool clear = false)
        {
            SetKey(key, color, clear);
        }

        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        [Obsolete("Set is deprecated, please use SetKeys(Color, Key, Key[][]).", false)]
        public void Set(Color color, Key key, params Key[] keys)
        {
            SetKeys(color, key, keys);
        }

        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">
        /// If <c>true</c>, the keyboard keys will be cleared before
        /// applying the new colors.
        /// </param>
        [Obsolete("Set is deprecated, please use SetKeys(INumerable<Key>, Color, bool).", false)]
        public void Set(IEnumerable<Key> keys, Color color, bool clear = false)
        {
            SetKeys(keys, color, clear);
        }

        /// <summary>
        /// Sets a reactive effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Reactive).", false)]
        public void Set(Reactive effect)
        {
            SetReactive(effect);
        }

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        public void Set(Static effect)
        {
            SetStatic(effect);
        }

        /// <summary>
        /// Sets a wave effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        public void Set(Wave effect)
        {
            SetWave(effect);
        }
    }
}