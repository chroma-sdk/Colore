// ---------------------------------------------------------------------------------------
// <copyright file="IChroma.cs" company="Corale">
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
    using System.Diagnostics.CodeAnalysis;

    using Corale.Colore.Events;
    using Corale.Colore.Razer;

    using JetBrains.Annotations;

    /// <summary>
    /// Interface for basic Chroma functionality.
    /// </summary>
    public interface IChroma
    {
        /// <summary>
        /// Raised when information about application state is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="HandleMessage" />.
        /// </remarks>
        [PublicAPI]
        event EventHandler<ApplicationStateEventArgs> ApplicationState;

        /// <summary>
        /// Raised when information about device access is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="HandleMessage" />.
        /// </remarks>
        [PublicAPI]
        event EventHandler<DeviceAccessEventArgs> DeviceAccess;

        /// <summary>
        /// Raised when information about SDK support is received from messages.
        /// </summary>
        /// <remarks>
        /// Requires that application has registered for receiving messages with
        /// <see cref="Register" /> and that Windows messages are being forwarded to
        /// Colore using <see cref="HandleMessage" />.
        /// </remarks>
        [PublicAPI]
        event EventHandler<SdkSupportEventArgs> SdkSupport;

        /// <summary>
        /// Gets an instance of the <see cref="IKeyboard" /> interface
        /// for interacting with a Razer Chroma keyboard.
        /// </summary>
        [PublicAPI]
        IKeyboard Keyboard { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IMouse" /> interface
        /// for interacting with a Razer Chroma mouse.
        /// </summary>
        [PublicAPI]
        IMouse Mouse { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IHeadset" /> interface
        /// for interacting with a Razer Chroma headset.
        /// </summary>
        [PublicAPI]
        IHeadset Headset { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IMousepad" /> interface
        /// for interacting with a Razer Chroma mouse pad.
        /// </summary>
        [PublicAPI]
        IMousepad Mousepad { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IKeypad" /> interface
        /// for interacting with a Razer Chroma keypad.
        /// </summary>
        [PublicAPI]
        IKeypad Keypad { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IChromaLink" /> interface
        /// for interacting with ChromaLink devices.
        /// </summary>
        [PublicAPI]
        IChromaLink ChromaLink { get; }

        /// <summary>
        /// Gets a value indicating whether the Chroma
        /// SDK has been initialized or not.
        /// </summary>
        [PublicAPI]
        bool Initialized { get; }

        /// <summary>
        /// Gets the version of the Chroma SDK that Colore is currently using.
        /// </summary>
        [PublicAPI]
        SdkVersion SdkVersion { get; }

        /// <summary>
        /// Gets the <see cref="System.Version" /> of Colore.
        /// </summary>
        [PublicAPI]
        Version Version { get; }

        /// <summary>
        /// Initializes the SDK if it hasn't already.
        /// </summary>
        /// <remarks>
        /// Manually modifying the SDK init state is <b>untested</b>
        /// and may result in <emph>undefined behaviour</emph>, usage
        /// is at <b>your own risk</b>.
        /// </remarks>
        [PublicAPI]
        void Initialize();

        /// <summary>
        /// Uninitializes the SDK if it has been initialized.
        /// </summary>
        /// <remarks>
        /// Manually modifying the SDK init state is <b>untested</b>
        /// and may result in <emph>undefined behaviour</emph>, usage
        /// is at <b>your own risk</b>.
        /// </remarks>
        [PublicAPI]
        void Uninitialize();

        /// <summary>
        /// Queries the SDK for information regarding a specific device.
        /// </summary>
        /// <param name="deviceId">The device ID to query for, valid IDs can be found in <see cref="Devices" />.</param>
        /// <returns>A struct with information regarding the device type and whether it's connected.</returns>
        [PublicAPI]
        DeviceInfo Query(Guid deviceId);

        /// <summary>
        /// Gets an instance of <see cref="IGenericDevice" /> for
        /// the device with the specified ID.
        /// </summary>
        /// <param name="deviceId">
        /// The <see cref="Guid" /> of the device to get,
        /// valid IDs can be found in <see cref="Devices" />.
        /// </param>
        /// <returns>An instance of <see cref="IGenericDevice" />.</returns>
        [PublicAPI]
        IGenericDevice Get(Guid deviceId);

        /// <summary>
        /// Handles a Windows message and fires the appropriate events.
        /// </summary>
        /// <param name="handle">The <c>HWnd</c> property of the Message struct.</param>
        /// <param name="msgId">The <c>Msg</c> property of the Message struct.</param>
        /// <param name="wParam">The <c>wParam</c> property of the Message struct.</param>
        /// <param name="lParam">The <c>lParam</c> property of the Message struct.</param>
        /// <returns><c>true</c> if the message was handled by Chroma, <c>false</c> otherwise (message was ignored).</returns>
        /// <remarks>Non-Chroma messages will be ignored.</remarks>
        [PublicAPI]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation",
            Justification = "Parameter names match those in the Message struct.")]
        bool HandleMessage(IntPtr handle, int msgId, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Registers to start receiving Chroma events.
        /// </summary>
        /// <param name="handle">Handle to the application Window that is running the message loop.</param>
        /// <remarks>
        /// Chroma events are sent using the Windows message API, as such, there has to be something handling
        /// Windows messages to receive them. Messages need to be passed to the message handler in Colore to
        /// be processed, as this cannot be automated.
        /// </remarks>
        [PublicAPI]
        void Register(IntPtr handle);

        /// <summary>
        /// Unregisters from receiving Chroma events.
        /// </summary>
        [PublicAPI]
        void Unregister();

        /// <summary>
        /// Sets all Chroma devices to the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set.</param>
        [PublicAPI]
        void SetAll(Color color);
    }
}
