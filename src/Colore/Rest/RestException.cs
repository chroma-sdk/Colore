// ---------------------------------------------------------------------------------------
// <copyright file="RestException.cs" company="Corale">
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

namespace Colore.Rest
{
    using System;
    using System.Net;

#if NET451
    using System.Runtime.Serialization;
#endif

    using Colore.Api;
    using Colore.Data;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Represents an error in the Chroma REST API.
    /// </summary>
#if NET451
    [Serializable]
#endif
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
            [CanBeNull] string restData = null)
            : base(message, result)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
            StatusCode = statusCode;
            RestData = restData;
        }

#if NET451
        /// <summary>
        /// Initializes a new instance of the <see cref="RestException" /> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="info" /> parameter is <see langword="null" />.
        /// </exception>
        /// <exception cref="SerializationException">
        /// The class name is <see langword="null" /> or <see cref="Exception.HResult" /> is zero (0).
        /// </exception>
        private RestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            var uriString = info.GetString($"{nameof(RestException)}.{nameof(Uri)}");
            var uriKind = (UriKind)info.GetInt32($"{nameof(RestException)}.{nameof(UriKind)}");
            Uri = uriString == null ? null : new Uri(uriString, uriKind);
            StatusCode = (HttpStatusCode)info.GetInt32($"{nameof(RestException)}.{nameof(StatusCode)}");
            RestData = info.GetString($"{nameof(RestException)}.{nameof(RestData)}");
        }
#endif

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
        public string RestData { get; }

#if NET451
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext" /> that contains contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="info" /> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            var isAbsolute = Uri?.IsAbsoluteUri == true;
            info.AddValue($"{nameof(RestException)}.{nameof(Uri)}", Uri?.ToString());
            info.AddValue(
                $"{nameof(RestException)}.{nameof(UriKind)}",
                (int)(isAbsolute ? UriKind.Absolute : UriKind.RelativeOrAbsolute));

            info.AddValue($"{nameof(RestException)}.{nameof(StatusCode)}", (int)StatusCode);
            info.AddValue($"{nameof(RestException)}.{nameof(RestData)}", RestData);
        }
#endif
    }
}
