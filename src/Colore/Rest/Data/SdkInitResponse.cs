// ---------------------------------------------------------------------------------------
// <copyright file="SdkInitResponse.cs" company="Corale">
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
    using System.Diagnostics.CodeAnalysis;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Response returned from Chroma REST API on initialization.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class SdkInitResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SdkInitResponse" /> class.
        /// </summary>
        /// <param name="session">Session ID.</param>
        /// <param name="uri">API URI.</param>
        [JsonConstructor]
        public SdkInitResponse(int session, Uri? uri)
        {
            Session = session;
            Uri = uri;
        }

        /// <summary>
        /// Gets the session ID.
        /// </summary>
        [JsonProperty("sessionid")]
        public int Session { get; }

        /// <summary>
        /// Gets the URI to use for subsequent API calls.
        /// </summary>
        [JsonProperty("uri")]
        public Uri? Uri { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"SDK Init[ SessionID={Session}; Uri={Uri} ]";
    }
}
