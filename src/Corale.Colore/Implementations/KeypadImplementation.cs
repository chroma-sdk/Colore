// ---------------------------------------------------------------------------------------
// <copyright file="KeypadImplementation.cs" company="Corale">
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
    using Corale.Colore.Effects.Keypad;

    /// <inheritdoc cref="IKeypad" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with a Chroma keypad.
    /// </summary>
    internal sealed class KeypadImplementation : DeviceImplementation, IKeypad
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(KeypadImplementation));

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct used for
        /// the indexer.
        /// </summary>
        private Custom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="KeypadImplementation" /> class.
        /// </summary>
        public KeypadImplementation(IChromaApi api)
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
        /// <param name="row">The row to access (between <c>0</c> and <see cref="Constants.MaxRows" />, exclusive upper-bound).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="Constants.MaxColumns" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
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
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetEffectAsync(await Api.CreateKeypadEffectAsync(effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(Custom effect)
        {
            return await SetEffectAsync(await Api.CreateKeypadEffectAsync(Effect.Custom, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetEffectAsync(await Api.CreateKeypadEffectAsync(Effect.Static, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new Static(color)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Keypad.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            _custom.Clear();
            return await SetEffectAsync(Effect.None).ConfigureAwait(false);
        }
    }
}
