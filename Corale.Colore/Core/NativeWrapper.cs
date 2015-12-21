// ---------------------------------------------------------------------------------------
// <copyright file="NativeWrapper.cs" company="Corale">
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
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    using Corale.Colore.Razer;

    using log4net;

    /// <summary>
    /// Helper class to more easily make calls to native Chroma SDK functions.
    /// </summary>
    internal static class NativeWrapper
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(NativeWrapper));

        /// <summary>
        /// Creates an effect for a device.
        /// </summary>
        /// <param name="device">The device to create the effect on.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateEffect" />
        internal static Guid CreateEffect(Guid device, Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateEffect(device, effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateEffect", result);
            return guid;
        }

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateKeyboardEffect" />
        internal static Guid CreateKeyboardEffect(Razer.Keyboard.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateKeyboardEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateKeyboardEffect", result);
            return guid;
        }

        /// <summary>
        /// Creates a standard mouse effect with the specified parameters.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateMouseEffect" />
        internal static Guid CreateMouseEffect(Razer.Mouse.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateMouseEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateMouseEffect", result);
            return guid;
        }

        /// <summary>
        /// Creates a standard headset effect with the specified parameters.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateHeadsetEffect(Razer.Headset.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateHeadsetEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateHeadsetEffect", result);
            return guid;
        }

        /// <summary>
        /// Creates a standard mouse pad effect with the specified parameters.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateMousepadEffect(Razer.Mousepad.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateMousepadEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateMousepadEffect", result);
            return guid;
        }

        /// <summary>
        /// Creates a standard keypad effect with the specified parameters.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Effect-specific parameters.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateKeypadEffect(Razer.Keypad.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateKeypadEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateKeypadEffect", result);
            return guid;
        }

        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" />.
        /// </summary>
        /// <param name="guid">Effect ID to delete.</param>
        internal static void DeleteEffect(Guid guid)
        {
            var result = NativeMethods.DeleteEffect(guid);
            if (!result)
                throw new NativeCallException("DeleteEffect", result);
        }

        /// <summary>
        /// Initializes the Chroma SDK.
        /// </summary>
        internal static void Init()
        {
            var result = NativeMethods.Init();
            if (!result)
                throw new NativeCallException("Init", result);
        }

        /// <summary>
        /// Registers for Chroma SDK notifications.
        /// </summary>
        /// <param name="hwnd">App handle for the window handling events.</param>
        internal static void RegisterEventNotification(IntPtr hwnd)
        {
            var result = NativeMethods.RegisterEventNotification(hwnd);
            if (!result)
                throw new NativeCallException("RegisterEventNotification", result);
        }

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="guid">Effect ID to set.</param>
        internal static void SetEffect(Guid guid)
        {
            var result = NativeMethods.SetEffect(guid);
            if (result)
                return;

            if (result == Result.RzResourceDisabled || result == Result.RzAccessDenied)
                Log.WarnFormat("Ambiguous {0} error thrown from call to native function SetEffect.", result);
            else
                throw new NativeCallException("SetEffect", result);
        }

        /// <summary>
        /// Uninitializes the Chroma SDK.
        /// </summary>
        internal static void UnInit()
        {
            var result = NativeMethods.UnInit();
            if (!result)
                throw new NativeCallException("UnInit", result);
        }

        /// <summary>
        /// Unregisters from receiving Chroma SDK notifications.
        /// </summary>
        internal static void UnregisterEventNotification()
        {
            var result = NativeMethods.UnregisterEventNotification();
            if (!result)
                throw new NativeCallException("UnregisterEventNotification", result);
        }

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="id">Device ID, found in <see cref="Devices" />.</param>
        /// <returns>A populated <see cref="DeviceInfo" /> structure with information about the requested device.</returns>
        [SecuritySafeCritical]
        internal static DeviceInfo QueryDevice(Guid id)
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DeviceInfo)));

            try
            {
                var result = NativeMethods.QueryDevice(id, ptr);

                if (!result)
                    throw new NativeCallException("QueryDevice", result);

                if (ptr == IntPtr.Zero)
                    throw new ColoreException("Device query failed, ptr NULL.");

                var info = (DeviceInfo)Marshal.PtrToStructure(ptr, typeof(DeviceInfo));

                return info;
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Helper method for creating keyboard effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        [SecuritySafeCritical]
        internal static Guid CreateKeyboardEffect<T>(Razer.Keyboard.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);
            try
            {
                return CreateKeyboardEffect(effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Helper method for creating mouse effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        [SecuritySafeCritical]
        internal static Guid CreateMouseEffect<T>(Razer.Mouse.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);
            try
            {
                return CreateMouseEffect(effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Helper method for creating headset effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        [SecuritySafeCritical]
        internal static Guid CreateHeadsetEffect<T>(Razer.Headset.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);

            try
            {
                return CreateHeadsetEffect(effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Helper method for creating mouse pad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        [SecuritySafeCritical]
        internal static Guid CreateMousepadEffect<T>(Razer.Mousepad.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);

            try
            {
                return CreateMousepadEffect(effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// Helper method for creating keypad effects with parameter struct.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        [SecuritySafeCritical]
        internal static Guid CreateKeypadEffect<T>(Razer.Keypad.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);

            try
            {
                return CreateKeypadEffect(effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
