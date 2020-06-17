// ---------------------------------------------------------------------------------------
// <copyright file="KeyboardCustomConverter.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Serialization
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Colore.Effects.Keyboard;
    using Colore.Rest.Data;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Converts keyboard <see cref="CustomKeyboardEffect" /> objects to JSON.
    /// </summary>
    /// <remarks>Does not support converting JSON into <see cref="CustomKeyboardEffect" /> objects.</remarks>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class KeyboardCustomConverter : JsonConverter
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of a keyboard <see cref="CustomKeyboardEffect" /> object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The <see cref="CustomKeyboardEffect" /> value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var effect = (CustomKeyboardEffect)value;
#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
            var (colors, keys) = effect.ToMultiArrays();
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
            var data = new EffectData(KeyboardEffectType.CustomKey, new { color = colors, key = keys });
            serializer.Serialize(writer, data);
        }

        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotSupportedException("Only writing of Keyboard Custom objects is supported.");
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(CustomKeyboardEffect);
    }
}
