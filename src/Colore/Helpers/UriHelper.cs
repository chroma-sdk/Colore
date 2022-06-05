// ---------------------------------------------------------------------------------------
// <copyright file="UriHelper.cs" company="Corale">
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

namespace Colore.Helpers
{
    using System;

    using JetBrains.Annotations;

    /// <summary>
    /// Provides helper methods for working with URIs.
    /// </summary>
    internal static class UriHelper
    {
        /// <summary>
        /// Appends a <see cref="Uri" /> to another.
        /// </summary>
        /// <param name="uri">The "left" part of the new URI.</param>
        /// <param name="resource">The resource, or "right" part of the new URI.</param>
        /// <returns>
        /// A new <see cref="Uri" /> with <paramref name="resource" /> appended to <paramref name="uri" />.
        /// </returns>
        internal static Uri Append(this Uri uri, Uri resource)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            if (resource is null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            var left = uri.ToString().TrimEnd('/');
            var right = resource.ToString().TrimStart('/');
            return new Uri($"{left}/{right}");
        }
    }
}
