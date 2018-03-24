// ---------------------------------------------------------------------------------------
// <copyright file="IRestClient.cs" company="Corale">
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
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// Represents a REST API client able to do basic HTTP requests and return response data.
    /// </summary>
    public interface IRestClient : IDisposable
    {
        /// <summary>
        /// Gets or sets the base address where calls will be routed.
        /// </summary>
        Uri BaseAddress { get; set; }

        /// <summary>
        /// Performs a POST request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        Task<IRestResponse<T>> PostAsync<T>(string resource, object data);

        /// <summary>
        /// Performs a PUT request to the specified resource without any data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        Task<IRestResponse<T>> PutAsync<T>(string resource);

        /// <summary>
        /// Performs a PUT request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        Task<IRestResponse<T>> PutAsync<T>(string resource, object data);

        /// <summary>
        /// Performs a DELETE request to the specified resource.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        Task<IRestResponse<T>> DeleteAsync<T>(string resource);

        /// <summary>
        /// Performs a DELETE request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        Task<IRestResponse<T>> DeleteAsync<T>(string resource, object data);
    }
}
