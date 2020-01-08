// ---------------------------------------------------------------------------------------
// <copyright file="ColoreException.cs" company="Corale">
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

namespace Colore
{
    using System;

#if NET451
    using System.Runtime.Serialization;
#endif

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Generic Colore library exception.
    /// </summary>
#if NET451
    [Serializable]
#endif
    public class ColoreException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ColoreException" /> class.
        /// </summary>
        [PublicAPI]
        public ColoreException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ColoreException" /> class.
        /// </summary>
        /// <param name="message">Message describing the exception.</param>
        [PublicAPI]
        public ColoreException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ColoreException" /> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="innerException">Inner exception object.</param>
        public ColoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if NET451
        /// <summary>
        /// Initializes a new instance of the <see cref="ColoreException" /> class with serialized data.
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
        protected ColoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}
