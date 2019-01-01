// ---------------------------------------------------------------------------------------
// <copyright file="NativeCallExceptionTests.cs" company="Corale">
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

namespace Colore.Tests.Native
{
    using System.ComponentModel;

    using Colore.Data;
    using Colore.Native;

    using NUnit.Framework;

    [TestFixture]
    public class NativeCallExceptionTests
    {
        [Test]
        public void ShouldSetCorrectFunctionName()
        {
            Assert.AreEqual("TestFunc", new NativeCallException("TestFunc", Result.RzSuccess).Function);
        }

        [Test]
        public void ShouldSetCorrectResult()
        {
            Assert.AreEqual(Result.RzSuccess, new NativeCallException("TestFunc", Result.RzSuccess).Result);
        }

        [Test]
        public void ShouldSetMessage()
        {
            const string Func = "TestFunc";
            var result = Result.RzSuccess;
            var expected = $"Call to native Chroma SDK function {Func} failed with error: {result}";

            Assert.AreEqual(expected, new NativeCallException(Func, result).Message);
        }

        [Test]
        public void ShouldSetInnerException()
        {
            var result = Result.RzSuccess;
            var expected = new Win32Exception(result);
            var actual = new NativeCallException("TestFunc", result).InnerException;
            Assert.AreEqual(expected.GetType(), actual.GetType(), "Expected types to be equal.");
            Assert.AreEqual(expected.HResult, actual.HResult, "Expected HResults to be equal.");
            Assert.AreEqual(expected.Message, actual.Message, "Expected message to be equal.");
            Assert.AreEqual(
                expected.NativeErrorCode,
                ((Win32Exception)actual).NativeErrorCode,
                "Expected native error codes to be equal.");
        }
    }
}
