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

namespace Corale.Colore.Native.Kernel32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Native methods from <c>kernel32</c> module.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Name of the DLL from which functions are imported.
        /// </summary>
        private const string DllName = "kernel32.dll";

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", ExactSpelling = true,
            SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr module, string procName);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "LoadLibrary", ExactSpelling = true,
            SetLastError = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string filename);

        [DllImport(DllName, CharSet = CharSet.Ansi, EntryPoint = "GetCurrentProcess", ExactSpelling = true,
            SetLastError = true)]
        internal static extern IntPtr GetCurrentProcess();

        [DllImport(DllName, CharSet = CharSet.Auto, EntryPoint = "GetModuleHandle", ExactSpelling = true,
            SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport(DllName, EntryPoint = "IsWow64Process", ExactSpelling = true, SetLastError = true,
            CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] [MarshalAs(UnmanagedType.Bool)] out bool wow64Process);
    }
}
