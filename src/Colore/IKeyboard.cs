// ---------------------------------------------------------------------------------------
// <copyright file="IKeyboard.cs" company="Corale">
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

namespace Colore
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Colore.Data;
    using Colore.Effects.Keyboard;
    using Colore.Implementations;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Interface for keyboard functionality.
    /// </summary>
    [PublicAPI]
    public interface IKeyboard : IDevice
    {
        /// <summary>
        /// Gets a value indicating whether a Razer Deathstalker Chroma is connected to the system.
        /// </summary>
        bool IsDeathstalkerConnected { get; }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific <see cref="Key" /> on the keyboard.
        /// The SDK will translate this appropriately depending on user configuration.
        /// </summary>
        /// <param name="key">The key to access.</param>
        /// <returns>The color currently set for the specified key.</returns>
        Color this[Key key] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific row and column on the
        /// keyboard grid.
        /// </summary>
        /// <param name="row">Row to query, between 0 and <see cref="KeyboardConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">Column to query, between 0 and <see cref="KeyboardConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The color currently set on the specified position.</returns>
        Color this[int row, int column] { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific Deathstalker zone.
        /// </summary>
        /// <param name="zoneIndex">Zone to query, between 0 and <see cref="KeyboardConstants.MaxDeathstalkerZones" /> (exclusive upper bound).</param>
        /// <returns>The color currently set for the specified zone.</returns>
        Color this[int zoneIndex] { get; set; }

        /// <summary>
        /// Returns whether a certain key has had a custom color set.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns><c>true</c> if the key has a color set, otherwise <c>false</c>.</returns>
        bool IsSet(Key key);

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="CustomKeyboardEffect" />
        /// struct in the <see cref="KeyboardImplementation" /> class.
        /// </remarks>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetCustom(CustomKeyboardEffect effect);

        /// <summary>
        /// Sets a custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <remarks>
        /// This will overwrite the current internal <see cref="CustomKeyboardEffect" />
        /// struct in the <see cref="KeyboardImplementation" /> class.
        /// </remarks>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetCustomAsync(CustomKeyboardEffect effect);

        /// <summary>
        /// Sets an extended custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetExtendedCustom(ExtendedCustomKeyboardEffect effect);

        /// <summary>
        /// Sets an extended custom grid effect on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetExtendedCustomAsync(ExtendedCustomKeyboardEffect effect);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="KeyboardEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetEffect(KeyboardEffectType effectType);

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="KeyboardEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetEffectAsync(KeyboardEffectType effectType);

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="KeyboardConstants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="KeyboardConstants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetPosition(int row, int column, Color color, bool clear = false);

        /// <summary>
        /// Sets the color on a specific row and column on the keyboard grid.
        /// </summary>
        /// <param name="row">Row to set, between 1 and <see cref="KeyboardConstants.MaxRows" />.</param>
        /// <param name="column">Column to set, between 1 and <see cref="KeyboardConstants.MaxColumns" />.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">Whether or not to clear the existing colors before setting this one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetPositionAsync(int row, int column, Color color, bool clear = false);

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the key.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetKey(Key key, Color color, bool clear = false);

        /// <summary>
        /// Sets the color of a specific key on the keyboard.
        /// </summary>
        /// <param name="key">Key to modify.</param>
        /// <param name="color">Color to set.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the key.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetKeyAsync(Key key, Color color, bool clear = false);

        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetKeys(Color color, Key key, params Key[] keys);

        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the keys.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetKeys(IEnumerable<Key> keys, Color color, bool clear = false);

        /// <summary>
        /// Sets the specified color on a set of keys.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="key">First key to change.</param>
        /// <param name="keys">Additional keys that should also have the color applied.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetKeysAsync(Color color, Key key, params Key[] keys);

        /// <summary>
        /// Sets a color on a collection of keys.
        /// </summary>
        /// <param name="keys">The keys which should have their color changed.</param>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        /// <param name="clear">If <c>true</c>, the keyboard will first be cleared before setting the keys.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetKeysAsync(IEnumerable<Key> keys, Color color, bool clear = false);

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetStatic(StaticKeyboardEffect effect);

        /// <summary>
        /// Sets a static color on the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetStaticAsync(StaticKeyboardEffect effect);

        /// <summary>
        /// Sets the specified Deathstalker zone to a color.
        /// </summary>
        /// <param name="zoneIndex">The index of the Deathstalker zone to set.</param>
        /// <param name="color">The color to set.</param>
        /// <param name="clear">Whether to clear all colors before setting the new one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetDeathstalkerZone(int zoneIndex, Color color, bool clear = false);

        /// <summary>
        /// Sets the specified Deathstalker zone to a color.
        /// </summary>
        /// <param name="zoneIndex">The index of the Deathstalker zone to set.</param>
        /// <param name="color">The color to set.</param>
        /// <param name="clear">Whether to clear all colors before setting the new one.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetDeathstalkerZoneAsync(int zoneIndex, Color color, bool clear = false);

        /// <summary>
        /// Sets a Deathstalker grid effect.
        /// </summary>
        /// <param name="effect">The Deathstalker grid effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Guid SetDeathstalker(DeathstalkerGridEffect effect);

        /// <summary>
        /// Sets a Deathstalker grid effect.
        /// </summary>
        /// <param name="effect">The Deathstalker grid effect to set.</param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        Task<Guid> SetDeathstalkerAsync(DeathstalkerGridEffect effect);
    }
}
