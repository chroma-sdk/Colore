// ---------------------------------------------------------------------------------------
// <copyright file="RegistryHelper.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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
    using System.Security;

    using Common.Logging;

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
        /// The path to the Razer Chroma SDK registry key.
        /// </summary>
        private static readonly string SdkRegKeyPath = GetSdkRegKeyPath();

        /// <summary>
        /// Checks if the Chroma SDK is available on this system.
        /// </summary>
        /// <returns><c>true</c> if Chroma SDK is available, otherwise <c>false</c>.</returns>
        internal static bool IsSdkAvailable()
        {
            bool dllValid;

            if (EnvironmentHelper.Is64Bit())
                dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK64.dll") != IntPtr.Zero;
            else
                dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK.dll") != IntPtr.Zero;

            bool regEnabled;

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(SdkRegKeyPath))
                {
                    if (key != null)
                    {
                        var value = key.GetValue("Enable");

                        if (value is int)
                            regEnabled = (int)value == 1;
                        else
                        {
                            regEnabled = true;
                            Log.Warn(
                                "Chroma SDK has changed registry setting format. Please update Colore to latest version.");
                            Log.DebugFormat("New Enabled type: {0}", value.GetType());
                        }
                    }
                    else
                        regEnabled = false;
                }
            }
            catch (SecurityException ex)
            {
                // If we can't access the registry, best to just assume
                // it is enabled.
                Log.Warn("System raised SecurityException during read of SDK enable flag in registry.", ex);
                regEnabled = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                // If we can't access the registry, best to just assume
                // it is enabled.
                Log.Warn("Not authorized to read registry for SDK enable flag.", ex);
                regEnabled = true;
            }

            return dllValid && regEnabled;
        }

        /// <summary>
        /// Attempts to extract the SDK version from the registry.
        /// </summary>
        /// <param name="ver">The SDK version will be saved in this variable.</param>
        /// <returns>
        /// <c>true</c> if the version was retrieved successfully and stored in <paramref name="ver" />,
        /// otherwise <c>false</c> with <c>(0, 0, 0)</c> stored in <paramref name="ver" />.
        /// </returns>
        internal static bool TryGetSdkVersion(out SdkVersion ver)
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(SdkRegKeyPath, false))
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

        /// <summary>
        /// Gets the path to the Razer Chroma SDK registry key.
        /// </summary>
        /// <returns>Path to the Razer Chroma SDK registry key.</returns>
        private static string GetSdkRegKeyPath()
        {
            return EnvironmentHelper.Is64Bit()
                ? @"SOFTWARE\WOW6432Node\Razer Chroma SDK"
                : @"SOFTWARE\Razer Chroma SDK";
        }
    }
}
