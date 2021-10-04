// ---------------------------------------------------------------------------------------
// <copyright file="ColoreProvider.cs" company="Corale">
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

namespace Colore
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Implementations;
    using Colore.Logging;
    using Colore.Native;
    using Colore.Rest;

    using JetBrains.Annotations;

    /// <summary>
    /// Provides helper methods to instantiate a new <see cref="IChroma" /> instance.
    /// </summary>
    [PublicAPI]
    public static class ColoreProvider
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.GetLogger(typeof(ColoreProvider));

        /// <summary>
        /// Keeps track of the currently initialized <see cref="IChroma" /> instance.
        /// </summary>
        private static IChroma? _instance;

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the native Razer Chroma SDK.
        /// </summary>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateNativeAsync()
        {
            Log.Debug("Creating new native IChroma instance");

            // The native SDK currently does not make use of application info
            return await CreateAsync(null, new NativeApi()).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the Chroma REST API.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="endpoint">The endpoint to use for initializing the Chroma SDK.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateRestAsync(AppInfo info, string endpoint = RestApi.DefaultEndpoint)
        {
            return await CreateRestAsync(info, new Uri(endpoint)).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the Chroma REST API.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="ssl">Whether to use the HTTPS endpoint for SDK communication.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static Task<IChroma> CreateRestAsync(AppInfo info, bool ssl)
        {
            var endpoint = ssl ? RestApi.DefaultSslEndpoint : RestApi.DefaultEndpoint;
            return CreateRestAsync(info, endpoint);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the Chroma REST API.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="endpoint">The endpoint to use for initializing the Chroma SDK.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        [SuppressMessage("ReSharper", "CA2000", Justification = "Caller can dispose allocated resources with IChroma.Dispose")]
        public static async Task<IChroma> CreateRestAsync(AppInfo info, Uri endpoint)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Log.DebugFormat("Creating new REST API IChroma instance at {0}", endpoint.ToString());

            return await CreateAsync(info, new RestApi(new RestClient(endpoint))).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the specified API instance.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="api">The API instance to use to route SDK calls.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateAsync(AppInfo? info, IChromaApi api)
        {
            await ClearCurrentAsync().ConfigureAwait(false);
            _instance = new ChromaImplementation(api, info);
            return _instance;
        }

        /// <summary>
        /// Clears the current <see cref="IChroma" /> instance, if necessary.
        /// </summary>
        /// <returns>An object representing the progress of this asynchronous task.</returns>
        private static async Task ClearCurrentAsync()
        {
            if (_instance is null)
            {
                return;
            }

            Log.Debug("Uninitializing current IChroma instance");
            await _instance.UninitializeAsync().ConfigureAwait(false);
        }
    }
}
