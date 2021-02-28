// ---------------------------------------------------------------------------------------
// <copyright file="HeadsetImplementation.cs" company="Corale">
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

namespace Colore.Implementations
{
    using System;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Headset;
    using Colore.Logging;

    /// <inheritdoc cref="IHeadset" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with Chroma Headsets.
    /// </summary>
    internal sealed class HeadsetImplementation : DeviceImplementation, IHeadset
    {
        /// <summary>
        /// Loggers instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<HeadsetImplementation>();

        /// <summary>
        /// Internal <see cref="CustomHeadsetEffect" /> struct used for effects.
        /// </summary>
        private CustomHeadsetEffect _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="HeadsetImplementation" /> class.
        /// </summary>
        /// <param name="api">Reference to the Chroma API instance in use.</param>
        public HeadsetImplementation(IChromaApi api)
            : base(api)
        {
            Log.Info("Headset is initializing");
            _custom = CustomHeadsetEffect.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the color of a specific LED on the headset.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="Color" /> at the <paramref name="index" />.</returns>
        public Color this[int index]
        {
            get => _custom[index];

            set
            {
                _custom[index] = value;
                SetCustomAsync(_custom).Wait();
            }
        }

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _custom.Set(color);
            return await SetStaticAsync(new StaticHeadsetEffect(color)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="HeadsetEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">The type of effect to set.</param>
        public async Task<Guid> SetEffectAsync(HeadsetEffectType effectType)
        {
            return await SetEffectAsync(await Api.CreateHeadsetEffectAsync(effectType).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="StaticHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        public async Task<Guid> SetStaticAsync(StaticHeadsetEffect effect)
        {
            return await SetEffectAsync(await Api.CreateHeadsetEffectAsync(HeadsetEffectType.Static, effect).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a new <see cref="StaticHeadsetEffect" /> effect on
        /// the headset using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> of the effect.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new StaticHeadsetEffect(color)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a new <see cref="CustomHeadsetEffect" /> effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="CustomHeadsetEffect" /> struct
        /// describing the effect.
        /// </param>
        /// <returns>A <see cref="Guid" /> for the effect that was set.</returns>
        public async Task<Guid> SetCustomAsync(CustomHeadsetEffect effect)
        {
            return await SetEffectAsync(await Api.CreateHeadsetEffectAsync(HeadsetEffectType.Custom, effect).ConfigureAwait(false))
                .ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Headset.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(HeadsetEffectType.None).ConfigureAwait(false);
        }
    }
}
