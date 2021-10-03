// ---------------------------------------------------------------------------------------
// <copyright file="RestExceptionTests.cs" company="Corale">
//     Copyright © 2015-2021 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Rest
{
    using System;
    using System.Net;

    using Colore.Data;
    using Colore.Rest;

    using NUnit.Framework;

    [TestFixture]
    public class RestExceptionTests
    {
        [TestCase("hello")]
        [TestCase("hello world")]
        [TestCase("Hello foo, bar, and baz")]
        public void ShouldConstructWithCorrectMessage(string message)
        {
            var exception = new RestException(message);
            Assert.AreEqual(message, exception.Message);
        }

        [Test]
        public void ShouldConstructWithCorrectInnerException()
        {
            var expected = new Exception("I'm the inner exception");
            var exception = new RestException("test", expected);
            Assert.AreEqual(expected, exception.InnerException);
        }

        [Test]
        public void ShouldConstructWithCorrectResult()
        {
            var expected = Result.RzResourceDisabled;
            var exception = new RestException("test", expected, new Uri("http://example.com"), HttpStatusCode.OK);
            Assert.AreEqual(expected, exception.Result);
        }

        [TestCase("http://google.se")]
        [TestCase("http://example.org/foobar")]
        [TestCase("https://a.website.com:123/with?a=port")]
        public void ShouldConstructWithCorrectUri(string url)
        {
            var expected = new Uri(url);
            var exception = new RestException("test", Result.RzSuccess, expected, HttpStatusCode.OK);
            Assert.AreEqual(expected, exception.Uri);
        }

        [Test]
        public void ShouldThrowOnNullUri()
        {
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(
                () => new RestException(null, Result.RzSuccess, null!, HttpStatusCode.OK));
        }

        [Test]
        public void ShouldConstructWithNullUriOnEmptyConstructor()
        {
            Assert.IsNull(new RestException().Uri);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.Accepted)]
        [TestCase(HttpStatusCode.Created)]
        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.MethodNotAllowed)]
        [TestCase(HttpStatusCode.Forbidden)]
        [TestCase(HttpStatusCode.NotFound)]
        public void ShouldConstructWithCorrectStatusCode(HttpStatusCode statusCode)
        {
            var exception = new RestException("test", Result.RzSuccess, new Uri("http://example.com"), statusCode);
            Assert.AreEqual(statusCode, exception.StatusCode);
        }

        [Test]
        public void ShouldConstructWithCorrectRestData()
        {
            const string Expected = "This is my data";
            var exception = new RestException(
                "test",
                Result.RzSuccess,
                new Uri("http://example.com"),
                HttpStatusCode.OK,
                Expected);

            Assert.AreEqual(Expected, exception.RestData);
        }

        [Test]
        public void ShouldConstructWithNullDataByDefault()
        {
            Assert.IsNull(new RestException().RestData);
        }
    }
}
