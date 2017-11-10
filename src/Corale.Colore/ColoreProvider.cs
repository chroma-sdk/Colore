// ---------------------------------------------------------------------------------------
// <copyright file="ColoreProvider.cs" company="Corale">
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
    using Corale.Colore.Api;
    using Corale.Colore.Implementations;
    using Corale.Colore.Native;

    using JetBrains.Annotations;

    /// <summary>
    /// Provides helper methods to instantiate a new <see cref="IChroma" /> instance.
    /// </summary>
    [PublicAPI]
    public static class ColoreProvider
    {
        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the native Razer Chroma SDK.
        /// </summary>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static IChroma CreateNative()
        {
            return Create(new NativeApi());
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the specified API instance.
        /// </summary>
        /// <param name="api">The API instance to use to route SDK calls.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static IChroma Create(IChromaApi api)
        {
            return new ChromaImplementation(api);
        }
    }
}
