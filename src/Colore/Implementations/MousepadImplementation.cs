// ---------------------------------------------------------------------------------------
// <copyright file="MousepadImplementation.cs" company="Corale">
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

namespace Colore.Implementations
{
    using System;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Mousepad;
    using Colore.Logging;

    /// <inheritdoc cref="IMousepad" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with a Chroma mouse pad.
    /// </summary>
    internal sealed class MousepadImplementation : DeviceImplementation, IMousepad
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<MousepadImplementation>();

        /// <summary>
        /// Internal <see cref="MousepadCustom" /> struct used for effects.
        /// </summary>
        private MousepadCustom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="MousepadImplementation" /> class.
        /// </summary>
        public MousepadImplementation(IChromaApi api)
            : base(api)
        {
            Log.Debug("Mousepad is initializing.");
            _custom = MousepadCustom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a specific LED on the mouse pad.
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
            return await SetCustomAsync(_custom).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="MousepadEffect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(MousepadEffect effect)
        {
            return await SetEffectAsync(await Api.CreateMousepadEffectAsync(effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="MousepadStatic" /> struct.</param>
        public async Task<Guid> SetStaticAsync(MousepadStatic effect)
        {
            return await SetEffectAsync(await Api.CreateMousepadEffectAsync(MousepadEffect.Static, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new MousepadStatic(color)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="MousepadCustom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(MousepadCustom effect)
        {
            return await SetEffectAsync(await Api.CreateMousepadEffectAsync(MousepadEffect.Custom, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Mousepad.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(MousepadEffect.None).ConfigureAwait(false);
        }
    }
}
