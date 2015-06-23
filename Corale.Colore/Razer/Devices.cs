// ---------------------------------------------------------------------------------------
// <copyright file="Devices.cs" company="Corale">
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

namespace Corale.Colore.Razer
{
    using System;

    using Corale.Colore.Annotations;

    /// <summary>
    /// Contains device IDs for devices that have Chroma support.
    /// </summary>
    public static class Devices
    {
        /// <summary>
        /// Blackwidow Chroma edition.
        /// </summary>
        [PublicAPI]
        public static readonly Guid BlackwidowChroma = new Guid(
            0x2ea1bb63,
            0xca28,
            0x428d,
            0x9f,
            0x06,
            0x19,
            0x6b,
            0x88,
            0x33,
            0x0b,
            0xbb);

        /// <summary>
        /// Deathadder Chroma edition.
        /// </summary>
        [PublicAPI]
        public static readonly Guid DeathadderChroma = new Guid(
            0xaec50d91,
            0xb1f1,
            0x452f,
            0x8e,
            0x16,
            0x7b,
            0x73,
            0xf3,
            0x76,
            0xfd,
            0xf3);

        /// <summary>
        /// Naga Epic Chroma edition.
        /// </summary>
        [PublicAPI]
        public static readonly Guid NagaEpicChroma = new Guid(
            0x29a02acb,
            0xb882,
            0x4ee3,
            0x80,
            0x45,
            0xae,
            0x7a,
            0x8a,
            0x21,
            0x0,
            0x92);

        /// <summary>
        /// Kraken 7.1 Chroma edition.
        /// </summary>
        [PublicAPI]
        public static readonly Guid Kraken71Chroma = new Guid(
            0xcd1e09a5,
            0xd5e6,
            0x4a6c,
            0xa9,
            0x3b,
            0xe6,
            0xd9,
            0xbf,
            0x1d,
            0x20,
            0x92);

        /// <summary>
        /// Firefly Chroma edition.
        /// </summary>
        [PublicAPI]
        public static readonly Guid FireflyChroma = new Guid(
            0x80f95a94,
            0x73d2,
            0x48ca,
            0xae,
            0x9a,
            0x9,
            0x86,
            0x78,
            0x9a,
            0x9a,
            0xf2);

        /// <summary>
        /// ID matching every Chroma device.
        /// </summary>
        [PublicAPI]
        public static readonly Guid AllChromaDevices = new Guid(
            0x5bf2a39c,
            0x621b,
            0x4b04,
            0xba,
            0x4d,
            0xdf,
            0xfc,
            0x10,
            0x7c,
            0x21,
            0x4d);

        /// <summary>
        /// Returns whether a specified <see cref="Guid" /> is a valid device identifier.
        /// </summary>
        /// <param name="id">the <see cref="Guid" /> to check.</param>
        /// <returns><c>true</c> if it's a valid device identifier, otherwise <c>false</c>.</returns>
        [PublicAPI]
        public static bool IsValidId(Guid id)
        {
            return id == BlackwidowChroma || id == DeathadderChroma || id == NagaEpicChroma || id == Kraken71Chroma
                   || id == FireflyChroma || id == AllChromaDevices;
        }
    }
}
