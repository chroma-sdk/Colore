// ---------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Native
{
    using System;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Effects.Keyboard;
    using Colore.Effects.Mouse;
    using Colore.Helpers;
    using Colore.Logging;

    /// <summary>
    /// Native methods from Razer's Chroma SDK.
    /// </summary>
    internal class NativeMethods : IDisposable
    {
        /// <summary>
        /// Calling convention for API functions.
        /// </summary>
        //// For some reason FxCop doesn't see that this field is actually used.
#pragma warning disable CA1823 // Avoid unused private fields
        private const CallingConvention FunctionConvention = CallingConvention.Cdecl;
#pragma warning restore CA1823 // Avoid unused private fields

        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NativeMethods>();

        /// <summary>
        /// Holds the pointer to the native Chroma SDK library.
        /// </summary>
        private readonly IntPtr _chromaSdkPointer;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeMethods" /> class.
        /// </summary>
        internal NativeMethods()
        {
            Log.Info("Loading native Chroma SDK");

            _chromaSdkPointer = Kernel32.NativeMethods.LoadLibrary(
                EnvironmentHelper.Is64Bit() ? "RzChromaSDK64.dll" : "RzChromaSDK.dll");

            Log.Debug("Native Chroma SDK loaded at pointer value {SdkPointer}", _chromaSdkPointer);

            if (_chromaSdkPointer == IntPtr.Zero)
            {
                throw new ColoreException(
                    "Failed to dynamically load Chroma SDK library (Error " + Marshal.GetLastWin32Error() + ").");
            }

            Log.Debug("Loading native SDK function delegates");

            Init = GetDelegateFromLibrary<InitDelegate>(_chromaSdkPointer, "Init");

            UnInit = GetDelegateFromLibrary<UnInitDelegate>(_chromaSdkPointer, "UnInit");

            CreateEffect = GetDelegateFromLibrary<CreateEffectDelegate>(_chromaSdkPointer, "CreateEffect");

            CreateKeyboardEffect = GetDelegateFromLibrary<CreateKeyboardEffectDelegate>(
                _chromaSdkPointer,
                "CreateKeyboardEffect");

            CreateMouseEffect =
                GetDelegateFromLibrary<CreateMouseEffectDelegate>(_chromaSdkPointer, "CreateMouseEffect");

            CreateHeadsetEffect = GetDelegateFromLibrary<CreateHeadsetEffectDelegate>(
                _chromaSdkPointer,
                "CreateHeadsetEffect");

            CreateMousepadEffect = GetDelegateFromLibrary<CreateMousepadEffectDelegate>(
                _chromaSdkPointer,
                "CreateMousepadEffect");

            CreateKeypadEffect = GetDelegateFromLibrary<CreateKeypadEffectDelegate>(
                _chromaSdkPointer,
                "CreateKeypadEffect");

            CreateChromaLinkEffect = GetDelegateFromLibrary<CreateChromaLinkEffectDelegate>(
                _chromaSdkPointer,
                "CreateChromaLinkEffect");

            DeleteEffect = GetDelegateFromLibrary<DeleteEffectDelegate>(_chromaSdkPointer, "DeleteEffect");

            SetEffect = GetDelegateFromLibrary<SetEffectDelegate>(_chromaSdkPointer, "SetEffect");

            RegisterEventNotification = GetDelegateFromLibrary<RegisterEventNotificationDelegate>(
                _chromaSdkPointer,
                "RegisterEventNotification");

            UnregisterEventNotification = GetDelegateFromLibrary<UnregisterEventNotificationDelegate>(
                _chromaSdkPointer,
                "UnregisterEventNotification");

            QueryDevice = GetDelegateFromLibrary<QueryDeviceDelegate>(_chromaSdkPointer, "QueryDevice");

            Log.Debug("Function delgates loaded");
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="NativeMethods" /> class.
        /// </summary>
        ~NativeMethods()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initialize Chroma SDK.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result InitDelegate();

        /// <summary>
        /// Uninitialize Chroma SDK.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result UnInitDelegate();

        /// <summary>
        /// Creates an effect for a device.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device, refer to <see cref="Devices" /> for supported IDs.</param>
        /// <param name="effect">The effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <param name="effectId">Valid effect ID if successful. Use <see cref="Guid.Empty" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateEffectDelegate(
            [In] Guid deviceId,
            [In] Effects.Generic.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type, like <see cref="KeyboardEffect.Static" />.
        /// </param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Use <see cref="Guid.Empty" /> if not required.</param>
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
        /// <term><see cref="KeyboardEffect.Static" /></term>
        /// <term><see cref="KeyboardStatic" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="KeyboardEffect.CustomKey" /></term>
        /// <term><see cref="KeyboardCustom" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateKeyboardEffectDelegate(
            [In] KeyboardEffect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create mouse effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type, like <see cref="MouseEffect.Static" />.
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
        /// <term><see cref="MouseEffect.Static" /></term>
        /// <term><see cref="MouseStatic" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="MouseEffect.Custom" /></term>
        /// <term><see cref="MouseCustom" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateMouseEffectDelegate(
            [In] MouseEffect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create headset effect.
        /// </summary>
        /// <param name="effect">Standard effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Set to valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateHeadsetEffectDelegate(
            [In] Effects.Headset.HeadsetEffect effect,
            [In] IntPtr param,
            [In] [Out] ref Guid effectId);

        /// <summary>
        /// Create mousepad effect.
        /// </summary>
        /// <param name="effect">Mousemat effect type.</param>
        /// <param name="param">Pointer to a parameter specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateMousepadEffectDelegate(
            [In] Effects.Mousepad.MousepadEffect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create keypad effect.
        /// </summary>
        /// <param name="effect">Keypad effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateKeypadEffectDelegate(
            [In] Effects.Keypad.KeypadEffect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create Chroma Link effect.
        /// </summary>
        /// <param name="effect">Chroma Link effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateChromaLinkEffectDelegate(
            [In] Effects.ChromaLink.ChromaLinkEffect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Delete effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be deleted.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result DeleteEffectDelegate([In] Guid effectId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be set.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result SetEffectDelegate([In] Guid effectId);

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
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result RegisterEventNotificationDelegate([In] IntPtr hwnd);

        /// <summary>
        /// Unregister for event notification.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result UnregisterEventNotificationDelegate();

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <param name="info">Will contain device information for the device specified by <paramref name="deviceId" />.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result QueryDeviceDelegate([In] Guid deviceId, [Out] IntPtr info);

        /// <summary>
        /// Gets a reference to the loaded <see cref="InitDelegate" />.
        /// </summary>
        internal InitDelegate Init { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="UnInitDelegate" />.
        /// </summary>
        internal UnInitDelegate UnInit { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateEffectDelegate" />.
        /// </summary>
        internal CreateEffectDelegate CreateEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateKeyboardEffectDelegate" />.
        /// </summary>
        internal CreateKeyboardEffectDelegate CreateKeyboardEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateMouseEffectDelegate" />.
        /// </summary>
        internal CreateMouseEffectDelegate CreateMouseEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateHeadsetEffectDelegate" />.
        /// </summary>
        internal CreateHeadsetEffectDelegate CreateHeadsetEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateMousepadEffectDelegate" />.
        /// </summary>
        internal CreateMousepadEffectDelegate CreateMousepadEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateKeypadEffectDelegate" />.
        /// </summary>
        internal CreateKeypadEffectDelegate CreateKeypadEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="CreateChromaLinkEffectDelegate" />.
        /// </summary>
        internal CreateChromaLinkEffectDelegate CreateChromaLinkEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="DeleteEffectDelegate" />.
        /// </summary>
        internal DeleteEffectDelegate DeleteEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="SetEffectDelegate" />.
        /// </summary>
        internal SetEffectDelegate SetEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="RegisterEventNotificationDelegate" />.
        /// </summary>
        internal RegisterEventNotificationDelegate RegisterEventNotification { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="UnregisterEventNotificationDelegate" />.
        /// </summary>
        internal UnregisterEventNotificationDelegate UnregisterEventNotification { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="QueryDeviceDelegate" />.
        /// </summary>
        internal QueryDeviceDelegate QueryDevice { get; }

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets a delegate with a specified name from a dynamically loaded library.
        /// </summary>
        /// <typeparam name="T">The type of delegate to get.</typeparam>
        /// <param name="lib">A pointer to the loaded library to load the function from.</param>
        /// <param name="name">Name of the function to load.</param>
        /// <returns>A delegate of type <typeparamref name="T" /> for the specified function.</returns>
        private static T GetDelegateFromLibrary<T>(IntPtr lib, string name)
        {
            var functionPtr = Kernel32.NativeMethods.GetProcAddress(lib, name);
            if (functionPtr == IntPtr.Zero)
            {
                throw new ColoreException(
                    "Failed to dynamically load function, GetProcAddress returned NULL for  " + name);
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
        }

        /// <summary>
        /// Disposes resources used by this class.
        /// </summary>
        /// <param name="disposing"><c>true</c> if calling from <see cref="Dispose" />, <c>false</c> if calling from the finalizer.</param>
        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        /// <remarks>Calls <see cref="Kernel32.NativeMethods.FreeLibrary" /> on the Chroma SDK pointer.</remarks>
        private void ReleaseUnmanagedResources()
        {
            if (_chromaSdkPointer != IntPtr.Zero)
                Kernel32.NativeMethods.FreeLibrary(_chromaSdkPointer);
        }
    }
}
