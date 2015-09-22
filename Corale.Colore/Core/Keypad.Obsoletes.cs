// ---------------------------------------------------------------------------------------
// <copyright file="Keypad.Obsoletes.cs" company="Corale">
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
    using System;
    using Corale.Colore.Razer.Keypad.Effects;

    /// <summary>
    /// Class for interacting with a Chroma keypad.
    /// </summary>
    public sealed partial class Keypad : Device, IKeypad
    {
        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        [Obsolete("Set is deprecated, please use SetAll(Effect).", false)]
        public override void Set(Color color)
        {
            SetAll(color);
        }

        /// <summary>
        /// Updates the device to use the effect pointed to by the specified GUID.
        /// </summary>
        /// <param name="guid">GUID to set.</param>
        [Obsolete("Set is deprecated, please use SetGuid(Guid).", false)]
        public override void Set(Guid guid)
        {
            if (CurrentEffectId != Guid.Empty)
            {
                NativeWrapper.DeleteEffect(CurrentEffectId);
                CurrentEffectId = Guid.Empty;
            }

            NativeWrapper.SetEffect(guid);
            CurrentEffectId = guid;
        }

        /// <summary>
        /// Sets an effect without any parameters.
        /// Currently, this only works for the <see cref="Effect.None" /> effect.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        [Obsolete("Set is deprecated, please use SetEffect(Effect).", false)]
        public void Set(Effect effect)
        {
            SetEffect(effect);
        }

        /// <summary>
        /// Sets a <see cref="Breathing" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Breathing" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetBreathing(Breathing).", false)]
        public void Set(Breathing effect)
        {
            SetBreathing(effect);
        }

        /// <summary>
        /// Sets a <see cref="Custom" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Custom" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetCustom(Custom).", false)]
        public void Set(Custom effect)
        {
            SetCustom(effect);
        }

        /// <summary>
        /// Sets a <see cref="Reactive" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Reactive" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetReactive(Reactive).", false)]
        public void Set(Reactive effect)
        {
           SetReactive(effect);
        }

        /// <summary>
        /// Sets a <see cref="Static" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Static" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetStatic(Static).", false)]
        public void Set(Static effect)
        {
            SetStatic(effect);
        }

        /// <summary>
        /// Sets a <see cref="Wave" /> effect on the keypad.
        /// </summary>
        /// <param name="effect">An instance of the <see cref="Wave" /> struct.</param>
        [Obsolete("Set is deprecated, please use SetWave(Wave).", false)]
        public void Set(Wave effect)
        {
            SetWave(effect);
        }
    }
}
