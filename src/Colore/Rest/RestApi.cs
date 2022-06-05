// ---------------------------------------------------------------------------------------
// <copyright file="RestApi.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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
    using System.Threading;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Generic;
    using Colore.Logging;
    using Colore.Rest.Data;

    /// <inheritdoc cref="IChromaApi" />
    /// <summary>
    /// An implementation of the REST API backend for the Chroma SDK.
    /// </summary>
    internal sealed class RestApi : IChromaApi, IDisposable
    {
        /// <summary>
        /// Default endpoint for accessing the Chroma SDK on the local machine over HTTP.
        /// </summary>
        internal const string DefaultEndpoint = "http://localhost:54235";

        /// <summary>
        /// Default endpoint for accessing the Chroma SDK on the local machine over HTTPS.
        /// </summary>
        internal const string DefaultSslEndpoint = "https://localhost:54236";

        /// <summary>
        /// Interval (in milliseconds) to wait between each heartbeat call.
        /// </summary>
        private const int HeartbeatInterval = 1000;

        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<RestApi>();

        /// <summary>
        /// Underlying <see cref="IRestClient" /> used for API calls.
        /// </summary>
        private readonly IRestClient _client;

        /// <summary>
        /// Timer to dispatch regular heartbeat calls.
        /// </summary>
        private readonly Timer _heartbeatTimer;

        /// <summary>
        /// The original base URI for the Chroma REST API.
        /// </summary>
        /// <remarks>
        /// Used for calls to init if the SDK was previously initialized,
        /// to restore the original base address before the new init call.
        /// </remarks>
        private readonly Uri _baseUri;

        /// <summary>
        /// Keeps track of current session ID.
        /// </summary>
        private int _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestApi" /> class.
        /// </summary>
        /// <param name="client">The instance of <see cref="IRestClient" /> to use for API calls.</param>
        public RestApi(IRestClient client)
        {
            Log.InfoFormat("Initializing REST API client at {0}", client.BaseAddress);
            _client = client;
            _baseUri = client.BaseAddress;
            _heartbeatTimer = new Timer(_ => SendHeartbeat(), null, Timeout.Infinite, HeartbeatInterval);
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes the Chroma SDK by sending a POST request to <c>/razer/chromasdk</c>.
        /// </summary>
        /// <param name="info">Information about the application.</param>
        /// <returns>An object representing the progress of this asynchronous task.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="info" /> is <c>null</c>.</exception>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        public async Task InitializeAsync(AppInfo? info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Log.Info("Initializing SDK via /razer/chromasdk endpoint");

            _client.BaseAddress = _baseUri;

            var response = await _client.PostAsync<SdkInitResponse>("/razer/chromasdk", info).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                var ex = new RestException(
                    "Failed to initialize Chroma REST API",
                    Result.RzFailed,
                    new Uri(_client.BaseAddress, "/razer/chromasdk"),
                    response.Status);

                Log.Error(ex, "Chroma SDK initialization failed");
                throw ex;
            }

            var data = response.Data;

            if (data is null)
            {
                var ex = new RestException(
                    "REST API returned NULL data",
                    Result.RzFailed,
                    new Uri(_client.BaseAddress, "/razer/chromasdk"),
                    response.Status);

                Log.Error(ex, "Got NULL data from REST API");
                throw ex;
            }

            if (data.Session == 0 || data.Uri == default)
            {
                // Attempt to deserialize into SdkResponse to handle Razer's invalid REST API
                if (!string.IsNullOrWhiteSpace(response.Content))
                {
                    var sdkResponse = response.Deserialize<SdkResponse>();

                    if (sdkResponse != default)
                    {
                        throw new ApiException("Exception when calling initialize API", sdkResponse.Result);
                    }
                }

                var ex = new RestException(
                    "REST API returned invalid session ID or URL",
                    Result.RzFailed,
                    new Uri(_client.BaseAddress, "/razer/chromasdk"),
                    response.Status,
                    data.ToString());

                Log.Error(ex, "Got invalid session response from REST init API");

                throw ex;
            }

            _session = data.Session;
            _client.BaseAddress = data.Uri;

            Log.InfoFormat("New REST API session {0} at {1}", _session, _client.BaseAddress);
            Log.Debug("Starting heartbeat timer");
            _heartbeatTimer.Change(HeartbeatInterval, HeartbeatInterval);
        }

        /// <inheritdoc />
        /// <summary>
        /// Uninitializes the Chroma SDK by sending a DELETE request to <c>/</c>.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        /// <exception cref="ApiException">Thrown if the SDK responds with an error code.</exception>
        public async Task UninitializeAsync()
        {
            var response = await _client.DeleteAsync<SdkResponse>("/").ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                Log.Error("Chroma SDK uninitialization failed");
                throw new RestException(
                    "Failed to uninitialize Chroma REST API",
                    response.Data?.Result ?? Result.RzFailed,
                    new Uri(_client.BaseAddress, "/"),
                    response.Status,
                    response.Data?.ToString());
            }

            var data = response.Data;

            if (data is null)
            {
                throw new ApiException("Uninitialize API returned NULL response");
            }

            if (!data.Result)
            {
                throw new ApiException("Exception when calling uninitialize API", data.Result);
            }

            Log.Debug("Stopping heartbeat timer");
            _heartbeatTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <inheritdoc />
        public Task<SdkDeviceInfo> QueryDeviceAsync(Guid deviceId)
        {
            throw new NotSupportedException("Chroma REST API does not support device querying");
        }

        /// <inheritdoc />
        /// <summary>
        /// Set effect by sending a PUT request to <c>/effect</c>.
        /// </summary>
        /// <param name="effectId">Effect ID to set.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        /// <exception cref="ApiException">Thrown if the SDK responds with an error code.</exception>
        public async Task SetEffectAsync(Guid effectId)
        {
            var response = await _client.PutAsync<SdkResponse>("/effect", new { id = effectId })
                                        .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                Log.Error("Failed to set effect ID");
                throw new RestException(
                    "Failed to set effect ID",
                    response.Data?.Result ?? Result.RzFailed,
                    new Uri(_client.BaseAddress, "/effect"),
                    response.Status,
                    response.Data?.ToString());
            }

            var data = response.Data;

            if (data is null)
            {
                throw new ApiException("SetEffect API returned NULL response");
            }

            if (!data.Result)
            {
                throw new ApiException("Exception when calling SetEffect API", data.Result);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Deletes an effect with the specified <see cref="Guid" /> by sending a DELETE request to <c>/effect</c>.
        /// </summary>
        /// <param name="effectId">Effect ID to delete.</param>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        /// <exception cref="ApiException">Thrown if the SDK responds with an error code.</exception>
        public async Task DeleteEffectAsync(Guid effectId)
        {
            var response = await _client.DeleteAsync<SdkResponse>("/effect", new { id = effectId })
                                        .ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                Log.Error("Failed to delete effect ID");
                throw new RestException(
                    "Failed to delete effect ID",
                    response.Data?.Result ?? Result.RzFailed,
                    new Uri(_client.BaseAddress, "/effect"),
                    response.Status,
                    response.Data?.ToString());
            }

            var data = response.Data;

            if (data is null)
            {
                throw new ApiException("DeleteEffect API returned NULL response");
            }

            if (!data.Result)
            {
                throw new ApiException("Exception when calling DeleteEffect API", data.Result);
            }
        }

        /// <inheritdoc />
        public Task<Guid> CreateDeviceEffectAsync(Guid deviceId, EffectType effectType)
        {
            throw new NotSupportedException("Chroma REST API does not support generic device effects");
        }

        /// <inheritdoc />
        public Task<Guid> CreateDeviceEffectAsync<T>(Guid deviceId, EffectType effectType, T data) where T : struct
        {
            throw new NotSupportedException("Chroma REST API does not support generic device effects");
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keyboard effect without any effect data by sending a POST request to the keyboard API.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateKeyboardEffectAsync(Effects.Keyboard.KeyboardEffectType effectType)
        {
            return await CreateEffectAsync("/keyboard", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keyboard effect with the specified effect data by sending a POST request to the keyboard API.
        /// </summary>
        /// <typeparam name="T">The structure type, needs to be compatible with the effect type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">The effect structure parameter.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateKeyboardEffectAsync<T>(Effects.Keyboard.KeyboardEffectType effectType, T data)
            where T : struct
        {
            return await CreateEffectAsync("/keyboard", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mouse effect without any effect data by sending a POST request to the mouse API.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateMouseEffectAsync(Effects.Mouse.MouseEffectType effectType)
        {
            return await CreateEffectAsync("/mouse", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mouse effect with the specified effect data by sending a POST request to the mouse API.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateMouseEffectAsync<T>(Effects.Mouse.MouseEffectType effectType, T data)
            where T : struct
        {
            return await CreateEffectAsync("/mouse", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new headset effect without any effect data by sending a POST request to the headset API.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateHeadsetEffectAsync(Effects.Headset.HeadsetEffectType effectType)
        {
            return await CreateEffectAsync("/headset", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new headset effect with the specified effect data by sending a POST request to the headset API.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateHeadsetEffectAsync<T>(Effects.Headset.HeadsetEffectType effectType, T data)
            where T : struct
        {
            return await CreateEffectAsync("/headset", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mousepad effect without any effect data by sending a POST request to the mousepad API.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateMousepadEffectAsync(Effects.Mousepad.MousepadEffectType effectType)
        {
            return await CreateEffectAsync("/mousepad", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new mousepad effect with the specified effect data by sending a POST request to the mousepad API.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateMousepadEffectAsync<T>(Effects.Mousepad.MousepadEffectType effectType, T data)
            where T : struct
        {
            return await CreateEffectAsync("/mousepad", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keypad effect without any effect data by sending a POST request to the keypad API.
        /// </summary>
        /// <param name="effectType">THe type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateKeypadEffectAsync(Effects.Keypad.KeypadEffectType effectType)
        {
            return await CreateEffectAsync("/keypad", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new keypad effect with the specified effect data by sending a POST request to the keypad API.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateKeypadEffectAsync<T>(Effects.Keypad.KeypadEffectType effectType, T data)
            where T : struct
        {
            return await CreateEffectAsync("/keypad", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new Chroma Link effect without any effect data by sending a POST request to the Chroma Link API.
        /// </summary>
        /// <param name="effectType">The type of effect to create.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateChromaLinkEffectAsync(Effects.ChromaLink.ChromaLinkEffectType effectType)
        {
            return await CreateEffectAsync("/chromalink", new EffectData(effectType)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new Chroma Link effect with the specified effect data by sending a POST request to the Chroma Link API.
        /// </summary>
        /// <typeparam name="T">The effect struct type.</typeparam>
        /// <param name="effectType">The type of effect to create.</param>
        /// <param name="data">Effect options struct.</param>
        /// <returns>A <see cref="Guid" /> for the created effect.</returns>
        public async Task<Guid> CreateChromaLinkEffectAsync<T>(
            Effects.ChromaLink.ChromaLinkEffectType effectType,
            T data) where T : struct
        {
            return await CreateEffectAsync("/chromalink", data).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public void RegisterEventNotifications(IntPtr windowHandle)
        {
            throw new NotSupportedException("Event notifications are not supported in Chroma REST API");
        }

        /// <inheritdoc />
        public void UnregisterEventNotifications()
        {
            throw new NotSupportedException("Event notifications are not supported in Chroma REST API");
        }

        /// <inheritdoc />
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        [SuppressMessage(
            "ReSharper",
            "ConditionalAccessQualifierIsNonNullableAccordingToAPIContract",
            Justification = "Could potentially be null depending on calls made")]
        public void Dispose()
        {
            _heartbeatTimer?.Dispose();
            _client?.Dispose();
        }

        /// <summary>
        /// Handles sending regular calls to the heartbeat API, in order to keep the connection alive.
        /// </summary>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        private void SendHeartbeat()
        {
#if DEBUG
            Log.Trace("Sending heartbeat");
#endif

            var response = _client.PutAsync<HeartbeatResponse>("/heartbeat").Result;

            if (!response.IsSuccessful)
            {
                var ex = new RestException(
                    "Call to heartbeat API failed",
                    Result.RzFailed,
                    new Uri(_client.BaseAddress, "/heartbeat"),
                    response.Status);

                Log.Error(ex, "Heartbeat call failed");
                throw ex;
            }

            if (response.Data is null)
            {
                var ex = new RestException(
                    "Got NULL data from heartbeat call",
                    Result.RzFailed,
                    new Uri(_client.BaseAddress, "/heartbeat"),
                    response.Status);

                Log.Error(ex, "Got NULL data from heartbeat call");
                throw ex;
            }

#if DEBUG
            Log.TraceFormat("Heartbeat complete, tick: {0}", response.Data.Tick);
#endif
        }

        /// <summary>
        /// Creates a Chroma effect using the specified API endpoint and effect data.
        /// </summary>
        /// <param name="endpoint">Device endpoint to create effect at.</param>
        /// <param name="data">Effect data.</param>
        /// <returns>A <see cref="Guid" /> identifying the newly created effect.</returns>
        /// <exception cref="RestException">Thrown if there is an error calling the REST API.</exception>
        /// <exception cref="ApiException">Thrown if the SDK returns an exception creating the effect.</exception>
        private async Task<Guid> CreateEffectAsync(string endpoint, object data)
        {
            var response = await _client.PostAsync<SdkEffectResponse>(endpoint, data).ConfigureAwait(false);

            if (!response.IsSuccessful)
            {
                var ex = new RestException(
                    $"Failed to create effect at {endpoint}",
                    response.Data?.Result ?? Result.RzFailed,
                    new Uri(_client.BaseAddress, endpoint),
                    response.Status,
                    response.Data?.ToString());

                Log.Error(ex, "Failed to create effect");
                throw ex;
            }

            var responseData = response.Data;

            if (responseData is null)
            {
                throw new ApiException("Effect creation API returned NULL response");
            }

            if (!responseData.Result)
            {
                throw new ApiException("Exception when calling SetEffect API", responseData.Result);
            }

            if (responseData.EffectId is null)
            {
                throw new ApiException("Got NULL GUID from creating effect", responseData.Result);
            }

            return responseData.EffectId.Value;
        }
    }
}
