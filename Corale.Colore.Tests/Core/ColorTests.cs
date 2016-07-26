// ---------------------------------------------------------------------------------------
// <copyright file="ColorTests.cs" company="Corale">
//     Copyright © 2015-2016 by Adam Hellberg and Brandon Scott.
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

namespace Corale.Colore.Tests.Core
{
    using Corale.Colore.Core;

    using NUnit.Framework;

    using SystemColor = System.Drawing.Color;
    using WpfColor = System.Windows.Media.Color;

    [TestFixture]
    public class ColorTests
    {
        [Test]
        public void ShouldConstructCorrectly()
        {
            Assert.AreEqual(new Color(0x78123456).Value, 0x78123456);
        }

        [Test]
        public void ShouldConstructFromColor()
        {
            var c = new Color(0x78123456);
            Assert.AreEqual(new Color(c), c);
        }

        [Test]
        public void ShouldConvertRgbBytesCorrectly()
        {
            const uint V = 0x56F5C8;
            const byte R = 200;
            const byte G = 245;
            const byte B = 86;
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, R);
            Assert.AreEqual(c.G, G);
            Assert.AreEqual(c.B, B);
        }

        [Test]
        public void ShouldConvertRgbDoublesCorrectly()
        {
            const uint V = 0x3D89CC;
            const double R = 0.8;
            const double G = 0.54;
            const double B = 0.24;
            const byte ExpectedR = (byte)(R * 255);
            const byte ExpectedG = (byte)(G * 255);
            const byte ExpectedB = (byte)(B * 255);
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, ExpectedR);
            Assert.AreEqual(c.G, ExpectedG);
            Assert.AreEqual(c.B, ExpectedB);
        }

        [Test]
        public void ShouldConvertRgbFloatsCorrectly()
        {
            const uint V = 0xE533CC;
            const float R = 0.8f;
            const float G = 0.2f;
            const float B = 0.9f;
            const byte ExpectedR = (byte)(R * 255);
            const byte ExpectedG = (byte)(G * 255);
            const byte ExpectedB = (byte)(B * 255);
            var c = new Color(R, G, B);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, ExpectedR);
            Assert.AreEqual(c.G, ExpectedG);
            Assert.AreEqual(c.B, ExpectedB);
        }

        [Test]
        public void ShouldConstructFromRgb()
        {
            var expected = new Color(0x123456);
            var actual = Color.FromRgb(0x563412);

            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.R, actual.R);
            Assert.AreEqual(expected.G, actual.G);
            Assert.AreEqual(expected.B, actual.B);
        }

        [Test]
        public void ShouldDefaultToEmptyColor()
        {
            Assert.AreEqual(default(Color).Value, 0);
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
            const uint B = 0xFF00FF;
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
            Assert.True(b == A);
            Assert.False(b != A);
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
            Assert.False(b == A);
            Assert.True(b != A);
        }
    }
}
