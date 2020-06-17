// ---------------------------------------------------------------------------------------
// <copyright file="IRestResponse.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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

    /// <summary>
    /// Represents a response from calling a REST API.
    /// </summary>
    /// <typeparam name="TData">The type contained in this response.</typeparam>
    [PublicAPI]
    public interface IRestResponse<out TData>
    {
        /// <summary>
        /// Gets the HTTP status of the response.
        /// </summary>
        HttpStatusCode Status { get; }

        /// <summary>
        /// Gets a value indicating whether the REST request was successful.
        /// </summary>
        bool IsSuccessful { get; }

        /// <summary>
        /// Gets the body content as a <see cref="string" />, or <c>null</c> if no content.
        /// </summary>
        [CanBeNull]
        string Content { get; }

        /// <summary>
        /// Gets the data returned from the request.
        /// </summary>
        [CanBeNull]
        TData Data { get; }

        /// <summary>
        /// Deserializes the response content into the specified type.
        /// </summary>
        /// <typeparam name="T">The type to deserialize into.</typeparam>
        /// <returns>An instance of <typeparamref name="T" />.</returns>
        [CanBeNull]
        T Deserialize<T>();
    }
}
