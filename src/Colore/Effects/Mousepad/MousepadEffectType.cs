// ---------------------------------------------------------------------------------------
// <copyright file="MousepadEffectType.cs" company="Corale">
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

namespace Colore.Effects.Mousepad
{
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    using JetBrains.Annotations;

    /// <summary>
    /// Effects supported by Chroma mouse pads.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MousepadEffectType
    {
        /// <summary>
        /// No effect.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_NONE")]
        None = 0,

        /// <summary>
        /// A custom effect.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_CUSTOM")]
        Custom = 2,

        /// <summary>
        /// Static color.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_STATIC")]
        Static = 4,

        /// <summary>
        /// Invalid effect type.
        /// </summary>
        [PublicAPI]
        [EnumMember(Value = "CHROMA_INVALID")]
        Invalid = 6
    }
}
