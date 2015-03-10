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

#endif

    [StructLayout(LayoutKind.Explicit, Size = sizeof(size_t))]
    public struct Size : IComparable<Size>, IEquatable<Size>, IComparable<size_t>, IEquatable<size_t>
    {
        [FieldOffset(0)]
        private readonly size_t _value;

        public Size(size_t value)
        {
            _value = value;
        }

        public static bool operator ==(Size left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Size left, object right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Size left, Size right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Size left, Size right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <(Size left, Size right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Size left, Size right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static implicit operator size_t(Size size)
        {
            return size._value;
        }

        public static implicit operator Size(size_t sizet)
        {
            return new Size(sizet);
        }

        public int CompareTo(size_t other)
        {
            return _value.CompareTo(other);
        }

        public int CompareTo(Size other)
        {
            return _value.CompareTo(other._value);
        }

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

        public bool Equals(size_t other)
        {
            return _value.Equals(other);
        }

        public bool Equals(Size other)
        {
            return _value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
