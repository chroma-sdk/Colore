// <copyright file="RegistryHelper.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System;
    using System.Security;

    using log4net;

    using Microsoft.Win32;

    /// <summary>
    /// Contains helper methods for accessing the registry.
    /// </summary>
    internal static class RegistryHelper
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(RegistryHelper));

        /// <summary>
        /// Attempts to extract the SDK version from the registry.
        /// </summary>
        /// <param name="ver">The SDK version will be saved in this variable.</param>
        /// <returns>
        /// <c>true</c> if the version was retrieved successfully and stored in <paramref name="ver" />,
        /// otherwise <c>false</c> with <c>(0, 0, 0)</c> stored in <paramref name="ver" />.
        /// </returns>
        [SecurityCritical]
        internal static bool TryGetSdkVersion(out SdkVersion ver)
        {
#if ANYCPU
#pragma warning disable SA1312 // Variable names must begin with lower-case letter
            // ReSharper disable once InconsistentNaming
            var RegKey = @"SOFTWARE\Razer Chroma SDK";
#pragma warning restore SA1312 // Variable names must begin with lower-case letter

            if (EnvironmentHelper.Is64BitProcess() && EnvironmentHelper.Is64BitOperatingSystem())
                RegKey = @"SOFTWARE\WOW6432Node\Razer Chroma SDK";
#elif WIN64
            const string RegKey = @"SOFTWARE\WOW6432Node\Razer Chroma SDK";
#else
            const string RegKey = @"SOFTWARE\Razer Chroma SDK";
#endif

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(RegKey, false))
                {
                    if (key == null)
                    {
                        Log.Warn("Failed to open registry key to read SDK version.");
                        ver = new SdkVersion(0, 0, 0);
                        return false;
                    }

                    var major = key.GetValue("MajorVersion");
                    var minor = key.GetValue("MinorVersion");
                    var revision = key.GetValue("RevisionNumber");

                    if (major is int && minor is int && revision is int)
                    {
                        ver = new SdkVersion((int)major, (int)minor, (int)revision);
                        return true;
                    }

                    Log.WarnFormat(
                        "Unknown version component types, please tell a developer: Ma is {0}, Mi is {1}, R is {2}.",
                        major.GetType(),
                        minor.GetType(),
                        revision.GetType());

                    ver = new SdkVersion(0, 0, 0);
                    return false;
                }
            }
            catch (SecurityException ex)
            {
                Log.Warn("System raised SecurityException during read of SDK version in registry.", ex);
                ver = new SdkVersion(0, 0, 0);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Warn("Not authorized to read registry for SDK version.", ex);
                ver = new SdkVersion(0, 0, 0);
                return false;
            }
        }
    }
}
