// ---------------------------------------------------------------------------------------
// <copyright file="Devices.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    /// <summary>
    /// Contains device IDs for devices that have Chroma support.
    /// </summary>
    public static class Devices
    {
        /// <summary>
        /// Blackwidow Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow Chroma")]
        public static readonly Guid Blackwidow = new Guid(
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
        [Description("Razer Deathadder Chroma")]
        public static readonly Guid Deathadder = new Guid(
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
        /// Kraken 7.1 Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Kraken 7.1 Chroma")]
        public static readonly Guid Kraken71 = new Guid(
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
        [Description("Razer Firefly Chroma")]
        public static readonly Guid Firefly = new Guid(
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
        /// Orbweaver Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Orbweaver Chroma")]
        public static readonly Guid Orbweaver = new Guid(
            0x9d24b0ab,
            0x162,
            0x466c,
            0x96,
            0x40,
            0x7a,
            0x92,
            0x4a,
            0xa4,
            0xd9,
            0xfd);

        /// <summary>
        /// Tartarus Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Tartarus Chroma")]
        public static readonly Guid Tartarus = new Guid(
            0xf0545c,
            0xe180,
            0x4ad1,
            0x8e,
            0x8a,
            0x41,
            0x90,
            0x61,
            0xce,
            0x50,
            0x5e);

        /// <summary>
        /// Mamba TE Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Mamba Chroma Tournament Edition")]
        public static readonly Guid MambaTe = new Guid(
            0x7ec00450,
            0xe0ee,
            0x4289,
            0x89,
            0xd5,
            0xd,
            0x87,
            0x9c,
            0x19,
            0x6,
            0x1a);

        /// <summary>
        /// BlackWidow TE Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow Chroma Tournament Edition")]
        public static readonly Guid BlackwidowTe = new Guid(
            0xed1c1b82,
            0xbfbe,
            0x418f,
            0xb4,
            0x9d,
            0xd0,
            0x3f,
            0x5,
            0xb1,
            0x49,
            0xdf);

        /// <summary>
        /// Deathstalker Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Deathstalker Chroma")]
        public static readonly Guid Deathstalker = new Guid(
            0x18c5ad9b,
            0x4326,
            0x4828,
            0x92,
            0xc4,
            0x26,
            0x69,
            0xa6,
            0x6d,
            0x22,
            0x83);

        /// <summary>
        /// Diamondback Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Diamondback Chroma")]
        public static readonly Guid Diamondback = new Guid(
            0xff8a5929,
            0x4512,
            0x4257,
            0x8d,
            0x59,
            0xc6,
            0x47,
            0xbf,
            0x99,
            0x35,
            0xd0);

        /// <summary>
        /// Orochi Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Orochi Chroma")]
        public static readonly Guid Orochi = new Guid(
            0x52c15681,
            0x4ece,
            0x4dd9,
            0x8a,
            0x52,
            0xa1,
            0x41,
            0x84,
            0x59,
            0xeb,
            0x34);

        /// <summary>
        /// Blade Stealth.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blade Stealth Chroma")]
        public static readonly Guid BladeStealth = new Guid(
            0xc83bdfe8,
            0xe7fc,
            0x40e0,
            0x99,
            0xdb,
            0x87,
            0x2e,
            0x23,
            0xf1,
            0x98,
            0x91);

        /// <summary>
        /// Blade 14 (2016 edition).
        /// </summary>
        [PublicAPI]
        [Description("Razer Blade 2014 Chroma")]
        public static readonly Guid Blade14 = new Guid(
            0xf2bedfaf,
            0xa0fe,
            0x4651,
            0x9d,
            0x41,
            0xb6,
            0xce,
            0x60,
            0x3a,
            0x3d,
            0xdd);

        /// <summary>
        /// Overwatch Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Overwatch Keyboard Chroma")]
        public static readonly Guid OverwatchKeyboard = new Guid(
            0x872ab2a9,
            0x7959,
            0x4478,
            0x9f,
            0xed,
            0x15,
            0xf6,
            0x18,
            0x6e,
            0x72,
            0xe4);

        /// <summary>
        /// Blackwidow X Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow X Chroma")]
        public static readonly Guid BlackwidowX = new Guid(
            0x5af60076,
            0xade9,
            0x43d4,
            0xb5,
            0x74,
            0x52,
            0x59,
            0x92,
            0x93,
            0xb5,
            0x54);

        /// <summary>
        /// Blackwidow X TE Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow X Chroma Tournament Edition")]
        public static readonly Guid BlackwidowXTe = new Guid(
            0x2d84dd51,
            0x3290,
            0x4aac,
            0x9a,
            0x89,
            0xd8,
            0xaf,
            0xde,
            0x38,
            0xb5,
            0x7c);

        /// <summary>
        /// Mamba (wireless) Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Mamba Chroma")]
        public static readonly Guid Mamba = new Guid(
            0xd527cbdc,
            0xeb0a,
            0x483a,
            0x9e,
            0x89,
            0x66,
            0xd5,
            0x4,
            0x63,
            0xec,
            0x6c);

        /// <summary>
        /// Naga Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Chroma")]
        public static readonly Guid Naga = new Guid(
            0xf1876328,
            0x6ca4,
            0x46ae,
            0xbe,
            0x4,
            0xbe,
            0x81,
            0x2b,
            0x41,
            0x44,
            0x33);

        /// <summary>
        /// Naga Epic Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Epic Chroma")]
        public static readonly Guid NagaEpic = new Guid(
            0xd714c50b,
            0x7158,
            0x4368,
            0xb9,
            0x9c,
            0x60,
            0x1a,
            0xcb,
            0x98,
            0x5e,
            0x98);

        /// <summary>
        /// Naga Hex V2
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Hex Chroma")]
        public static readonly Guid NagaHex = new Guid(
            0x195d70f5,
            0xf285,
            0x4cff,
            0x99,
            0xf2,
            0xb8,
            0xc0,
            0xe9,
            0x65,
            0x8d,
            0xb4);

        /// <summary>
        /// Core (external graphics enclosure).
        /// </summary>
        [PublicAPI]
        [Description("Razer Core Chroma")]
        public static readonly Guid Core = new Guid(
            0x201203b,
            0x62f3,
            0x4c50,
            0x83,
            0xdd,
            0x59,
            0x8b,
            0xab,
            0xd2,
            0x8,
            0xe0);

        /// <summary>
        /// Chroma enabled Lenovo Y900.
        /// </summary>
        [PublicAPI]
        [Description("Lenovo Y900")]
        public static readonly Guid LenovoY900 = new Guid(
            0x35f6f18d,
            0x1ae5,
            0x436c,
            0xa5,
            0x75,
            0xab,
            0x44,
            0xa1,
            0x27,
            0x90,
            0x3a);

        /// <summary>
        /// Chroma enabled Lenovo Y27.
        /// </summary>
        [PublicAPI]
        [Description("Lenovo Y27")]
        public static readonly Guid LenovoY27 = new Guid(
            0x47db1fa7,
            0x6b9b,
            0x4ee6,
            0xb6,
            0xf4,
            0x40,
            0x71,
            0xa3,
            0xb2,
            0x5,
            0x3b);

        /// <summary>
        /// Razer Ornata Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Ornata Chroma")]
        public static readonly Guid Ornata = new Guid(
            0x803378c1,
            0xcc48,
            0x4970,
            0x85,
            0x39,
            0xd8,
            0x28,
            0xcc,
            0x1d,
            0x42,
            0xa);

        /// <summary>
        /// Dictionary holding cached metadata for each device GUID.
        /// </summary>
        private static readonly ConcurrentDictionary<Guid, Metadata> MetadataCache =
            new ConcurrentDictionary<Guid, Metadata>();

        /// <summary>
        /// Returns whether a specified <see cref="Guid" /> is a valid device identifier.
        /// </summary>
        /// <param name="id">the <see cref="Guid" /> to check.</param>
        /// <returns><c>true</c> if it's a valid device identifier, otherwise <c>false</c>.</returns>
        [PublicAPI]
        public static bool IsValidId(Guid id)
        {
            return id == Blackwidow || id == Deathadder || id == Orbweaver || id == Tartarus || id == MambaTe
                   || id == BlackwidowTe || id == Kraken71 || id == Firefly || id == Deathstalker || id == Diamondback
                   || id == Mamba || id == OverwatchKeyboard || id == Orochi || id == BladeStealth || id == Naga
                   || id == NagaEpic || id == Core || id == LenovoY27 || id == LenovoY900 || id == Blade14
                   || id == BlackwidowX || id == BlackwidowXTe || id == NagaHex || id == Ornata;
        }

        /// <summary>
        /// Gets the name associated with a specified device ID.
        /// </summary>
        /// <param name="deviceId">The device ID to get a name for.</param>
        /// <returns>The name of the device.</returns>
        /// <exception cref="ArgumentException">Thrown if the device ID is invalid.</exception>
        [PublicAPI]
        public static string GetName(Guid deviceId)
        {
            return GetDeviceMetadata(deviceId).Name;
        }

        /// <summary>
        /// Gets the description associated with a specified device ID.
        /// </summary>
        /// <param name="deviceId">The device ID to get a description for.</param>
        /// <returns>The description of the device.</returns>
        /// <exception cref="ArgumentException">Thrown if the device ID is invalid.</exception>
        [PublicAPI]
        public static string GetDescription(Guid deviceId)
        {
            return GetDeviceMetadata(deviceId).Description;
        }

        /// <summary>
        /// Gets device metadata for a specific device ID.
        /// </summary>
        /// <param name="deviceId">The device ID to get metadata for.</param>
        /// <returns>An instance of <see cref="Metadata" /> for the specified device.</returns>
        internal static Metadata GetDeviceMetadata(Guid deviceId)
        {
            return !MetadataCache.ContainsKey(deviceId) ? BuildMetadataCacheWithId(deviceId) : MetadataCache[deviceId];
        }

        /// <summary>
        /// Builds the metadata cache and returns cache entry for the specified device ID.
        /// </summary>
        /// <param name="deviceId">Device ID to return metadata for.</param>
        /// <returns>An instance of <see cref="Metadata" /> for the specified device.</returns>
        private static Metadata BuildMetadataCacheWithId(Guid deviceId)
        {
            var fields = typeof(Devices).GetFields(BindingFlags.Static | BindingFlags.Public)
                                        .Where(p => p.FieldType == typeof(Guid));

            var mappings = fields.ToDictionary(
                f => (Guid)f.GetValue(null),
                f =>
                {
                    var descriptionAttr = f.GetCustomAttribute<DescriptionAttribute>();
                    return new Metadata(f.Name, descriptionAttr?.Description);
                });

            foreach (var mapping in mappings)
            {
                MetadataCache.TryAdd(mapping.Key, mapping.Value);
            }

            if (mappings.ContainsKey(deviceId))
                return mappings[deviceId];

            throw new ArgumentException(
                "The specified device ID was not found in the known devices.",
                nameof(deviceId));
        }

        /// <summary>
        /// Holds metadata for a device.
        /// </summary>
        internal struct Metadata
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Metadata" /> struct.
            /// </summary>
            /// <param name="name">Device name.</param>
            /// <param name="description">Device description.</param>
            internal Metadata(string name, string description)
            {
                Name = name;
                Description = description ?? name;
            }

            /// <summary>
            /// Gets the name of the device.
            /// </summary>
            internal string Name { get; }

            /// <summary>
            /// Gets a description of the device.
            /// </summary>
            internal string Description { get; }
        }
    }
}
