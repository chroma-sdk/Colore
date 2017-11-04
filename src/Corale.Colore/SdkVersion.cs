// ---------------------------------------------------------------------------------------
// <copyright file="SdkVersion.cs" company="Corale">
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

namespace Corale.Colore
{
    using System;

    using JetBrains.Annotations;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <inheritdoc cref="IComparable{T}" />
    /// <inheritdoc cref="IComparable" />
    /// <summary>
    /// Describes an SDK version.
    /// </summary>
    public struct SdkVersion : IEquatable<SdkVersion>, IComparable<SdkVersion>, IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SdkVersion" /> struct.
        /// </summary>
        /// <param name="major">The major component of the version.</param>
        /// <param name="minor">The minor component of the version.</param>
        /// <param name="revision">The revision component of the version.</param>
        public SdkVersion(int major, int minor, int revision)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
        }

        /// <summary>
        /// The major part of the version.
        /// </summary>
        [PublicAPI]
        public int Major { get; }

        /// <summary>
        /// The minor part of the version.
        /// </summary>
        [PublicAPI]
        public int Minor { get; }

        /// <summary>
        /// The revision part of the version.
        /// </summary>
        [PublicAPI]
        public int Revision { get; }

        /// <summary>
        /// Compares an instance of <see cref="SdkVersion" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(SdkVersion left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="SdkVersion" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(SdkVersion left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Checks if an instance of <see cref="SdkVersion" /> is considered
        /// less than another object.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is less than <paramref name="right" />, otherwise <c>false</c>.</returns>
        public static bool operator <(SdkVersion left, object right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Checks if an instance of <see cref="SdkVersion" /> is considered
        /// greater than another object.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is greater than <paramref name="right" />, otherwise <c>false</c>.</returns>
        public static bool operator >(SdkVersion left, object right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Checks if an instance of <see cref="SdkVersion" /> is considered
        /// to be less than or equal to another object.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />, otherwise <c>false</c>.</returns>
        public static bool operator <=(SdkVersion left, object right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Checks if an instance of <see cref="SdkVersion" /> is considered
        /// to be greater than or equal to another object.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="SdkVersion" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if <paramref name="left" /> is greater than or equal to <paramref name="right" />, otherwise <c>false</c>.</returns>
        public static bool operator >=(SdkVersion left, object right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(SdkVersion other)
        {
            return Major == other.Major && Minor == other.Minor && Revision == other.Revision;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            return obj is SdkVersion version && Equals(version);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Major;
                hashCode = (hashCode * 397) ^ Minor;
                hashCode = (hashCode * 397) ^ Revision;
                return hashCode;
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <list type="table">
        /// <listheader>
        /// <item>Value</item>
        /// <item>Meaning</item>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <term>This object is less than the <paramref name="other" /> parameter</term>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <term>This object is equal to <paramref name="other" /></term>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <term>This object is greater than <paramref name="other" /></term>
        /// </item>
        /// </list>
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(SdkVersion other)
        {
            if (Major != other.Major)
                return Major.CompareTo(other.Major);

            return Minor != other.Minor ? Minor.CompareTo(other.Minor) : Revision.CompareTo(other.Revision);
        }

        /// <inheritdoc />
        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates
        /// whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table">
        /// <listheader>
        /// <item>Value</item>
        /// <item>Meaning</item>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <term>This object is less than the <paramref name="obj" /> parameter</term>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <term>This object is equal to <paramref name="obj" /></term>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <term>This object is greater than <paramref name="obj" /></term>
        /// </item>
        /// </list>
        /// </returns>
        /// <param name="obj">An object to compare with this instance. </param>
        /// <exception cref="T:System.ArgumentException"><paramref name="obj" /> is not the same type as this instance. </exception>
        public int CompareTo(object obj)
        {
            if (!(obj is SdkVersion version))
                throw new ArgumentException("Object must be of type SdkVersion", nameof(obj));

            return CompareTo(version);
        }

        /// <summary>
        /// Returns the string representation of this SDK version.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> representing this struct instance.
        /// </returns>
        public override string ToString()
        {
            return $"{Major}.{Minor}.{Revision}";
        }
    }
}
