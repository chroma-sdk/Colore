// ---------------------------------------------------------------------------------------
// <copyright file="Keypad.cs" company="Corale">
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

    using Corale.Colore.Razer.Keypad.Effects;

    /// <inheritdoc cref="IKeypad" />
    /// <inheritdoc cref="Device" />
    /// <summary>
    /// Class for interacting with a Chroma keypad.
    /// </summary>
    public sealed class Keypad : Device, IKeypad
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Keypad));

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct used for
        /// the indexer.
        /// </summary>
        private Custom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Corale.Colore.Core.Keypad" /> class.
        /// </summary>
        public Keypad(IChromaApi api)
            : base(api)
        {
            Log.Debug("Keypad is initializing");
            _custom = Custom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a color at the specified position in the keypad's
        /// grid layout.
        /// </summary>
        /// <param name="row">The row to access (between <c>0</c> and <see cref="F:Corale.Colore.Razer.Keypad.Constants.MaxRows" />, exclusive upper-bound).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="F:Corale.Colore.Razer.Keypad.Constants.MaxColumns" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="T:Corale.Colore.Core.Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get => _custom[row, column];

            set
            {
                _custom[row, column] = value;
                SetCustomAsync(_custom).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns whether a key has had a custom color set.
        /// </summary>
        /// <param name="row">The row to query.</param>
        /// <param name="column">The column to query.</param>
        /// <returns><c>true</c> if the position has a color set that is not black, otherwise <c>false</c>.</returns>
        public bool IsSet(int row, int column)
        {
            return this[row, column] != Color.Black;
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
        /// Currently, this only works for the <see cref="F:Corale.Colore.Razer.Keypad.Effects.Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetGuidAsync(await Api.CreateKeypadEffectAsync(effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.Keypad.Effects.Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Keypad.Effects.Custom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(Custom effect)
        {
            return await SetGuidAsync(await Api.CreateKeypadEffectAsync(Effect.Custom, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.Keypad.Effects.Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.Keypad.Effects.Static" /> struct.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetGuidAsync(await Api.CreateKeypadEffectAsync(Effect.Static, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.Keypad.Effects.Static" /> effect on the keypad.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new Static(color));
        }

        /// <inheritdoc cref="Device.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Keypad.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(Effect.None);
        }
    }
}
