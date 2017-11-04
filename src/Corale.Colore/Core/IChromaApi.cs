// ---------------------------------------------------------------------------------------
// <copyright file="IChromaApi.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Core
{
    using System;
    using System.Threading.Tasks;

    using Corale.Colore.Razer;

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
        Task InitializeAsync();

        /// <summary>
        /// Uninitializes the Chroma SDK.
        /// </summary>
        Task UninitializeAsync();

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <returns>
        /// A populated <see cref="DeviceInfo" /> structure with information about the requested device.
        /// </returns>
        Task<DeviceInfo> QueryDeviceAsync(Guid deviceId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">Effect ID to set.</param>
        Task SetEffectAsync(Guid effectId);

        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" />.
        /// </summary>
        /// <param name="effectId">Effect ID to delete.</param>
        Task DeleteEffectAsync(Guid effectId);

        /// <summary>
        /// Creates a new device effect without any effect data.
        /// </summary>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateDeviceEffectAsync(Guid deviceId, Effect effect);

        /// <summary>
        /// Creates a new device effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateDeviceEffectAsync<T>(Guid deviceId, Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new keyboard effect without any effect data.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeyboardEffectAsync(Razer.Keyboard.Effects.Effect effect);

        /// <summary>
        /// Creates a new keyboard effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeyboardEffectAsync<T>(Razer.Keyboard.Effects.Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new mouse effect without any effect data.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMouseEffectAsync(Razer.Mouse.Effects.Effect effect);

        /// <summary>
        /// Creates a new mouse effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMouseEffectAsync<T>(Razer.Mouse.Effects.Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new headset effect without any effect data.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateHeadsetEffectAsync(Razer.Headset.Effects.Effect effect);

        /// <summary>
        /// Creates a new headset effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateHeadsetEffectAsync<T>(Razer.Headset.Effects.Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new mousepad effect without any effect data.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMousepadEffectAsync(Razer.Mousepad.Effects.Effect effect);

        /// <summary>
        /// Creates a new mousepad effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateMousepadEffectAsync<T>(Razer.Mousepad.Effects.Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new keypad effect without any effect data.
        /// </summary>
        /// <param name="effect">THe type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeypadEffectAsync(Razer.Keypad.Effects.Effect effect);

        /// <summary>
        /// Creates a new keypad effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateKeypadEffectAsync<T>(Razer.Keypad.Effects.Effect effect, T @struct) where T : struct;

        /// <summary>
        /// Creates a new Chroma Link effect without any effect data.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateChromaLinkEffectAsync(Razer.ChromaLink.Effects.Effect effect);

        /// <summary>
        /// Creates a new Chroma Link effect with the specified effect data.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        Task<Guid> CreateChromaLinkEffectAsync<T>(Razer.ChromaLink.Effects.Effect effect, T @struct) where T : struct;

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
