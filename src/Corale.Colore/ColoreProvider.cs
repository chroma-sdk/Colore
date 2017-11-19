// ---------------------------------------------------------------------------------------
// <copyright file="ColoreProvider.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore
{
    using System;
    using System.Threading.Tasks;

    using Common.Logging;

    using Corale.Colore.Api;
    using Corale.Colore.Data;
    using Corale.Colore.Implementations;
    using Corale.Colore.Native;
    using Corale.Colore.Rest;

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
        private static readonly ILog Log = LogManager.GetLogger(typeof(ColoreProvider));

        /// <summary>
        /// Keeps track of the currently initialized <see cref="IChroma" /> instance.
        /// </summary>
        private static IChroma _instance;

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the native Razer Chroma SDK.
        /// </summary>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateNative()
        {
            Log.Debug("Creating new native IChroma instance");

            // The native SDK currently does not make use of application info
            return await Create(null, new NativeApi()).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the Chroma REST API.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="endpoint">The endpoint to use for initializing the Chroma SDK.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateRest(AppInfo info, string endpoint = RestApi.DefaultEndpoint)
        {
            return await CreateRest(info, new Uri(endpoint)).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the Chroma REST API.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="endpoint">The endpoint to use for initializing the Chroma SDK.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> CreateRest(AppInfo info, Uri endpoint)
        {
            Log.DebugFormat("Creating new REST API IChroma instance at {0}", endpoint.ToString());
            return await Create(info, new RestApi(new RestClient(endpoint))).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a new <see cref="IChroma" /> instance using the specified API instance.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <param name="api">The API instance to use to route SDK calls.</param>
        /// <returns>A new instance of <see cref="IChroma" />.</returns>
        public static async Task<IChroma> Create(AppInfo info, IChromaApi api)
        {
            await ClearCurrent().ConfigureAwait(false);
            _instance = new ChromaImplementation(api, info);
            return _instance;
        }

        /// <summary>
        /// Clears the current <see cref="IChroma" /> instance, if necessary.
        /// </summary>
        /// <returns>An object representing the progress of this asynchronous task.</returns>
        private static async Task ClearCurrent()
        {
            if (_instance == null)
                return;

            Log.Debug("Uninitializing current IChroma instance");
            await _instance.UninitializeAsync().ConfigureAwait(false);
        }
    }
}
