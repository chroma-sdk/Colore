// ---------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Corale">
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

namespace Corale.Colore.Razer
{
    using System;
    using System.Runtime.InteropServices;

#if ANYCPU
    using System.Diagnostics.CodeAnalysis;
#endif

    /// <summary>
    /// Native methods from Razer's Chroma SDK.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Calling convention for API functions.
        /// </summary>
        private const CallingConvention FunctionConvention = CallingConvention.Cdecl;

#if WIN64 || WIN32
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
#else
        private const string DllName = "RzChromaSDK.dll";
#endif
#endif

#if ANYCPU

        /// <summary>
        /// Stores a reference to the loaded <see cref="InitDelegate" />.
        /// </summary>
        internal static readonly InitDelegate Init;

        /// <summary>
        /// Stores a reference to the loaded <see cref="UnInitDelegate" />.
        /// </summary>
        internal static readonly UnInitDelegate UnInit;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateEffectDelegate" />.
        /// </summary>
        internal static readonly CreateEffectDelegate CreateEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateKeyboardEffectDelegate" />.
        /// </summary>
        internal static readonly CreateKeyboardEffectDelegate CreateKeyboardEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateMouseEffectDelegate" />.
        /// </summary>
        internal static readonly CreateMouseEffectDelegate CreateMouseEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateHeadsetEffectDelegate" />.
        /// </summary>
        internal static readonly CreateHeadsetEffectDelegate CreateHeadsetEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateMousepadEffectDelegate" />.
        /// </summary>
        internal static readonly CreateMousepadEffectDelegate CreateMousepadEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateKeypadEffectDelegate" />.
        /// </summary>
        internal static readonly CreateKeypadEffectDelegate CreateKeypadEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="CreateChromaLinkEffectDelegate" />.
        /// </summary>
        internal static readonly CreateChromaLinkEffectDelegate CreateChromaLinkEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="DeleteEffectDelegate" />.
        /// </summary>
        internal static readonly DeleteEffectDelegate DeleteEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="SetEffectDelegate" />.
        /// </summary>
        internal static readonly SetEffectDelegate SetEffect;

        /// <summary>
        /// Stores a reference to the loaded <see cref="RegisterEventNotificationDelegate" />.
        /// </summary>
        internal static readonly RegisterEventNotificationDelegate RegisterEventNotification;

        /// <summary>
        /// Stores a reference to the loaded <see cref="UnregisterEventNotificationDelegate" />.
        /// </summary>
        internal static readonly UnregisterEventNotificationDelegate UnregisterEventNotification;

        /// <summary>
        /// Stores a reference to the loaded <see cref="QueryDeviceDelegate" />.
        /// </summary>
        internal static readonly QueryDeviceDelegate QueryDevice;

