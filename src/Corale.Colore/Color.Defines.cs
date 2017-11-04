// ---------------------------------------------------------------------------------------
// <copyright file="Color.Defines.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore
{
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents an RGB color.
    /// </summary>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage", Justification = "Nothing to inherit from.")]
    public partial struct Color
    {
        /// <summary>
        /// Black color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Black = new Color(0, 0, 0);

        /// <summary>
        /// (Dark) blue color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Blue = new Color(0, 0, 255);

        /// <summary>
        /// (Neon/bright) green color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Green = new Color(0, 255, 0);

        /// <summary>
        /// Hot pink color.
        /// </summary>
        [PublicAPI]
        public static readonly Color HotPink = new Color(255, 105, 180);

        /// <summary>
        /// Orange color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Orange = FromRgb(0xFFA500);

        /// <summary>
        /// Pink color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Pink = new Color(255, 0, 255);

        /// <summary>
        /// Purple color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Purple = FromRgb(0x800080);

        /// <summary>
        /// Red color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Red = new Color(255, 0, 0);

        /// <summary>
        /// White color.
        /// </summary>
        [PublicAPI]
        public static readonly Color White = new Color(255, 255, 255);

        /// <summary>
        /// Yellow color.
        /// </summary>
        [PublicAPI]
        public static readonly Color Yellow = new Color(255, 255, 0);
    }
}
