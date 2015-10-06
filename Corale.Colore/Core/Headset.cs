// ---------------------------------------------------------------------------------------
// <copyright file="Headset.cs" company="Corale">
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
    using Corale.Colore.Razer.Headset.Effects;

    using log4net;

    /// <summary>
    /// Class for interacting with Chroma Headsets.
    /// </summary>
    public sealed partial class Headset : Device, IHeadset
    {
        /// <summary>
        /// Loggers instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(Headset));

        /// <summary>
        /// Holds the application-wide instance of the <see cref="IHeadset" /> interface.
        /// </summary>
        private static IHeadset _instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="Headset" /> class from being created.
        /// </summary>
        private Headset()
        {
            Log.Info("Headset is initializing");
            Chroma.Initialize();
        }

        /// <summary>
        /// Gets the application-wide instance of the <see cref="IHeadset" /> interface.
        /// </summary>
        public static IHeadset Instance
        {
            get
            {
                return _instance ?? (_instance = new Headset());
            }
        }

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            SetStatic(new Static(color));
        }

        /// <summary>
        /// Sets an effect on the headset that doesn't
        /// take any parameters, currently only valid
        /// for the <see cref="Effect.SpectrumCycling" /> effect.
        /// </summary>
        /// <param name="effect">The type of effect to set.</param>
        public void SetEffect(Effect effect)
        {
            SetGuid(NativeWrapper.CreateHeadsetEffect(effect));
        }

        /// <summary>
        /// Sets a new static effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Static" /> struct
        /// describing the effect.
        /// </param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateHeadsetEffect(effect));
        }

        /// <summary>
        /// Sets a new <see cref="Static" /> effect on
        /// the headset using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color" /> of the effect.</param>
        public void SetStatic(Color color)
        {
            SetStatic(new Static(color));
        }

        /// <summary>
        /// Sets a new breathing effect on the headset.
        /// </summary>
        /// <param name="effect">
        /// An instance of the <see cref="Breathing" /> struct
        /// describing the effect.
        /// </param>
        public void SetBreathing(Breathing effect)
        {
            SetGuid(NativeWrapper.CreateHeadsetEffect(effect));
        }

        /// <summary>
        /// Sets a new <see cref="Breathing" /> effect on the headset
        /// using the specified <see cref="Color" />.
        /// </summary>
        /// <param name="color"><see cref="Color"/> of the effect.</param>
        public void SetBreathing(Color color)
        {
            SetBreathing(new Breathing(color));
        }
    }
}