        /// <summary>
        /// Initializes static members of the <see cref="NativeMethods" /> class.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations",
            Justification = "Can't get rid of this exception as we depend on architecture and library to work.")]
        static NativeMethods()
        {
            IntPtr chromaSdkPointer;

            if (EnvironmentHelper.Is64BitProcess() && EnvironmentHelper.Is64BitOperatingSystem())
            {
                // We are running 64-bit!
                chromaSdkPointer = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK64.dll");
            }
            else
            {
                // We are running 32-bit!
                chromaSdkPointer = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK.dll");
            }

            if (chromaSdkPointer == IntPtr.Zero)
            {
                throw new ColoreException(
                    "Failed to dynamically load Chroma SDK library (Error " + Marshal.GetLastWin32Error() + ").");
            }

            Init = GetDelegateFromLibrary<InitDelegate>(chromaSdkPointer, "Init");

            UnInit = GetDelegateFromLibrary<UnInitDelegate>(chromaSdkPointer, "UnInit");

            CreateEffect = GetDelegateFromLibrary<CreateEffectDelegate>(chromaSdkPointer, "CreateEffect");

            CreateKeyboardEffect = GetDelegateFromLibrary<CreateKeyboardEffectDelegate>(
                chromaSdkPointer,
                "CreateKeyboardEffect");

            CreateMouseEffect = GetDelegateFromLibrary<CreateMouseEffectDelegate>(chromaSdkPointer, "CreateMouseEffect");

            CreateHeadsetEffect = GetDelegateFromLibrary<CreateHeadsetEffectDelegate>(
                chromaSdkPointer,
                "CreateHeadsetEffect");

            CreateMousepadEffect = GetDelegateFromLibrary<CreateMousepadEffectDelegate>(
                chromaSdkPointer,
                "CreateMousepadEffect");

            CreateKeypadEffect = GetDelegateFromLibrary<CreateKeypadEffectDelegate>(
                chromaSdkPointer,
                "CreateKeypadEffect");

            CreateChromaLinkEffect = GetDelegateFromLibrary<CreateChromaLinkEffectDelegate>(
                    chromaSdkPointer,
                    "CreateChromaLinkEffect");

            DeleteEffect = GetDelegateFromLibrary<DeleteEffectDelegate>(chromaSdkPointer, "DeleteEffect");

            SetEffect = GetDelegateFromLibrary<SetEffectDelegate>(chromaSdkPointer, "SetEffect");

            RegisterEventNotification = GetDelegateFromLibrary<RegisterEventNotificationDelegate>(
                chromaSdkPointer,
                "RegisterEventNotification");

            UnregisterEventNotification = GetDelegateFromLibrary<UnregisterEventNotificationDelegate>(
                chromaSdkPointer,
                "UnregisterEventNotification");

            QueryDevice = GetDelegateFromLibrary<QueryDeviceDelegate>(chromaSdkPointer, "QueryDevice");
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
            [In] Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type. These include <see cref="Keyboard.Effects.Effect.Wave" />,
        /// <see cref="Keyboard.Effects.Effect.SpectrumCycling" />, <see cref="Keyboard.Effects.Effect.Breathing" />,
        /// <see cref="Keyboard.Effects.Effect.Reactive" />, and <see cref="Keyboard.Effects.Effect.Static" />.
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
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateKeyboardEffectDelegate(
            [In] Keyboard.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create mouse effect.
        /// </summary>
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
        /// </list>
        /// </remarks>
        [UnmanagedFunctionPointer(FunctionConvention, SetLastError = true)]
        internal delegate Result CreateMouseEffectDelegate(
            [In] Mouse.Effects.Effect effect,
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
            [In] Headset.Effects.Effect effect,
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
            [In] Mousepad.Effects.Effect effect,
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
            [In] Keypad.Effects.Effect effect,
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
            [In] ChromaLink.Effects.Effect effect,
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
        /// Gets a delegate with a specified name from a dynamically loaded library.
        /// </summary>
        /// <typeparam name="T">The type of delegate to get.</typeparam>
        /// <param name="lib">A pointer to the loaded library to load the function from.</param>
        /// <param name="name">Name of the function to load.</param>
        /// <returns>A delegate of type <typeparamref name="T" /> for the specified function.</returns>
        private static T GetDelegateFromLibrary<T>(IntPtr lib, string name)
        {
            var functionPtr = Native.Kernel32.NativeMethods.GetProcAddress(lib, name);
            if (functionPtr == IntPtr.Zero)
            {
                throw new ColoreException(
                    "Failed to dynamically load function, GetProcAddress returned NULL for  " + name);
            }

            return (T)(object)Marshal.GetDelegateForFunctionPointer(functionPtr, typeof(T));
        }

#else

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
        /// Creates an effect for a device.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device, refer to <see cref="Devices" /> for supported IDs.</param>
        /// <param name="effect">The effect to create.</param>
        /// <param name="param">Effect-specific parameter.</param>
        /// <param name="effectId">Valid effect ID if successful. Use <see cref="Guid.Empty" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateEffect", SetLastError = true)]
        internal static extern Result CreateEffect(
            [In] Guid deviceId,
            [In] Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create keyboard effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type. These include <see cref="Razer.Keyboard.Effects.Effect.Wave" />,
        /// <see cref="Razer.Keyboard.Effects.Effect.SpectrumCycling" />, <see cref="Razer.Keyboard.Effects.Effect.Breathing" />,
        /// <see cref="Razer.Keyboard.Effects.Effect.Reactive" />, and <see cref="Razer.Keyboard.Effects.Effect.Static" />.
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
        /// <term><see cref="Razer.Keyboard.Effects.Effect.Wave" /></term>
        /// <term><see cref="Razer.Keyboard.Effects.Wave" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Keyboard.Effects.Effect.SpectrumCycling" /></term>
        /// <term><i>None</i> (<see cref="IntPtr.Zero" />)</term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Keyboard.Effects.Effect.Breathing" /></term>
        /// <term><see cref="Razer.Keyboard.Effects.Breathing" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Keyboard.Effects.Effect.Reactive" /></term>
        /// <term><see cref="Razer.Keyboard.Effects.Reactive" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Keyboard.Effects.Effect.Static" /></term>
        /// <term><see cref="Razer.Keyboard.Effects.Static" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeyboardEffect",
            SetLastError = true)]
        internal static extern Result CreateKeyboardEffect(
            [In] Keyboard.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectid);

        /// <summary>
        /// Create mouse effect.
        /// </summary>
        /// <param name="effect">
        /// Standard effect type. These include <see cref="Razer.Mouse.Effects.Effect.SpectrumCycling" />,
        /// <see cref="Razer.Mouse.Effects.Effect.Breathing" />, and <see cref="Razer.Mouse.Effects.Effect.Static" />.
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
        /// <term><see cref="Razer.Mouse.Effects.Effect.SpectrumCycling" /></term>
        /// <term><i>None</i> (<see cref="IntPtr.Zero" />)</term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Mouse.Effects.Effect.Breathing" /></term>
        /// <term><see cref="Razer.Mouse.Effects.Breathing" /></term>
        /// </item>
        /// <item>
        /// <term><see cref="Razer.Mouse.Effects.Effect.Static" /></term>
        /// <term><see cref="Razer.Mouse.Effects.Static" /></term>
        /// </item>
        /// </list>
        /// </remarks>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMouseEffect",
            SetLastError = true)]
        internal static extern Result CreateMouseEffect(
            [In] Mouse.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create headset effect.
        /// </summary>
        /// <param name="effect">Standard effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Set to valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateHeadsetEffect", SetLastError = true)]
        internal static extern Result CreateHeadsetEffect(
            [In] Headset.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create mousepad effect.
        /// </summary>
        /// <param name="effect">Mousemat effect type.</param>
        /// <param name="param">Pointer to a parameter specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateMousepadEffect", SetLastError = true)]
        internal static extern Result CreateMousepadEffect(
            [In] Mousepad.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create keypad effect.
        /// </summary>
        /// <param name="effect">Keypad effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateKeypadEffect", SetLastError = true)]
        internal static extern Result CreateKeypadEffect(
            [In] Keypad.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Create Chroma Link effect.
        /// </summary>
        /// <param name="effect">Chroma Link effect type.</param>
        /// <param name="param">Pointer to a parameter type specified by <paramref name="effect" />.</param>
        /// <param name="effectId">Valid effect ID if successful. Pass <see cref="IntPtr.Zero" /> if not required.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "CreateChromaLinkEffect", SetLastError = true)]
        internal static extern Result CreateChromaLinkEffect(
            [In] ChromaLink.Effects.Effect effect,
            [In] IntPtr param,
            [In, Out] ref Guid effectId);

        /// <summary>
        /// Delete effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be deleted.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "DeleteEffect", SetLastError = true)]
        internal static extern Result DeleteEffect([In] Guid effectId);

        /// <summary>
        /// Set effect.
        /// </summary>
        /// <param name="effectId">ID of the effect that needs to be set.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "SetEffect", SetLastError = true)]
        internal static extern Result SetEffect([In] Guid effectId);

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
        internal static extern Result RegisterEventNotification([In] IntPtr hwnd);

        /// <summary>
        /// Unregister for event notification.
        /// </summary>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "UnregisterEventNotification",
            SetLastError = true)]
        internal static extern Result UnregisterEventNotification();

        /// <summary>
        /// Query for device information.
        /// </summary>
        /// <param name="deviceId">Device ID, found in <see cref="Devices" />.</param>
        /// <param name="info">Will contain device information for the device specified by <paramref name="deviceId" />.</param>
        /// <returns><see cref="Result" /> value indicating success.</returns>
        [DllImport(DllName, CallingConvention = FunctionConvention, EntryPoint = "QueryDevice", SetLastError = true)]
        internal static extern Result QueryDevice([In] Guid deviceId, [Out] IntPtr info);

#endif
    }
}
