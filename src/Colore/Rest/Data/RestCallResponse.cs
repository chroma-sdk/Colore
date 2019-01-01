// ---------------------------------------------------------------------------------------
// <copyright file="RestCallResponse.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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
    using System.Diagnostics.CodeAnalysis;

    using Colore.Data;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains responses from the Razer Chroma REST API.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class RestCallResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestCallResponse" /> class.
        /// </summary>
        /// <param name="result">Result code.</param>
        /// <param name="effectId">Effect ID (<c>null</c> if PUT was used).</param>
        [JsonConstructor]
        public RestCallResponse(Result result, Guid? effectId)
        {
            Result = result;
            EffectId = effectId;
        }

        /// <summary>
        /// Gets the result code obtained from the API call.
        /// </summary>
        [JsonProperty("result")]
        public Result Result { get; }

        /// <summary>
        /// Gets the effect ID obtained from the API call (will be <c>null</c> if PUT was used to create an effect).
        /// </summary>
        [CanBeNull]
        [JsonProperty("id")]
        public Guid? EffectId { get; }
    }
}
