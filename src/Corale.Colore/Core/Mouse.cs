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

    using Common.Logging;

    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;

    using JetBrains.Annotations;

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
        /// Lock object for thread-safe init.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Holds the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        private static IMouse _instance;

        /// <summary>
        /// Internal instance of a <see cref="CustomGrid" /> struct.
        /// </summary>
        private CustomGrid _customGrid;

        /// <summary>
        /// Prevents a default instance of the <see cref="Mouse" /> class from being created.
        /// </summary>
        private Mouse()
        {
            Log.Info("Mouse is initializing");
            Chroma.InitInstance();
            _customGrid = CustomGrid.Create();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IMouse" /> interface.
        /// </summary>
        [PublicAPI]
        public static IMouse Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new Mouse());
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specific position
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="row">The row to query, between <c>0</c> and <see cref="Constants.MaxRows" /> (exclusive upper-bound).</param>
        /// <param name="column">The column to query, between <c>0</c> and <see cref="Constants.MaxColumns" /> (exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get
            {
                return _customGrid[row, column];
            }

            set
            {
                _customGrid[row, column] = value;
                SetGrid(_customGrid);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for a specified <see cref="GridLed" />
        /// on the mouse's virtual grid.
        /// </summary>
        /// <param name="led">The <see cref="GridLed" /> to query.</param>
        /// <returns>The <see cref="Color" /> currently set for the specified <see cref="GridLed" />.</returns>
        public Color this[GridLed led]
        {
            get
            {
                return _customGrid[led];
            }

            set
            {
                _customGrid[led] = value;
                SetGrid(_customGrid);
            }
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(effect, IntPtr.Zero));
        }

        /// <summary>
        /// Sets a static color on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> effect.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.Static, effect));
        }

        /// <summary>
        /// Sets a static effect on the mouse.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <param name="led">Which LED(s) to affect.</param>
        public void SetStatic(Color color, Led led = Led.All)
        {
            SetStatic(new Static(led, color));
        }

        /// <summary>
        /// Sets the color of all LEDs on the mouse.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            _customGrid.Set(color);
            SetGrid(_customGrid);
        }

        /// <summary>
        /// Sets a custom grid effect on the mouse.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="CustomGrid" /> struct.</param>
        public void SetGrid(CustomGrid effect)
        {
            SetGuid(NativeWrapper.CreateMouseEffect(Effect.CustomGrid, effect));
        }

        /// <summary>
        /// Clears the current effect on the Mouse.
        /// </summary>
        public override void Clear()
        {
            _customGrid.Clear();
            SetEffect(Effect.None);
        }
    }
}
