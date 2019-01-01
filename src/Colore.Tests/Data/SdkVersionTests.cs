// ---------------------------------------------------------------------------------------
// <copyright file="SdkVersionTests.cs" company="Corale">
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

namespace Colore.Tests.Data
{
    using System;

    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class SdkVersionTests
    {
        [Test]
        public void ShouldConstructWithCorrectValues()
        {
            var ver = new SdkVersion(1, 2, 3);

            Assert.That(ver.Major, Is.EqualTo(1));
            Assert.That(ver.Minor, Is.EqualTo(2));
            Assert.That(ver.Revision, Is.EqualTo(3));
        }

        [Test]
        public void ShouldToStringCorrectly()
        {
            const string Expected = "1.2.3";
            var ver = new SdkVersion(1, 2, 3);
            Assert.That(ver.ToString(), Is.EqualTo(Expected));
        }

        [Test]
        public void ShouldEqualIdenticalVersion()
        {
            var a = new SdkVersion(1, 2, 3);
            var b = new SdkVersion(1, 2, 3);

            Assert.AreEqual(a, b);
            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
        }

        [Test]
        public void ShouldNotEqualDifferentVersion()
        {
            var a = new SdkVersion(1, 2, 3);
            var b = new SdkVersion(3, 2, 1);

            Assert.AreNotEqual(a, b);
            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var ver = new SdkVersion(1, 2, 3);
            var obj = new object();

            Assert.AreNotEqual(ver, obj);
            Assert.False(ver == obj);
            Assert.True(ver != obj);
            Assert.False(ver.Equals(obj));
        }

        [Test]
        public void ShouldBeLessThanWhenMajorLess()
        {
            var old = new SdkVersion(1, 2, 3);
            var @new = new SdkVersion(2, 2, 3);

            Assert.That(old, Is.LessThan(@new));
            Assert.That(old, Is.LessThanOrEqualTo(@new));
            Assert.True(old < @new);
            Assert.True(old <= @new);
            Assert.False(old > @new);
            Assert.False(old >= @new);
        }

        [Test]
        public void ShouldBeLessThanWhenMinoLess()
        {
            var old = new SdkVersion(1, 2, 3);
            var @new = new SdkVersion(1, 3, 3);

            Assert.That(old, Is.LessThan(@new));
            Assert.That(old, Is.LessThanOrEqualTo(@new));
            Assert.True(old < @new);
            Assert.True(old <= @new);
            Assert.False(old > @new);
            Assert.False(old >= @new);
        }

        [Test]
        public void ShouldBeLessThanWhenRevisionLess()
        {
            var old = new SdkVersion(1, 2, 3);
            var @new = new SdkVersion(1, 2, 4);

            Assert.That(old, Is.LessThan(@new));
            Assert.That(old, Is.LessThanOrEqualTo(@new));
            Assert.True(old < @new);
            Assert.True(old <= @new);
            Assert.False(old > @new);
            Assert.False(old >= @new);
        }

        [Test]
        public void ShouldBeGreaterThanWhenMajorGreater()
        {
            var @new = new SdkVersion(2, 2, 3);
            var old = new SdkVersion(1, 2, 3);

            Assert.That(@new, Is.GreaterThan(old));
            Assert.That(@new, Is.GreaterThanOrEqualTo(old));
            Assert.True(@new > old);
            Assert.True(@new >= old);
            Assert.False(@new < old);
            Assert.False(@new <= old);
        }

        [Test]
        public void ShouldBeGreaterThanWhenMinorGreater()
        {
            var @new = new SdkVersion(1, 2, 3);
            var old = new SdkVersion(1, 1, 3);

            Assert.That(@new, Is.GreaterThan(old));
            Assert.That(@new, Is.GreaterThanOrEqualTo(old));
            Assert.True(@new > old);
            Assert.True(@new >= old);
            Assert.False(@new < old);
            Assert.False(@new <= old);
        }

        [Test]
        public void ShouldBeGreaterThanWhenRevisionGreater()
        {
            var @new = new SdkVersion(1, 2, 3);
            var old = new SdkVersion(1, 2, 2);

            Assert.That(@new, Is.GreaterThan(old));
            Assert.That(@new, Is.GreaterThanOrEqualTo(old));
            Assert.True(@new > old);
            Assert.True(@new >= old);
            Assert.False(@new < old);
            Assert.False(@new <= old);
        }

        [Test]
        public void ShouldBeLessOrGreaterOrEqualWhenIdentical()
        {
            var a = new SdkVersion(1, 2, 3);
            var b = new SdkVersion(1, 2, 3);

            Assert.That(a, Is.LessThanOrEqualTo(b));
            Assert.That(a, Is.GreaterThanOrEqualTo(b));
            Assert.True(a <= b);
            Assert.True(a >= b);
            Assert.False(a > b);
            Assert.False(a < b);
        }

        [Test]
        public void ShouldThrowExceptionWhenComparingDifferentObjects()
        {
            var ver = new SdkVersion(1, 2, 3);
            var obj = new object();

            Assert.That(
                () => ver.CompareTo(obj),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("obj"));
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            var ver = new SdkVersion();
            Assert.Zero(ver.GetHashCode());
        }
    }
}
