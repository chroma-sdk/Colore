// ---------------------------------------------------------------------------------------
// <copyright file="MouseStaticConverter.cs" company="Corale">
//     Copyright © 2015-2021 by Adam Hellberg and Brandon Scott.
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

    using Colore.Effects.Mouse;
    using Colore.Rest.Data;

    using Newtonsoft.Json;

    /// <inheritdoc />
    /// <summary>
    /// Converts mouse <see cref="StaticMouseEffect" /> objects to JSON.
    /// </summary>
    /// <remarks>Does not support converting JSON into <see cref="StaticMouseEffect" /> objects.</remarks>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class MouseStaticConverter : JsonConverter
    {
        /// <inheritdoc />
        /// <summary>Writes the JSON representation of a mouse <see cref="StaticMouseEffect" /> object.</summary>
        /// <param name="writer">The <see cref="JsonWriter" /> to write to.</param>
        /// <param name="value">The <see cref="StaticMouseEffect" /> value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();

                return;
            }

            if (value is not StaticMouseEffect effect)
            {
                throw new JsonSerializationException(
                    $"{nameof(MouseStaticConverter)} only supports objects of type {nameof(StaticMouseEffect)}");
            }

            var data = new EffectData(MouseEffectType.Static, effect.Color);
            serializer.Serialize(writer, data);
        }

        /// <inheritdoc />
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            throw new NotSupportedException("Only writing of mouse Static objects is supported.");
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType) => objectType == typeof(StaticMouseEffect);
    }
}
