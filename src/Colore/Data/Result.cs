// ---------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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
    using System.Collections.Generic;

    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Struct for containing the result of running a native Chroma SDK function.
    /// </summary>
    /// <remarks>
    /// <c>RZRESULT</c> is a <c>typedef</c> of <c>LONG</c> on C-side. <c>LONG</c> is always 32-bit in WinAPI.
    /// This means we don't need to have architecture-dependent base type.
    /// </remarks>
    [JsonConverter(typeof(ResultConverter))]
    public struct Result : IEquatable<int>, IEquatable<Result>
    {
        /// <summary>
        /// Access denied.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzAccessDenied = 5;

        /// <summary>
        /// Generic fail error.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzFailed = unchecked(-2147467259);

        /// <summary>
        /// Invalid error.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzInvalid = -1;

        /// <summary>
        /// Invalid parameter passed to function.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzInvalidParameter = 87;

        /// <summary>
        /// The requested operation is not supported.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzNotSupported = 50;

        /// <summary>
        /// The request was aborted.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzRequestAborted = 1235;

        /// <summary>
        /// Resource not available or disabled.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzResourceDisabled = 4309;

        /// <summary>
        /// Cannot start more than one instance of the specified program.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzSingleInstanceApp = 1152;

        /// <summary>
        /// Returned when a function is successful.
        /// </summary>
        [PublicAPI]
        public static readonly Result RzSuccess = 0;

        /// <summary>
        /// Contains a mapping of result codes to their names and descriptions.
        /// </summary>
        private static readonly Dictionary<Result, KeyValuePair<string, string>> Mappings =
            new Dictionary<Result, KeyValuePair<string, string>>
            {
                { 5, new KeyValuePair<string, string>(nameof(RzAccessDenied), "Access denied.") },
                { unchecked(-2147467259), new KeyValuePair<string, string>(nameof(RzFailed), "General failure.") },
                { -1, new KeyValuePair<string, string>(nameof(RzInvalid), "Invalid.") },
                {
                    87,
                    new KeyValuePair<string, string>(nameof(RzInvalidParameter), "Invalid parameter.")
                },
                { 50, new KeyValuePair<string, string>(nameof(RzNotSupported), "Not supported.") },
                { 1235, new KeyValuePair<string, string>(nameof(RzRequestAborted), "Request aborted.") },
                {
                    4309,
                    new KeyValuePair<string, string>(nameof(RzResourceDisabled), "Resource not available or disabled.")
                },
                {
                    1152,
                    new KeyValuePair<string, string>(
                        nameof(RzSingleInstanceApp),
                        "Cannot start more than one instance of the specified program.")
                },
                { 0, new KeyValuePair<string, string>(nameof(RzSuccess), "Success.") }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> struct.
        /// </summary>
        /// <param name="value">Value to store.</param>
        public Result(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the internal result value.
        /// </summary>
        [PublicAPI]
        public int Value { get; }

        /// <summary>
        /// Gets the help description for the current error value.
        /// </summary>
        public string Description => Mappings.ContainsKey(this) ? Mappings[this].Value : "Unknown.";

        /// <summary>
        /// Gets a value indicating whether the result means failure.
        /// </summary>
        public bool Failed => this != RzSuccess;

        /// <summary>
        /// Gets the name of the error as defined in source code.
        /// </summary>
        public string Name => Mappings.ContainsKey(this) ? Mappings[this].Key : "Unknown";

        /// <summary>
        /// Gets a value indicating whether the result was a success.
        /// </summary>
        public bool Success => this == RzSuccess;

        /// <summary>
        /// Gets a value indicating whether this instance of <see cref="Result" /> is truthy.
        /// </summary>
        /// <returns><c>true</c> if this is a successful result, otherwise <c>false</c>.</returns>
        public bool IsTrue => this;

        /// <summary>
        /// Indicates whether an instance of the <see cref="Result" /> struct is
        /// equal to another object.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Result" /> struct.</param>
        /// <param name="right">Right operand, an object to compare with.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        [Pure]
        public static bool operator ==(Result left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Converts a <see cref="Result" /> object to <c>false</c>.
        /// </summary>
        /// <param name="result"><see cref="Result" /> object to convert.</param>
        /// <returns>
        /// <c>false</c> if the <see cref="Result" /> object represents a boolean <c>false</c> value,
        /// otherwise <c>true</c>.
        /// </returns>
        [Pure]
        public static bool operator false(Result result)
        {
            return !result;
        }

#pragma warning disable SA1201 // Elements must appear in the correct order

        /// <summary>
        /// Converts a <see cref="Result" /> struct to its integer equivalent.
        /// </summary>
        /// <param name="result">An instance of the <see cref="Result" /> to convert.</param>
        /// <returns>The integer equivalent of the <paramref name="result" />.</returns>
        [Pure]
        public static implicit operator int(Result result)
        {
            return result.Value;
        }

#pragma warning restore SA1201 // Elements must appear in the correct order

        /// <summary>
        /// Converts an integer value to its equivalent <see cref="Result" /> object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The <see cref="Result" /> equivalent of the <paramref name="value" />.</returns>
        [Pure]
        public static implicit operator Result(int value)
        {
            return new Result(value);
        }

        /// <summary>
        /// Converts an instance of the <see cref="Result" /> struct to its boolean equivalent.
        /// </summary>
        /// <param name="result">The <see cref="Result" /> to convert.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="result" /> represents a <c>true</c> value (success),
        /// otherwise <c>false</c> (failure).
        /// </returns>
        [Pure]
        public static implicit operator bool(Result result)
        {
            return result == RzSuccess;
        }

        /// <summary>
        /// Indicates whether an instance of the <see cref="Result" /> struct and
        /// another object are not equal.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Result" /> struct.</param>
        /// <param name="right">Right operand, an object to compare to.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        [Pure]
        public static bool operator !=(Result left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Converts a <see cref="Result" /> object to a boolean <c>true</c> value.
        /// </summary>
        /// <param name="result">Object to convert.</param>
        /// <returns><c>true</c> if the object represents a boolean <c>true</c> value, <c>false</c> otherwise.</returns>
        [Pure]
        public static bool operator true(Result result)
        {
            return result;
        }

        /// <summary>
        /// Convert an integer value to a <see cref="Result" /> object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>An instance of <see cref="Result" />.</returns>
        [Pure]
        public static Result FromResult(int value)
        {
            return value;
        }

        /// <summary>
        /// Converts this instance of <see cref="Result" /> to an integer value.
        /// </summary>
        /// <returns>The integer value of this <see cref="Result" />.</returns>
        [Pure]
        public int ToInt32()
        {
            return this;
        }

        /// <summary>
        /// Converts this <see cref="Result" /> to a boolean value.
        /// </summary>
        /// <returns>The result of the conversion.</returns>
        [Pure]
        public bool ToBoolean()
        {
            return this;
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the internal value of the current <see cref="Result" /> struct
        /// is equal to another value.
        /// </summary>
        /// <param name="other">A value to compare with this object's internal value.</param>
        /// <returns><c>true</c> if the internal value is equal to the <paramref name="other" /> parameter, otherwise <c>false</c>.</returns>
        [Pure]
        public bool Equals(int other)
        {
            return Value.Equals(other);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public bool Equals(Result other)
        {
            return Equals(other.Value);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to. </param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance are the same type
        /// and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        [Pure]
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            switch (obj)
            {
                case Result result:
                    return Equals(result);

                case int value:
                    return Equals(value);
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        [Pure]
        public override int GetHashCode()
        {
            return Value;
        }

        /// <summary>
        /// Returns a string representation of the result.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> containing a string representation
        /// of the result complete with name, description, and numeric value.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        [Pure]
        public override string ToString()
        {
            return $"{Name}: {Description} ({Value})";
        }
    }
}
