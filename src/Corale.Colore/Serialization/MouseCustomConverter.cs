// ---------------------------------------------------------------------------------------
// <copyright file="MouseCustomConverter.cs" company="Corale">
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

namespace Corale.Colore.Serialization
{
    using System;

    using Corale.Colore.Effects.Mouse;
    using Corale.Colore.Rest.Data;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Converts mouse <see cref="Custom" /> objects to JSON.
    /// </summary>
    /// <remarks>Does not support converting JSON into <see cref="Custom" /> objects.</remarks>
    internal sealed class MouseCustomConverter : JsonConverter
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of a mouse <see cref="Custom" /> object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The <see cref="Custom" /> value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var effect = (Custom)value;
#pragma warning disable SA1008 // Opening parenthesis must be spaced correctly
            var colors = effect.ToMultiArray();
#pragma warning restore SA1008 // Opening parenthesis must be spaced correctly
            var data = new EffectData(Effect.Custom, colors);
            serializer.Serialize(writer, data);
        }

        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            throw new NotSupportedException("Only writing of mouse Custom objects is supported.");
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(Custom);
    }
}
