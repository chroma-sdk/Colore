// ---------------------------------------------------------------------------------------
// <copyright file="Keyboard.cs" company="Corale">
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
    /// Class for interacting with a Chroma keyboard.
    /// </summary>
    [PublicAPI]
    public sealed class Keyboard : Device, IKeyboard
    {
        /// <summary>
        /// Holds the application-wide instance of the <see cref="Keyboard" /> class.
        /// </summary>
        private static IKeyboard _instance;

        /// <summary>
        /// Array with custom effect structs for every key.
        /// Used when setting colors on a per-key basis.
        /// </summary>
        private readonly Custom[] _custom;

        /// <summary>
        /// Grid struct used for the helper methods.
        /// </summary>
        private readonly CustomGrid _grid;

        /// <summary>
        /// Maps <see cref="Key" /> enumeration values to their respective index in the
        /// <see cref="_custom" /> array.
        /// </summary>
        private readonly Dictionary<Key, int> _keyIndexMapping;

        /// <summary>
        /// Prevents a default instance of the <see cref="Keyboard" /> class from being created.
        /// </summary>
        private Keyboard()
        {
            CurrentEffectId = Guid.Empty;

            // Initialize the color array
            var names = Enum.GetNames(typeof(Key));
            _custom = new Custom[names.Length];
            _keyIndexMapping = new Dictionary<Key, int>(names.Length);

            for (var i = 0; i < names.Length; i++)
            {
                var name = names[i];
                Key key;
                var parsed = Enum.TryParse(name, false, out key);

                if (!parsed)
                {
                    throw new ColoreException(
                        "Failed to parse defined enum value, expected following to parse: " + name);
                }

                _keyIndexMapping[key] = i;
                _custom[i] = new Custom { Color = Color.Black, Key = key };
            }

            var gridArray = new Color[Constants.MaxRows][];
            for (Size i = 0; i < Constants.MaxRows; i++)
                gridArray[i] = new Color[Constants.MaxColumns];

            _grid = new CustomGrid(gridArray);
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IKeyboard" /> interface.
        /// </summary>
        [PublicAPI]
        public static IKeyboard Instance
        {
            get
            {
                Chroma.Initialize();
                return _instance ?? (_instance = new Keyboard());
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific <see cref="Key" /> on the keyboard.
        /// </summary>
        /// <param name="key">The key to access.</param>
        /// <returns>The color currently set for the specified key.</returns>
        public Color this[Key key]
        {
            get
            {
                return _custom[_keyIndexMapping[key]].Color;
            }

            set
            {
                Set(key, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific row and column on the
        /// keyboard grid.
        /// </summary>
        /// <param name="row">Row to query, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to query, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <returns>The color currently set on the specified position.</returns>
        public Color this[Size row, Size column]
        {
            get
            {
                return _grid.Colors[row - 1][column - 1];
            }

            set
            {
                Set(row, column, value);
            }
        }

        /// <summary>
        /// Returns whether a certain key has had a custom color set.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns><c>true</c> if the key has a color set, otherwise <c>false</c>.</returns>
        public bool IsSet(Key key)
        {
            return _custom[_keyIndexMapping[key]].Color != Color.Black;
        }

        /// <summary>
        /// Sets a breathing effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Breathing effect)
        {
            Set(NativeWrapper.CreateKeyboardEffect(effect));
        }

        /// <summary>
        /// Sets the color of all keys on the keyboard.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void Set(Color color)
        {
            Set(NativeWrapper.CreateKeyboardEffect(new Static { Color = color }));
        }

        /// <summary>
        /// Sets a breathing effect on the keyboard, fading between the
        /// two specified colors.
        /// </summary>
        /// <param name="first">Color to start from.</param>
        /// <param name="second">Color to reach, before going back to <paramref name="first" />.</param>
        public void Set(Color first, Color second)
        {
            Set(new Breathing(first, second));
        }

        /// <summary>
        /// Sets a reactive effect on the keyboard with the specified
        /// color and duration.
        /// </summary>
        /// <param name="color">Color to emit on key press.</param>
        /// <param name="duration">How long to illuminate the key after being pressed.</param>
        public void Set(Color color, Duration duration)
        {
            Set(new Reactive(color, duration));
        }

        /// <summary>
        /// Sets a custom grid effect on the keyboard using
        /// a two dimensional array of color values.
        /// </summary>
        /// <param name="colors">The grid of colors to use.</param>
        /// <remarks>
        /// The passed in arrays cannot have more than <see cref="Constants.MaxRows" /> rows and
        /// not more than <see cref="Constants.MaxColumns" /> columns in any row.
        /// </remarks>
        public void Set(Color[][] colors)
        {
            Set(new CustomGrid(colors));
        }

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(CustomGrid effect)
        {
            Set(NativeWrapper.CreateKeyboardCustomGridEffects(effect));
        }

        /// <summary>
        /// Sets a wave effect on the keyboard in the specified direction.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        public void Set(Direction direction)
        {
            Set(new Wave(direction));
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.SpectrumCycling" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Effect effect)
        {
            Set(NativeWrapper.CreateKeyboardEffect(effect));
        }

        /// <summary>
        /// Sets the colors of specific keys, using values from <see cref="Key" /> to
        /// specify the keys.
        /// </summary>
        /// <param name="effects">A collection of custom effect structs.</param>
        public void Set(IEnumerable<Custom> effects)
        {
            Set(NativeWrapper.CreateKeyboardCustomEffects(effects));
        }

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="Constants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="Constants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <exception cref="ArgumentException">Thrown if the row or column parameters are outside the valid ranges.</exception>
        public void Set(Size row, Size column, Color color, bool clear = false)
        {
            if (row > Constants.MaxRows)
                throw new ArgumentException("Row was outside the valid range (1-" + Constants.MaxRows + ").", "row");

            if (column > Constants.MaxColumns)
            {
                throw new ArgumentException(
                    "Column was outside the range of valid values (1-" + Constants.MaxColumns + ").",
                    "column");
            }

            if (clear)
            {
                // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                _grid.Clear();
            }

            _grid.Colors[row][column] = color;
            Set(_grid);
        }

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If true, the keyboard will first be cleared before setting the key.</param>
        public void Set(Key key, Color color, bool clear = false)
        {
            if (clear)
            {
                for (var i = 0; i < _custom.Length; i++)
                    _custom[i].Color = Color.Black;
            }

            _custom[_keyIndexMapping[key]].Color = color;
            Set(NativeWrapper.CreateKeyboardCustomEffects(_custom));
        }

        /// <summary>
        /// Sets a reactive effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Reactive effect)
        {
            Set(NativeWrapper.CreateKeyboardEffect(effect));
        }

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Static effect)
        {
            Set(NativeWrapper.CreateKeyboardEffect(effect));
        }

        /// <summary>
        /// Sets a wave effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Wave effect)
        {
            Set(NativeWrapper.CreateKeyboardEffect(effect));
        }
    }
}
