// ---------------------------------------------------------------------------------------
// <copyright file="GenericDevice.cs" company="Corale">
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
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using Corale.Colore.Annotations;
    using Corale.Colore.Razer;

    using log4net;

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
        /// The <see cref="Guid" /> of this device.
        /// </summary>
        private readonly Guid _deviceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDevice" /> class.
        /// </summary>
        /// <param name="deviceId">The <see cref="Guid" /> of the device.</param>
        private GenericDevice(Guid deviceId)
        {
            Log.InfoFormat("New generic device initializing: {0}", deviceId);

            if (!Devices.IsValidId(deviceId))
                throw new UnsupportedDeviceException(deviceId);

            _deviceId = deviceId;
        }

        /// <summary>
        /// Gets the <see cref="Guid" /> of this device.
        /// </summary>
        public Guid DeviceId
        {
            get
            {
                return _deviceId;
            }
        }

        /// <summary>
        /// Gets a <see cref="IGenericDevice" /> instance for the device
        /// with the specified ID.
        /// </summary>
        /// <param name="deviceId">The ID of the device to get.</param>
        /// <returns>An instance of <see cref="IGenericDevice" /> for the requested device.</returns>
        [PublicAPI]
        public static IGenericDevice Get(Guid deviceId)
        {
            Chroma.Initialize();

            if (!Instances.ContainsKey(deviceId))
                Instances[deviceId] = new GenericDevice(deviceId);

            return Instances[deviceId];
        }

        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public override void SetAll(Color color)
        {
            var colorPtr = Marshal.AllocHGlobal(Marshal.SizeOf(color));
            Marshal.StructureToPtr(color, colorPtr, false);

            try
            {
                SetGuid(NativeWrapper.CreateEffect(DeviceId, Effect.Static, colorPtr));
            }
            finally
            {
                Marshal.FreeHGlobal(colorPtr);
            }
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
    }
}
