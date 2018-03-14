// ---------------------------------------------------------------------------------------
// <copyright file="EnvironmentHelper.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Helpers
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Helper to get the architecture of the OS.
    /// </summary>
    internal static class EnvironmentHelper
    {
        /// <summary>
        /// Determines whether the system is 64-bit.
        /// </summary>
        /// <returns><c>true</c> if the system is 64-bit, otherwise <c>false</c>.</returns>
        internal static bool Is64Bit()
        {
            var cpuArch = RuntimeInformation.ProcessArchitecture;
            var osArch = RuntimeInformation.OSArchitecture;
            return cpuArch == Architecture.X64 && osArch == Architecture.X64;
        }
    }
}
