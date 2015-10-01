// ---------------------------------------------------------------------------------------
// <copyright file="IKeyboard.Obsoletes.cs" company="Corale">
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
    using Corale.Colore.Annotations;
    using Corale.Colore.Razer.Keyboard;
    using Corale.Colore.Razer.Keyboard.Effects;

    /// <summary>
    /// Interface for keyboard functionality.
    /// </summary>
    public partial interface IKeyboard
    {
        /// <summary>
        /// Sets a breathing effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        [PublicAPI]
        void Set(Breathing effect);

        /// <summary>
        /// Sets a breathing effect on the keyboard, fading between the
        /// two specified colors.
        /// </summary>
        /// <param name="first">Color to start from.</param>
        /// <param name="second">Color to reach, before going back to <paramref name="first" />.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Color, Color).", false)]
        [PublicAPI]
        void Set(Color first, Color second);

        /// <summary>
        /// Sets a reactive effect on the keyboard with the specified
        /// color and duration.
        /// </summary>
        /// <param name="color">Color to emit on key press.</param>
        /// <param name="duration">How long to illuminate the key after being pressed.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Color, Duration).", false)]
        [PublicAPI]
        void Set(Color color, Duration duration);

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
        [PublicAPI]
        void Set(Color[][] colors);

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="Custom" />
        /// struct in the <see cref="Keyboard" /> class.
        /// </remarks>
        [Obsolete("Set is deprecated, please use SetCustom(Custom).", false)]
        [PublicAPI]
        void Set(Custom effect);

        /// <summary>
        /// Sets a wave effect on the keyboard in the specified direction.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        [Obsolete("Set is deprecated, please use SetWave(Direction).", false)]
        [PublicAPI]
        void Set(Direction direction);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> and <see cref="Effect.SpectrumCycling" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        [PublicAPI]
        void Set(Effect effect);

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        [Obsolete("Set is deprecated, please use SetPosition(Size, Size, Color, bool).", false)]
        [PublicAPI]
        void Set(Size row, Size column, Color color, bool clear = false);

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the key.</param>
        [Obsolete("Set is deprecated, please use SetKey(Key, Color, bool).", false)]
        [PublicAPI]
        void Set(Key key, Color color, bool clear = false);

        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        [Obsolete("Set is deprecated, please use SetKeys(Color, Key, Key[][]).", false)]
        [PublicAPI]
        void Set(Color color, Key key, params Key[] keys);

        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the keys.</param>
        [Obsolete("Set is deprecated, please use SetKeys(INumerable<Key>, Color, bool).", false)]
        [PublicAPI]
        void Set(IEnumerable<Key> keys, Color color, bool clear = false);

        /// <summary>
        /// Sets a reactive effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Reactive).", false)]
        [PublicAPI]
        void Set(Reactive effect);

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        [PublicAPI]
        void Set(Static effect);

        /// <summary>
        /// Sets a wave effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        [PublicAPI]
        void Set(Wave effect);
    }
}
