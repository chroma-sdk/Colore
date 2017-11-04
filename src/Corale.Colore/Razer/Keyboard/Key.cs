// ---------------------------------------------------------------------------------------
// <copyright file="Key.cs" company="Corale">
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

namespace Corale.Colore.Razer.Keyboard
{
    using JetBrains.Annotations;

    /// <summary>
    /// Definition of all keys available on the keyboard.
    /// </summary>
    public enum Key
    {
        /// <summary>
        /// The Razer logo on the keyboard.
        /// </summary>
        [PublicAPI]
        Logo = 0x0014,

        /// <summary>
        /// Esc key.
        /// </summary>
        [PublicAPI]
        Escape = 0x0001,

        /// <summary>
        /// F1 key.
        /// </summary>
        [PublicAPI]
        F1 = 0x0003,

        /// <summary>
        /// F2 key.
        /// </summary>
        [PublicAPI]
        F2 = 0x0004,

        /// <summary>
        /// F3 key.
        /// </summary>
        [PublicAPI]
        F3 = 0x0005,

        /// <summary>
        /// F4 key.
        /// </summary>
        [PublicAPI]
        F4 = 0x0006,

        /// <summary>
        /// F5 key.
        /// </summary>
        [PublicAPI]
        F5 = 0x0007,

        /// <summary>
        /// F6 key.
        /// </summary>
        [PublicAPI]
        F6 = 0x0008,

        /// <summary>
        /// F7 key.
        /// </summary>
        [PublicAPI]
        F7 = 0x0009,

        /// <summary>
        /// F8 key.
        /// </summary>
        [PublicAPI]
        F8 = 0x000A,

        /// <summary>
        /// F9 key.
        /// </summary>
        [PublicAPI]
        F9 = 0x000B,

        /// <summary>
        /// F10 key.
        /// </summary>
        [PublicAPI]
        F10 = 0x000C,

        /// <summary>
        /// F11 key.
        /// </summary>
        [PublicAPI]
        F11 = 0x000D,

        /// <summary>
        /// F12 key.
        /// </summary>
        [PublicAPI]
        F12 = 0x000E,

        /// <summary>
        /// 1 key.
        /// </summary>
        [PublicAPI]
        D1 = 0x0102,

        /// <summary>
        /// 2 key.
        /// </summary>
        [PublicAPI]
        D2 = 0x0103,

        /// <summary>
        /// 3 key.
        /// </summary>
        [PublicAPI]
        D3 = 0x0104,

        /// <summary>
        /// 4 key.
        /// </summary>
        [PublicAPI]
        D4 = 0x0105,

        /// <summary>
        /// 5 key.
        /// </summary>
        [PublicAPI]
        D5 = 0x0106,

        /// <summary>
        /// 6 key.
        /// </summary>
        [PublicAPI]
        D6 = 0x0107,

        /// <summary>
        /// 7 key.
        /// </summary>
        [PublicAPI]
        D7 = 0x0108,

        /// <summary>
        /// 8 key.
        /// </summary>
        [PublicAPI]
        D8 = 0x0109,

        /// <summary>
        /// 9 key.
        /// </summary>
        [PublicAPI]
        D9 = 0x010A,

        /// <summary>
        /// 0 key.
        /// </summary>
        [PublicAPI]
        D0 = 0x010B,

        /// <summary>
        /// A key.
        /// </summary>
        [PublicAPI]
        A = 0x0302,

        /// <summary>
        /// B key.
        /// </summary>
        [PublicAPI]
        B = 0x0407,

        /// <summary>
        /// C key.
        /// </summary>
        [PublicAPI]
        C = 0x0405,

        /// <summary>
        /// D key.
        /// </summary>
        [PublicAPI]
        D = 0x0304,

        /// <summary>
        /// E key.
        /// </summary>
        [PublicAPI]
        E = 0x0204,

        /// <summary>
        /// F key.
        /// </summary>
        [PublicAPI]
        F = 0x0305,

        /// <summary>
        /// G key.
        /// </summary>
        [PublicAPI]
        G = 0x0306,

        /// <summary>
        /// H key.
        /// </summary>
        [PublicAPI]
        H = 0x0307,

        /// <summary>
        /// I key.
        /// </summary>
        [PublicAPI]
        I = 0x0209,

        /// <summary>
        /// J key.
        /// </summary>
        [PublicAPI]
        J = 0x0308,

        /// <summary>
        /// K key.
        /// </summary>
        [PublicAPI]
        K = 0x0309,

        /// <summary>
        /// L key.
        /// </summary>
        [PublicAPI]
        L = 0x030A,

        /// <summary>
        /// M key.
        /// </summary>
        [PublicAPI]
        M = 0x0409,

        /// <summary>
        /// N key.
        /// </summary>
        [PublicAPI]
        N = 0x0408,

        /// <summary>
        /// O key.
        /// </summary>
        [PublicAPI]
        O = 0x020A,

        /// <summary>
        /// P key.
        /// </summary>
        [PublicAPI]
        P = 0x020B,

        /// <summary>
        /// Q key.
        /// </summary>
        [PublicAPI]
        Q = 0x0202,

