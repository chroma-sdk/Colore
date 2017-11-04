// ---------------------------------------------------------------------------------------
// <copyright file="Device.cs" company="Corale">
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
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// Base class for devices, containing code common between all devices.
    /// </summary>
    public abstract class Device : IDevice
    {
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the ID of the currently active effect.
        /// </summary>
        public Guid CurrentEffectId { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device" /> class.
        /// </summary>
        /// <param name="api">Reference to the Chroma API in use.</param>
        protected Device(IChromaApi api)
        {
            Api = api;
        }

        /// <summary>
        /// Gets the Chroma API instance.
        /// </summary>
        protected IChromaApi Api { get; }

        /// <inheritdoc />
        /// <summary>
        /// Clears the current effect on the device.
        /// </summary>
        public abstract Task<Guid> ClearAsync();

        /// <inheritdoc />
        /// <summary>
        /// Sets the color of all components on this device.
        /// </summary>
        /// <param name="color">Color to set.</param>
        public abstract Task<Guid> SetAllAsync(Color color);

        /// <inheritdoc />
        /// <summary>
        /// Updates the device to use the effect pointed to by the specified GUID.
        /// </summary>
        /// <param name="guid">GUID to set.</param>
        public async Task<Guid> SetGuidAsync(Guid guid)
        {
            await DeleteCurrentEffect();
            await Api.SetEffectAsync(guid);
            CurrentEffectId = guid;
            return CurrentEffectId;
        }

        /// <summary>
        /// Deletes the currently set effect.
        /// </summary>
        internal async Task DeleteCurrentEffect()
        {
            if (CurrentEffectId == Guid.Empty)
                return;

            await Api.DeleteEffectAsync(CurrentEffectId);
            CurrentEffectId = Guid.Empty;
        }
    }
}
