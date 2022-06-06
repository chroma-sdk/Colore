// ---------------------------------------------------------------------------------------
// <copyright file="KeyboardImplementation.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Keyboard;
    using Colore.Logging;

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
        private static readonly ILog Log = LogProvider.For<KeyboardImplementation>();

        /// <summary>
        /// Grid struct used for the helper methods.
        /// </summary>
        private CustomKeyboardEffect _grid;

        /// <summary>
        /// Deathstalker grid struct used for the helper methods.
        /// </summary>
        private DeathstalkerGridEffect _deathstalkerGrid;

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
            _grid = CustomKeyboardEffect.Create();

            Log.Debug("Initializing private copy of Deathstalker grid");
            _deathstalkerGrid = DeathstalkerGridEffect.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether a Razer Deathstalker Chroma is connected to the system.
        /// </summary>
        /// <remarks>
        /// This performs an SDK query on each access, to avoid caching issues.
        /// </remarks>
        public bool IsDeathstalkerConnected
        {
            get
            {
                var result = Api.QueryDeviceAsync(Devices.Deathstalker).Result;
                return result.Connected;
            }
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
            set
            {
#if NETSTANDARD2_0 || NETSTANDARD2_1
                if (IsRestApi)
                {
                    SetKeyAsync(key, value).GetAwaiter().GetResult();
                }
                else
                {
                    SetKey(key, value);
                }
#else
                SetKey(key, value);
#endif
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific row and column on the
        /// keyboard grid.
        /// </summary>
        /// <param name="row">Row to query, between 0 and <see cref="KeyboardConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to query, between 0 and <see cref="KeyboardConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The color currently set on the specified position.</returns>
        public Color this[int row, int column]
        {
            get => _grid[row, column];
            set
            {
#if NETSTANDARD2_0 || NETSTANDARD2_1
                if (IsRestApi)
                {
                    SetPositionAsync(row, column, value).GetAwaiter().GetResult();
                }
                else
                {
                    SetPosition(row, column, value);
                }
#else
                SetPosition(row, column, value);
#endif
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific Deathstalker zone.
        /// </summary>
        /// <param name="zoneIndex">Zone to query, between 0 and <see cref="KeyboardConstants.MaxDeathstalkerZones" /> (exclusive upper bound).</param>
        /// <returns>The color currently set for the specified zone.</returns>
        public Color this[int zoneIndex]
        {
            get => _deathstalkerGrid[zoneIndex];
            set
            {
#if NETSTANDARD2_0 || NETSTANDARD2_1
                if (IsRestApi)
                {
                    SetDeathstalkerZoneAsync(zoneIndex, value).GetAwaiter().GetResult();
                }
                else
                {
                    SetDeathstalkerZone(zoneIndex, value);
                }
#else
                SetDeathstalkerZone(zoneIndex, value);
#endif
            }
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
        public static bool IsKeySafe(Key key) => !PositionData.UnsafePositions.Contains((int)key);

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
        public static bool IsPositionSafe(int row, int column) =>
            !PositionData.UnsafePositions.Contains((row << 8) | column);

        /// <inheritdoc />
        /// <summary>
        /// Returns whether a certain key has had a custom color set.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns><c>true</c> if the key has a color set, otherwise <c>false</c>.</returns>
        public bool IsSet(Key key) => _grid[key] != Color.Black;

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all keys on the keyboard.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override Guid SetAll(Color color)
        {
            _grid.Set(color);
            _deathstalkerGrid.Set(color);

            // See note in SetAllAsync about why we use the deathstalker grid
            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.Custom, _deathstalkerGrid));
        }

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all keys on the keyboard.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _grid.Set(color);
            _deathstalkerGrid.Set(color);

            /* Use the Deathstalker grid effect to set all colors, rather than the newer CUSTOM_KEY effect.
             * Since using CUSTOM_KEY has the same effect on everything except the Deathstalker,
             * using the old CUSTOM effect for setting all colors means that SetAllAsync will work "for free"
             * on the Deathstalker as well, saving developers the need to do their own special handling for it.
             */
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.Custom, _deathstalkerGrid)
                             .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="CustomKeyboardEffect" />
        /// struct in the <see cref="KeyboardImplementation" /> class.
        /// </remarks>
        public Guid SetCustom(CustomKeyboardEffect effect)
        {
            _grid = effect;

            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.CustomKey, _grid));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="CustomKeyboardEffect" />
        /// struct in the <see cref="KeyboardImplementation" /> class.
        /// </remarks>
        public async Task<Guid> SetCustomAsync(CustomKeyboardEffect effect)
        {
            _grid = effect;
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.CustomKey, _grid).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an extended custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public Guid SetExtendedCustom(ExtendedCustomKeyboardEffect effect)
        {
            var effectGuid = Api.CreateKeyboardEffect(KeyboardEffectType.ExtendedCustom, effect);

            return SetEffect(effectGuid);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an extended custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public async Task<Guid> SetExtendedCustomAsync(ExtendedCustomKeyboardEffect effect)
        {
            var effectGuid = await Api.CreateKeyboardEffectAsync(KeyboardEffectType.ExtendedCustom, effect)
                                      .ConfigureAwait(false);

            return await SetEffectAsync(effectGuid).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="KeyboardEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        public Guid SetEffect(KeyboardEffectType effectType) => SetEffect(Api.CreateKeyboardEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="KeyboardEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        public async Task<Guid> SetEffectAsync(KeyboardEffectType effectType) =>
            await SetEffectAsync(await Api.CreateKeyboardEffectAsync(effectType).ConfigureAwait(false))
                .ConfigureAwait(false);

        /// <inheritdoc />
        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 0 and <see cref="KeyboardConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to set, between 0 and <see cref="KeyboardConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <exception cref="System.ArgumentException">Thrown if the row or column parameters are outside the valid ranges.</exception>
        public Guid SetPosition(int row, int column, Color color, bool clear = false)
        {
            if (clear)
            {
                _grid.Clear();
            }

            _grid[row, column] = color;

            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.CustomKey, _grid));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 0 and <see cref="KeyboardConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to set, between 0 and <see cref="KeyboardConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <exception cref="System.ArgumentException">Thrown if the row or column parameters are outside the valid ranges.</exception>
        public async Task<Guid> SetPositionAsync(int row, int column, Color color, bool clear = false)
        {
            if (clear)
            {
                _grid.Clear();
            }

            _grid[row, column] = color;
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.CustomKey, _grid).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If true, the keyboard will first be cleared before setting the key.</param>
        public Guid SetKey(Key key, Color color, bool clear = false)
        {
            if (clear)
            {
                _grid.Clear();
            }

            _grid[key] = color;

            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.CustomKey, _grid));
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
            {
                _grid.Clear();
            }

            _grid[key] = color;
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.CustomKey, _grid).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        public Guid SetKeys(Color color, Key key, params Key[] keys)
        {
            var guid = SetKey(key, color);
            foreach (var additional in keys)
            {
                guid = SetKey(additional, color);
            }

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
        public Guid SetKeys(IEnumerable<Key> keys, Color color, bool clear = false)
        {
            var guid = Guid.Empty;

            if (clear)
            {
                guid = Clear();
            }

            foreach (var key in keys)
            {
                guid = SetKey(key, color);
            }

            return guid;
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
            {
                guid = await ClearAsync().ConfigureAwait(false);
            }

            foreach (var key in keys)
                guid = await SetKeyAsync(key, color).ConfigureAwait(false);

            return guid;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public Guid SetStatic(StaticKeyboardEffect effect) =>
            SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.Static, effect));

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetStaticAsync(StaticKeyboardEffect effect) =>
            await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.Static, effect).ConfigureAwait(false))
                .ConfigureAwait(false);

        /// <inheritdoc />
        /// <summary>
        /// Sets the specified Deathstalker zone to a color.
        /// </summary>
        /// <param name="zoneIndex">The index of the Deathstalker zone to set.</param>
        /// <param name="color">The color to set.</param>
        /// <param name="clear">Whether to clear all colors before setting the new one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public Guid SetDeathstalkerZone(int zoneIndex, Color color, bool clear = false)
        {
            if (clear)
            {
                _deathstalkerGrid.Clear();
            }

            _deathstalkerGrid[zoneIndex] = color;

            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.Custom, _deathstalkerGrid));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the specified Deathstalker zone to a color.
        /// </summary>
        /// <param name="zoneIndex">The index of the Deathstalker zone to set.</param>
        /// <param name="color">The color to set.</param>
        /// <param name="clear">Whether to clear all colors before setting the new one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public async Task<Guid> SetDeathstalkerZoneAsync(int zoneIndex, Color color, bool clear = false)
        {
            if (clear)
            {
                _deathstalkerGrid.Clear();
            }

            _deathstalkerGrid[zoneIndex] = color;
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.Custom, _deathstalkerGrid)
                             .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a Deathstalker grid effect.
        /// </summary>
        /// <param name="effect">The Deathstalker grid effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public Guid SetDeathstalker(DeathstalkerGridEffect effect)
        {
            _deathstalkerGrid = effect;

            return SetEffect(Api.CreateKeyboardEffect(KeyboardEffectType.Custom, _deathstalkerGrid));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a Deathstalker grid effect.
        /// </summary>
        /// <param name="effect">The Deathstalker grid effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public async Task<Guid> SetDeathstalkerAsync(DeathstalkerGridEffect effect)
        {
            _deathstalkerGrid = effect;
            return await SetEffectAsync(
                    await Api.CreateKeyboardEffectAsync(KeyboardEffectType.Custom, _deathstalkerGrid)
                             .ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Keyboard.
        /// </summary>
        public override Guid Clear()
        {
            _grid.Clear();
            _deathstalkerGrid.Clear();

            return SetEffect(KeyboardEffectType.None);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Keyboard.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _grid.Clear();
            _deathstalkerGrid.Clear();
            return await SetEffectAsync(KeyboardEffectType.None).ConfigureAwait(false);
        }
    }
}
