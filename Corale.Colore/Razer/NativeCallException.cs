// ---------------------------------------------------------------------------------------
// <copyright file="NativeCallException.cs" company="Corale">
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

namespace Corale.Colore.Razer
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Thrown when a native function returns an erroneous result value.
    /// </summary>
    [Serializable]
    public class NativeCallException : ColoreException
    {
        /// <summary>
        /// Template used to construct exception message from.
        /// </summary>
        private const string MessageTemplate = "Call to native Chroma SDK function {0} failed with error: {1}";

        /// <summary>
        /// The function that was called.
        /// </summary>
        private readonly string _function;

        /// <summary>
        /// The result returned from the function.
        /// </summary>
        private readonly Result _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeCallException" /> class.
        /// </summary>
        /// <param name="info">Serialization info object.</param>
        /// <param name="context">Streaming context.</param>
        protected NativeCallException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _function = info.GetString("Function");
            _result = info.GetInt32("Result");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeCallException" /> class.
        /// </summary>
        /// <param name="function">The name of the function that was called.</param>
        /// <param name="result">The result returned from the called function.</param>
        internal NativeCallException(string function, Result result)
            : base(
                string.Format(CultureInfo.InvariantCulture, MessageTemplate, function, result),
                new Win32Exception(result))
        {
            _function = function;
            _result = result;
        }

        /// <summary>
        /// Gets the name of the native function that was called.
        /// </summary>
        public string Function
        {
            get
            {
                return _function;
            }
        }

        /// <summary>
        /// Gets the <see cref="Result" /> object indicating
        /// the result returned from the native function.
        /// </summary>
        public Result Result
        {
            get
            {
                return _result;
            }
        }

        /// <summary>
        /// Adds object data to serialization object.
        /// </summary>
        /// <param name="info">Serialization info object.</param>
        /// <param name="context">Streaming context.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Function", _function);
            info.AddValue("Result", (int)Result);
        }
    }
}
