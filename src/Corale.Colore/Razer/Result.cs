// ---------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Razer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    /// <summary>
    /// Struct for containing the result of running a native Chroma SDK function.
    /// </summary>
    /// <remarks>
    /// <c>RZRESULT</c> is a <c>typedef</c> of <c>LONG</c> on C-side. <c>LONG</c> is always 32-bit in WinAPI.
    /// This means we don't need to have architecture-dependent base type.
    /// </remarks>
    public struct Result : IEquatable<int>, IEquatable<Result>
    {
        /// <summary>
        /// Access denied.
        /// </summary>
        [Description("Access denied.")]
        [PublicAPI]
        public static readonly Result RzAccessDenied = 5;

        /// <summary>
        /// Generic fail error.
        /// </summary>
        [Description("General failure.")]
        [PublicAPI]
        public static readonly Result RzFailed = unchecked((int)2147500037);

        /// <summary>
        /// Invalid error.
        /// </summary>
        [Description("Invalid.")]
        [PublicAPI]
        public static readonly Result RzInvalid = -1;

        /// <summary>
        /// Invalid parameter passed to function.
        /// </summary>
        [Description("Invalid parameter.")]
        [PublicAPI]
        public static readonly Result RzInvalidParameter = 87;

        /// <summary>
        /// The requested operation is not supported.
        /// </summary>
        [Description("Not supported.")]
        [PublicAPI]
        public static readonly Result RzNotSupported = 50;

        /// <summary>
        /// The request was aborted.
        /// </summary>
        [Description("Request aborted.")]
        [PublicAPI]
        public static readonly Result RzRequestAborted = 1235;

        /// <summary>
        /// Resource not available or disabled.
        /// </summary>
        [Description("Resource not available or disabled.")]
        [PublicAPI]
        public static readonly Result RzResourceDisabled = 4309;

        /// <summary>
        /// Cannot start more than one instance of the specified program.
        /// </summary>
        [Description("Cannot start more than one instance of the specified program.")]
        [PublicAPI]
        public static readonly Result RzSingleInstanceApp = 1152;

        /// <summary>
        /// Returned when a function is successful.
        /// </summary>
        [Description("Success.")]
        [PublicAPI]
        public static readonly Result RzSuccess = 0;

        /// <summary>
        /// Dictionary used to cache the metadata of the pre-defined error values.
        /// </summary>
        private static readonly IDictionary<Result, Metadata> FieldMetadata;

        /// <summary>
        /// Internal result value.
        /// </summary>
        private readonly int _value;

        /// <summary>
        /// Initializes static members of the <see cref="Result" /> struct.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2207:InitializeValueTypeStaticFieldsInline",
            Justification = "This is temporary(TM) until ReSharper fixes their bug with sorting members.")]
        static Result()
        {
            FieldMetadata = GetMetadata();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result" /> struct.
        /// </summary>
        /// <param name="value">Value to store.</param>
        public Result(int value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the help description for the current error value.
        /// </summary>
        public string Description => FieldMetadata.ContainsKey(this) ? FieldMetadata[this].Description : "Unknown.";

        /// <summary>
        /// Gets a value indicating whether the result means failure.
        /// </summary>
        public bool Failed => this != RzSuccess;

        /// <summary>
        /// Gets the name of the error as defined in source code.
        /// </summary>
        public string Name => FieldMetadata.ContainsKey(this) ? FieldMetadata[this].Name : "Unknown";

        /// <summary>
        /// Gets a value indicating whether the result was a success.
        /// </summary>
        public bool Success => this == RzSuccess;

        /// <summary>
        /// Indicates whether an instance of the <see cref="Result" /> struct is
        /// equal to another object.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Result" /> struct.</param>
        /// <param name="right">Right operand, an object to compare with.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
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
        public static implicit operator int(Result result)
        {
            return result._value;
        }
#pragma warning restore SA1201 // Elements must appear in the correct order

        /// <summary>
        /// Converts an integer value to its equivalent <see cref="Result" /> object.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The <see cref="Result" /> equivalent of the <paramref name="value" />.</returns>
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
        public static bool operator !=(Result left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Converts a <see cref="Result" /> object to a boolean <c>true</c> value.
        /// </summary>
        /// <param name="result">Object to convert.</param>
        /// <returns><c>true</c> if the object represents a boolean <c>true</c> value, <c>false</c> otherwise.</returns>
        public static bool operator true(Result result)
        {
            return result;
        }

        /// <summary>
        /// Indicates whether the internal value of the current <see cref="Result" /> struct
        /// is equal to another value.
        /// </summary>
        /// <param name="other">A value to compare with this object's internal value.</param>
        /// <returns><c>true</c> if the internal value is equal to the <paramref name="other" /> parameter, otherwise <c>false</c>.</returns>
        public bool Equals(int other)
        {
            return _value.Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Result other)
        {
            return Equals(other._value);
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
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Result)
                return Equals((Result)obj);

            if (obj is int)
                return Equals((int)obj);

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return _value;
        }

        /// <summary>
        /// Returns a string representation of the result.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a string representation
        /// of the result complete with name, description, and numeric value.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{Name}: {Description} ({_value})";
        }

        /// <summary>
        /// Retrieves field metadata and returns it as a dictionary.
        /// </summary>
        /// <returns>Field metadata.</returns>
        private static Dictionary<Result, Metadata> GetMetadata()
        {
            var cache = new Dictionary<Result, Metadata>();

            var fieldsInfo = typeof(Result).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var fieldInfo in fieldsInfo.Where(fi => fi.FieldType == typeof(Result)))
            {
                var value = fieldInfo.GetValue(null);
                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                if (attr != null && value is Result)
                    cache[(Result)value] = new Metadata(fieldInfo.Name, attr.Description);
            }

            return cache;
        }

        /// <summary>
        /// Contains metadata for a specific result in the <see cref="Result" /> struct.
        /// </summary>
        private struct Metadata
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Metadata" /> struct.
            /// </summary>
            /// <param name="name">Result name.</param>
            /// <param name="description">Result description.</param>
            internal Metadata(string name, string description)
            {
                Name = name;
                Description = description;
            }

            /// <summary>
            /// Gets a human-readable description for the result.
            /// </summary>
            internal string Description { get; }

            /// <summary>
            /// Gets the name of the result.
            /// </summary>
            internal string Name { get; }
        }

        /// <summary>
        /// Description attribute used for fields in the <see cref="Result" /> struct.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        private sealed class DescriptionAttribute : Attribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="DescriptionAttribute" /> class.
            /// </summary>
            /// <param name="description">Description to set.</param>
            internal DescriptionAttribute(string description)
            {
                Description = description;
            }

            /// <summary>
            /// Gets a human-readable description of the result.
            /// </summary>
            internal string Description { get; }
        }
    }
}
