// ---------------------------------------------------------------------------------------
// <copyright file="DeviceAccessEventArgs.cs" company="Corale">
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

namespace Corale.Colore.Events
{
    using System;

    using Corale.Colore.Annotations;

    /// <summary>
    /// Event arguments for the device access event.
    /// </summary>
    public sealed class DeviceAccessEventArgs : EventArgs
    {
        /// <summary>
        /// Whether or not device access has been granted.
        /// </summary>
        private readonly bool _granted;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceAccessEventArgs" /> class.
        /// </summary>
        /// <param name="granted">Value indicating whether device access was granted.</param>
        internal DeviceAccessEventArgs(bool granted)
        {
            _granted = granted;
        }

        /// <summary>
        /// Gets a value indicating whether device access has been granted.
        /// </summary>
        [PublicAPI]
        public bool Granted
        {
            get
            {
                return _granted;
            }
        }
    }
}
