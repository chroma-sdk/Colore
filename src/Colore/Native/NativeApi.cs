// ---------------------------------------------------------------------------------------
// <copyright file="NativeApi.cs" company="Corale">
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

namespace Colore.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.ChromaLink;
    using Colore.Effects.Generic;
    using Colore.Effects.Headset;
    using Colore.Effects.Keyboard;
    using Colore.Effects.Keypad;
    using Colore.Effects.Mouse;
    using Colore.Effects.Mousepad;
    using Colore.Helpers;
    using Colore.Logging;

    /// <inheritdoc />
    /// <summary>
    /// Helper class to more easily make calls to native Chroma SDK functions.
    /// </summary>
    public class NativeApi : IChromaApi
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NativeApi>();

        /// <summary>
        /// A reference to an instance of <see cref="NativeSdkMethods" /> providing access to native Chroma SDK functions.
        /// </summary>
        private readonly INativeSdkMethods _nativeSdkMethods;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeApi" /> class.
        /// </summary>
        /// <param name="nativeSdkMethods">
        /// The instance of <see cref="NativeSdkMethods" /> to use
        /// for accessing the Chroma SDK functions.
        /// If <c>null</c>, a default implementation will be used.
        /// </param>
        public NativeApi(INativeSdkMethods? nativeSdkMethods = null) =>
            _nativeSdkMethods = nativeSdkMethods ?? new NativeSdkMethods();

        /// <inheritdoc />
        /// <param name="info">Information about the application, currently unused for the native SDK.</param>
        public void Initialize(AppInfo? info)
        {
            var result = _nativeSdkMethods.Init();
            if (!result)
            {
                throw new NativeCallException(nameof(_nativeSdkMethods.Init), result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes the Chroma SDK.
        /// </summary>
        /// <param name="info">Information about the application, currently unused for the native SDK.</param>
        public Task InitializeAsync(AppInfo? info)
        {
            Initialize(info);

            return TaskHelper.CompletedTask;
        }

        /// <inheritdoc />
        public void Uninitialize()
        {
            var result = _nativeSdkMethods.UnInit();
            if (!result)
            {
                throw new NativeCallException(nameof(_nativeSdkMethods.UnInit), result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Uninitializes the Chroma SDK.
        /// </summary>
        public Task UninitializeAsync()
        {
            Uninitialize();

            return TaskHelper.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <returns>
        /// A populated <see cref="SdkDeviceInfo" /> structure with information about the requested device.
        /// </returns>
        public SdkDeviceInfo QueryDevice(Guid deviceId)
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf<SdkDeviceInfo>());

            try
            {
                var result = _nativeSdkMethods.QueryDevice(deviceId, ptr);

                if (!result)
                {
                    if (result == Result.DeviceNotConnected)
                    {
                        return new SdkDeviceInfo(DeviceType.Unknown, 0);
                    }

                    throw new NativeCallException(nameof(_nativeSdkMethods.QueryDevice), result);
                }

                if (ptr == IntPtr.Zero)
                {
                    throw new ColoreException("Device query failed, ptr NULL.");
                }

                var info = Marshal.PtrToStructure<SdkDeviceInfo>(ptr);

                return info;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <returns>A populated <see cref="SdkDeviceInfo" /> structure with information about the requested device.</returns>
        public Task<SdkDeviceInfo> QueryDeviceAsync(Guid deviceId) => Task.FromResult(QueryDevice(deviceId));

        /// <inheritdoc />
        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">Effect ID to set.</param>
        public void SetEffect(Guid effectId)
        {
            var result = _nativeSdkMethods.SetEffect(effectId);
            if (result)
            {
                return;
            }

            if (result == Result.RzResourceDisabled || result == Result.RzAccessDenied)
            {
                Log.WarnFormat("Ambiguous {0} error thrown from call to native function SetEffect.", result);
            }
            else
            {
                throw new NativeCallException("SetEffect", result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">Effect ID to set.</param>
        public Task SetEffectAsync(Guid effectId)
        {
            SetEffect(effectId);

            return TaskHelper.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" />.
        /// </summary>
        /// <param name="effectId">Effect ID to delete.</param>
        public void DeleteEffect(Guid effectId)
        {
            var result = _nativeSdkMethods.DeleteEffect(effectId);
            if (!result)
            {
                throw new NativeCallException(nameof(_nativeSdkMethods.DeleteEffect), result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" />.
        /// </summary>
        /// <param name="effectId">Effect ID to delete.</param>
        public Task DeleteEffectAsync(Guid effectId)
        {
            DeleteEffect(effectId);

            return TaskHelper.CompletedTask;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new device effect without any effect data.
        /// </summary>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateDeviceEffect(Guid deviceId, EffectType effectType) =>
            CreateEffect(deviceId, effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating device effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateDeviceEffect<T>(Guid deviceId, EffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);
            try
            {
                return CreateEffect(deviceId, effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new device effect without any effect data.
        /// </summary>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateDeviceEffectAsync(Guid deviceId, EffectType effectType) =>
            Task.FromResult(CreateDeviceEffect(deviceId, effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating device effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="deviceId">The ID of the device to create the effect for.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateDeviceEffectAsync<T>(Guid deviceId, EffectType effectType, T data) where T : struct =>
            Task.FromResult(CreateDeviceEffect(deviceId, effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keyboard effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateKeyboardEffect(KeyboardEffectType effectType) =>
            CreateKeyboardEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating keyboard effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateKeyboardEffect<T>(KeyboardEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);
            try
            {
                return CreateKeyboardEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keyboard effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateKeyboardEffectAsync(Effects.Keyboard.KeyboardEffectType effectType) =>
            Task.FromResult(CreateKeyboardEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating keyboard effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateKeyboardEffectAsync<T>(Effects.Keyboard.KeyboardEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateKeyboardEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mouse effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateMouseEffect(MouseEffectType effectType) => CreateMouseEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating mouse effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateMouseEffect<T>(MouseEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);
            try
            {
                return CreateMouseEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mouse effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateMouseEffectAsync(Effects.Mouse.MouseEffectType effectType) =>
            Task.FromResult(CreateMouseEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating mouse effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateMouseEffectAsync<T>(Effects.Mouse.MouseEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateMouseEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new headset effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateHeadsetEffect(HeadsetEffectType effectType) => CreateHeadsetEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating headset effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateHeadsetEffect<T>(HeadsetEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);

            try
            {
                return CreateHeadsetEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new headset effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateHeadsetEffectAsync(Effects.Headset.HeadsetEffectType effectType) =>
            Task.FromResult(CreateHeadsetEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating headset effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateHeadsetEffectAsync<T>(Effects.Headset.HeadsetEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateHeadsetEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mousepad effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateMousepadEffect(MousepadEffectType effectType) =>
            CreateMousepadEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating mouse pad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateMousepadEffect<T>(MousepadEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);

            try
            {
                return CreateMousepadEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mousepad effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateMousepadEffectAsync(Effects.Mousepad.MousepadEffectType effectType) =>
            Task.FromResult(CreateMousepadEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating mouse pad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateMousepadEffectAsync<T>(Effects.Mousepad.MousepadEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateMousepadEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keypad effect without any effect data.
        /// </summary>
        /// <param name="effectType">THe type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateKeypadEffect(KeypadEffectType effectType) => CreateKeypadEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating keypad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateKeypadEffect<T>(KeypadEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);

            try
            {
                return CreateKeypadEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keypad effect without any effect data.
        /// </summary>
        /// <param name="effectType">THe type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateKeypadEffectAsync(Effects.Keypad.KeypadEffectType effectType) =>
            Task.FromResult(CreateKeypadEffect(effectType, IntPtr.Zero));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating keypad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateKeypadEffectAsync<T>(Effects.Keypad.KeypadEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateKeypadEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Creates a new Chroma Link effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateChromaLinkEffect(ChromaLinkEffectType effectType) =>
            CreateChromaLinkEffect(effectType, IntPtr.Zero);

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating Chroma Link effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Guid CreateChromaLinkEffect<T>(ChromaLinkEffectType effectType, T data) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
            Marshal.StructureToPtr(data, ptr, false);

            try
            {
                return CreateChromaLinkEffect(effectType, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new Chroma Link effect without any effect data.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateChromaLinkEffectAsync(Effects.ChromaLink.ChromaLinkEffectType effectType) =>
            Task.FromResult(CreateChromaLinkEffect(effectType));

        /// <inheritdoc />
        /// <summary>
        /// Helper method for creating Chroma Link effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public Task<Guid> CreateChromaLinkEffectAsync<T>(Effects.ChromaLink.ChromaLinkEffectType effectType, T data)
            where T : struct =>
            Task.FromResult(CreateChromaLinkEffect(effectType, data));

        /// <inheritdoc />
        /// <summary>
        /// Registers for Chroma SDK notifications.
        /// </summary>
        /// <param name="hwnd">App handle for the window handling events.</param>
        public void RegisterEventNotifications(IntPtr hwnd)
        {
            var result = _nativeSdkMethods.RegisterEventNotification(hwnd);
            if (!result)
            {
                throw new NativeCallException("RegisterEventNotification", result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Unregisters from receiving Chroma SDK notifications.
        /// </summary>
        public void UnregisterEventNotifications()
        {
            var result = _nativeSdkMethods.UnregisterEventNotification();
            if (!result)
            {
                throw new NativeCallException("UnregisterEventNotification", result);
            }
        }

        /// <summary>
        /// Creates an effect for a device.
        /// </summary>
        /// <param name="device">The device to create the effect on.</param>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="NativeSdkMethods.CreateEffect" />
        private Guid CreateEffect(Guid device, EffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateEffect(device, effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="NativeSdkMethods.CreateKeyboardEffect" />
        private Guid CreateKeyboardEffect(Effects.Keyboard.KeyboardEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateKeyboardEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateKeyboardEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Creates a standard mouse effect with the specified parameters.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="NativeSdkMethods.CreateMouseEffect" />
        private Guid CreateMouseEffect(Effects.Mouse.MouseEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateMouseEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateMouseEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Creates a standard headset effect with the specified parameters.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private Guid CreateHeadsetEffect(Effects.Headset.HeadsetEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateHeadsetEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateHeadsetEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Creates a standard mouse pad effect with the specified parameters.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private Guid CreateMousepadEffect(Effects.Mousepad.MousepadEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateMousepadEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateMousepadEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Creates a standard keypad effect with the specified parameters.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameters.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private Guid CreateKeypadEffect(Effects.Keypad.KeypadEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateKeypadEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateKeypadEffect", result);
            }

            return guid;
        }

        /// <summary>
        /// Creates a Chroma Link effect with the specified parameters.
        /// </summary>
        /// <param name="effectType">The type of Chroma Link effect to create.</param>
        /// <param name="param">Effect-specific parameters.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private Guid CreateChromaLinkEffect(Effects.ChromaLink.ChromaLinkEffectType effectType, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = _nativeSdkMethods.CreateChromaLinkEffect(effectType, param, ref guid);

            if (!result)
            {
                throw new NativeCallException("CreateChromaLinkEffect", result);
            }

            return guid;
        }
    }
}
