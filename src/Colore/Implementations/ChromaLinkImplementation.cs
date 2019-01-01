// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLinkImplementation.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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
    using Colore.Effects.ChromaLink;
    using Colore.Logging;

    /// <inheritdoc cref="IChromaLink" />
    /// <inheritdoc cref="DeviceImplementation" />
    /// <summary>
    /// Class for interacting with a Chroma Link.
    /// </summary>
    internal sealed class ChromaLinkImplementation : DeviceImplementation, IChromaLink
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<ChromaLinkImplementation>();

        /// <summary>
        /// Internal instance of a <see cref="ChromaLinkCustom" /> struct used for
        /// the indexer.
        /// </summary>
        private ChromaLinkCustom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ChromaLinkImplementation" /> class.
        /// </summary>
        public ChromaLinkImplementation(IChromaApi api)
            : base(api)
        {
            Log.Debug("Chroma Link is initializing");
            _custom = ChromaLinkCustom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a color at the specified position in the Chroma Link
        /// </summary>
        /// <param name="index">The index to access (between <c>0</c> and <see cref="ChromaLinkConstants.MaxLeds" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int index]
        {
            get => _custom[index];

            set
            {
                _custom[index] = value;
                SetCustomAsync(_custom).Wait();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns whether an element has had a custom color set.
        /// </summary>
        /// <param name="index">The index to query.</param>
        /// <returns><c>true</c> if the position has a color set that is not black, otherwise <c>false</c>.</returns>
        public bool IsSet(int index)
        {
            return this[index] != Color.Black;
        }

        /// <inheritdoc cref="DeviceImplementation.SetAllAsync" />
        /// <summary>
        /// Sets the color of all lights in Chroma Link
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
        /// Currently, this only works for the <see cref="ChromaLinkEffect.None" /> and <see cref="ChromaLinkEffect.Static" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(ChromaLinkEffect effect)
        {
            return await SetEffectAsync(await Api.CreateChromaLinkEffectAsync(effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="ChromaLinkCustom" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="ChromaLinkCustom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(ChromaLinkCustom effect)
        {
            return await SetEffectAsync(await Api.CreateChromaLinkEffectAsync(ChromaLinkEffect.Custom, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="ChromaLinkStatic" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="ChromaLinkStatic" /> struct.</param>
        public async Task<Guid> SetStaticAsync(ChromaLinkStatic effect)
        {
            return await SetEffectAsync(await Api.CreateChromaLinkEffectAsync(ChromaLinkEffect.Static, effect).ConfigureAwait(false)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="ChromaLinkStatic" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new ChromaLinkStatic(color)).ConfigureAwait(false);
        }

        /// <inheritdoc cref="DeviceImplementation.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Chroma Link.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            return await SetEffectAsync(ChromaLinkEffect.None).ConfigureAwait(false);
        }
    }
}
