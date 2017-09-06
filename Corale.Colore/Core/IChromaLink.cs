// ---------------------------------------------------------------------------------------
// <copyright file="IChromaLink.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System.Collections.Generic;

    using Corale.Colore.Annotations;
    using Razer.ChromaLink.Effects;

    /// <summary>
    /// Interface for Chroma Link functionality.
    /// </summary>
    public interface IChromaLink
    {
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific zone on the Chroma Link device.
        /// The SDK will translate this appropriately depending on user configuration.
        /// </summary>
        /// <param name="index">The zone to access.</param>
        /// <returns>The color currently set for the specified key.</returns>
        [PublicAPI]
        Color this[int index] { get; set; }

        /// <summary>
        /// Sets an effect without any parameters.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [PublicAPI]
        void SetEffect(Effect effect);

        /// <summary>
        /// Returns whether an element has had a custom color set.
        /// </summary>
        /// <param name="index">The index to query.</param>
        /// <returns><c>true</c> if the position has a color set that is not black, otherwise <c>false</c>.</returns>
        [PublicAPI]
        bool IsSet(int index);

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [PublicAPI]
        void SetCustom(Custom effect);

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [PublicAPI]
        void SetStatic(Static effect);

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        [PublicAPI]
        void SetStatic(Color color);
    }
}
