// ---------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Wpf
{
    using Corale.Colore.Annotations;

    using ColoreColor = Corale.Colore.Core.Color;
    using WpfColor = System.Windows.Media.Color;

    /// <summary>
    /// Extension methods for integrating Colore with WPF.
    /// </summary>
    [PublicAPI]
    public static class Extensions
    {
        /// <summary>
        /// Converts a WPF <see cref="WpfColor" /> to a
        /// Colore <see cref="ColoreColor" />.
        /// </summary>
        /// <param name="source">The color to convert.</param>
        /// <returns>
        /// A <see cref="ColoreColor" /> representing the
        /// given <see cref="WpfColor" />.
        /// </returns>
        public static ColoreColor ToColoreColor(this WpfColor source)
        {
            return new ColoreColor(source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts a Colore <see cref="ColoreColor" /> to a
        /// WPF <see cref="WpfColor" />
        /// </summary>
        /// <param name="source">The color to convert.</param>
        /// <returns>
        /// A <see cref="WpfColor" /> representing the
        /// given <see cref="ColoreColor" />.
        /// </returns>
        public static WpfColor ToWpfColor(this ColoreColor source)
        {
            return WpfColor.FromRgb(source.R, source.G, source.B);
        }
    }
}
