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

    /// <summary>
    /// Interface for basic Chroma functionality.
    /// </summary>
    public interface IChroma
    {
        /// <summary>
        /// Gets an instance of the <see cref="IKeyboard" /> interface
        /// for interacting with a Razer Chroma keyboard.
        /// </summary>
        IKeyboard Keyboard { get; }

        /// <summary>
        /// Gets an instance of the <see cref="IMouse" /> interface
        /// for interacting with a Razer Chroma mouse.
        /// </summary>
        IMouse Mouse { get; }

        /// <summary>
        /// Handles a Windows message and fires the appropriate events.
        /// </summary>
        /// <param name="msgId">The <c>Msg</c> property of the Message struct.</param>
        /// <param name="wParam">The <c>wParam</c> property of the Message struct.</param>
        /// <param name="lParam">The <c>lParam</c> property of the Message struct.</param>
        /// <remarks>Non-Chroma messages will be ignored.</remarks>
        void HandleMessage(int msgId, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Registers to start receiving Chroma events.
        /// </summary>
        /// <remarks>
        /// Chroma events are sent using the Windows message API, as such, there has to be something handling
        /// Windows messages to receive them. Messages need to be passed to the message handler in Colore to
        /// be processed, as this cannot be automated.
        /// </remarks>
        void Register();

        /// <summary>
        /// Unregisters from receiving Chroma events.
        /// </summary>
        void Unregister();
    }
}
