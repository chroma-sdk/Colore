// ---------------------------------------------------------------------------------------
// <copyright file="Key.cs" company="">
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

namespace Colore.Razer.Keyboard
{
    /// <summary>
    /// Definition of all keys available on the keyboard.
    /// </summary>
    public enum Key
    {
        /// <summary>
        /// Esc.
        /// </summary>
        Escape = 0x0001,

        /// <summary>
        /// F1.
        /// </summary>
        F1 = 0x0003,

        /// <summary>
        /// F2.
        /// </summary>
        F2 = 0x0004,

        /// <summary>
        /// F3.
        /// </summary>
        F3 = 0x0005,

        /// <summary>
        /// F4.
        /// </summary>
        F4 = 0x0006,

        /// <summary>
        /// F5.
        /// </summary>
        F5 = 0x0007,

        /// <summary>
        /// F6.
        /// </summary>
        F6 = 0x0008,

        /// <summary>
        /// F7.
        /// </summary>
        F7 = 0x0009,

        /// <summary>
        /// F8.
        /// </summary>
        F8 = 0x000A,

        /// <summary>
        /// F9.
        /// </summary>
        F9 = 0x000B,

        /// <summary>
        /// F10.
        /// </summary>
        F10 = 0x000C,

        /// <summary>
        /// F11.
        /// </summary>
        F11 = 0x000D,

        /// <summary>
        /// F12.
        /// </summary>
        F12 = 0x000E,

        /// <summary>
        /// 1.
        /// </summary>
        One = 0x0102,

        /// <summary>
        /// 2.
        /// </summary>
        Two = 0x0103,

        /// <summary>
        /// 3.
        /// </summary>
        Three = 0x0104,

        /// <summary>
        /// 4.
        /// </summary>
        Four = 0x0105,

        /// <summary>
        /// 5.
        /// </summary>
        Five = 0x0106,

        /// <summary>
        /// 6.
        /// </summary>
        Six = 0x0107,

        /// <summary>
        /// 7.
        /// </summary>
        Seven = 0x0108,

        /// <summary>
        /// 8.
        /// </summary>
        Eight = 0x0109,

        /// <summary>
        /// 9.
        /// </summary>
        Nine = 0x010A,

        /// <summary>
        /// 0.
        /// </summary>
        Zero = 0x010B,

        /// <summary>
        /// A.
        /// </summary>
        A = 0x0302,

        /// <summary>
        /// B.
        /// </summary>
        B = 0x0407,

        /// <summary>
        /// C.
        /// </summary>
        C = 0x0405,

        /// <summary>
        /// D.
        /// </summary>
        D = 0x0304,

        /// <summary>
        /// E.
        /// </summary>
        E = 0x0204,

        /// <summary>
        /// F.
        /// </summary>
        F = 0x0305,

        /// <summary>
        /// G.
        /// </summary>
        G = 0x0306,

        /// <summary>
        /// H.
        /// </summary>
        H = 0x0307,

        /// <summary>
        /// I.
        /// </summary>
        I = 0x0209,

        /// <summary>
        /// J.
        /// </summary>
        J = 0x0308,

        /// <summary>
        /// K.
        /// </summary>
        K = 0x0309,

        /// <summary>
        /// L.
        /// </summary>
        L = 0x030A,

        /// <summary>
        /// M.
        /// </summary>
        M = 0x0409,

        /// <summary>
        /// N.
        /// </summary>
        N = 0x0408,

        /// <summary>
        /// O.
        /// </summary>
        O = 0x020A,

        /// <summary>
        /// P.
        /// </summary>
        P = 0x020B,

        /// <summary>
        /// Q.
        /// </summary>
        Q = 0x0202,

        /// <summary>
        /// R.
        /// </summary>
        R = 0x0205,

        /// <summary>
        /// S.
        /// </summary>
        S = 0x0303,

        /// <summary>
        /// T.
        /// </summary>
        T = 0x0206,

        /// <summary>
        /// U.
        /// </summary>
        U = 0x0208,

        /// <summary>
        /// V.
        /// </summary>
        V = 0x0406,

        /// <summary>
        /// W.
        /// </summary>
        W = 0x0203,

        /// <summary>
        /// X.
        /// </summary>
        X = 0x0404,

        /// <summary>
        /// Y.
        /// </summary>
        Y = 0x0207,

        /// <summary>
        /// Z.
        /// </summary>
        Z = 0x0403,

        /// <summary>
        /// Numlock.
        /// </summary>
        NumLock = 0x0112,

        /// <summary>
        /// Numpad 0.
        /// </summary>
        Num0 = 0x0513,

        /// <summary>
        /// Numpad 1.
        /// </summary>
        Num1 = 0x0412,

        /// <summary>
        /// Numpad 2.
        /// </summary>
        Num2 = 0x0413,

        /// <summary>
        /// Numpad 3.
        /// </summary>
        Num3 = 0x0414,

        /// <summary>
        /// Numpad 4.
        /// </summary>
        Num4 = 0x0312,

        /// <summary>
        /// Numpad 5.
        /// </summary>
        Num5 = 0x0313,

        /// <summary>
        /// Numpad 6.
        /// </summary>
        Num6 = 0x0314,

        /// <summary>
        /// Numpad 7.
        /// </summary>
        Num7 = 0x0212,

        /// <summary>
        /// Numpad 8.
        /// </summary>
        Num8 = 0x0213,

        /// <summary>
        /// Numpad 9.
        /// </summary>
        Num9 = 0x0214,

        /// <summary>
        /// Divide.
        /// </summary>
        NumDivide = 0x0113,

        /// <summary>
        /// Multiply.
        /// </summary>
        NumMultiply = 0x0114,

        /// <summary>
        /// Subtract.
        /// </summary>
        NumSubtract = 0x0115,

