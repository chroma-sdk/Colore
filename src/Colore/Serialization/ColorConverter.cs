// ---------------------------------------------------------------------------------------
// <copyright file="ColorConverter.cs" company="Corale">
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

namespace Colore.Serialization
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    using Colore.Data;

    /// <inheritdoc />
    /// <summary>
    /// Converts <see cref="Color" /> to/from JSON.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class ColorConverter : JsonConverter<Color>
    {
        /// <inheritdoc />
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var hasElement = JsonElement.TryParseValue(ref reader, out var elementResult);

            if (!hasElement || !elementResult.HasValue)
            {
                throw new JsonException("Could not find a JsonElement to deserialize into Color");
            }

            var element = elementResult.Value;

            if (element.ValueKind is JsonValueKind.Number && element.TryGetUInt32(out var value))
            {
                return new Color(value);
            }

            if (element.ValueKind is not JsonValueKind.Object)
            {
                throw new JsonException("Only integers and Color objects can be converted to Color");
            }

            var hasValueProperty = element.TryGetProperty(nameof(Color.Value), out var valueProperty);

            if (!hasValueProperty)
            {
                throw new JsonException("Cannot deserialize Color object with missing Value property");
            }

            var hasValue = valueProperty.TryGetUInt32(out var propertyValue);

            if (!hasValue)
            {
                throw new JsonException("Failed to get UInt32 value from Value property");
            }

            return new Color(propertyValue);
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}
