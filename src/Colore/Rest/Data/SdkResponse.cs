// ---------------------------------------------------------------------------------------
// <copyright file="SdkResponse.cs" company="Corale">
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
    using Colore.Data;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains simple result-only responses from the Razer Chroma REST API.
    /// </summary>
    internal class SdkResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SdkResponse" /> class.
        /// </summary>
        /// <param name="result">Result code.</param>
        [JsonConstructor]
        public SdkResponse(Result result)
        {
            Result = result;
        }

        /// <summary>
        /// Gets the result code obtained from the API call.
        /// </summary>
        [JsonProperty("result")]
        public Result Result { get; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Result.ToString();
    }
}
