// ---------------------------------------------------------------------------------------
// <copyright file="IChromaApi.cs" company="Corale">
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

namespace Colore.Api
{
    using System;
    using System.Threading.Tasks;

    using Colore.Data;
    using Colore.Effects.Generic;

    using JetBrains.Annotations;

    /// <summary>
    /// Chroma API contract.
    /// </summary>
    [PublicAPI]
    public interface IChromaApi
    {
        /// <summary>
        /// Initializes the Chroma SDK.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        void Initialize(AppInfo? info);

        /// <summary>
        /// Initializes the Chroma SDK.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task InitializeAsync(AppInfo? info);

        /// <summary>
        /// Uninitializes the Chroma SDK.
        /// </summary>
        void Uninitialize();

        /// <summary>
        /// Uninitializes the Chroma SDK.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UninitializeAsync();

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <returns>
        /// A populated <see cref="SdkDeviceInfo" /> structure with information about the requested device.
        /// </returns>
        Task<SdkDeviceInfo> QueryDeviceAsync(Guid deviceId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">Effect ID to set.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SetEffectAsync(Guid effectId);

        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" />.
        /// </summary>
        /// <param name="effectId">Effect ID to delete.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteEffectAsync(Guid effectId);

        /// <summary>
        /// Creates a new device effect without any effect data.
        /// </summary>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateDeviceEffectAsync(Guid deviceId, EffectType effectType);

        /// <summary>
        /// Creates a new device effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateDeviceEffectAsync<T>(Guid deviceId, EffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new keyboard effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeyboardEffectAsync(Effects.Keyboard.KeyboardEffectType effectType);

        /// <summary>
        /// Creates a new keyboard effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeyboardEffectAsync<T>(Effects.Keyboard.KeyboardEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new mouse effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMouseEffectAsync(Effects.Mouse.MouseEffectType effectType);

        /// <summary>
        /// Creates a new mouse effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMouseEffectAsync<T>(Effects.Mouse.MouseEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new headset effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateHeadsetEffectAsync(Effects.Headset.HeadsetEffectType effectType);

        /// <summary>
        /// Creates a new headset effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateHeadsetEffectAsync<T>(Effects.Headset.HeadsetEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new mousepad effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMousepadEffectAsync(Effects.Mousepad.MousepadEffectType effectType);

        /// <summary>
        /// Creates a new mousepad effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMousepadEffectAsync<T>(Effects.Mousepad.MousepadEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new keypad effect without any effect data.
        /// </summary>
        /// <param name="effectType">THe type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeypadEffectAsync(Effects.Keypad.KeypadEffectType effectType);

        /// <summary>
        /// Creates a new keypad effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeypadEffectAsync<T>(Effects.Keypad.KeypadEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Creates a new Chroma Link effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateChromaLinkEffectAsync(Effects.ChromaLink.ChromaLinkEffectType effectType);

        /// <summary>
        /// Creates a new Chroma Link effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateChromaLinkEffectAsync<T>(Effects.ChromaLink.ChromaLinkEffectType effectType, T data) where T : struct;

        /// <summary>
        /// Registers for Chroma SDK notifications.
        /// </summary>
        /// <param name="windowHandle">App handle for the window handling events.</param>
        void RegisterEventNotifications(IntPtr windowHandle);

        /// <summary>
        /// Unregisters from receiving Chroma SDK notifications.
        /// </summary>
        void UnregisterEventNotifications();
    }
}
