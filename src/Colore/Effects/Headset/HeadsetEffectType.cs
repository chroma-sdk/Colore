// ---------------------------------------------------------------------------------------
// <copyright file="HeadsetEffectType.cs" company="Corale">
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

namespace Colore.Effects.Headset
{
    using System.Runtime.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Standard effects for headsets.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HeadsetEffectType
    {
        /// <summary>
        /// No effect.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_NONE")]
        None = 0,

        /// <summary>
        /// Static color.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_STATIC")]
        Static = 1,

        /// <summary>
        /// Custom headset effect.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_CUSTOM")]
        Custom = 4,

        /// <summary>
        /// Invalid effect.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_INVALID")]
        Invalid = 5
    }
}