        /// <summary>
        /// R key.
        /// </summary>
        [PublicAPI]
        R = 0x0205,

        /// <summary>
        /// S key.
        /// </summary>
        [PublicAPI]
        S = 0x0303,

        /// <summary>
        /// T key.
        /// </summary>
        [PublicAPI]
        T = 0x0206,

        /// <summary>
        /// U key.
        /// </summary>
        [PublicAPI]
        U = 0x0208,

        /// <summary>
        /// V key.
        /// </summary>
        [PublicAPI]
        V = 0x0406,

        /// <summary>
        /// W key.
        /// </summary>
        [PublicAPI]
        W = 0x0203,

        /// <summary>
        /// X key.
        /// </summary>
        [PublicAPI]
        X = 0x0404,

        /// <summary>
        /// Y key.
        /// </summary>
        [PublicAPI]
        Y = 0x0207,

        /// <summary>
        /// Z key.
        /// </summary>
        [PublicAPI]
        Z = 0x0403,

        /// <summary>
        /// Numlock key.
        /// </summary>
        [PublicAPI]
        NumLock = 0x0112,

        /// <summary>
        /// Numpad 0.
        /// </summary>
        [PublicAPI]
        Num0 = 0x0513,

        /// <summary>
        /// Numpad 1.
        /// </summary>
        [PublicAPI]
        Num1 = 0x0412,

        /// <summary>
        /// Numpad 2.
        /// </summary>
        [PublicAPI]
        Num2 = 0x0413,

        /// <summary>
        /// Numpad 3.
        /// </summary>
        [PublicAPI]
        Num3 = 0x0414,

        /// <summary>
        /// Numpad 4.
        /// </summary>
        [PublicAPI]
        Num4 = 0x0312,

        /// <summary>
        /// Numpad 5.
        /// </summary>
        [PublicAPI]
        Num5 = 0x0313,

        /// <summary>
        /// Numpad 6.
        /// </summary>
        [PublicAPI]
        Num6 = 0x0314,

        /// <summary>
        /// Numpad 7.
        /// </summary>
        [PublicAPI]
        Num7 = 0x0212,

        /// <summary>
        /// Numpad 8.
        /// </summary>
        [PublicAPI]
        Num8 = 0x0213,

        /// <summary>
        /// Numpad 9.
        /// </summary>
        [PublicAPI]
        Num9 = 0x0214,

        /// <summary>
        /// Divide key.
        /// </summary>
        [PublicAPI]
        NumDivide = 0x0113,

        /// <summary>
        /// Multiply key.
        /// </summary>
        [PublicAPI]
        NumMultiply = 0x0114,

        /// <summary>
        /// Subtract key.
        /// </summary>
        [PublicAPI]
        NumSubtract = 0x0115,

        /// <summary>
        /// Add key.
        /// </summary>
        [PublicAPI]
        NumAdd = 0x0215,

        /// <summary>
        /// Enter key.
        /// </summary>
        [PublicAPI]
        NumEnter = 0x0415,

        /// <summary>
        /// Decimal key.
        /// </summary>
        [PublicAPI]
        NumDecimal = 0x0514,

        /// <summary>
        /// Print Screen.
        /// </summary>
        [PublicAPI]
        PrintScreen = 0x000F,

        /// <summary>
        /// Scroll Lock.
        /// </summary>
        [PublicAPI]
        Scroll = 0x0010,

        /// <summary>
        /// Pause key.
        /// </summary>
        [PublicAPI]
        Pause = 0x0011,

        /// <summary>
        /// Insert key.
        /// </summary>
        [PublicAPI]
        Insert = 0x010F,

        /// <summary>
        /// Home key.
        /// </summary>
        [PublicAPI]
        Home = 0x0110,

        /// <summary>
        /// Page Up.
        /// </summary>
        [PublicAPI]
        PageUp = 0x0111,

        /// <summary>
        /// Delete key.
        /// </summary>
        [PublicAPI]
        Delete = 0x020f,

        /// <summary>
        /// End key.
        /// </summary>
        [PublicAPI]
        End = 0x0210,

        /// <summary>
        /// Page Down key.
        /// </summary>
        [PublicAPI]
        PageDown = 0x0211,

        /// <summary>
        /// Up key.
        /// </summary>
        [PublicAPI]
        Up = 0x0410,

        /// <summary>
        /// Left key.
        /// </summary>
        [PublicAPI]
        Left = 0x050F,

        /// <summary>
        /// Down key.
        /// </summary>
        [PublicAPI]
        Down = 0x0510,

        /// <summary>
        /// Right key.
        /// </summary>
        [PublicAPI]
        Right = 0x0511,

        /// <summary>
        /// Tab key.
        /// </summary>
        [PublicAPI]
        Tab = 0x0201,

        /// <summary>
        /// Caps Lock.
        /// </summary>
        [PublicAPI]
        CapsLock = 0x0301,

        /// <summary>
        /// Backspace key.
        /// </summary>
        [PublicAPI]
        Backspace = 0x010E,

