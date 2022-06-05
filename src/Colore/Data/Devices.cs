// ---------------------------------------------------------------------------------------
// <copyright file="Devices.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Diagnostics;
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
        public static readonly Guid Blackwidow = new Guid("2ea1bb63ca28428d9f06196b88330bbb");

        /// <summary>
        /// Deathadder Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Deathadder Chroma")]
        public static readonly Guid Deathadder = new Guid("aec50d91b1f1452f8e167b73f376fdf3");

        /// <summary>
        /// Kraken 7.1 Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Kraken 7.1 Chroma")]
        public static readonly Guid Kraken71 = new Guid("cd1e09a5d5e64a6ca93be6d9bf1d2092");

        /// <summary>
        /// Firefly Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Firefly Chroma")]
        public static readonly Guid Firefly = new Guid("80f95a9473d248caae9a0986789a9af2");

        /// <summary>
        /// Orbweaver Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Orbweaver Chroma")]
        public static readonly Guid Orbweaver = new Guid("9d24b0ab0162466c96407a924aa4d9fd");

        /// <summary>
        /// Tartarus Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Tartarus Chroma")]
        public static readonly Guid Tartarus = new Guid("00f0545ce1804ad18e8a419061ce505e");

        /// <summary>
        /// Mamba TE Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Mamba Chroma Tournament Edition")]
        public static readonly Guid MambaTe = new Guid("7ec00450e0ee428989d50d879c19061a");

        /// <summary>
        /// BlackWidow TE Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow Chroma Tournament Edition")]
        public static readonly Guid BlackwidowTe = new Guid("ed1c1b82bfbe418fb49dd03f05b149df");

        /// <summary>
        /// Deathstalker Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Deathstalker Chroma")]
        public static readonly Guid Deathstalker = new Guid("18c5ad9b4326482892c42669a66d2283");

        /// <summary>
        /// Diamondback Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Diamondback Chroma")]
        public static readonly Guid Diamondback = new Guid("ff8a5929451242578d59c647bf9935d0");

        /// <summary>
        /// Orochi Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Orochi Chroma")]
        public static readonly Guid Orochi = new Guid("52c156814ece4dd98a52a1418459eb34");

        /// <summary>
        /// Blade Stealth.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blade Stealth Chroma")]
        public static readonly Guid BladeStealth = new Guid("c83bdfe8e7fc40e099db872e23f19891");

        /// <summary>
        /// Blade 14 (2016 edition).
        /// </summary>
        [PublicAPI]
        [Description("Razer Blade 2014 Chroma")]
        public static readonly Guid Blade14 = new Guid("f2bedfafa0fe46519d41b6ce603a3ddd");

        /// <summary>
        /// Overwatch Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Overwatch Keyboard Chroma")]
        public static readonly Guid OverwatchKeyboard = new Guid("872ab2a9795944789fed15f6186e72e4");

        /// <summary>
        /// Blackwidow X Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow X Chroma")]
        public static readonly Guid BlackwidowX = new Guid("5af60076ade943d4b57452599293b554");

        /// <summary>
        /// Blackwidow X TE Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Blackwidow X Chroma Tournament Edition")]
        public static readonly Guid BlackwidowXTe = new Guid("2d84dd5132904aac9a89d8afde38b57c");

        /// <summary>
        /// Mamba (wireless) Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Mamba Chroma")]
        public static readonly Guid Mamba = new Guid("d527cbdceb0a483a9e8966d50463ec6c");

        /// <summary>
        /// Naga Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Chroma")]
        public static readonly Guid Naga = new Guid("f18763286ca446aebe04be812b414433");

        /// <summary>
        /// Naga Epic Chroma edition.
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Epic Chroma")]
        public static readonly Guid NagaEpic = new Guid("d714c50b71584368b99c601acb985e98");

        /// <summary>
        /// Naga Hex V2.
        /// </summary>
        [PublicAPI]
        [Description("Razer Naga Hex Chroma")]
        public static readonly Guid NagaHex = new Guid("195d70f5f2854cff99f2b8c0e9658db4");

        /// <summary>
        /// Core (external graphics enclosure).
        /// </summary>
        [PublicAPI]
        [Description("Razer Core Chroma")]
        public static readonly Guid Core = new Guid("0201203b62f34c5083dd598babd208e0");

        /// <summary>
        /// Chroma enabled Lenovo Y900.
        /// </summary>
        [PublicAPI]
        [Description("Lenovo Y900")]
        public static readonly Guid LenovoY900 = new Guid("35f6f18d1ae5436ca575ab44a127903a");

        /// <summary>
        /// Chroma enabled Lenovo Y27.
        /// </summary>
        [PublicAPI]
        [Description("Lenovo Y27")]
        public static readonly Guid LenovoY27 = new Guid("47db1fa76b9b4ee6b6f44071a3b2053b");

        /// <summary>
        /// Razer Ornata Keyboard.
        /// </summary>
        [PublicAPI]
        [Description("Razer Ornata Chroma")]
        public static readonly Guid Ornata = new Guid("803378c1cc4849708539d828cc1d420a");

        /// <summary>
        /// Dictionary holding cached metadata for each device GUID.
        /// </summary>
        private static readonly ConcurrentDictionary<Guid, Metadata> MetadataCache =
            new ConcurrentDictionary<Guid, Metadata>();

        /// <summary>
        /// Boolean value keeping track of if the metadata cache has been built.
        /// </summary>
        private static volatile bool _cacheBuilt;

        /// <summary>
        /// Returns whether a specified <see cref="Guid" /> is a valid device identifier.
        /// </summary>
        /// <param name="id">the <see cref="Guid" /> to check.</param>
        /// <returns><c>true</c> if it's a valid device identifier, otherwise <c>false</c>.</returns>
        [PublicAPI]
        public static bool IsValidId(Guid id)
        {
            BuildMetadataCache();
            return MetadataCache.ContainsKey(id);
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
            return MetadataCache.ContainsKey(deviceId) ? MetadataCache[deviceId] : BuildMetadataCacheWithId(deviceId);
        }

        /// <summary>
        /// Builds the metadata cache.
        /// </summary>
        private static void BuildMetadataCache()
        {
            if (_cacheBuilt)
            {
                return;
            }

            var fields = typeof(Devices).GetFields(BindingFlags.Static | BindingFlags.Public)
                                        .Where(p => p.FieldType == typeof(Guid));

            var mappings = fields.ToDictionary(
                f =>
                {
                    var fieldValue = f.GetValue(null);
                    Debug.Assert(
                        fieldValue is not null,
                        "fieldValue is not null",
                        "Found device GUID field with NULL value");

#if NETSTANDARD2_0
                    var guid = (Guid)fieldValue!;
#else
                    var guid = (Guid)fieldValue;
#endif

                    return guid;
                },
                f =>
                {
                    var descriptionAttr = f.GetCustomAttribute<DescriptionAttribute>();
                    return new Metadata(f.Name, descriptionAttr?.Description);
                });

            var success = true;

            foreach (var mapping in mappings)
            {
                if (!MetadataCache.TryAdd(mapping.Key, mapping.Value))
                {
                    success = false;
                }
            }

            _cacheBuilt = success;
        }

        /// <summary>
        /// Builds the metadata cache and returns cache entry for the specified device ID.
        /// </summary>
        /// <param name="deviceId">Device ID to return metadata for.</param>
        /// <returns>An instance of <see cref="Metadata" /> for the specified device.</returns>
        private static Metadata BuildMetadataCacheWithId(Guid deviceId)
        {
            BuildMetadataCache();

            if (MetadataCache.ContainsKey(deviceId))
            {
                return MetadataCache[deviceId];
            }

            throw new ArgumentException(
                "The specified device ID was not found in the known devices.",
                nameof(deviceId));
        }

        /// <summary>
        /// Holds metadata for a device.
        /// </summary>
        internal readonly struct Metadata
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Metadata" /> struct.
            /// </summary>
            /// <param name="name">Device name.</param>
            /// <param name="description">Device description.</param>
            internal Metadata(string name, string? description)
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
