// ---------------------------------------------------------------------------------------
// <copyright file="Mousepad.cs" company="Corale">
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

namespace Corale.Colore.Implementations
{
    using System;
    using System.Threading.Tasks;

    using Common.Logging;

    using Corale.Colore.Api;
    using Corale.Colore.Effects.Mousepad;

    /// <inheritdoc cref="IMousepad" />
    /// <inheritdoc cref="Device" />
    /// <summary>
    /// Class for interacting with a Chroma mouse pad.
    /// </summary>
    public sealed class Mousepad : Device, IMousepad
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Mousepad));

        /// <summary>
        /// Internal <see cref="Custom" /> struct used for effects.
        /// </summary>
        private Custom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Corale.Colore.Implementations.Mousepad" /> class.
        /// </summary>
        public Mousepad(IChromaApi api)
            : base(api)
        {
            Log.Debug("Mousepad is initializing.");
            _custom = Custom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a specific LED on the mouse pad.
        /// </summary>
        /// <param name="index">The index to access.</param>
        /// <returns>The current <see cref="T:Corale.Colore.Core.Color" /> at the <paramref name="index" />.</returns>
        public Color this[int index]
        {
            get => _custom[index];

            set
            {
                _custom[index] = value;
                SetCustomAsync(_custom).Wait();
            }
        }

        /// <inheritdoc cref="Device.SetAllAsync" />
        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _custom.Set(color);
            return await SetCustomAsync(_custom);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="F:Corale.Colore.Razer.Mousepad.Effects.Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetGuidAsync(await Api.CreateMousepadEffectAsync(effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Mousepad.Effects.Static" /> struct.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetGuidAsync(await Api.CreateMousepadEffectAsync(Effect.Static, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color effect on the mouse pad.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new Static(color));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom effect on the mouse pad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Mousepad.Effects.Custom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(Custom effect)
        {
            return await SetGuidAsync(await Api.CreateMousepadEffectAsync(Effect.Custom, effect));
        }

        /// <inheritdoc cref="Device.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Mousepad.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(Effect.None);
        }
    }
}
