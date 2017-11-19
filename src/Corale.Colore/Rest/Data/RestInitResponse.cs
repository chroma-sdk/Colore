// ---------------------------------------------------------------------------------------
// <copyright file="RestInitResponse.cs" company="Corale">
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

namespace Corale.Colore.Rest.Data
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Response returned from Chroma REST API on initialization.
    /// </summary>
    internal sealed class RestInitResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestInitResponse" /> class.
        /// </summary>
        /// <param name="session">Session ID.</param>
        /// <param name="uri">API URI.</param>
        [JsonConstructor]
        public RestInitResponse(int session, Uri uri)
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
        public Uri Uri { get; }
    }
}
