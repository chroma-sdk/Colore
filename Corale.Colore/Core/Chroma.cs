// ---------------------------------------------------------------------------------------
// <copyright file="Chroma.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security;

    using Corale.Colore.Annotations;
    using Corale.Colore.Events;
    using Corale.Colore.Razer;

    using log4net;

    using Microsoft.Win32;

    /// <summary>
    /// Main class for interacting with the Chroma SDK.
    /// </summary>
    public sealed class Chroma : IChroma
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Chroma));

        /// <summary>
        /// Mutex lock for thread-safe init calls.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Holds the application-wide instance of the <see cref="IChroma" /> interface.
        /// </summary>
        private static IChroma _instance;

        /// <summary>
        /// Keeps track of whether we have registered to receive Chroma events.
        /// </summary>
        private bool _registered;

        /// <summary>
        /// Keeps track of the window handle that is registered to receive events.
        /// </summary>
        private IntPtr _registeredHandle;

        /// <summary>
        /// Version of the Chroma SDK as retrieved from the registry at
        /// the point of initialization.
        /// </summary>
        private SdkVersion _sdkVersion;

        /// <summary>
        /// Prevents a default instance of the <see cref="Chroma" /> class from being created.
        /// </summary>
        private Chroma()
        {
            Initialize();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Chroma" /> class.
        /// </summary>
        /// <remarks>
        /// Calls the SDK <c>UnInit</c> function.
        /// </remarks>
        ~Chroma()
        {
            Uninitialize();
        }

        /// <summary>
        /// Raised when information about application state is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="IChroma.Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="IChroma.HandleMessage" />.
        /// </remarks>
        public event EventHandler<ApplicationStateEventArgs> ApplicationState;

        /// <summary>
        /// Raised when information about device access is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="IChroma.Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="IChroma.HandleMessage" />.
        /// </remarks>
        public event EventHandler<DeviceAccessEventArgs> DeviceAccess;

        /// <summary>
        /// Raised when information about SDK support is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="IChroma.Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="IChroma.HandleMessage" />.
        /// </remarks>
        public event EventHandler<SdkSupportEventArgs> SdkSupport;

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IChroma" /> interface.
        /// </summary>
        [PublicAPI]
        public static IChroma Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new Chroma());
                }
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="IKeyboard" /> interface
        /// for interacting with a Razer Chroma keyboard.
        /// </summary>
        public IKeyboard Keyboard => Core.Keyboard.Instance;

        /// <summary>
        /// Gets an instance of the <see cref="IMouse" /> interface
        /// for interacting with a Razer Chroma mouse.
        /// </summary>
        public IMouse Mouse => Core.Mouse.Instance;

        /// <summary>
        /// Gets an instance of the <see cref="IHeadset" /> interface
        /// for interacting with a Razer Chroma headset.
        /// </summary>
        public IHeadset Headset => Core.Headset.Instance;

        /// <summary>
        /// Gets an instance of the <see cref="IMousepad" /> interface
        /// for interacting with a Razer Chroma mouse pad.
        /// </summary>
        public IMousepad Mousepad => Core.Mousepad.Instance;

        /// <summary>
        /// Gets an instance of the <see cref="IKeypad" /> interface
        /// for interacting with a Razer Chroma keypad.
        /// </summary>
        public IKeypad Keypad => Core.Keypad.Instance;

        /// <summary>
        /// Gets a value indicating whether the Chroma
        /// SDK has been initialized or not.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// Gets the version of the Chroma SDK that Colore is currently using.
        /// </summary>
        public SdkVersion SdkVersion => _sdkVersion;

        /// <summary>
        /// Checks if the Chroma SDK is available on this system.
        /// </summary>
        /// <returns><c>true</c> if Chroma SDK is available, otherwise <c>false</c>.</returns>
        [PublicAPI]
        [SecurityCritical]
        public static bool IsSdkAvailable()
        {
            bool dllValid;
            var regKey = @"SOFTWARE\Razer Chroma SDK";

#if ANYCPU
            if (EnvironmentHelper.Is64BitProcess() && EnvironmentHelper.Is64BitOperatingSystem())
            {
                dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK64.dll") != IntPtr.Zero;
                regKey = @"SOFTWARE\Wow6432Node\Razer Chroma SDK";
            }
            else
                dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK.dll") != IntPtr.Zero;
#elif WIN64
            dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK64.dll") != IntPtr.Zero;
            regKey = @"SOFTWARE\Wow6432Node\Razer Chroma SDK";
#else
            dllValid = Native.Kernel32.NativeMethods.LoadLibrary("RzChromaSDK.dll") != IntPtr.Zero;
#endif

            bool regEnabled;

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(regKey))
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
        /// Initializes the SDK if it hasn't already.
        /// </summary>
        /// <remarks>
        /// <span style="color: red;">Manual manipulation of the SDK state is
        /// <strong>not supported by the CoraleStudios team</strong> and may
        /// result in <emph>undefined behaviour</emph>. Usage of this method is
        /// <strong>at your own risk</strong>.</span>
        /// </remarks>
        [SecuritySafeCritical]
        public void Initialize()
        {
            if (Initialized)
                return;

            Log.Info("Chroma is initializing.");

            Log.Debug("Retrieving SDK version");
            var versionSuccess = RegistryHelper.TryGetSdkVersion(out _sdkVersion);

            if (versionSuccess)
                Log.InfoFormat("Colore is running against SDK version {0}.", SdkVersion);
            else
                Log.Warn("Failed to retrieve SDK version from registry!");

            Log.Debug("Calling SDK Init function");
            NativeWrapper.Init();
            Initialized = true;
            Log.Debug("Resetting _registeredHandle");
            _registeredHandle = IntPtr.Zero;
        }

        /// <summary>
        /// Uninitializes the SDK if it has been initialized.
        /// </summary>
        /// <remarks>
        /// <span style="color: red;">Manual manipulation of the SDK state is
        /// <strong>not supported by the CoraleStudios team</strong> and may
        /// result in <emph>undefined behaviour</emph>. Usage of this method is
        /// <strong>at your own risk</strong>. Usage of SDK functions while
        /// the SDK is in an <emph>uninitialized</emph> state is <strong>highly
        /// advised against</strong> and <emph>WILL</emph> result in catastrophic
        /// failure. <strong>YOU HAVE BEEN WARNED</strong>.</span>
        /// </remarks>
        public void Uninitialize()
        {
            if (!Initialized)
                return;

            ((Device)Keyboard).DeleteCurrentEffect();
            ((Device)Mouse).DeleteCurrentEffect();
            ((Device)Keypad).DeleteCurrentEffect();
            ((Device)Mousepad).DeleteCurrentEffect();
            ((Device)Headset).DeleteCurrentEffect();

            Unregister();
            NativeWrapper.UnInit();

            Initialized = false;
        }

        /// <summary>
        /// Queries the SDK for information regarding a specific device.
        /// </summary>
        /// <param name="deviceId">The device ID to query for, valid IDs can be found in <see cref="Devices" />.</param>
        /// <returns>A struct with information regarding the device type and whether it's connected.</returns>
        [SecurityCritical]
        public DeviceInfo Query(Guid deviceId)
        {
            if (!Devices.IsValidId(deviceId))
                throw new ArgumentException("The specified ID does not match any of the valid IDs.", nameof(deviceId));

            Log.DebugFormat("Information for {0} requested", deviceId);
            return NativeWrapper.QueryDevice(deviceId);
        }

        /// <summary>
        /// Gets an instance of <see cref="IGenericDevice" /> for
        /// the device with the specified ID.
        /// </summary>
        /// <param name="deviceId">
        /// The <see cref="Guid" /> of the device to get,
        /// valid IDs can be found in <see cref="Devices" />.
        /// </param>
        /// <returns>An instance of <see cref="IGenericDevice" />.</returns>
        public IGenericDevice Get(Guid deviceId)
        {
            Log.DebugFormat("Device {0} requested", deviceId);
            return GenericDevice.Get(deviceId);
        }

        /// <summary>
        /// Handles a Windows message and fires the appropriate events.
        /// </summary>
        /// <param name="handle">The <c>HWnd</c> property of the Message struct.</param>
        /// <param name="msgId">The <c>Msg</c> property of the Message struct.</param>
        /// <param name="wParam">The <c>wParam</c> property of the Message struct.</param>
        /// <param name="lParam">The <c>lParam</c> property of the Message struct.</param>
        /// <returns><c>true</c> if the message was handled by Chroma, <c>false</c> otherwise (message was ignored).</returns>
        /// <remarks>Non-Chroma messages will be ignored.</remarks>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Parameter names match those in the Message struct.")]
        public bool HandleMessage(IntPtr handle, int msgId, IntPtr wParam, IntPtr lParam)
        {
            if (!_registered)
                throw new InvalidOperationException("Register must be called before event handling can be performed.");

            if (handle != _registeredHandle)
            {
                throw new ArgumentException(
                    "The specified window handle does not match the currently registered one.",
                    nameof(handle));
            }

            if (msgId != Constants.WmChromaEvent)
                return false;

            var handled = false;

            var type = wParam.ToInt32();
            var state = lParam.ToInt32();

            switch (type)
            {
                case 1: // Chroma SDK support
                    OnSdkSupport(state != 0);
                    handled = true;
                    break;

                case 2: // Access to device
                    OnDeviceAccess(state != 0);
                    handled = true;
                    break;

                case 3: // Application state
                    OnApplicationState(state != 0);
                    handled = true;
                    break;
            }

            return handled;
        }

        /// <summary>
        /// Registers to start receiving Chroma events.
        /// </summary>
        /// <param name="handle">Handle to the application Window that is running the message loop.</param>
        /// <remarks>
        /// Chroma events are sent using the Windows message API, as such, there has to be something handling
        /// Windows messages to receive them. Messages need to be passed to the message handler in Colore to
        /// be processed, as this cannot be automated.
        /// </remarks>
        public void Register(IntPtr handle)
        {
            Log.Debug("Registering for Chroma event notifications");

            if (_registered)
            {
                Log.Debug("Already registered, unregistering before continuing with registration");
                NativeWrapper.UnregisterEventNotification();
            }

            NativeWrapper.RegisterEventNotification(handle);
            _registered = true;
            _registeredHandle = handle;
        }

        /// <summary>
        /// Unregisters from receiving Chroma events.
        /// </summary>
        public void Unregister()
        {
            if (!_registered)
                return;

            Log.Debug("Unregistering from Chroma event notifications");

            NativeWrapper.UnregisterEventNotification();
            _registered = false;
            _registeredHandle = IntPtr.Zero;
        }

        /// <summary>
        /// Sets all Chroma devices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public void SetAll(Color color)
        {
            Keyboard.SetAll(color);
            Mouse.SetAll(color);
            Mousepad.SetAll(color);
            Keypad.SetAll(color);
            Headset.SetAll(color);
        }

        /// <summary>
        /// Explicitly creates and initializes the <see cref="_instance" /> field
        /// with a new instance of the <see cref="Chroma" /> class.
        /// </summary>
        /// <remarks>
        /// For internal use by singleton accessors in device interface implementations.
        /// </remarks>
        internal static void InitInstance()
        {
            Instance.Initialize();
        }

        /// <summary>
        /// Invokes the application state event handlers with the specified parameter.
        /// </summary>
        /// <param name="enabled">Whether or not the application was put in an enabled state.</param>
        private void OnApplicationState(bool enabled)
        {
            ApplicationState?.Invoke(this, new ApplicationStateEventArgs(enabled));
        }

        /// <summary>
        /// Invokes the device access event handlers with the specified parameter.
        /// </summary>
        /// <param name="granted">Whether or not access to the device was granted.</param>
        private void OnDeviceAccess(bool granted)
        {
            DeviceAccess?.Invoke(this, new DeviceAccessEventArgs(granted));
        }

        /// <summary>
        /// Invokes the SDK support event handlers with the specified parameter.
        /// </summary>
        /// <param name="enabled">Whether or not the SDK is supported.</param>
        private void OnSdkSupport(bool enabled)
        {
            SdkSupport?.Invoke(this, new SdkSupportEventArgs(enabled));
        }
    }
}
