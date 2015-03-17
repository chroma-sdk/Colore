// ---------------------------------------------------------------------------------------
// <copyright file="IChroma.cs" company="Corale">
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

    using Corale.Colore.Annotations;
    using Corale.Colore.Events;

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
    }
}
