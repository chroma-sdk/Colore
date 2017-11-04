// ---------------------------------------------------------------------------------------
// <copyright file="Mouse.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Core
{
    using System;
    using System.Threading.Tasks;

    using Common.Logging;

    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;

    using JetBrains.Annotations;

    /// <inheritdoc cref="IMouse" />
    /// <inheritdoc cref="Device" />
    /// <summary>
    /// Class for interacting with a Chroma mouse.
    /// </summary>
    [PublicAPI]
    public sealed class Mouse : Device, IMouse
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Mouse));

        /// <summary>
        /// Internal instance of a <see cref="CustomGrid" /> struct.
        /// </summary>
        private CustomGrid _customGrid;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Corale.Colore.Core.Mouse" /> class.
        /// </summary>
        public Mouse(IChromaApi api)
            : base(api)
        {
            Log.Info("Mouse is initializing");
            _customGrid = CustomGrid.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="T:Corale.Colore.Core.Color" /> for a specific position
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="row">The row to query, between <c>0</c> and <see cref="F:Corale.Colore.Razer.Mouse.Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">The column to query, between <c>0</c> and <see cref="F:Corale.Colore.Razer.Mouse.Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="T:Corale.Colore.Core.Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get => _customGrid[row, column];

            set
            {
                _customGrid[row, column] = value;
                SetGridAsync(_customGrid).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the <see cref="T:Corale.Colore.Core.Color" /> for a specified <see cref="T:Corale.Colore.Razer.Mouse.GridLed" />
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="led">The <see cref="T:Corale.Colore.Razer.Mouse.GridLed" /> to query.</param>
        /// <returns>The <see cref="T:Corale.Colore.Core.Color" /> currently set for the specified <see cref="T:Corale.Colore.Razer.Mouse.GridLed" />.</returns>
        public Color this[GridLed led]
        {
            get => _customGrid[led];

            set
            {
                _customGrid[led] = value;
                SetGridAsync(_customGrid).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="F:Corale.Colore.Razer.Mouse.Effects.Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetGuidAsync(await Api.CreateMouseEffectAsync(effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Mouse.Effects.Static" /> effect.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetGuidAsync(await Api.CreateMouseEffectAsync(Effect.Static, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a static effect on the mouse.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        public async Task<Guid> SetStaticAsync(Color color, Led led = Led.All)
        {
            return await SetStaticAsync(new Static(led, color));
        }

        /// <inheritdoc cref="Device.SetAllAsync" />
        /// <summary>
        /// Sets the color of all LEDs on the mouse.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override async Task<Guid> SetAllAsync(Color color)
        {
            _customGrid.Set(color);
            return await SetGridAsync(_customGrid);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a custom grid effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Mouse.Effects.CustomGrid" /> struct.</param>
        public async Task<Guid> SetGridAsync(CustomGrid effect)
        {
            return await SetGuidAsync(await Api.CreateMouseEffectAsync(Effect.CustomGrid, effect));
        }

        /// <inheritdoc cref="Device.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Mouse.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _customGrid.Clear();
            return await SetEffectAsync(Effect.None);
        }
    }
}
