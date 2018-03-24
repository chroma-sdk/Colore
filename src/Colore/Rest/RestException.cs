// ---------------------------------------------------------------------------------------
// <copyright file="RestException.cs" company="Corale">
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
    using System;
    using System.Net;

    using Colore.Api;
    using Colore.Data;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Represents an error in the Chroma REST API.
    /// </summary>
    public sealed class RestException : ApiException
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException" /> class.
        /// </summary>
        [PublicAPI]
        public RestException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException" /> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        [PublicAPI]
        public RestException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException" /> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        [PublicAPI]
        public RestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException" /> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="result">The result code returned from the Chroma SDK.</param>
        /// <param name="uri">The REST API URI where the exception occurred.</param>
        /// <param name="statusCode">HTTP status code returned by the API.</param>
        /// <param name="restData">Any data returned in the API response body.</param>
        public RestException(
            string message,
            Result result,
            [NotNull] Uri uri,
            HttpStatusCode statusCode,
            object restData = null)
            : base(message, result)
        {
            Uri = uri;
            StatusCode = statusCode;
            RestData = restData;
        }

        /// <summary>
        /// Gets the <see cref="Uri" /> called which caused the exception.
        /// </summary>
        [CanBeNull]
        [PublicAPI]
        public Uri Uri { get; }

        /// <summary>
        /// Gets the <see cref="HttpStatusCode" /> returned by the API.
        /// </summary>
        [PublicAPI]
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the data (if any) returned by the API.
        /// </summary>
        [CanBeNull]
        [PublicAPI]
        public object RestData { get; }
    }
}
