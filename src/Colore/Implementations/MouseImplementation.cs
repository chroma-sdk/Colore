// ---------------------------------------------------------------------------------------
// <copyright file="MouseImplementation.cs" company="Corale">
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

namespace Colore.Implementations
{
    using System;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Mouse;
    using Colore.Logging;

    using JetBrains.Annotations;

    /// <inheritdoc cref="IMouse" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with a Chroma mouse.
    /// </summary>
    [PublicAPI]
    internal sealed class MouseImplementation : DeviceImplementation, IMouse
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<MouseImplementation>();

        /// <summary>
        /// Internal instance of a <see cref="CustomMouseEffect" /> struct.
        /// </summary>
        private CustomMouseEffect _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="MouseImplementation" /> class.
        /// </summary>
        public MouseImplementation(IChromaApi api)
            : base(api)
        {
            Log.Info("Mouse is initializing");
            _custom = CustomMouseEffect.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific position
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="row">The row to query, between <c>0</c> and <see cref="MouseConstants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">The column to query, between <c>0</c> and <see cref="MouseConstants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get => _custom[row, column];

            set
            {
                _custom[row, column] = value;
                SetGridAsync(_custom).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specified <see cref="GridLed" />
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="led">The <see cref="GridLed" /> to query.</param>
        /// <returns>The <see cref="Color" /> currently set for the specified <see cref="GridLed" />.</returns>
        public Color this[GridLed led]
        {
            get => _custom[led];

            set
            {
                _custom[led] = value;
                SetGridAsync(_custom).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="MouseEffectType.None" /> effect.
        /// </summary>
        /// <param name="effectType">Effect options.</param>
        public async Task<Guid> SetEffectAsync(MouseEffectType effectType)
        {
            return await SetEffectAsync(await Api.CreateMouseEffectAsync(effectType).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="StaticMouseEffect" /> effect.</param>
        public async Task<Guid> SetStaticAsync(StaticMouseEffect effect)
        {
            return await SetEffectAsync(await Api.CreateMouseEffectAsync(MouseEffectType.Static, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static effect on the mouse.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        public async Task<Guid> SetStaticAsync(Color color, Led led = Led.All)
        {
            return await SetStaticAsync(new StaticMouseEffect(led, color)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all LEDs on the mouse.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _custom.Set(color);
            return await SetGridAsync(_custom).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom grid effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomMouseEffect" /> struct.</param>
        public async Task<Guid> SetGridAsync(CustomMouseEffect effect)
        {
            return await SetEffectAsync(await Api.CreateMouseEffectAsync(MouseEffectType.Custom, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Mouse.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(MouseEffectType.None).ConfigureAwait(false);
        }
    }
}
