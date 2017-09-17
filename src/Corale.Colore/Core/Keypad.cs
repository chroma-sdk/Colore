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

    using Corale.Colore.Logging;
    using Corale.Colore.Razer.Keypad;
    using Corale.Colore.Razer.Keypad.Effects;

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
        /// Lock object for thread-safe init.
        /// </summary>
        private static readonly object InitLock = new object();

        /// <summary>
        /// Singleton instance of this class.
        /// </summary>
        private static IKeypad _instance;

        /// <summary>
        /// Internal instance of a <see cref="Custom" /> struct used for
        /// the indexer.
        /// </summary>
        private Custom _custom;

        /// <summary>
        /// Prevents a default instance of the <see cref="Keypad" /> class from being created.
        /// </summary>
        private Keypad()
        {
            Log.Debug("Keypad is initializing");
            Chroma.InitInstance();

            _custom = Custom.Create();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IKeypad" /> interface.
        /// </summary>
        public static IKeypad Instance
        {
            get
            {
                lock (InitLock)
                {
                    return _instance ?? (_instance = new Keypad());
                }
            }
        }

        /// <summary>
        /// Gets or sets a color at the specified position in the keypad's
        /// grid layout.
        /// </summary>
        /// <param name="row">The row to access (between <c>0</c> and <see cref="Constants.MaxRows" />, exclusive upper-bound).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="Constants.MaxColumns" />, exclusive upper-bound).</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        public Color this[int row, int column]
        {
            get
            {
                return _custom[row, column];
            }

            set
            {
                _custom[row, column] = value;
                SetCustom(_custom);
            }
        }

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

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            _custom.Set(color);
            SetCustom(_custom);
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(effect, IntPtr.Zero));
        }

        /// <summary>
        /// Sets a <see cref="Breathing" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        public void SetBreathing(Breathing effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(Effect.Breathing, effect));
        }

        /// <summary>
        /// Sets a <see cref="Breathing" /> effect on the keypad
        /// using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="first">The first color to breathe into.</param>
        /// <param name="second">Second color to breathe into.</param>
        public void SetBreathing(Color first, Color second)
        {
            SetBreathing(new Breathing(first, second));
        }

        /// <summary>
        /// Sets an effect on the keypad to breathe
        /// between randomly chosen colors.
        /// </summary>
        public void SetBreathing()
        {
            SetBreathing(new Breathing(BreathingType.Random, Color.Black, Color.Black));
        }

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public void SetCustom(Custom effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(Effect.Custom, effect));
        }

        /// <summary>
        /// Sets a <see cref="Reactive" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Reactive" /> struct.</param>
        public void SetReactive(Reactive effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(Effect.Reactive, effect));
        }

        /// <summary>
        /// Sets a <see cref="Reactive" /> effect on the keypad
        /// with the specified parameters.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        /// <param name="duration">Duration of the effect.</param>
        public void SetReactive(Color color, Duration duration)
        {
            SetReactive(new Reactive(color, duration));
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(Effect.Static, effect));
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        public void SetStatic(Color color)
        {
            SetStatic(new Static(color));
        }

        /// <summary>
        /// Sets a <see cref="Wave" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        public void SetWave(Wave effect)
        {
            SetGuid(NativeWrapper.CreateKeypadEffect(Effect.Wave, effect));
        }

        /// <summary>
        /// Sets a <see cref="Wave" /> effect on the keypad.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        public void SetWave(Direction direction)
        {
            SetWave(new Wave(direction));
        }

        /// <summary>
        /// Clears the current effect on the Keypad.
        /// </summary>
        public override void Clear()
        {
            _custom.Clear();
            SetEffect(Effect.None);
        }
    }
}
