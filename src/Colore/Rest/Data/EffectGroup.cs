// ---------------------------------------------------------------------------------------
// <copyright file="EffectGroup.cs" company="Corale">
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

namespace Colore.Rest.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a collection of <see cref="EffectData" />.
    /// </summary>
    internal struct EffectGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EffectGroup" /> structure.
        /// </summary>
        /// <param name="effects"><see cref="EffectData" /> to include in the group.</param>
        internal EffectGroup(IEnumerable<EffectData> effects)
        {
            Effects = effects ?? throw new ArgumentNullException(nameof(effects));
        }

        /// <summary>
        /// Gets the various <see cref="EffectData" /> contained in this group.
        /// </summary>
        [JsonPropertyName("effects")]
        public IEnumerable<EffectData> Effects { get; }
    }
}
