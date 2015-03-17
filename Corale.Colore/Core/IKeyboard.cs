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
        Color this[int row, int column] { get; set; }

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
        void Set(Breathing effect);

        /// <summary>
        /// Sets a breathing effect on the keyboard, fading between the
        /// two specified colors.
        /// </summary>
        /// <param name="first">Color to start from.</param>
        /// <param name="second">Color to reach, before going back to <paramref name="first" />.</param>
        void Set(Color first, Color second);

        /// <summary>
        /// Sets a reactive effect on the keyboard with the specified
        /// color and duration.
        /// </summary>
        /// <param name="color">Color to emit on key press.</param>
        /// <param name="duration">How long to illuminate the key after being pressed.</param>
        void Set(Color color, Duration duration);

        /// <summary>
        /// Sets a custom grid effect on the keyboard using
        /// a two dimensional array of color values.
        /// </summary>
        /// <param name="colors">The grid of colors to use.</param>
        /// <remarks>
        /// The passed in arrays cannot have more than <see cref="Constants.MaxRows" /> rows and
        /// not more than <see cref="Constants.MaxColumns" /> columns in any row.
        /// </remarks>
        void Set(Color[][] colors);

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        void Set(CustomGrid effect);

        /// <summary>
        /// Sets a wave effect on the keyboard in the specified direction.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        void Set(Direction direction);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.SpectrumCycling" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        void Set(Effect effect);

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        void Set(int row, int column, Color color, bool clear = false);

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If true, the keyboard will first be cleared before setting the key.</param>
        [PublicAPI]
        void Set(Key key, Color color, bool clear = false);

        /// <summary>
        /// Sets a reactive effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void Set(Reactive effect);

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void Set(Static effect);

        /// <summary>
        /// Sets a wave effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void Set(Wave effect);
    }
}
