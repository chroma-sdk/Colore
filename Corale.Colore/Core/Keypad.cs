// ---------------------------------------------------------------------------------------
// <copyright file="Keypad.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using Corale.Colore.Razer.Keypad;
    using Corale.Colore.Razer.Keypad.Effects;

    using log4net;

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
            Chroma.Initialize();

            _custom = new Custom(Color.Black);
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IKeypad" /> interface.
        /// </summary>
        public static IKeypad Instance
        {
            get
            {
                return _instance ?? (_instance = new Keypad());
            }
        }

        /// <summary>
        /// Gets or sets a color at the specified position in the keypad's
        /// grid layout.
        /// </summary>
        /// <param name="row">The row to access (between <c>0</c> and <see cref="Constants.MaxRows" />).</param>
        /// <param name="column">The column to access (between <c>0</c> and <see cref="Constants.MaxColumns" />).</param>
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
                Set(_custom);
            }
        }

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void Set(Color color)
        {
            Set(NativeWrapper.CreateKeypadEffect(new Static(color)));
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void Set(Effect effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

        /// <summary>
        /// Sets a <see cref="Breathing" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        public void Set(Breathing effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        public void Set(Custom effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

        /// <summary>
        /// Sets a <see cref="Reactive" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Reactive" /> struct.</param>
        public void Set(Reactive effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        public void Set(Static effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

        /// <summary>
        /// Sets a <see cref="Wave" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        public void Set(Wave effect)
        {
            Set(NativeWrapper.CreateKeypadEffect(effect));
        }

    }
}
