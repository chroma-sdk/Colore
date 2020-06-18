// ---------------------------------------------------------------------------------------
// <copyright file="ApiExceptionTests.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Api
{
    using System;
    using System.ComponentModel;

    using Colore.Api;
    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class ApiExceptionTests
    {
        [TestCase("hello")]
        [TestCase("Hello world")]
        [TestCase("foo, bar, and baz")]
        public void ShouldConstructWithCorrectMessage(string message)
        {
            var exception = new ApiException(message);
            Assert.AreEqual(message, exception.Message);
        }

        [Test]
        public void ShouldSetInnerException()
        {
            var expected = new Exception("I'm an inner exception!");
            var exception = new ApiException("test", expected);
            Assert.AreEqual(expected, exception.InnerException);
        }

        [Test]
        public void ShouldConstructWithCorrectResult()
        {
            var expected = Result.RzInvalidParameter;
            var exception = new ApiException("test", expected);
            Assert.AreEqual(expected, exception.Result);
        }

        [Test]
        public void ShouldUseCorrectInnerExceptionWithResult()
        {
            var exception = new ApiException("test", Result.RzFailed);
            Assert.IsInstanceOf<Win32Exception>(exception.InnerException);
        }

        [Test]
        public void ShouldDefaultToSuccessResult()
        {
            var exception = new ApiException();
            Assert.AreEqual(Result.RzSuccess, exception.Result);
        }
    }
}
