// ---------------------------------------------------------------------------------------
// <copyright file="ApplicationStateEventArgs.cs" company="Corale">
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

namespace Corale.Colore.Events
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    /// Event arguments for the application state event.
    /// </summary>
    public sealed class ApplicationStateEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationStateEventArgs" /> class.
        /// </summary>
        /// <param name="enabled">Value indicating whether the application has been enabled.</param>
        public ApplicationStateEventArgs(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating whether or not the application has been enabled.
        /// </summary>
        [PublicAPI]
        public bool Enabled { get; }
    }
}
