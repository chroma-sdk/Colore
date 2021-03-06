// ---------------------------------------------------------------------------------------
// <copyright file="AuthorTests.cs" company="Corale">
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

namespace Colore.Tests.Data
{
    using System;

    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class AuthorTests
    {
        [Test]
        public void ShouldSetNameFromConstructor()
        {
            const string Name = "Foobar";
            var author = new Author(Name, "test");
            Assert.AreEqual(Name, author.Name);
        }

        [Test]
        public void ShouldSetContactFromConstructor()
        {
            const string Contact = "me@home.com";
            var author = new Author("test", Contact);
            Assert.AreEqual(Contact, author.Contact);
        }

        [Test]
        public void ShouldThrowOnTooLongName()
        {
            var name = new string('f', 257);

            // ReSharper disable once ObjectCreationAsStatement
            var ex = Assert.Throws<ArgumentException>(() => new Author(name, "test"));
            Assert.AreEqual(nameof(Author.Name).ToLowerInvariant(), ex.ParamName);
        }

        [Test]
        public void ShouldThrowOnTooLongContact()
        {
            var contact = new string('f', 257);

            // ReSharper disable once ObjectCreationAsStatement
            var ex = Assert.Throws<ArgumentException>(() => new Author("test", contact));
            Assert.AreEqual(nameof(Author.Contact).ToLowerInvariant(), ex.ParamName);
        }
    }
}
