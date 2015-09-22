// ---------------------------------------------------------------------------------------
// <copyright file="IKeyboard.cs" company="Corale">
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
    using System.Collections.Generic;

    using Corale.Colore.Annotations;
    using Corale.Colore.Razer.Keyboard;
    using Corale.Colore.Razer.Keyboard.Effects;

    /// <summary>
    /// Interface for keyboard functionality.
    /// </summary>
    public interface IKeyboard : IDevice
    {
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific <see cref="Key" /> on the keyboard.
        /// </summary>
        /// <param name="key">The key to access.</param>
        /// <returns>The color currently set for the specified key.</returns>
        [PublicAPI]
        Color this[Key key] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific row and column on the
        /// keyboard grid.
        /// </summary>
        /// <param name="row">Row to query, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to query, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <returns>The color currently set on the specified position.</returns>
        [PublicAPI]
        Color this[Size row, Size column] { get; set; }

        /// <summary>
        /// Returns whether a certain key has had a custom color set.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns><c>true</c> if the key has a color set, otherwise <c>false</c>.</returns>
        [PublicAPI]
        bool IsSet(Key key);

        /// <summary>
        /// Sets a breathing effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetBreathing(Breathing effect);

        /// <summary>
        /// Sets a breathing effect on the keyboard, fading between the
        /// two specified colors.
        /// </summary>
        /// <param name="first">Color to start from.</param>
        /// <param name="second">Color to reach, before going back to <paramref name="first" />.</param>
        [PublicAPI]
        void SetBreathing(Color first, Color second);

        /// <summary>
        /// Sets a reactive effect on the keyboard with the specified
        /// color and duration.
        /// </summary>
        /// <param name="color">Color to emit on key press.</param>
        /// <param name="duration">How long to illuminate the key after being pressed.</param>
        [PublicAPI]
        void SetReactive(Color color, Duration duration);

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
        [PublicAPI]
        void SetGrid(Color[][] colors);

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="Custom" />
        /// struct in the <see cref="Keyboard" /> class.
        /// </remarks>
        [PublicAPI]
        void SetCustom(Custom effect);

        /// <summary>
        /// Sets a wave effect on the keyboard in the specified direction.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        [PublicAPI]
        void SetWave(Direction direction);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> and <see cref="Effect.SpectrumCycling" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetEffect(Effect effect);

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        [PublicAPI]
        void SetPosition(Size row, Size column, Color color, bool clear = false);

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the key.</param>
        [PublicAPI]
        void SetKey(Key key, Color color, bool clear = false);

        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        [PublicAPI]
        void SetKeys(Color color, Key key, params Key[] keys);

        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the keys.</param>
        [PublicAPI]
        void SetKeys(IEnumerable<Key> keys, Color color, bool clear = false);

        /// <summary>
        /// Sets a reactive effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetReactive(Reactive effect);

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a wave effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetWave(Wave effect);
    }
}
