// ---------------------------------------------------------------------------------------
// <copyright file="Size.cs" company="">
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
//     Disclaimer: Colore is in no way affiliated with Razer and/or any of its employees
//     and/or licensors. Adam Hellberg and Brandon Scott do not take responsibility
//     for any harm caused, direct or indirect, to any Razer peripherals
//     via the use of Colore.
// 
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Colore.Razer
{
    using System;

#if WIN64
    using size_t = System.UInt64;
#elif WIN32
    using size_t = System.UInt32;
#else
#error Unsupported build configuration.
#endif

    public struct Size : IComparable, IFormattable, IConvertible, IComparable<size_t>, IEquatable<size_t>
    {
        private readonly size_t _value;

        public Size(size_t value)
        {
            _value = value;
        }

        public static implicit operator size_t(Size size)
        {
            return size._value;
        }

        public static implicit operator Size(size_t sizet)
        {
            return new Size(sizet);
        }

        public int CompareTo(object obj)
        {
            return ((IComparable)_value).CompareTo(obj);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ((IFormattable)_value).ToString(format, formatProvider);
        }

        public TypeCode GetTypeCode()
        {
            return ((IConvertible)_value).GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToBoolean(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToChar(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToSByte(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToByte(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt16(provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt32(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt64(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt64(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToSingle(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDouble(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDecimal(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDateTime(provider);
        }

        public string ToString(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)_value).ToType(conversionType, provider);
        }

        public int CompareTo(size_t other)
        {
            return _value.CompareTo(other);
        }

        public bool Equals(size_t other)
        {
            return _value.Equals(other);
        }
    }
}
