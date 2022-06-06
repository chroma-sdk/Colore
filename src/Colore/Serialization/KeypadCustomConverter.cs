// ---------------------------------------------------------------------------------------
// <copyright file="KeypadCustomConverter.cs" company="Corale">
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

    using Colore.Effects.Keypad;
    using Colore.Rest.Data;

    /// <inheritdoc />
    /// <summary>
    /// Converts keypad <see cref="CustomKeypadEffect" /> objects to JSON.
    /// </summary>
    /// <remarks>Does not support converting JSON into <see cref="CustomKeypadEffect" /> objects.</remarks>
    [SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by Newtonsoft.Json")]
    internal sealed class KeypadCustomConverter : JsonConverter<CustomKeypadEffect>
    {
        /// <inheritdoc />
        public override CustomKeypadEffect Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            throw new NotSupportedException("Only writing of keypad Custom objects is supported.");

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, CustomKeypadEffect value, JsonSerializerOptions options)
        {
            var colors = value.ToMultiArray();
            var data = new EffectData(KeypadEffectType.Custom, colors);
            JsonSerializer.Serialize(writer, data, options);
        }
    }
}
