//  ---------------------------------------------------------------------------------------
//  <copyright file="ChromaAPI.cs" company="">
//      Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of
//      this software and associated documentation files (the "Software"), to deal in
//      the Software without restriction, including without limitation the rights to
//      use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//      of the Software, and to permit persons to whom the Software is furnished to do
//      so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//      WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//      Disclaimer: Colore is in no way affiliated
//      with Razer and/or any of its employees and/or licensors.
//      Adam Hellberg and Brandon Scott do not take responsibility for any harm caused, direct
//      or indirect, to any Razer peripherals via the use of Colore.
//
//      "Razer" is a trademark of Razer USA Ltd.
//  </copyright>
//  ---------------------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;

namespace Colore.Native
{
    using Colore.Razer;
    using Colore.Razer.Keyboard;

    public static class ChromaAPI
    {
        #region Functions

        internal static class NativeMethods
        {
            /// <summary>
            /// The DLL containing the Chroma SDK functions.
            /// </summary>
            private const string DllName = "RzChromaSDK64.dll";

            [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
            private static extern int LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

            [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
            private static extern IntPtr GetProcAddress(int hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

            
        }

        #endregion Functions

        #region Enumerations

        //! Maximum number of rows in a keyboard.
        internal const int MAX_ROW = 6;

        //! Maximum number of columns in a keyboard.
        private const int MAX_COLUMN = 22;

        //! Maximum number of keys.
        private const int MAX_KEYS = MAX_ROW * MAX_COLUMN;

        //! Maximum number of custom effects.
        internal const int MAX_CUSTOM_EFFECTS = MAX_KEYS;

        //! Keyboard grid layout.
        //const int RZKEY_GRID_LAYOUT[MAX_ROW][MAX_COLUMN] = {};

            //! Chroma keyboard effect types

        public struct WAVE_EFFECT_TYPE
        {
            //! Direction of the wave effect.
            private enum Direction
            {
                DIRECTION_NONE = 0, //!< No direction.
                DIRECTION_LEFT_TO_RIGHT, //!< Left to right.
                DIRECTION_RIGHT_TO_LEFT, //!< Right to left.
                DIRECTION_INVALID //!< Invalid direction.
            } //!< Direction of the wave.
        }

        //! Breathing effect type
        public struct BREATHING_EFFECT_TYPE
        {
            private int Color1;    //!< First color.
            private int Color2;    //!< Second color.
        }

        //! Reactive effect type
        public struct REACTIVE_EFFECT_TYPE
        {
            private int Color;         //!< Color of the effect

            //! Duration of the effect.
            private enum Duration
            {
                DURATION_NONE = 0, //!< No duration.
                DURATION_SHORT, //!< Short duration.
                DURATION_MEDIUM, //!< Medium duration.
                DURATION_LONG, //!< Long duration.
                DURATION_INVALID //!< Invalid duraiont
            }
        }

        private struct STATIC_EFFECT_TYPE
        {
            private int Color;
        }

        //! Custom effect using RZKEY type
        public struct CUSTOM_EFFECT_TYPE
        {
            private Key Key;
            private int Color;
        }

        public struct CUSTOM_GRID_EFFECT_TYPE
        {
            private int Key[MAX_ROW][MAX_COLUMN];
        }

        #endregion Enumerations
    }
}