        /// <summary>
        /// Add.
        /// </summary>
        NumAdd = 0x0215,

        /// <summary>
        /// Enter.
        /// </summary>
        NumEnter = 0x0415,

        /// <summary>
        /// Decimal.
        /// </summary>
        NumDecimal = 0x0514,

        /// <summary>
        /// Print Screen.
        /// </summary>
        PrintScreen = 0x000F,

        /// <summary>
        /// Scroll Lock.
        /// </summary>
        Scroll = 0x0010,

        /// <summary>
        /// Pause.
        /// </summary>
        Pause = 0x0011,

        /// <summary>
        /// Insert.
        /// </summary>
        Insert = 0x010F,

        /// <summary>
        /// Home.
        /// </summary>
        Home = 0x0110,

        /// <summary>
        /// Page Up.
        /// </summary>
        PageUp = 0x0111,

        /// <summary>
        /// Delete.
        /// </summary>
        Delete = 0x020f,

        /// <summary>
        /// End.
        /// </summary>
        End = 0x0210,

        /// <summary>
        /// Page Down.
        /// </summary>
        PageDown = 0x0211,

        /// <summary>
        /// Up.
        /// </summary>
        Up = 0x0410,

        /// <summary>
        /// Left.
        /// </summary>
        Left = 0x050F,

        /// <summary>
        /// Down.
        /// </summary>
        Down = 0x0510,

        /// <summary>
        /// Right.
        /// </summary>
        Right = 0x0511,

        /// <summary>
        /// Tab.
        /// </summary>
        Tab = 0x0201,

        /// <summary>
        /// Caps Lock.
        /// </summary>
        CapsLock = 0x0301,

        /// <summary>
        /// Backspace.
        /// </summary>
        Backspace = 0x010E,

        /// <summary>
        /// Enter.
        /// </summary>
        Enter = 0x030E,

        /// <summary>
        /// Left Control.
        /// </summary>
        LeftControl = 0x0501,

        /// <summary>
        /// Left Window.
        /// </summary>
        LeftWindows = 0x0502,

        /// <summary>
        /// Left Alt.
        /// </summary>
        LeftAlt = 0x0503,

        /// <summary>
        /// Spacebar.
        /// </summary>
        Space = 0x0507,

        /// <summary>
        /// Right Alt.
        /// </summary>
        RightAlt = 0x050B,

        /// <summary>
        /// 'Fn' function key.
        /// </summary>
        Function = 0x050C,

        /// <summary>
        /// Right Menu.
        /// </summary>
        RightMenu = 0x050D,

        /// <summary>
        /// Right Control.
        /// </summary>
        RightControl = 0x050E,

        /// <summary>
        /// Left Shift.
        /// </summary>
        LeftShift = 0x0401,

        /// <summary>
        /// Right Shift.
        /// </summary>
        RightShift = 0x040E,

        /// <summary>
        /// Macro key 1.
        /// </summary>
        Macro1 = 0x0100,

        /// <summary>
        /// Macro key 2.
        /// </summary>
        Macro2 = 0x0200,

        /// <summary>
        /// Macro Key 3.
        /// </summary>
        Macro3 = 0x0300,

        /// <summary>
        /// Macro key 4.
        /// </summary> 
        Macro4 = 0x0400,

        /// <summary>
        /// Macro Key 5.
        /// </summary>
        Macro5 = 0x0500,

        /// <summary>
        /// ~ tilde. 半角/全角.
        /// </summary>
        Oem1 = 0x0101,

        /// <summary>
        /// - minus.
        /// </summary>
        Oem2 = 0x010C,

        /// <summary>
        /// = equal.
        /// </summary>
        Oem3 = 0x010D,

        /// <summary>
        /// [ left sqaure bracket.
        /// </summary>
        Oem4 = 0x020C,

        /// <summary>
        /// ] right square bracket.
        /// </summary>
        Oem5 = 0x020D,

        /// <summary>
        /// \ forward slash.
        /// </summary>
        Oem6 = 0x020E,

        /// <summary>
        /// ; semi-colon.
        /// </summary>
        Oem7 = 0x030B,

        /// <summary>
        /// ' apostrophe.
        /// </summary>
        Oem8 = 0x030C,

        /// <summary>
        /// ,comma.
        /// </summary>
        Oem9 = 0x040A,

        /// <summary>
        /// . period.
        /// </summary>
        Oem10 = 0x040B,

        /// <summary>
        /// / backslash.
        /// </summary>
        Oem11 = 0x040C,

        /// <summary>
        /// #.
        /// </summary>
        Eur1 = 0x030D,

        /// <summary>
        /// \.
        /// </summary>
        Eur2 = 0x0402,

        /// <summary>
        /// ¥.
        /// </summary>
        Jpn1 = 0x0015,

        /// <summary>
        /// /.
        /// </summary>
        Jpn2 = 0x040D,

        /// <summary>
        /// 無変換.
        /// </summary>
        Jpn3 = 0x0504,

        /// <summary>
        /// 変換.
        /// </summary>
        Jpn4 = 0x0509,

        /// <summary>
        /// ひらがな/カタカナ.
        /// </summary>
        Jpn5 = 0x050A,

        /// <summary>
        /// |.
        /// </summary>
        Kor1 = 0x0015,

        Kor2 = 0x030D,

        Kor3 = 0x0402,

        Kor4 = 0x040D,

        /// <summary>
        /// 한자.
        /// </summary>
        Kor5 = 0x0504,

        /// <summary>
        /// 한/영.
        /// </summary>
        Kor6 = 0x0509,

        Kor7 = 0x050A,

        /// <summary>
        /// Invalid keys.
        /// </summary>
        Invalid = 0xFFFF 
    };
}