// ---------------------------------------------------------------------------------------
// <copyright file="EnvironmentHelper.cs" company="Corale">
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

namespace Corale.Colore
{
    using System;
    using System.Security;

    using Corale.Colore.Native.Kernel32;

    /// <summary>
    /// Helper to get the architecture of the OS.
    /// Taken from here: http://stackoverflow.com/a/28866330/1104531
    /// </summary>
    internal static class EnvironmentHelper
    {
        /// <summary>
        /// Determines whether the current system is 64-bit.
        /// </summary>
        /// <returns><c>true</c> if the system is 64-bit.</returns>
        internal static bool Is64BitOperatingSystem()
        {
            // Check if this process is natively an x64 process. If it is, it will only run on x64 environments, thus, the environment must be x64.
            if (IntPtr.Size == 8)
                return true;

            // Check if this process is an x86 process running on an x64 environment.
            var moduleHandle = NativeMethods.GetModuleHandle("kernel32");
            if (moduleHandle == IntPtr.Zero)
                return false;

            var processAddress = NativeMethods.GetProcAddress(moduleHandle, "IsWow64Process");
            if (processAddress == IntPtr.Zero)
                return false;

            bool result;
            return NativeMethods.IsWow64Process(NativeMethods.GetCurrentProcess(), out result) && result;
        }

        /// <summary>
        /// Determines whether this process is a 64-bit process.
        /// </summary>
        /// <returns><c>true</c> if this process is 64-bit, otherwise <c>false</c>.</returns>
        internal static bool Is64BitProcess()
        {
            return IntPtr.Size == 8;
        }
    }
}
