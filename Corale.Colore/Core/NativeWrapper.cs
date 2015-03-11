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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
// 
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

using Corale.Colore.Razer;
using Corale.Colore.Razer.Keyboard.Effects;
using Corale.Colore.Razer.Mouse;

namespace Corale.Colore.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    using global::Corale.Colore.Razer;
    using global::Corale.Colore.Razer.Keyboard.Effects;
    using global::Corale.Colore.Razer.Mouse;

    /// <summary>
    /// Helper class to more easily make calls to native Chroma SDK functions.
    /// </summary>
    internal static class NativeWrapper
    {
        /// <summary>
        /// Create keyboard custom effects.
        /// </summary>
        /// <param name="effects">An array of effects with maximum size of <see cref="Razer.Keyboard.Constants.MaxCustomEffects" />.</param>
        /// <returns>A <see cref="Guid" /> for the created effects.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateKeyboardCustomEffects" />
        internal static Guid CreateKeyboardCustomEffects(Custom[] effects)
        {
            Size size = (uint)effects.Length;
            var guid = Guid.Empty;
            var result = NativeMethods.CreateKeyboardCustomEffects(size, effects, ref guid);
            if (!result)
                throw new NativeCallException("CreateKeyboardCustomEffects", result);
            return guid;
        }

        /// <summary>
        /// Create keyboard custom effects.
        /// </summary>
        /// <param name="effects">An enumerable list of effects with maximum size of <see cref="Razer.Keyboard.Constants.MaxCustomEffects" />.</param>
        /// <returns>A <see cref="Guid" /> for the created effects.</returns>
        internal static Guid CreateKeyboardCustomEffects(IEnumerable<Custom> effects)
        {
            return CreateKeyboardCustomEffects(effects.ToArray());
        }

        /// <summary>
        /// Create keyboard custom grid effects.
        /// </summary>
        /// <param name="effects">Grid layout of the keyboard. Size is 6 rows by 22 columns.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateKeyboardCustomGridEffects" />
        internal static Guid CreateKeyboardCustomGridEffects(CustomGrid effects)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateKeyboardCustomGridEffects(effects, ref guid);
            if (!result)
                throw new NativeCallException("CreateKeyboardCustomGridEffects", result);
            return guid;
        }

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateKeyboardEffect" />
        internal static Guid CreateKeyboardEffect(Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateKeyboardEffect(effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateKeyboardEffect", result);
            return guid;
        }

        /// <summary>
        /// Create a keyboard effect without a parameter.
        /// </summary>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <remarks>This is currently only valid for <see cref="Razer.Keyboard.Effects.Effect.SpectrumCycling" />.</remarks>
        internal static Guid CreateKeyboardEffect(Effect effect)
        {
            return CreateKeyboardEffect(effect, IntPtr.Zero);
        }

        /// <summary>
        /// Creates a breathing effect for the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateKeyboardEffect(Breathing effect)
        {
            return CreateKeyboardEffect(Effect.Breathing, effect);
        }

        /// <summary>
        /// Creates a reactive effect for the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateKeyboardEffect(Reactive effect)
        {
            return CreateKeyboardEffect(Effect.Reactive, effect);
        }

        /// <summary>
        /// Creates a static color effect for the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateKeyboardEffect(Static effect)
        {
            return CreateKeyboardEffect(Effect.Static, effect);
        }

        /// <summary>
        /// Creates a wave effect for the keyboard.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateKeyboardEffect(Wave effect)
        {
            return CreateKeyboardEffect(Effect.Wave, effect);
        }

        /// <summary>
        /// Creates a custom effect for the mouse.
        /// </summary>
        /// <param name="effect">Custom effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateMouseCustomEffects" />
        internal static Guid CreateMouseCustomEffects(Razer.Mouse.Effects.Custom effect)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateMouseCustomEffects(effect, ref guid);
            if (!result)
                throw new NativeCallException("CreateMouseCustomEffects", result);
            return guid;
        }

        /// <summary>
        /// Creates a standard mouse effect with the specified parameters.
        /// </summary>
        /// <param name="led">The LED to use.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="param">Context-sensitive effect parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <seealso cref="Razer.NativeMethods.CreateMouseEffect" />
        internal static Guid CreateMouseEffect(Led led, Razer.Mouse.Effects.Effect effect, IntPtr param)
        {
            var guid = Guid.Empty;
            var result = NativeMethods.CreateMouseEffect((uint)led, effect, param, ref guid);
            if (!result)
                throw new NativeCallException("CreateMouseEffect", result);
            return guid;
        }

        /// <summary>
        /// Creates a mouse effect without parameter.
        /// </summary>
        /// <param name="led">The LED to use.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        /// <remarks>This is currently only valid for <see cref="Razer.Mouse.Effects.Effect.SpectrumCycling" />.</remarks>
        internal static Guid CreateMouseEffect(Led led, Razer.Mouse.Effects.Effect effect)
        {
            return CreateMouseEffect(led, effect, IntPtr.Zero);
        }

        /// <summary>
        /// Creates a breathing effect for the mouse.
        /// </summary>
        /// <param name="led">The LED to use.</param>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateMouseEffect(Led led, Razer.Mouse.Effects.Breathing effect)
        {
            return CreateMouseEffect(led, Razer.Mouse.Effects.Effect.Breathing, effect);
        }

        /// <summary>
        /// Creates a static color effect for the mouse.
        /// </summary>
        /// <param name="led">The LED to use.</param>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateMouseEffect(Led led, Razer.Mouse.Effects.Static effect)
        {
            return CreateMouseEffect(led, Razer.Mouse.Effects.Effect.Static, effect);
        }

        /// <summary>
        /// Creates a custom mouse effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        internal static Guid CreateMouseEffect(Razer.Mouse.Effects.Custom effect)
        {
            return CreateMouseEffect(effect.Led, Razer.Mouse.Effects.Effect.Custom, effect);
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
            if (!result)
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
        /// Helper method for creating keyboard effects with relevant structure parameter.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private static Guid CreateKeyboardEffect<T>(Effect effect, T @struct) where T : struct
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
        /// <param name="led">The LED to use.</param>
        /// <param name="effect">The type of effect to create.</param>
        /// <param name="struct">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        private static Guid CreateMouseEffect<T>(Led led, Razer.Mouse.Effects.Effect effect, T @struct) where T : struct
        {
            var ptr = Marshal.AllocHGlobal(Marshal.SizeOf(@struct));
            Marshal.StructureToPtr(@struct, ptr, false);
            try
            {
                return CreateMouseEffect(led, effect, ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
