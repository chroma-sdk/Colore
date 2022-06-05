// ---------------------------------------------------------------------------------------
// <copyright file="AppInfoTests.cs" company="Corale">
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

namespace Colore.Tests.Data
{
    using System;
    using System.Linq;

    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class AppInfoTests
    {
        [Test]
        public void ShouldConstructWithCorrectTitle()
        {
            var info = new AppInfo("foobar", "fizz", "alice", "@home", Category.Application);
            Assert.AreEqual("foobar", info.Title);
        }

        [Test]
        public void ShouldConstructWithCorrectDescription()
        {
            var info = new AppInfo("foobar", "My description", "test", "foo", Category.Game);
            Assert.AreEqual("My description", info.Description);
        }

        [Test]
        public void ShouldConstructWithCorrectAuthor()
        {
            var info = new AppInfo("foobar", "test", "Alice", "test", Category.Application);
            Assert.AreEqual("Alice", info.Author.Name);
        }

        [Test]
        public void ShouldConstructWithCorrectContact()
        {
            var info = new AppInfo("test", "test", "test", "alice@example.com", Category.Game);
            Assert.AreEqual("alice@example.com", info.Author.Contact);
        }

        [TestCase(Category.Game)]
        [TestCase(Category.Application)]
        public void ShouldConstructWithCorrectCategory(Category category)
        {
            var info = new AppInfo("test", "test", "test", "test", category);
            Assert.AreEqual(category, info.Category);
        }

        [Test]
        public void ShouldConstructWithAllDevicesWhenNotSpecified()
        {
            var info = new AppInfo("test", "test", "test", "test", Category.Game);
            var supported = info.SupportedDevices.ToList();

            Assert.Contains(ApiDeviceType.Keyboard, supported);
            Assert.Contains(ApiDeviceType.Mouse, supported);
            Assert.Contains(ApiDeviceType.Headset, supported);
            Assert.Contains(ApiDeviceType.Mousepad, supported);
            Assert.Contains(ApiDeviceType.Keypad, supported);
            Assert.Contains(ApiDeviceType.ChromaLink, supported);
        }

        [Test]
        public void ShouldConstructWithCorrectDevices()
        {
            var supportedDevices = new[] { ApiDeviceType.Keyboard, ApiDeviceType.Headset };
            var info = new AppInfo("test", "test", "test", "test", supportedDevices, Category.Application);
            Assert.AreEqual(supportedDevices, info.SupportedDevices);
        }

        [Test]
        public void ShouldThrowExceptionOnTooLongTitle()
        {
            var title = new string('f', 257);

            // ReSharper disable once ObjectCreationAsStatement
            var ex = Assert.Throws<ArgumentException>(() => new AppInfo(title, "test", "test", "test", Category.Game));
            Assert.AreEqual(nameof(AppInfo.Title).ToLowerInvariant(), ex?.ParamName);
        }

        [Test]
        public void ShouldThrowExceptionOnTooLongDescription()
        {
            var desc = new string('f', 1025);

            // ReSharper disable once ObjectCreationAsStatement
            var ex = Assert.Throws<ArgumentException>(() => new AppInfo("test", desc, "test", "test", Category.Game));
            Assert.AreEqual(nameof(AppInfo.Description).ToLowerInvariant(), ex?.ParamName);
        }
    }
}
