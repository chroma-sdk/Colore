// ---------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Data
{
    using JetBrains.Annotations;

    /// <summary>
    /// The definitions of generic constant values used in the project
    /// </summary>
    [PublicAPI]
    public static class Constants
    {
        /// <summary>
        /// Maximum number of rows for a generic custom effect.
        /// </summary>
        public const int MaxRows = 30;

        /// <summary>
        /// Maximum number of columns for a generic custom effect.
        /// </summary>
        public const int MaxColumns = 30;

        /// <summary>
        /// Maximum number of color entries for a generic custom effect.
        /// </summary>
        public const int MaxColors = MaxRows * MaxColumns;

        /// <summary>
        /// Used by Razer code to send Chroma event messages.
        /// </summary>
        public const uint WmChromaEvent = WmApp + 0x2000;

        /// <summary>
        /// Max size of an application title.
        /// </summary>
        internal const int MaxAppTitleLength = 256;

        /// <summary>
        /// Max size of an application description.
        /// </summary>
        internal const int MaxAppDescriptionLength = 1024;

        /// <summary>
        /// Max size of an author name.
        /// </summary>
        internal const int MaxAuthorNameLength = 256;

        /// <summary>
        /// Max size of an author's contact information.
        /// </summary>
        internal const int MaxAuthorContactLength = 256;

        /// <summary>
        /// Used to define private messages, usually of the form WM_APP+x, where x is an integer value.
        /// </summary>
        /// <remarks>
        /// The <strong>WM_APP</strong> constant is used to distinguish between message values
        /// that are reserved for use by the system and values that can be used by an
        /// application to send messages within a private window class.
        /// </remarks>
        private const uint WmApp = 0x8000;
    }
}
