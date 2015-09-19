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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Corale.Colore
{
    /// <summary>
    /// Helper to get the architecture of the OS.
    /// Taken from here: http://stackoverflow.com/a/28866330/1104531
    /// </summary>
    public static class EnvironmentHelper
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

        public static bool Is64BitOperatingSystem()
        {
            // Check if this process is natively an x64 process. If it is, it will only run on x64 environments, thus, the environment must be x64.
            if (IntPtr.Size == 8)
                return true;
            // Check if this process is an x86 process running on an x64 environment.
            IntPtr moduleHandle = GetModuleHandle("kernel32");
            if (moduleHandle != IntPtr.Zero)
            {
                IntPtr processAddress = GetProcAddress(moduleHandle, "IsWow64Process");
                if (processAddress != IntPtr.Zero)
                {
                    bool result;
                    if (IsWow64Process(GetCurrentProcess(), out result) && result)
                        return true;
                }
            }
            // The environment must be an x86 environment.
            return false;
        }

    }
}
