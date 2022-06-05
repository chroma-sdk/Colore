// ---------------------------------------------------------------------------------------
// <copyright file="UriHelperTests.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Helpers
{
    using System;

    using Colore.Helpers;

    using NUnit.Framework;

    [TestFixture]
    public class UriHelperTests
    {
        [TestCase("http://example.com", "/myresource", "http://example.com/myresource")]
        [TestCase("http://example.com/", "myresource", "http://example.com/myresource")]
        [TestCase("http://example.com", "myresource", "http://example.com/myresource")]
        [TestCase("http://example.com/", "/myresource", "http://example.com/myresource")]
        [TestCase("http://example.com/////", "myresource", "http://example.com/myresource")]
        [TestCase("http://example.com", "//////myresource", "http://example.com/myresource")]
        [TestCase("http://example.com//////", "//////myresource", "http://example.com/myresource")]
        public void ShouldConstructWellFormedUri(string baseUrl, string resource, string expected)
        {
            var baseUri = new Uri(baseUrl);
            var resourceUri = new Uri(resource, UriKind.Relative);
            var combined = baseUri.Append(resourceUri);
            Assert.AreEqual(new Uri(expected), combined);
        }

        [Test]
        public void ShouldThrowOnNullBaseUri()
        {
            Assert.Throws<ArgumentNullException>(() => ((Uri)null!).Append(new Uri("myresource", UriKind.Relative)));
        }

        [Test]
        public void ShouldThrowOnNullResource()
        {
            Assert.Throws<ArgumentNullException>(() => new Uri("http://example.com").Append(null!));
        }
    }
}
