// ---------------------------------------------------------------------------------------
// <copyright file="Mouse.cs" company="Corale">
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
    
    using Corale.Colore.Razer.Mouse;

    /// <summary>
    /// Class for interacting with a Chroma mouse.
    /// </summary>
    public class Mouse : IMouse
    {
        /// <summary>
        /// Holds the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        private static IMouse _instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="Mouse" /> class from being created.
        /// </summary>
        private Mouse()
        {
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        public static IMouse Instance
        {
            get
            {
                Chroma.Initialize();
                return _instance ?? (_instance = new Mouse());
            }
        }

        /// <summary>
        /// Sets the color of a specific LED on the mouse.
        /// </summary>
        /// <param name="led">Which LED to modify.</param>
        /// <param name="color">Color to set.</param>
        public void Set(Led led, Color color)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sets the color of all LEDs on the mouse.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public void Set(Color color)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the device to use the effect pointed to by the specified GUID.
        /// </summary>
        /// <param name="guid">Guid to set.</param>
        public void Set(Guid guid)
        {
            throw new NotImplementedException();
        }
    }
}
