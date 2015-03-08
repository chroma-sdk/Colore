// ---------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Corale">
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

namespace Colore.Razer
{
    using System;
    using System.Runtime.InteropServices;

    using Colore.Core;

    // RZID is typedef'd as DWORD in the C headers
    // DWORD is 32-bit unsigned integer on Windows
    using RZID = System.UInt32;

    /// <summary>
    /// Native methods from Razer's Chroma SDK.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// DLL to import SDK functions from.
        /// </summary>
        /// <remarks>
        /// This field is set to <c>RzChromaSDK64.dll</c> when built
        /// for 64-bit platforms, and <c>RzChromaSDK.dll</c> when
        /// built for 32-bit platforms.
        /// </remarks>
#if WIN64
        private const string DllName = "RzChromaSDK64.dll";
#elif WIN32
        private const string DllName = "RzChromaSDK.dll";
#endif

        /// <summary>
        /// Calling convention for API functions.
        /// </summary>
        private const CallingConvention FunctionConvention = CallingConvention.Cdecl;

        /// <summary>
        /// Initialize Chroma SDK.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "Init", SetLastError = true)]
        internal static extern Result Init();

        /// <summary>
        /// Uninitialize Chroma SDK.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "UnInit", SetLastError = true)]
        internal static extern Result UnInit();

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type. These include <see cref="Keyboard.Effects.Effect.Wave" />,
        /// <see cref="Keyboard.Effects.Effect.SpectrumCycling" />, <see cref="Keyboard.Effects.Effect.Breathing" />,
        /// <see cref="Keyboard.Effects.Effect.Reactive" />, and <see cref="Keyboard.Effects.Effect.Static" />.
        /// </param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectid">Valid effect ID if successful. Use <see cref="Guid.Empty" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        /// <remarks>
        /// The <paramref name="param" /> parameter should point to the relevant struct for the passed in effect,
        /// according to the following list.
        /// <list type="table">
        /// <listheader>
        /// <term>Effect type</term>
        /// <term>Effect struct</term>
        /// </listheader>
        /// <item>
        /// <term><see cref="Keyboard.Effects.Effect.Wave" /></term>
        /// <term><see cref="Keyboard.Effects.Wave" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Keyboard.Effects.Effect.SpectrumCycling" /></term>
        /// <term><i>None</i> (<see cref="IntPtr.Zero" />)</term>
        /// </item>
        /// <item>
        /// <term><see cref="Keyboard.Effects.Effect.Breathing" /></term>
        /// <term><see cref="Keyboard.Effects.Breathing" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Keyboard.Effects.Effect.Reactive" /></term>
        /// <term><see cref="Keyboard.Effects.Reactive" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Keyboard.Effects.Effect.Static" /></term>
        /// <term><see cref="Keyboard.Effects.Static" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardEffect",
            SetLastError = true)]
        internal static extern Result CreateKeyboardEffect(
            Keyboard.Effects.Effect effect,
            IntPtr param,
            ref Guid effectid);

        /// <summary>
        /// Create keyboard custom effects.
        /// </summary>
        /// <param name="size">Number of effects.</param>
        /// <param name="effectType">An array of effects with maximum size of <see cref="Keyboard.Constants.MaxCustomEffects" />.</param>
        /// <param name="effectId">Set to valid effect ID if successful. Pass <c>null</c> (<see cref="Guid.Empty" />) if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        /// <remarks>To turn off all LEDs, call with <c>0</c>, <c>null</c>, and a <c>ref</c> to a variable with <see cref="Guid.Empty" />.</remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardCustomEffects",
            SetLastError = true)]
        internal static extern Result CreateKeyboardCustomEffects(
            Size size,
            [MarshalAs(UnmanagedType.LPArray)] Keyboard.Effects.Custom[] effectType,
            ref Guid effectId);

        /// <summary>
        /// Create keyboard custom grid effects.
        /// </summary>
        /// <param name="effects">Grid layout of the keyboard. Size is 6 rows by 22 columns.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <c>null</c> (<see cref="Guid.Empty" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        /// <remarks>To turn all LEDs off, call with empty <see cref="Keyboard.Effects.CustomGrid" /> and a <c>ref</c> to <see cref="Guid.Empty" />.</remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardCustomGridEffects",
            SetLastError = true)]
        internal static extern Result CreateKeyboardCustomGridEffects(
            Keyboard.Effects.CustomGrid effects,
            ref Guid effectId);

        /// <summary>
        /// Create mouse effect.
        /// </summary>
        /// <param name="zone">The zone, or in this case LED, in which the effect is going to be applied.</param>
        /// <param name="effect">
        /// Standard effect type. These include <see cref="Mouse.Effects.Effect.SpectrumCycling" />,
        /// <see cref="Mouse.Effects.Effect.Breathing" />, and <see cref="Mouse.Effects.Effect.Static" />.
        /// Depends on which LED.
        /// </param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Set to valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        /// <remarks>
        /// The <paramref name="param" /> parameter should point to the relevant struct for the passed in effect,
        /// according to the following list.
        /// <list type="table">
        /// <listheader>
        /// <term>Effect type</term>
        /// <term>Effect struct</term>
        /// </listheader>
        /// <item>
        /// <term><see cref="Mouse.Effects.Effect.SpectrumCycling" /></term>
        /// <term><i>None</i> (<see cref="IntPtr.Zero" />)</term>
        /// </item>
        /// <item>
        /// <term><see cref="Mouse.Effects.Effect.Breathing" /></term>
        /// <term><see cref="Mouse.Effects.Breathing" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Mouse.Effects.Effect.Static" /></term>
        /// <term><see cref="Mouse.Effects.Static" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Mouse.Effects.Effect.Custom" /></term>
        /// <term><see cref="Mouse.Effects.Custom" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMouseEffect",
            SetLastError = true)]
        internal static extern Result CreateMouseEffect(
            RZID zone,
            Mouse.Effects.Effect effect,
            IntPtr param,
            ref Guid effectId);

        /// <summary>
        /// Create mouse custom effect.
        /// </summary>
        /// <param name="effect">The custom mouse effect which includes LED and color.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMouseCustomEffects",
            SetLastError = true)]
        internal static extern Result CreateMouseCustomEffects(Mouse.Effects.Custom effect, ref Guid effectId);

        /// <summary>
        /// Delete effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be deleted.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "DeleteEffect", SetLastError = true)]
        internal static extern Result DeleteEffect(Guid effectId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be set.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "SetEffect", SetLastError = true)]
        internal static extern Result SetEffect(Guid effectId);

        /// <summary>
        /// Register for event notification.
        /// </summary>
        /// <param name="hwnd">Application window handle.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        /// <remarks>
        /// <strong>Messages can only be handled in a Windows message pumping thread.</strong>
        /// <see cref="Constants.WmChromaEvent" /> will be sent if there is an event.
        /// Possible combination of <c>wParam</c> and <c>lParam</c> values are explained in the below table.
        /// <list type="table">
        /// <listheader>
        /// <term><c>wParam</c> value</term>
        /// <term><c>lParam</c>l value</term>
        /// <term>Meaning</term>
        /// </listheader>
        /// <item>
        /// <term>1</term>
        /// <term>1</term>
        /// <term>Chroma SDK support enabled</term>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <term>0</term>
        /// <term>Chroma SDK support disabled</term>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <term>1</term>
        /// <term>Access to device granted</term>
        /// </item>
        /// <item>
        /// <term>2</term>
        /// <term>0</term>
        /// <term>Access to device revoked</term>
        /// </item>
        /// <item>
        /// <term>3</term>
        /// <term>1</term>
        /// <term>Application state enabled</term>
        /// </item>
        /// <item>
        /// <term>3</term>
        /// <term>0</term>
        /// <item>Application state disabled</item>
        /// </item>
        /// </list>
        /// </remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "RegisterEventNotification",
            SetLastError = true)]
        internal static extern Result RegisterEventNotification(IntPtr hwnd);

        /// <summary>
        /// Unregister for event notification.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "UnregisterEventNotification",
            SetLastError = true)]
        internal static extern Result UnregisterEventNotification();
    }
}