        /// <summary>
        /// Enter key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        Enter = 0x030E,

        /// <summary>
        /// Left control key.
        /// </summary>
        [PublicAPI]
        LeftControl = 0x0501,

        /// <summary>
        /// Left windows key.
        /// </summary>
        [PublicAPI]
        LeftWindows = 0x0502,

        /// <summary>
        /// Left alt key.
        /// </summary>
        [PublicAPI]
        LeftAlt = 0x0503,

        /// <summary>
        /// Spacebar key.
        /// </summary>
        [PublicAPI]
        Space = 0x0507,

        /// <summary>
        /// Right alt key.
        /// </summary>
        [PublicAPI]
        RightAlt = 0x050B,

        /// <summary>
        /// "Fn" function key.
        /// </summary>
        [PublicAPI]
        Function = 0x050C,

        /// <summary>
        /// Right menu key.
        /// </summary>
        [PublicAPI]
        RightMenu = 0x050D,

        /// <summary>
        /// Right control key.
        /// </summary>
        [PublicAPI]
        RightControl = 0x050E,

        /// <summary>
        /// Left shift key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        LeftShift = 0x0401,

        /// <summary>
        /// Right shift key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        RightShift = 0x040E,

        /// <summary>
        /// Macro key 1.
        /// </summary>
        [PublicAPI]
        Macro1 = 0x0100,

        /// <summary>
        /// Macro key 2.
        /// </summary>
        [PublicAPI]
        Macro2 = 0x0200,

        /// <summary>
        /// Macro key 3.
        /// </summary>
        [PublicAPI]
        Macro3 = 0x0300,

        /// <summary>
        /// Macro key 4.
        /// </summary>
        [PublicAPI]
        Macro4 = 0x0400,

        /// <summary>
        /// Macro Key 5.
        /// </summary>
        [PublicAPI]
        Macro5 = 0x0500,

        /// <summary>
        /// Tilde (~) key. 半角/全角.
        /// </summary>
        [PublicAPI]
        OemTilde = 0x0101,

        /// <summary>
        /// Minus (-) key.
        /// </summary>
        [PublicAPI]
        OemMinus = 0x010C,

        /// <summary>
        /// Equal sign (=) key.
        /// </summary>
        [PublicAPI]
        OemEquals = 0x010D,

        /// <summary>
        /// Left square bracket ([) key.
        /// </summary>
        [PublicAPI]
        OemLeftBracket = 0x020C,

        /// <summary>
        /// Right square bracket (]) key.
        /// </summary>
        [PublicAPI]
        OemRightBracket = 0x020D,

        /// <summary>
        /// Backslash (\) key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        OemBackslash = 0x020E,

        /// <summary>
        /// Semi-colon (;) key.
        /// </summary>
        [PublicAPI]
        OemSemicolon = 0x030B,

        /// <summary>
        /// Apostrophe (') key.
        /// </summary>
        [PublicAPI]
        OemApostrophe = 0x030C,

        /// <summary>
        /// Comma (,) key.
        /// </summary>
        [PublicAPI]
        OemComma = 0x040A,

        /// <summary>
        /// Period/full stop (.) key.
        /// </summary>
        [PublicAPI]
        OemPeriod = 0x040B,

        /// <summary>
        /// Forwards slash (/) key.
        /// </summary>
        [PublicAPI]
        OemSlash = 0x040C,

        /// <summary>
        /// Pound sign (#) key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        EurPound = 0x030D,

        /// <summary>
        /// Backslash (\) key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        EurBackslash = 0x0402,

        /// <summary>
        /// Yen (¥) key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        JpnYen = 0x0015,

        /// <summary>
        /// Forward slash (/) key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        JpnSlash = 0x040D,

        /// <summary>
        /// 無変換 key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        Jpn3 = 0x0504,

        /// <summary>
        /// 変換 key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        Jpn4 = 0x0509,

        /// <summary>
        /// ひらがな/カタカナ key.
        /// </summary>
        /// <remarks>Unsafe key.</remarks>
        [PublicAPI]
        Jpn5 = 0x050A,

        /// <summary>
        /// Pipe character (|) key.
        /// </summary>
        [PublicAPI]
        KorPipe = 0x0015,

        /// <summary>
        /// Unknown Korean key.
        /// </summary>
        [PublicAPI]
        Kor2 = 0x030D,

        /// <summary>
        /// Unknown Korean key.
        /// </summary>
        [PublicAPI]
        Kor3 = 0x0402,

        /// <summary>
        /// Unknown Korean key.
        /// </summary>
        [PublicAPI]
        Kor4 = 0x040D,

        /// <summary>
        /// 한자 key.
        /// </summary>
        [PublicAPI]
        Kor5 = 0x0504,

        /// <summary>
        /// 한/영 key.
        /// </summary>
        [PublicAPI]
        Kor6 = 0x0509,

        /// <summary>
        /// Unknown Korean key.
        /// </summary>
        [PublicAPI]
        Kor7 = 0x050A,

        /// <summary>
        /// Invalid key.
        /// </summary>
        [PublicAPI]
        Invalid = 0xFFFF
    }
}
