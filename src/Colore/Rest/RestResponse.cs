// ---------------------------------------------------------------------------------------
// <copyright file="RestResponse.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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
namespace Colore.Rest
{
    using System.Net;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Contains the response from calling a REST API method.
    /// </summary>
    /// <typeparam name="TData">The type contained in this response.</typeparam>
    internal sealed class RestResponse<TData> : IRestResponse<TData>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponse{TData}" /> class.
        /// </summary>
        /// <param name="status">HTTP status returned from the API.</param>
        /// <param name="content">String content returned from the API.</param>
        public RestResponse(HttpStatusCode status, [CanBeNull] string content)
        {
            Status = status;
            Content = content;

            var code = (int)Status;
            IsSuccessful = code >= 200 && code < 300;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets the HTTP status of the response.
        /// </summary>
        public HttpStatusCode Status { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        public bool IsSuccessful { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the string content of the response body.
        /// </summary>
        public string Content { get; }

        /// <inheritdoc />
        /// <summary>
        /// Gets the typed data returned from the request.
        /// </summary>
        public TData Data => Deserialize<TData>();

        /// <inheritdoc />
        /// <summary>
        /// Attempts to deserialize <see cref="Content" /> into the target type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        public T Deserialize<T>()
        {
            if (string.IsNullOrWhiteSpace(Content))
                return default(T);

            try
            {
                return JsonConvert.DeserializeObject<T>(Content);
            }
            catch (JsonSerializationException)
            {
                return default(T);
            }
        }
    }
}
