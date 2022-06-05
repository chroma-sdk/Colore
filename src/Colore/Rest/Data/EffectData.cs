// ---------------------------------------------------------------------------------------
// <copyright file="EffectData.cs" company="Corale">
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

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains data for an effect to be sent to the REST API.
    /// </summary>
    internal readonly struct EffectData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EffectData" /> structure.
        /// </summary>
        /// <param name="effect">Type of effect to create, should be a value from one of the <c>Effect</c> enumerations.</param>
        /// <param name="payload">Effect data, if applicable.</param>
        internal EffectData(object effect, object? payload = null)
        {
            Effect = effect ?? throw new ArgumentNullException(nameof(effect));
            Payload = payload;
        }

        /// <summary>
        /// Gets the type of the effect.
        /// </summary>
        [JsonProperty("effect")]
        public object Effect { get; }

        /// <summary>
        /// Gets effect data, or <c>null</c> if not applicable for the current effect type.
        /// </summary>
        [JsonProperty(
            "param",
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore)]
        public object? Payload { get; }
    }
}
