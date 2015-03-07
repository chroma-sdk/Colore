//  ---------------------------------------------------------------------------------------
//  <copyright file="RzResult.cs" company="">
//      Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy of
//      this software and associated documentation files (the "Software"), to deal in
//      the Software without restriction, including without limitation the rights to
//      use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//      of the Software, and to permit persons to whom the Software is furnished to do
//      so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in all
//      copies or substantial portions of the Software.
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//      WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//      CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//      Disclaimer: Colore is in no way affiliated
//      with Razer and/or any of its employees and/or licensors.
//      Adam Hellberg and Brandon Scott do not take responsibility for any harm caused, direct
//      or indirect, to any Razer peripherals via the use of Colore.
//
//      "Razer" is a trademark of Razer USA Ltd.
//  </copyright>
//  ---------------------------------------------------------------------------------------

namespace Colore.Razer
{
    using System;

    using LONG = System.Int32;

    // RZRESULT is a typedef of LONG on C-side. LONG is always 32-bit in WinC.
    // TODO: Finish implementing overloads.
    public struct Result : IComparable, IFormattable, IConvertible, IComparable<LONG>, IEquatable<LONG>
    {
        private readonly LONG _value;

        public Result(LONG value)
        {
            _value = value;
        }

        public static implicit operator LONG(Result result)
        {
            return result._value;
        }

        public static implicit operator Result(LONG l)
        {
            return new Result(l);
        }

        public static bool operator ==(Result left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result left, object right)
        {
            return !left.Equals(right);
        }

        public int CompareTo(object obj)
        {
            return ((IComparable)_value).CompareTo(obj);
        }

        public int CompareTo(Result other)
        {
            return CompareTo(other._value);
        }

        public int CompareTo(LONG other)
        {
            return _value.CompareTo(other);
        }

        public bool Equals(Result other)
        {
            return Equals(other._value);
        }

        public bool Equals(LONG other)
        {
            return _value.Equals(other);
        }

        public TypeCode GetTypeCode()
        {
            return ((IConvertible)_value).GetTypeCode();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToBoolean(provider);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToByte(provider);
        }

        public char ToChar(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToChar(provider);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDateTime(provider);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDecimal(provider);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToDouble(provider);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt16(provider);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt32(provider);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToInt64(provider);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToSByte(provider);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToSingle(provider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ((IFormattable)_value).ToString(format, formatProvider);
        }

        public string ToString(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return ((IConvertible)_value).ToType(conversionType, provider);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt16(provider);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt32(provider);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return ((IConvertible)_value).ToUInt64(provider);
        }

        #region Razer codes

        public static Result Invalid = -1;

        public static Result Success = 0;

        public static Result AccessDenied = 5;

        public static Result NotSupported = 50;

        public static Result InvalidParameter = 87;

        public static Result SingleInstanceApp = 1152;

        public static Result RequestAborted = 1235;

        public static Result ResourceDisabled = 4309;

        // TODO: Here be dragons?
        public static Result Failed = unchecked((LONG)2147500037);

        #endregion Razer codes
    }
}
