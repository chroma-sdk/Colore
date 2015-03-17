// ---------------------------------------------------------------------------------------
// <copyright file="ColorTests.cs" company="Corale">
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

namespace Corale.Colore.Tests.Razer
{
    using Corale.Colore.Core;

    using NUnit.Framework;

    [TestFixture]
    public class ColorTests
    {
        [Test]
        public void ShouldConstructCorrectly()
        {
            Assert.AreEqual(new Color(0x00123456).Value, 0x00123456);
        }

        [Test]
        public void ShouldConstructFromColor()
        {
            var c = new Color(0x00123456);
            Assert.AreEqual(new Color(c), c);
        }

        [Test]
        public void ShouldConvertRgbBytesCorrectly()
        {
            const uint V = 0x00FFFFFF;
            const byte R = 255;
            const byte G = 255;
            const byte B = 255;
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, R);
            Assert.AreEqual(c.G, G);
            Assert.AreEqual(c.B, B);
        }

        [Test]
        public void ShouldConvertRgbDoublesCorrectly()
        {
            const uint V = 0x00FFFFFF;
            const double R = 1.0;
            const double G = 1.0;
            const double B = 1.0;
            const byte Expected = 255;
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, Expected);
            Assert.AreEqual(c.G, Expected);
            Assert.AreEqual(c.B, Expected);
        }

        [Test]
        public void ShouldConvertRgbFloatsCorrectly()
        {
            const uint V = 0x00FFFFFF;
            const float R = 1.0f;
            const float G = 1.0f;
            const float B = 1.0f;
            const byte Expected = 255;
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, Expected);
            Assert.AreEqual(c.G, Expected);
            Assert.AreEqual(c.B, Expected);
        }

        [Test]
        public void ShouldEqualIdenticalColor()
        {
            var a = new Color(0);
            var b = new Color(0);
            Assert.AreEqual(a, b);
            Assert.True(a == b);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldEqualIdenticalUint()
        {
            var a = new Color(255, 0, 255);
            const uint B = 0x00FF00FF;
            Assert.AreEqual(a, B);
            Assert.True(a == B);
            Assert.False(a != B);
        }

        [Test]
        public void ShouldHaveCorrectHashCode()
        {
            const uint V = 0x00123456;
            var expected = V.GetHashCode();
            var actual = new Color(V).GetHashCode();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldImplicitCastToUint()
        {
            const uint Expected = 0x00010203;
            uint actual = new Color(0x00010203);
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var c = new Color(0);
            var o = new object();
            Assert.AreNotEqual(c, o);
            Assert.False(c == o);
            Assert.True(c != o);
        }

        [Test]
        public void ShouldNotEqualDifferentColor()
        {
            var a = new Color(0);
            var b = new Color(1);
            Assert.AreNotEqual(a, b);
            Assert.False(a == b);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualDifferentUint()
        {
            var a = new Color(255, 0, 255);
            const uint B = 0x00FFFFFF;
            Assert.AreNotEqual(a, B);
            Assert.False(a == B);
            Assert.True(a != B);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var c = new Color(255, 255, 255);
            Assert.AreNotEqual(c, null);
            Assert.False(c == null);
            Assert.True(c != null);
            Assert.False(c.Equals(null));
        }

        [Test]
        public void UintShouldEqualIdenticalColor()
        {
            const uint A = 0x00FFFFFF;
            var b = new Color(0x00FFFFFF);
            Assert.AreEqual(A, b);
            Assert.True(A == b);
            Assert.False(A != b);
        }

        [Test]
        public void UintShouldImplicitCastToColor()
        {
            var expected = new Color(0x00123456);
            Color actual = 0x00123456;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UintShouldNotEqualDifferentColor()
        {
            const uint A = 0x00FF00FF;
            var b = new Color(0x00FFFFFF);
            Assert.AreNotEqual(A, b);
            Assert.False(A == b);
            Assert.True(A != b);
        }
    }
}
