// ---------------------------------------------------------------------------------------
// <copyright file="Led.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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
    /// LEDs that can be the target of color changes.
    /// </summary>
#pragma warning disable CA1028 // Enum Storage should be Int32

    public enum Led : uint
#pragma warning restore CA1028 // Enum Storage should be Int32
    {
        /// <summary>
        /// No LED.
        /// </summary>
        [PublicAPI]
        None = 0,

        /// <summary>
        /// The LED illuminating the scroll wheel.
        /// </summary>
        [PublicAPI]
        ScrollWheel = 1,

        /// <summary>
        /// The LED illuminating the logo present on the mouse.
        /// </summary>
        [PublicAPI]
        Logo = 2,

        /// <summary>
        /// The mouse backlight.
        /// </summary>
        [PublicAPI]
        Backlight = 3,

        /// <summary>
        /// Side strip LED 1 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip1 = 4,

        /// <summary>
        /// Side strip LED 2 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip2 = 5,

        /// <summary>
        /// Side strip LED 3 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip3 = 6,

        /// <summary>
        /// Side strip LED 4 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip4 = 7,

        /// <summary>
        /// Side strip LED 5 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip5 = 8,

        /// <summary>
        /// Side strip LED 6 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip6 = 9,

        /// <summary>
        /// Side strip LED 7 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip7 = 10,

        /// <summary>
        /// Side strip LED 8 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip8 = 11,

        /// <summary>
        /// Side strip LED 9 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip9 = 12,

        /// <summary>
        /// Side strip LED 10 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip10 = 13,

        /// <summary>
        /// Side strip LED 11 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip11 = 14,

        /// <summary>
        /// Side strip LED 12 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip12 = 15,

        /// <summary>
        /// Side strip LED 13 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip13 = 16,

        /// <summary>
        /// Side strip LED 14 (Mamba TE).
        /// </summary>
        [PublicAPI]
        Strip14 = 17,

        /// <summary>
        /// All LEDs.
        /// </summary>
        [PublicAPI]
        All = 0xFFFF
    }
}
