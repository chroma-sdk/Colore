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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Corale.Colore.Events;
    using Corale.Colore.Razer;

    /// <summary>
    /// Main class for interacting with the Chroma SDK.
    /// </summary>
    public sealed class Chroma : IChroma
    {
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
        /// Prevents a default instance of the <see cref="Chroma" /> class from being created.
        /// </summary>
        private Chroma()
        {
            NativeWrapper.Init();
            _registeredHandle = IntPtr.Zero;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Chroma" /> class.
        /// </summary>
        /// <remarks>
        /// Calls the SDK <c>UnInit</c> function.
        /// </remarks>
        ~Chroma()
        {
            Unregister();
            NativeWrapper.UnInit();
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
        public static IChroma Instance
        {
            get
            {
                return _instance ?? (_instance = new Chroma());
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="IKeyboard" /> interface
        /// for interacting with a Razer Chroma keyboard.
        /// </summary>
        public IKeyboard Keyboard
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="IMouse" /> interface
        /// for interacting with a Razer Chroma mouse.
        /// </summary>
        public IMouse Mouse
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Chroma main class has been initialized or not.
        /// </summary>
        internal static bool Initialized
        {
            get
            {
                return _instance != null;
            }
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
                    "handle");
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
            if (_registered)
                NativeWrapper.UnregisterEventNotification();

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

            NativeWrapper.UnregisterEventNotification();
            _registered = false;
            _registeredHandle = IntPtr.Zero;
        }

        /// <summary>
        /// Explicitly creates and initializes the <see cref="_instance" /> field
        /// with a new instance of the <see cref="Chroma" /> class.
        /// </summary>
        /// <remarks>
        /// For internal use by singleton accessors in device interface implementations.
        /// </remarks>
        internal static void Initialize()
        {
            if (!Initialized)
                _instance = new Chroma();
        }

        /// <summary>
        /// Invokes the application state event handlers with the specified parameter.
        /// </summary>
        /// <param name="enabled">Whether or not the application was put in an enabled state.</param>
        private void OnApplicationState(bool enabled)
        {
            var handler = ApplicationState;
            if (handler != null)
                handler(this, new ApplicationStateEventArgs(enabled));
        }

        /// <summary>
        /// Invokes the device access event handlers with the specified parameter.
        /// </summary>
        /// <param name="granted">Whether or not access to the device was granted.</param>
        private void OnDeviceAccess(bool granted)
        {
            var handler = DeviceAccess;
            if (handler != null)
                handler(this, new DeviceAccessEventArgs(granted));
        }

        /// <summary>
        /// Invokes the SDK support event handlers with the specified parameter.
        /// </summary>
        /// <param name="enabled">Whether or not the SDK is supported.</param>
        private void OnSdkSupport(bool enabled)
        {
            var handler = SdkSupport;
            if (handler != null)
                handler(this, new SdkSupportEventArgs(enabled));
        }
    }
}
