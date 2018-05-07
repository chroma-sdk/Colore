// ---------------------------------------------------------------------------------------
// <copyright file="ChromaImplementation.cs" company="Corale">
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

namespace Colore.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Events;
    using Colore.Helpers;
    using Colore.Logging;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Main class for interacting with the Chroma SDK.
    /// </summary>
    internal sealed class ChromaImplementation : IChroma
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<ChromaImplementation>();

        /// <summary>
        /// Reference to the API instance in use.
        /// </summary>
        private readonly IChromaApi _api;

        /// <summary>
        /// Cache of created <see cref="IGenericDevice" /> instances.
        /// </summary>
        private readonly Dictionary<Guid, IGenericDevice> _deviceInstances;

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
        /// Reference to lazy-loaded <see cref="Keyboard" /> instance.
        /// </summary>
        private KeyboardImplementation _keyboard;

        /// <summary>
        /// Reference to lazy-loaded <see cref="Mouse" /> instance.
        /// </summary>
        private MouseImplementation _mouse;

        /// <summary>
        /// Reference to lazy-loaded <see cref="Headset" /> instance.
        /// </summary>
        private HeadsetImplementation _headset;

        /// <summary>
        /// Reference to lazy-loaded <see cref="Mousepad" /> instance.
        /// </summary>
        private MousepadImplementation _mousepad;

        /// <summary>
        /// Reference to lazy-loaded <see cref="Keypad" /> instance.
        /// </summary>
        private KeypadImplementation _keypad;

        /// <summary>
        /// Reference to lazy-loaded <see cref="ChromaLink" /> instance.
        /// </summary>
        private ChromaLinkImplementation _chromaLink;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaImplementation" /> class.
        /// </summary>
        /// <param name="api">API instance to use.</param>
        /// <param name="info">Information about the application.</param>
        public ChromaImplementation(IChromaApi api, AppInfo info)
        {
            _api = api;
            _deviceInstances = new Dictionary<Guid, IGenericDevice>();
            Version = typeof(ChromaImplementation).GetTypeInfo().Assembly.GetName().Version;
            InitializeAsync(info).Wait();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ChromaImplementation" /> class.
        /// </summary>
        /// <remarks>
        /// Calls the SDK <c>UnInit</c> function.
        /// </remarks>
        ~ChromaImplementation()
        {
            UninitializeAsync().Wait();
        }

        /// <inheritdoc />
        /// <summary>
        /// Raised when information about application state is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="IChroma.Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="IChroma.HandleMessage" />.
        /// </remarks>
        public event EventHandler<ApplicationStateEventArgs> ApplicationState;

        /// <inheritdoc />
        /// <summary>
        /// Raised when information about device access is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="IChroma.Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="IChroma.HandleMessage" />.
        /// </remarks>
        public event EventHandler<DeviceAccessEventArgs> DeviceAccess;

        /// <inheritdoc />
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
        /// Gets a value indicating whether the SDK is available on this system.
        /// </summary>
        [PublicAPI]
        public static bool SdkAvailable => RegistryHelper.IsSdkAvailable();

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IKeyboard" /> interface
        /// for interacting with a Razer Chroma keyboard.
        /// </summary>
        public IKeyboard Keyboard => _keyboard ?? (_keyboard = new KeyboardImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IMouse" /> interface
        /// for interacting with a Razer Chroma mouse.
        /// </summary>
        public IMouse Mouse => _mouse ?? (_mouse = new MouseImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IHeadset" /> interface
        /// for interacting with a Razer Chroma headset.
        /// </summary>
        public IHeadset Headset => _headset ?? (_headset = new HeadsetImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IMousepad" /> interface
        /// for interacting with a Razer Chroma mouse pad.
        /// </summary>
        public IMousepad Mousepad => _mousepad ?? (_mousepad = new MousepadImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IKeypad" /> interface
        /// for interacting with a Razer Chroma keypad.
        /// </summary>
        public IKeypad Keypad => _keypad ?? (_keypad = new KeypadImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of the <see cref="IChromaLink" /> interface
        /// for interacting with ChromaLink devices.
        /// </summary>
        public IChromaLink ChromaLink => _chromaLink ?? (_chromaLink = new ChromaLinkImplementation(_api));

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the Chroma
        /// SDK has been initialized or not.
        /// </summary>
        public bool Initialized { get; private set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the version of the Chroma SDK that Colore is currently using.
        /// </summary>
        public SdkVersion SdkVersion => _sdkVersion;

        /// <inheritdoc />
        /// <summary>
        /// Gets the <see cref="System.Version" /> of Colore.
        /// </summary>
        public Version Version { get; }

        /// <inheritdoc />
        /// <summary>
        /// Initializes the SDK if it hasn't already.
        /// </summary>
        /// <param name="info">
        /// Information about the application. Can be <c>null</c> only if the Native SDK backend is used.
        /// </param>
        /// <remarks>
        /// <span style="color: red;">Manual manipulation of the SDK state is
        /// <strong>not supported by the CoraleStudios team</strong> and may
        /// result in <emph>undefined behaviour</emph>. Usage of this method is
        /// <strong>at your own risk</strong>.</span>
        /// </remarks>
        public async Task InitializeAsync(AppInfo info)
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
            await _api.InitializeAsync(info).ConfigureAwait(false);
            Initialized = true;
            Log.Debug("Resetting _registeredHandle");
            _registeredHandle = IntPtr.Zero;
        }

        /// <inheritdoc />
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
        public async Task UninitializeAsync()
        {
            if (!Initialized)
                return;

            if (_keyboard != null)
                await _keyboard.DeleteCurrentEffectAsync().ConfigureAwait(false);

            if (_mouse != null)
                await _mouse.DeleteCurrentEffectAsync().ConfigureAwait(false);

            if (_keypad != null)
                await _keypad.DeleteCurrentEffectAsync().ConfigureAwait(false);

            if (_mousepad != null)
                await _mousepad.DeleteCurrentEffectAsync().ConfigureAwait(false);

            if (_headset != null)
                await _headset.DeleteCurrentEffectAsync().ConfigureAwait(false);

            if (_chromaLink != null)
                await _chromaLink.DeleteCurrentEffectAsync().ConfigureAwait(false);

            Unregister();
            await _api.UninitializeAsync().ConfigureAwait(false);

            Initialized = false;
        }

        /// <inheritdoc />
        /// <summary>
        /// Queries the SDK for information regarding a specific device.
        /// </summary>
        /// <param name="deviceId">The device ID to query for, valid IDs can be found in <see cref="Devices" />.</param>
        /// <returns>A struct with information regarding the device type and whether it's connected.</returns>
        public async Task<DeviceInfo> QueryAsync(Guid deviceId)
        {
            if (!Devices.IsValidId(deviceId))
                throw new ArgumentException("The specified ID does not match any of the valid IDs.", nameof(deviceId));

            Log.DebugFormat("Information for {0} requested", deviceId);

            var sdkDeviceInfo = await _api.QueryDeviceAsync(deviceId).ConfigureAwait(false);
            var deviceMetadata = Devices.GetDeviceMetadata(deviceId);
            var deviceInfo = new DeviceInfo(sdkDeviceInfo, deviceId, deviceMetadata);
            return deviceInfo;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets an instance of <see cref="IGenericDevice" /> for
        /// the device with the specified ID.
        /// </summary>
        /// <param name="deviceId">
        /// The <see cref="Guid" /> of the device to get,
        /// valid IDs can be found in <see cref="Devices" />.
        /// </param>
        /// <returns>An instance of <see cref="IGenericDevice" />.</returns>
        public Task<IGenericDevice> GetDeviceAsync(Guid deviceId)
        {
            Log.DebugFormat("Device {0} requested", deviceId);
            if (_deviceInstances.ContainsKey(deviceId))
                return Task.FromResult(_deviceInstances[deviceId]);
            IGenericDevice device = new GenericDeviceImplementation(deviceId, _api);
            _deviceInstances[deviceId] = device;
            return Task.FromResult(device);
        }

        /// <inheritdoc />
        /// <summary>
        /// Handles a Windows message and fires the appropriate events.
        /// </summary>
        /// <param name="handle">The <c>HWnd</c> property of the Message struct.</param>
        /// <param name="msgId">The <c>Msg</c> property of the Message struct.</param>
        /// <param name="wParam">The <c>wParam</c> property of the Message struct.</param>
        /// <param name="lParam">The <c>lParam</c> property of the Message struct.</param>
        /// <returns><c>true</c> if the message was handled by Chroma, <c>false</c> otherwise (message was ignored).</returns>
        /// <remarks>Non-Chroma messages will be ignored.</remarks>
        [SuppressMessage(
            "StyleCop.CSharp.NamingRules",
            "SA1305:FieldNamesMustNotUseHungarianNotation",
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

            // Unprocessed types will default to not being handled.
            // ReSharper disable once SwitchStatementMissingSomeCases
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

        /// <inheritdoc />
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
                _api.UnregisterEventNotifications();
            }

            _api.RegisterEventNotifications(handle);
            _registered = true;
            _registeredHandle = handle;
        }

        /// <inheritdoc />
        /// <summary>
        /// Unregisters from receiving Chroma events.
        /// </summary>
        public void Unregister()
        {
            if (!_registered)
                return;

            Log.Debug("Unregistering from Chroma event notifications");

            _api.UnregisterEventNotifications();
            _registered = false;
            _registeredHandle = IntPtr.Zero;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets all Chroma devices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        public async Task SetAllAsync(Color color)
        {
            await Keyboard.SetAllAsync(color).ConfigureAwait(false);
            await Mouse.SetAllAsync(color).ConfigureAwait(false);
            await Mousepad.SetAllAsync(color).ConfigureAwait(false);
            await Keypad.SetAllAsync(color).ConfigureAwait(false);
            await Headset.SetAllAsync(color).ConfigureAwait(false);
            await ChromaLink.SetAllAsync(color).ConfigureAwait(false);
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
