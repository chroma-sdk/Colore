// <copyright file="Color.cs" company="Corale">
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
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>

namespace Corale.Colore.WinForms
{
    using Corale.Colore.Annotations;

    using ColoreColor = Corale.Colore.Core.Color;

    using SystemColor = System.Drawing.Color;

    [PublicAPI]
    public struct Color
    {
        private readonly SystemColor _internal;

        public Color(SystemColor source)
        {
            _internal = source;
        }

        public static implicit operator ColoreColor(Color color)
        {
            return new ColoreColor(color._internal.R, color._internal.G, color._internal.B);
        }

        public static implicit operator Color(SystemColor color)
        {
            return new Color(color);
        }

        public static implicit operator SystemColor(Color color)
        {
            return color._internal;
        }

        public static implicit operator Color(ColoreColor color)
        {
            return new Color(SystemColor.FromArgb(color.R, color.G, color.B));
        }
    }
}
