// ---------------------------------------------------------------------------------------
// <copyright file="KeyboardImplementation.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common.Logging;

    using Corale.Colore.Api;
    using Corale.Colore.Data;
    using Corale.Colore.Effects.Keyboard;

    using JetBrains.Annotations;

    /// <inheritdoc cref="IKeyboard" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with a Chroma keyboard.
    /// </summary>
    internal sealed class KeyboardImplementation : DeviceImplementation, IKeyboard
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(KeyboardImplementation));

        /// <summary>
        /// Grid struct used for the helper methods.
        /// </summary>
        private Custom _grid;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardImplementation" /> class.
        /// </summary>
        public KeyboardImplementation(IChromaApi api)
            : base(api)
        {
            Log.Info("Keyboard initializing...");

            CurrentEffectId = Guid.Empty;

            // We keep a local copy of a grid to speed up grid operations
            Log.Debug("Initializing private copy of Custom");
            _grid = Custom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific <see cref="Key" /> on the keyboard.
        /// The SDK will translate this appropriately depending on user configuration.
        /// </summary>
        /// <param name="key">The key to access.</param>
        /// <returns>The color currently set for the specified key.</returns>
        public Color this[Key key]
        {
            get => _grid[key];
            set => SetKeyAsync(key, value).Wait();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific row and column on the
        /// keyboard grid.
        /// </summary>
        /// <param name="row">Row to query, between 0 and <see cref="Effects.Keyboard.Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to query, between 0 and <see cref="Effects.Keyboard.Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The color currently set on the specified position.</returns>
        public Color this[int row, int column]
        {
            get => _grid[row, column];
            set => SetPositionAsync(row, column, value).Wait();
        }

        /// <summary>
        /// Returns whether the specified key is safe to use.
        /// </summary>
        /// <param name="key">The <see cref="Key" /> to test.</param>
        /// <returns><c>true</c> if the <see cref="Key" /> is safe, otherwise <c>false</c>.</returns>
        /// <remarks>
        /// A "safe" key means one that will always be visible if lit up,
        /// regardless of the physical layout of the keyboard.
        /// </remarks>
        [PublicAPI]
        public static bool IsKeySafe(Key key)
        {
            return !PositionData.UnsafePositions.Contains((int)key);
        }

        /// <summary>
        /// Returns whether the specified position is safe to use.
        /// </summary>
        /// <param name="row">Row to query.</param>
        /// <param name="column">Column to query.</param>
        /// <returns><c>true</c> if the position is safe, otherwise false.</returns>
        /// <remarks>
        /// A "safe" positions means one that will always be visible of lit up,
        /// regardless of the physical layout of the keyboard.
        /// </remarks>
        [PublicAPI]
        public static bool IsPositionSafe(int row, int column)
        {
            return !PositionData.UnsafePositions.Contains((row << 8) | column);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns whether a certain key has had a custom color set.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns><c>true</c> if the key has a color set, otherwise <c>false</c>.</returns>
        public bool IsSet(Key key)
        {
            return _grid[key] != Color.Black;
        }

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all keys on the keyboard.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _grid.Set(color);
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(Effect.CustomKey, _grid).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="Custom" />
        /// struct in the <see cref="KeyboardImplementation" /> class.
        /// </remarks>
        public async Task<Guid> SetCustomAsync(Custom effect)
        {
            _grid = effect;
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(Effect.CustomKey, _grid).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 0 and <see cref="Effects.Keyboard.Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to set, between 0 and <see cref="Effects.Keyboard.Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <exception cref="System.ArgumentException">Thrown if the row or column parameters are outside the valid ranges.</exception>
        public async Task<Guid> SetPositionAsync(int row, int column, Color color, bool clear = false)
        {
            if (clear)
                _grid.Clear();

            _grid[row, column] = color;
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(Effect.CustomKey, _grid).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If true, the keyboard will first be cleared before setting the key.</param>
        public async Task<Guid> SetKeyAsync(Key key, Color color, bool clear = false)
        {
            if (clear)
                _grid.Clear();

            _grid[key] = color;
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(Effect.CustomKey, _grid).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        public async Task<Guid> SetKeysAsync(Color color, Key key, params Key[] keys)
        {
            var guid = await SetKeyAsync(key, color).ConfigureAwait(false);
            foreach (var additional in keys)
                guid = await SetKeyAsync(additional, color).ConfigureAwait(false);

            return guid;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">
        /// If <c>true</c>, the keyboard keys will be cleared before
        /// applying the new colors.
        /// </param>
        public async Task<Guid> SetKeysAsync(IEnumerable<Key> keys, Color color, bool clear = false)
        {
            var guid = Guid.Empty;

            if (clear)
                guid = await ClearAsync().ConfigureAwait(false);

            foreach (var key in keys)
                guid = await SetKeyAsync(key, color).ConfigureAwait(false);

            return guid;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetEffectAsync(await Api.CreateKeyboardEffectAsync(Effect.Static, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Keyboard.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _grid.Clear();
            return await SetEffectAsync(Effect.None).ConfigureAwait(false);
        }
    }
}
