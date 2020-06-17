// ---------------------------------------------------------------------------------------
// <copyright file="GridLed.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Effects.Mouse
{
    using JetBrains.Annotations;

    /// <summary>
    /// LED definitions for the virtual grid.
    /// </summary>
    public enum GridLed
    {
        /// <summary>
        /// The LED illuminating the scroll wheel.
        /// </summary>
        [PublicAPI]
        ScrollWheel = 0x0203,

        /// <summary>
        /// The LED illuminating the logo present on the mouse.
        /// </summary>
        [PublicAPI]
        Logo = 0x0703,

        /// <summary>
        /// The mouse backlight.
        /// </summary>
        [PublicAPI]
        Backlight = 0x0403,

        /// <summary>
        /// First LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide1 = 0x0100,

        /// <summary>
        /// Second LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide2 = 0x0200,

        /// <summary>
        /// Third LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide3 = 0x0300,

        /// <summary>
        /// Fourth LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide4 = 0x0400,

        /// <summary>
        /// Fifth LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide5 = 0x0500,

        /// <summary>
        /// Sixth LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide6 = 0x0600,

        /// <summary>
        /// Seventh LED on left side.
        /// </summary>
        [PublicAPI]
        LeftSide7 = 0x0700,

        /// <summary>
        /// First bottom LED.
        /// </summary>
        [PublicAPI]
        Bottom1 = 0x0801,

        /// <summary>
        /// Second bottom LED.
        /// </summary>
        [PublicAPI]
        Bottom2 = 0x0802,

        /// <summary>
        /// Third bottom LED.
        /// </summary>
        [PublicAPI]
        Bottom3 = 0x0803,

        /// <summary>
        /// Fourth bottom LED.
        /// </summary>
        [PublicAPI]
        Bottom4 = 0x0804,

        /// <summary>
        /// Fifth bottom LED.
        /// </summary>
        [PublicAPI]
        Bottom5 = 0x0805,

        /// <summary>
        /// First LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide1 = 0x0106,

        /// <summary>
        /// Second LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide2 = 0x0206,

        /// <summary>
        /// Third LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide3 = 0x0306,

        /// <summary>
        /// Fourth LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide4 = 0x0406,

        /// <summary>
        /// Fifth LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide5 = 0x0506,

        /// <summary>
        /// Sixth LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide6 = 0x0606,

        /// <summary>
        /// Seventh LED on right side.
        /// </summary>
        [PublicAPI]
        RightSide7 = 0x0706
    }
}
