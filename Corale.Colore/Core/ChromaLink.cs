// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLink.cs" company="Corale">
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

namespace Corale.Colore.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Corale.Colore.Annotations;
    using Corale.Colore.Logging;

    using Razer.ChromaLink;
    using Razer.ChromaLink.Effects;

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
        /// Lock object for thread-safe init.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static IChromaLink _instance;

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct used for
        /// the indexer.
        /// </summary>
        private Custom _custom;

        /// <summary>
        /// Prevents a default instance of the <see cref="ChromaLink" /> class from being created.
        /// </summary>
        private ChromaLink()
        {
            Log.Debug("Chroma Link is initializing");
            Chroma.InitInstance();

            _custom = Custom.Create();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IChromaLink" /> interface.
        /// </summary>
        public static IChromaLink Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new ChromaLink());
                }
            }
        }

        /// <summary>
        /// Gets or sets a color at the specified position in the Chroma Link
        /// </summary>
        /// <param name="index">The index to access (between <c>0</c> and <see cref="Constants.MaxLeds" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int index]
        {
            get
            {
                return _custom[index];
            }

            set
            {
                _custom[index] = value;
                SetCustom(_custom);
            }
        }

        /// <summary>
        /// Returns whether an element has had a custom color set.
        /// </summary>
        /// <param name="index">The index to query.</param>
        /// <returns><c>true</c> if the position has a color set that is not black, otherwise <c>false</c>.</returns>
        public bool IsSet(int index)
        {
            return this[index] != Color.Black;
        }

        /// <summary>
        /// Sets the color of all lights in Chroma Link
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            _custom.Set(color);
            SetCustom(_custom);
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> and <see cref="Effect.Static" /> effects.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateChromaLinkEffect(effect, IntPtr.Zero));
        }

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public void SetCustom(Custom effect)
        {
            SetGuid(NativeWrapper.CreateChromaLinkEffect(Effect.Custom, effect));
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateChromaLinkEffect(Effect.Static, effect));
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the Chroma Link.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public void SetStatic(Color color)
        {
            SetStatic(new Static(color));
        }

        /// <summary>
        /// Clears the current effect on the Chroma Link.
        /// </summary>
        public override void Clear()
        {
            SetEffect(Effect.None);
        }
    }
}
