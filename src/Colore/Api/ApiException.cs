// ---------------------------------------------------------------------------------------
// <copyright file="ApiException.cs" company="Corale">
//     Copyright © 2015-2021 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Api
{
    using System;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    using Colore.Data;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Thrown when a cal to an API function fails.
    /// </summary>
    [Serializable]
    public class ApiException : ColoreException
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class.
        /// </summary>
        [PublicAPI]
        public ApiException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        [PublicAPI]
        public ApiException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        /// <param name="innerException">Inner exception.</param>
        [PublicAPI]
        public ApiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class.
        /// </summary>
        /// <param name="message">An error message detailing the exception.</param>
        /// <param name="result">The result code returned from the SDK.</param>
        public ApiException(string message, Result result)
            : base(message, new Win32Exception(result))
        {
            Result = result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException" /> class with serialized data.
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
        protected ApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Result = info.GetInt32($"{nameof(ApiException)}.{nameof(Result)}");
        }

        /// <summary>
        /// Gets the result code returned by the SDK.
        /// </summary>
        [PublicAPI]
        public int Result { get; }

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

            info.AddValue($"{nameof(ApiException)}.{nameof(Result)}", Result);
        }
    }
}
