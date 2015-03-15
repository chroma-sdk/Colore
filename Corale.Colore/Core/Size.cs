// ---------------------------------------------------------------------------------------
// <copyright file="Size.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Core
{
    using System;
    using System.Runtime.InteropServices;

#if WIN64

    using size_t = System.UInt64;

#elif WIN32

    using size_t = System.UInt32;

#else

    using size_t = System.UInt32;

#endif

    /// <summary>
    /// Dynamic size type for interacting with Razer's native code.
    /// Emulates <c>size_t</c> from C.
    /// </summary>
    /// <remarks>
    /// This struct will use either <see cref="ulong" /> or <see cref="uint" /> as its base,
    /// depending on compile-time or run-time architecture. On 64-bit systems, if compiled in <c>x64</c>,
    /// the base will be <see cref="ulong" />. On 32-bit systems, if compiled in <c>x86</c>,
    /// the base will be <see cref="uint" />. On 64- and 32-bit systems, if compiled in <c>Any CPU</c>,
    /// the base will be <see cref="uint" />, with context-sensitive code doing run-time conversions.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit, Size = sizeof(size_t))]
    public struct Size : IComparable<Size>, IEquatable<Size>, IComparable<size_t>, IEquatable<size_t>
    {
        [FieldOffset(0)]
        private readonly size_t _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Size" /> struct.
        /// </summary>
        /// <param name="value">Value to use.</param>
        public Size(size_t value)
        {
            _value = value;
        }

        /// <summary>
        /// Indicates whether an instance of <see cref="Size" /> is equal to another object.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Size" /> struct.</param>
        /// <param name="right">Right operand, an instance of an object, or <c>null</c>.</param>
        /// <returns><c>true</c> if the two operands are equal, <c>false</c> otherwise.</returns>
        public static bool operator ==(Size left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether a <see cref="Size" /> struct is greater than another
        /// <see cref="Size" /> struct.
        /// </summary>
        /// <param name="left">Left operand for use as the base of comparison.</param>
        /// <param name="right">Right operand to compare with.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="left" /> is greater than that of <paramref name="right" />, <c>false</c> otherwise.
        /// </returns>
        public static bool operator >(Size left, Size right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Indicates whether a <see cref="Size" /> struct is greater than or
        /// equal to another <see cref="Size" /> struct.
        /// </summary>
        /// <param name="left">Left operand for use as the base of comparison.</param>
        /// <param name="right">Right operand to compare with.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="left" /> is greater than or equal to that of
        /// <paramref name="right" />, otherwise <c>false</c>.
        /// </returns>
        public static bool operator >=(Size left, Size right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Converts a <see cref="Size" /> struct to a <see cref="size_t" />.
        /// </summary>
        /// <param name="size">An instance of <see cref="Size" /> to convert.</param>
        /// <returns>The resulting <see cref="size_t" /> value.</returns>
        public static implicit operator size_t(Size size)
        {
            return size._value;
        }

        /// <summary>
        /// Converts a <see cref="size_t" /> value to a new <see cref="Size" /> struct.
        /// </summary>
        /// <param name="sizet">The value to convert.</param>
        /// <returns>A new instance of the <see cref="Size" /> struct representing the value.</returns>
        public static implicit operator Size(size_t sizet)
        {
            return new Size(sizet);
        }

        /// <summary>
        /// Indicates whether an instance of <see cref="Size" /> is not equal to another object.
        /// </summary>
        /// <param name="left">Left operand, an instance of the <see cref="Size" /> struct.</param>
        /// <param name="right">Right operand, an object to compare with.</param>
        /// <returns><c>true</c> if the two objects are equal, <c>false</c> otherwise.</returns>
        public static bool operator !=(Size left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Indicates whether the value of a <see cref="Size" /> struct is less than
        /// that of another <see cref="Size" /> struct.
        /// </summary>
        /// <param name="left">Left operand to use as the base of comparison.</param>
        /// <param name="right">Right operand to compare with.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than <paramref name="right" />, otherwise <c>false</c>.
        /// </returns>
        public static bool operator <(Size left, Size right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Indicates whether the value of a <see cref="Size" /> struct is less than
        /// or equal to that of another <see cref="Size" /> struct.
        /// </summary>
        /// <param name="left">Left operand to use as the base of the comparison.</param>
        /// <param name="right">Right operand to compare with.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="left" /> is less than or equal to <paramref name="right" />.
        /// </returns>
        public static bool operator <=(Size left, Size right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Compares the internal value of the current <see cref="Size" /> struct to
        /// another value.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Meaning</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <term>The value is less than the <paramref name="other" /> parameter.</term>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <term>The value is equal to <paramref name="other" />.</term>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <term>The value is greater than <paramref name="other" />.</term>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(size_t other)
        {
            return _value.CompareTo(other);
        }

        /// <summary>
        /// Compares the current <see cref="Size" /> struct to another <see cref="Size" /> struct.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Meaning</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <term>This object is less than the <paramref name="other" /> parameter.</term>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <term>This object is equal to <paramref name="other" />.</term>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <term>This object is greater than <paramref name="other" />.</term>
        /// </item>
        /// </list>
        /// </returns>
        public int CompareTo(Size other)
        {
            return _value.CompareTo(other._value);
        }

        /// <summary>
        /// Indicates whether this instance of <see cref="Size" /> and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance of <see cref="Size" /> are the same type and
        /// represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (obj is Size)
                return Equals((Size)obj);

            if (obj is size_t)
                return Equals((size_t)obj);

            return false;
        }

        /// <summary>
        /// Indicates whether the stored <see cref="size_t" /> value in this instance of <see cref="Size" /> is
        /// equal to another <see cref="size_t" /> value <paramref name="other" />.
        /// </summary>
        /// <param name="other">A <see cref="size_t" /> value to compare the internal <see cref="size_t" /> value with.</param>
        /// <returns>
        /// <c>true</c> if the internal <see cref="size_t" /> value is equal to the <paramref name="other"/> value; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(size_t other)
        {
            return _value.Equals(other);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other"/> parameter; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(Size other)
        {
            return _value.Equals(other._value);
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
            return _value.GetHashCode();
        }

        /// <summary>
        /// Returns the string value of the internal <see cref="size_t" /> value.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> representing the
        /// numerical value of the internal <see cref="size_t" /> field.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
