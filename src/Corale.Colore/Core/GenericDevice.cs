// ---------------------------------------------------------------------------------------
// <copyright file="GenericDevice.cs" company="Corale">
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
    using System.Collections.Generic;

    using Common.Logging;

    using Corale.Colore.Razer;
    using Corale.Colore.Razer.Effects;

    using JetBrains.Annotations;

    /// <summary>
    /// A generic device.
    /// </summary>
    public sealed class GenericDevice : Device, IGenericDevice
    {
        /// <summary>
        /// Logger instance for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(GenericDevice));

        /// <summary>
        /// Holds generated instances of devices.
        /// </summary>
        private static readonly Dictionary<Guid, GenericDevice> Instances = new Dictionary<Guid, GenericDevice>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDevice" /> class.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device.</param>
        private GenericDevice(Guid deviceId)
        {
            Log.InfoFormat("New generic device initializing: {0}", deviceId);

            if (!Devices.IsValidId(deviceId))
                throw new UnsupportedDeviceException(deviceId);

            DeviceId = deviceId;
        }

        /// <summary>
        /// Gets the <see cref="Guid" /> of this device.
        /// </summary>
        public Guid DeviceId { get; }

        /// <summary>
        /// Gets a <see cref="IGenericDevice" /> instance for the device
        /// with the specified ID.
        /// </summary>
        /// <param name="deviceId">The ID of the device to get.</param>
        /// <returns>An instance of <see cref="IGenericDevice" /> for the requested device.</returns>
        [PublicAPI]
        public static IGenericDevice Get(Guid deviceId)
        {
            Chroma.InitInstance();

            if (!Instances.ContainsKey(deviceId))
                Instances[deviceId] = new GenericDevice(deviceId);

            return Instances[deviceId];
        }

        /// <summary>
        /// Clears the current effect on Generic Devices.
        /// </summary>
        public override void Clear()
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.None, default(None)));
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
        /// Sets a parameter-less effect on this device.
        /// </summary>
        /// <param name="effect">Effect to set.</param>
        public void SetEffect(Effect effect)
        {
            SetEffect(effect, IntPtr.Zero);
        }

        /// <summary>
        /// Sets an effect on this device, taking a parameter.
        /// </summary>
        /// <param name="effect">Effect to set.</param>
        /// <param name="param">Effect-specific parameter to use.</param>
        public void SetEffect(Effect effect, IntPtr param)
        {
            SetGuid(NativeWrapper.CreateEffect(DeviceId, effect, param));
        }

        /// <summary>
        /// Sets a blinking effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetBlinking(Blinking effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Blinking, effect));
        }

        /// <summary>
        /// Sets a blinking effect on this device.
        /// </summary>
        /// <param name="color">Color of the effect.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetBlinking(Color color, int parameter = 0)
        {
            SetBlinking(new Blinking(color, parameter));
        }

        /// <summary>
        /// Sets a breathing effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetBreathing(Breathing effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Breathing, effect));
        }

        /// <summary>
        /// Sets a breathing effect on this device,
        /// causing it to breathe between random colors.
        /// </summary>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetBreathing(int parameter = 0)
        {
            SetBreathing(new Breathing(parameter));
        }

        /// <summary>
        /// Sets a breathing effect on this device,
        /// causing it to breathe with a single color.
        /// </summary>
        /// <param name="color">The color to breathe with.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetBreathing(Color color, int parameter = 0)
        {
            SetBreathing(new Breathing(color, parameter));
        }

        /// <summary>
        /// Sets a breathing effect on this device,
        /// causing it to breathe between two colors.
        /// </summary>
        /// <param name="first">The first color to breathe with.</param>
        /// <param name="second">The second color to breathe with.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetBreathing(Color first, Color second, int parameter = 0)
        {
            SetBreathing(new Breathing(first, second, parameter));
        }

        /// <summary>
        /// Sets a custom effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetCustom(Custom effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Custom, effect));
        }

        /// <summary>
        /// Sets a reactive effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetReactive(Reactive effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Reactive, effect));
        }

        /// <summary>
        /// Sets a reactive effect on this device.
        /// </summary>
        /// <param name="color">Color to react with.</param>
        /// <param name="duration">How long the reaction effect should stay.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetReactive(Color color, Duration duration, int parameter = 0)
        {
            SetReactive(new Reactive(duration, color, parameter));
        }

        /// <summary>
        /// Sets a spectrum cycling effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetSpectrumCycling(SpectrumCycling effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.SpectrumCycling, effect));
        }

        /// <summary>
        /// Sets a spectrum cycling effect on this device.
        /// </summary>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetSpectrumCycling(int parameter = 0)
        {
            SetSpectrumCycling(new SpectrumCycling(parameter));
        }

        /// <summary>
        /// Sets a static effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetStatic(Static effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Static, effect));
        }

        /// <summary>
        /// Sets a static effect on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetStatic(Color color, int parameter = 0)
        {
            SetStatic(new Static(color, parameter));
        }

        /// <summary>
        /// Sets a wave effect on this device.
        /// </summary>
        /// <param name="effect">Effect options.</param>
        public void SetWave(Wave effect)
        {
            SetGuid(NativeWrapper.CreateDeviceEffect(DeviceId, Effect.Wave, effect));
        }

        /// <summary>
        /// Sets a wave effect on this device.
        /// </summary>
        /// <param name="direction">Direction of the wave.</param>
        /// <param name="parameter">Additional effect parameter.</param>
        public void SetWave(Direction direction, int parameter = 0)
        {
            SetWave(new Wave(direction, parameter));
        }
    }
}
