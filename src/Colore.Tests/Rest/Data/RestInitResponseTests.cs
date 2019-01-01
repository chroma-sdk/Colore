// ---------------------------------------------------------------------------------------
// <copyright file="RestInitResponseTests.cs" company="Corale">
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

namespace Colore.Tests.Rest.Data
{
    using System;

    using Colore.Rest.Data;

    using NUnit.Framework;

    [TestFixture]
    public class RestInitResponseTests
    {
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        [TestCase(-512513)]
        [TestCase(362362)]
        public void ShouldConstructWithCorrectSession(int session)
        {
            var response = new RestInitResponse(session, new Uri("http://google.com"));
            Assert.AreEqual(session, response.Session);
        }

        [TestCase("http://google.com")]
        [TestCase("https://google.com")]
        [TestCase("http://razer.com")]
        [TestCase("https://razer.com/foobar")]
        [TestCase("file:///home/test/chroma.html")]
        [TestCase("http://example.com/someapi")]
        [TestCase("https://example.org/someapi?with=args")]
        [TestCase("http://www.example.net/#withanchor")]
        [TestCase("http://example.xyz:12345/aport")]
        public void ShouldConstructWithCorrectUri(string url)
        {
            var expected = new Uri(url);
            var response = new RestInitResponse(0, expected);
            Assert.AreEqual(expected, response.Uri);
        }

        [Test]
        public void ShouldConstructWithNullUri()
        {
            Assert.IsNull(new RestInitResponse(0, null).Uri);
        }
    }
}
