// ---------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="">
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
//     Disclaimer: Colore is in no way affiliated with Razer and/or any of its employees
//     and/or licensors. Adam Hellberg and Brandon Scott do not take responsibility
//     for any harm caused, direct or indirect, to any Razer peripherals
//     via the use of Colore.
// 
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Colore.Razer
{
    using System;
    using System.Runtime.InteropServices;

    // RZID is typedef'd as DWORD in the C headers
    // DWORD is 32-bit unsigned integer on Windows
    using RZID = System.UInt32;

    internal static class NativeMethods
    {
#if WIN64
        private const string DllName = "RzChromaSDK64.dll";
#elif WIN32
        private const string DllName = "RzChromaSDK.dll";
#else
#error Unsupported build configuration.
#endif

        private const CallingConvention FunctionConvention = CallingConvention.Cdecl;

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "Init", SetLastError = true)]
        internal static extern Result Init();

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "UnInit", SetLastError = true)]
        internal static extern Result UnInit();

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardEffect",
            SetLastError = true)]
        internal static extern Result CreateKeyboardEffect(Keyboard.Effects.Effect effect, IntPtr param, ref Guid effectid);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardCustomEffects",
            SetLastError = true)]
        internal static extern Result CreateKeyboardCustomEffects(
            Size size,
            [MarshalAs(UnmanagedType.LPArray)] Keyboard.Effects.Custom[] effectType,
            ref Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardCustomGridEffects",
            SetLastError = true)]
        internal static extern Result CreateKeyboardCustomGridEffects(
            Keyboard.Effects.CustomGrid effects,
            ref Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMouseEffect",
            SetLastError = true)]
        internal static extern Result CreateMouseEffect(RZID zone, Mouse.Effects.Effect effect, IntPtr param, ref Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMouseCustomEffects",
            SetLastError = true)]
        internal static extern Result CreateMouseCustomEffects(Mouse.Effects.Custom effect, ref Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "DeleteEffect", SetLastError = true)]
        internal static extern Result DeleteEffect(Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "SetEffect", SetLastError = true)]
        internal static extern Result SetEffect(Guid effectId);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "RegisterEventNotification",
            SetLastError = true)]
        internal static extern Result RegisterEventNotification(IntPtr hwnd);

        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "UnregisterEventNotification",
            SetLastError = true)]
        internal static extern Result UnregisterEventNotification();
    }
}
