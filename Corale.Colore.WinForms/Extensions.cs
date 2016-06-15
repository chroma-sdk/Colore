// <copyright file="Extensions.cs" company="Corale">
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

    /// <summary>
    /// Extension methods for integrating Colore with WinForms.
    /// </summary>
    [PublicAPI]
    public static class Extensions
    {
        /// <summary>
        /// Converts a System.Drawing <see cref="SystemColor" /> to a
        /// Colore <see cref="ColoreColor" />.
        /// </summary>
        /// <param name="source">The color to convert.</param>
        /// <returns>
        /// A <see cref="ColoreColor" /> representing the
        /// given <see cref="SystemColor" />.
        /// </returns>
        public static ColoreColor ToColoreColor(this SystemColor source)
        {
            return new ColoreColor(source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts a Colore <see cref="ColoreColor" /> to a
        /// System.Drawing <see cref="WpfColor" />
        /// </summary>
        /// <param name="source">The color to convert.</param>
        /// <returns>
        /// A <see cref="SystemColor" /> representing the
        /// given <see cref="ColoreColor" />.
        /// </returns>
        public static SystemColor ToSystemColor(this ColoreColor source)
        {
            return SystemColor.FromRgb(source.R, source.G, source.B);
        }
    }
}
