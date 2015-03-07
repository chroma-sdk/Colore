// ---------------------------------------------------------------------------------------
// <copyright file="Constants.cs" company="">
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
//     Disclaimer: Colore is in no way affiliated with Razer and/or any of its employees
//     and/or licensors. Adam Hellberg and Brandon Scott do not take responsibility
//     for any harm caused, direct or indirect, to any Razer peripherals
//     via the use of Colore.
// 
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

using System;

namespace Colore.Razer.Keyboard
{
    /// <summary>
    /// The definitions of generic constant values used in the project
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The maximum number of rows on the keyboard
        /// </summary>
        public static readonly Size MaxRow = 6;

        /// <summary>
        /// The maximum number of columns on the keyboard
        /// </summary>
        public static readonly Size MaxColumn = 22;

        /// <summary>
        /// The maximum number of keys on the keyboard
        /// </summary>
        public static readonly Size MaxKeys = MaxRow * MaxColumn;

         /// <summary>
        /// The maximum number of custom effects based on the maximum keys
        /// </summary>
        public static readonly Size MaxCustomEffects = MaxKeys;

        /// <summary>
        /// A grid representation of the keyboard
        /// </summary>
        //Todo: Speak with Razer to implement
        //public static readonly Int32 RZKEY_GRID_LAYOUT[MAX_ROW][MAX_COLUMN] = {};
    }
}
