// ---------------------------------------------------------------------------------------
// <copyright file="Result.cs" company="Corale">
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

namespace Colore.Razer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // RZRESULT is a typedef of LONG on C-side. LONG is always 32-bit in WinC.
    // TODO: Finish implementing overloads.
    public struct Result : IComparable<int>, IComparable<Result>, IEquatable<int>, IEquatable<Result>
    {
        [Description("Access denied.")]
        public static readonly Result RzAccessDenied = 5;

        // TODO: Here be dragons?
        [Description("General failure.")]
        public static readonly Result RzFailed = unchecked((int)2147500037);

        [Description("Invalid.")]
        public static readonly Result RzInvalid = -1;

        [Description("Invalid parameter.")]
        public static readonly Result RzInvalidParameter = 87;

        [Description("Not supported.")]
        public static readonly Result RzNotSupported = 50;

        [Description("Request aborted.")]
        public static readonly Result RzRequestAborted = 1235;

        [Description("Resource not available or disabled.")]
        public static readonly Result RzResourceDisabled = 4309;

        [Description("Cannot start more than one instance of the specified program.")]
        public static readonly Result RzSingleInstanceApp = 1152;

        [Description("Success.")]
        public static readonly Result RzSuccess = 0;

        private static readonly IDictionary<Result, Metadata> FieldMetadata;

        private readonly int _value;

        static Result()
        {
            FieldMetadata = GetMetadata();
        }

        public Result(int value)
        {
            _value = value;
        }

        public string Description
        {
            get
            {
                return FieldMetadata.ContainsKey(this) ? FieldMetadata[this].Description : "Unknown.";
            }
        }

        public bool Failed
        {
            get
            {
                return this != RzSuccess;
            }
        }

        public string Name
        {
            get
            {
                return FieldMetadata.ContainsKey(this) ? FieldMetadata[this].Name : "Unknown";
            }
        }

        public bool Success
        {
            get
            {
                return this == RzSuccess;
            }
        }

        public static bool operator ==(Result left, object right)
        {
            return left.Equals(right);
        }

        public static bool operator false(Result result)
        {
            return !result;
        }

        public static bool operator >(Result left, Result right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >(Result left, int right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(Result left, Result right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator >=(Result left, int right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static implicit operator int(Result result)
        {
            return result._value;
        }

        public static implicit operator Result(int l)
        {
            return new Result(l);
        }

        public static implicit operator bool(Result result)
        {
            return result == RzSuccess;
        }

        public static bool operator !=(Result left, object right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(Result left, Result right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <(Result left, int right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(Result left, Result right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator <=(Result left, int right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator true(Result result)
        {
            return result;
        }

        public int CompareTo(int other)
        {
            return _value.CompareTo(other);
        }

        public int CompareTo(Result other)
        {
            return CompareTo(other._value);
        }

        public bool Equals(int other)
        {
            return _value.Equals(other);
        }

        public bool Equals(Result other)
        {
            return Equals(other._value);
        }

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

        public override int GetHashCode()
        {
            return _value;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} ({2})", Name, Description, _value);
        }

        private static Dictionary<Result, Metadata> GetMetadata()
        {
            var cache = new Dictionary<Result, Metadata>();

            var fieldsInfo = typeof(Result).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var fieldInfo in fieldsInfo.Where(fi => fi.FieldType == typeof(Result)))
            {
                var value = fieldInfo.GetValue(null);
                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>(false);

                if (attr != null && value is Result)
                    cache[(Result)value] = new Metadata(fieldInfo.Name, attr.Description);
            }

            return cache;
        }

        private struct Metadata
        {
            private readonly string _description;

            private readonly string _name;

            public Metadata(string name, string description)
            {
                _name = name;
                _description = description;
            }

            public string Description
            {
                get
                {
                    return _description;
                }
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }
        }

        [AttributeUsage(AttributeTargets.Field)]
        private class DescriptionAttribute : Attribute
        {
            private readonly string _description;

            public DescriptionAttribute(string description)
            {
                _description = description;
            }

            public string Description
            {
                get
                {
                    return _description;
                }
            }
        }
    }
}
