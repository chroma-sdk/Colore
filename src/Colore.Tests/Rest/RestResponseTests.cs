// ---------------------------------------------------------------------------------------
// <copyright file="RestResponseTests.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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

    using Colore.Rest;

    using Newtonsoft.Json;

    using NUnit.Framework;

    [TestFixture]
    public class RestResponseTests
    {
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
            var response = new RestResponse<object>(statusCode, null);
            Assert.AreEqual(statusCode, response.Status);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.Created)]
        public void ShouldConsider200StatusesSuccessful(HttpStatusCode statusCode)
        {
            Assert.True(new RestResponse<object>(statusCode, null).IsSuccessful);
        }

        [TestCase(HttpStatusCode.Continue)]
        public void ShouldConsider100StausesNonSuccessful(HttpStatusCode statusCode)
        {
            Assert.False(new RestResponse<object>(statusCode, null).IsSuccessful);
        }

        [TestCase(HttpStatusCode.Moved)]
        [TestCase(HttpStatusCode.MovedPermanently)]
        [TestCase(HttpStatusCode.NotModified)]
        public void ShouldConsider300StatusesNonSuccessful(HttpStatusCode statusCode)
        {
            Assert.False(new RestResponse<object>(statusCode, null).IsSuccessful);
        }

        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.Unauthorized)]
        [TestCase(HttpStatusCode.Forbidden)]
        public void ShouldConsider400StatusesNonSuccessful(HttpStatusCode statusCode)
        {
            Assert.False(new RestResponse<object>(statusCode, null).IsSuccessful);
        }

        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.BadGateway)]
        public void ShouldConsider500StatusesNonSuccessful(HttpStatusCode statusCode)
        {
            Assert.False(new RestResponse<object>(statusCode, null).IsSuccessful);
        }

        [TestCase("hello")]
        [TestCase("hello world")]
        [TestCase("This is a more complex string, with various 5ymb0l5 and #stuff!")]
        public void ShouldConstructWithCorrectContent(string content)
        {
            var response = new RestResponse<object>(HttpStatusCode.OK, content);
            Assert.AreEqual(content, response.Content);
        }

        [Test]
        public void ShouldConstructWithNullContent()
        {
            var response = new RestResponse<object>(HttpStatusCode.OK, null);
            Assert.IsNull(response.Content);
        }

        [Test]
        public void DataShouldBeNullIfContentIsNull()
        {
            var response = new RestResponse<object>(HttpStatusCode.OK, null);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void DataShouldBeNullIfContentIsEmpty()
        {
            var response = new RestResponse<object>(HttpStatusCode.OK, string.Empty);
            Assert.IsNull(response.Data);
        }

        [TestCase(" ")]
        [TestCase("   ")]
        [TestCase("\n")]
        [TestCase("\t")]
        [TestCase("\r")]
        [TestCase("\r\n")]
        [TestCase("\t\n")]
        [TestCase("\n    \t   \r\t    \r\n    \n\r \t")]
        public void DataShouldBeNullIfContentIsWhitespace(string content)
        {
            var response = new RestResponse<object>(HttpStatusCode.OK, content);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void DataShouldDefaultIfContentIsInvalidJson()
        {
            var json = "This is not valid JSON, not at all!";
            var response = new RestResponse<object>(HttpStatusCode.OK, json);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void DataShouldDeserializeProperly()
        {
            var data = new MyData { Name = "Foobar", Age = 42 };
            var response = new RestResponse<MyData>(HttpStatusCode.OK, JsonConvert.SerializeObject(data));
            Assert.True(data.Equals(response.Data));
        }

        private class MyData : IEquatable<MyData>
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public bool Equals(MyData other)
            {
                return other != null && Name == other.Name && Age == other.Age;
            }
        }
    }
}
