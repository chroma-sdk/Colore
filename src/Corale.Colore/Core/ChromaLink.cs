// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLink.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System;
    using System.Threading.Tasks;

    using Common.Logging;

    using Corale.Colore.Razer.ChromaLink.Effects;

    using JetBrains.Annotations;

    /// <inheritdoc cref="IChromaLink" />
    /// <inheritdoc cref="Device" />
    /// <summary>
    /// Class for interacting with a Chroma Link.
    /// </summary>
    [PublicAPI]
    public sealed class ChromaLink : Device, IChromaLink
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ChromaLink));

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct used for
        /// the indexer.
        /// </summary>
        private Custom _custom;

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Corale.Colore.Core.ChromaLink" /> class.
        /// </summary>
        public ChromaLink(IChromaApi api)
            : base(api)
        {
            Log.Debug("Chroma Link is initializing");
            _custom = Custom.Create();
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a color at the specified position in the Chroma Link
        /// </summary>
        /// <param name="index">The index to access (between <c>0</c> and <see cref="F:Corale.Colore.Razer.ChromaLink.Constants.MaxLeds" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="T:Corale.Colore.Core.Color" /> at the specified position.</returns>
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

        /// <inheritdoc cref="Device.SetAllAsync" />
        /// <summary>
        /// Sets the color of all lights in Chroma Link
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
        /// Currently, this only works for the <see cref="F:Corale.Colore.Razer.ChromaLink.Effects.Effect.None" /> and <see cref="F:Corale.Colore.Razer.ChromaLink.Effects.Effect.Static" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public async Task<Guid> SetEffectAsync(Effect effect)
        {
            return await SetGuidAsync(await Api.CreateChromaLinkEffectAsync(effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.ChromaLink.Effects.Custom" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.ChromaLink.Effects.Custom" /> struct.</param>
        public async Task<Guid> SetCustomAsync(Custom effect)
        {
            return await SetGuidAsync(await Api.CreateChromaLinkEffectAsync(Effect.Custom, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.ChromaLink.Effects.Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="T:Corale.Colore.Razer.ChromaLink.Effects.Static" /> struct.</param>
        public async Task<Guid> SetStaticAsync(Static effect)
        {
            return await SetGuidAsync(await Api.CreateChromaLinkEffectAsync(Effect.Static, effect));
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets a <see cref="T:Corale.Colore.Razer.ChromaLink.Effects.Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public async Task<Guid> SetStaticAsync(Color color)
        {
            return await SetStaticAsync(new Static(color));
        }

        /// <inheritdoc cref="Device.ClearAsync" />
        /// <summary>
        /// Clears the current effect on the Chroma Link.
        /// </summary>
        public override async Task<Guid> ClearAsync()
        {
            return await SetEffectAsync(Effect.None);
        }
    }
}
