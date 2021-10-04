// ---------------------------------------------------------------------------------------
// <copyright file="RestClient.cs" company="Corale">
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

namespace Colore.Rest
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Colore.Helpers;
    using Colore.Logging;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// An implementation of <see cref="IRestClient" />.
    /// </summary>
    internal sealed class RestClient : IRestClient
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<RestClient>();

        /// <summary>
        /// The underlying <see cref="HttpClient" /> performing all requests.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient" /> class.
        /// </summary>
        /// <param name="baseAddress">Base address to use.</param>
        public RestClient(string baseAddress)
            : this(new Uri(baseAddress))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient" /> class.
        /// </summary>
        /// <param name="baseAddress">Base address to use.</param>
        [SuppressMessage("ReSharper", "CA2000", Justification = "HttpMessageHandler is managed by HttpClient")]
        public RestClient(Uri baseAddress)
            : this(baseAddress, new HttpClientHandler())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestClient" /> class.
        /// </summary>
        /// <param name="baseAddress">Base address to use.</param>
        /// <param name="messageHandler">The message handler to use for the HTTP client.</param>
        public RestClient(Uri baseAddress, HttpMessageHandler messageHandler)
        {
            _httpClient = CreateClient(baseAddress, messageHandler);
            BaseAddress = baseAddress;
            Log.DebugFormat("REST client initialized at {0}", BaseAddress);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the base address where calls will be routed.
        /// </summary>
        public Uri BaseAddress { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Performs a POST request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}"/>.</returns>
        public async Task<IRestResponse<T>> PostAsync<T>(string resource, object? data)
        {
            var json = data is null ? null : JsonConvert.SerializeObject(data);
            using var content = json is null
                ? null
                : new StringContent(json, Encoding.UTF8, "application/json");

            var uri = CreateUri(resource);

            Log.TraceFormat("POSTing {0} to {1}", json, uri);

            var response = await _httpClient.PostAsync(uri, content).ConfigureAwait(false);
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new RestResponse<T>(response.StatusCode, body);
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs a PUT request to the specified resource without any data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        public async Task<IRestResponse<T>> PutAsync<T>(string resource)
        {
            return await PutAsync<T>(resource, null).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs a PUT request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        public async Task<IRestResponse<T>> PutAsync<T>(string resource, object? data)
        {
            using var content = data is null
                ? null
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var uri = CreateUri(resource);
            var response = await _httpClient.PutAsync(uri, content).ConfigureAwait(false);
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new RestResponse<T>(response.StatusCode, body);
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs a DELETE request to the specified resource.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        public async Task<IRestResponse<T>> DeleteAsync<T>(string resource)
        {
            var uri = CreateUri(resource);
            var response = await _httpClient.DeleteAsync(uri).ConfigureAwait(false);
            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new RestResponse<T>(response.StatusCode, body);
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs a DELETE request to the specified resource with the provided data.
        /// </summary>
        /// <typeparam name="T">The type of response to expect.</typeparam>
        /// <param name="resource">Resource path.</param>
        /// <param name="data">Request data.</param>
        /// <returns>An instance of <see cref="IRestResponse{TData}" />.</returns>
        public async Task<IRestResponse<T>> DeleteAsync<T>(string resource, object? data)
        {
            var content = data is null
                ? null
                : new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var uri = CreateUri(resource);

            using var request = new HttpRequestMessage(HttpMethod.Delete, uri) { Content = content };
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new RestResponse<T>(response.StatusCode, body);
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        /// Creates an instance of <see cref="HttpClient" /> for the specified address.
        /// </summary>
        /// <param name="baseAddress">The address to bind the client against.</param>
        /// <param name="messageHandler">The message handler to use for the HTTP client.</param>
        /// <returns>An instance of <see cref="HttpClient" />.</returns>
        private static HttpClient CreateClient(Uri baseAddress, HttpMessageHandler messageHandler)
        {
            Log.DebugFormat("Creating new HttpClient for {0}", baseAddress);
            return new HttpClient(messageHandler)
            {
                DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } }
            };
        }

        /// <summary>
        /// Creates an absolute SDK URI from the supplied resource.
        /// </summary>
        /// <param name="resource">The resource (relative SDK URI).</param>
        /// <returns>An instance of <see cref="Uri" /> with the absolute SDK URI.</returns>
        private Uri CreateUri(string resource) => CreateUri(new Uri(resource, UriKind.Relative));

        /// <summary>
        /// Creates an absolute SDK URI from the supplied resource URI.
        /// </summary>
        /// <param name="resource">The resource (relative SDK URI).</param>
        /// <returns>An instance of <see cref="Uri" /> with the absolute SDK URI.</returns>
        private Uri CreateUri(Uri resource) => BaseAddress.Append(resource);
    }
}
