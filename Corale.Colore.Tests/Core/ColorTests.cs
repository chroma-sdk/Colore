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
            const uint V = 0x8056F5C8;
            const byte R = 200;
            const byte G = 245;
            const byte B = 86;
            const byte A = 128;
            var c = new Color(R, G, B, A);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, R);
            Assert.AreEqual(c.G, G);
            Assert.AreEqual(c.B, B);
            Assert.AreEqual(c.A, A);
        }

        [Test]
        public void ShouldConvertRgbDoublesCorrectly()
        {
            const uint V = 0x7F3D89CC;
            const double R = 0.8;
            const double G = 0.54;
            const double B = 0.24;
            const double A = 0.5;
            const byte ExpectedR = (byte)(R * 255);
            const byte ExpectedG = (byte)(G * 255);
            const byte ExpectedB = (byte)(B * 255);
            const byte ExpectedA = (byte)(A * 255);
            var c = new Color(R, G, B, A);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, ExpectedR);
            Assert.AreEqual(c.G, ExpectedG);
            Assert.AreEqual(c.B, ExpectedB);
            Assert.AreEqual(c.A, ExpectedA);
        }

        [Test]
        public void ShouldConvertRgbFloatsCorrectly()
        {
            const uint V = 0xD8E533CC;
            const float R = 0.8f;
            const float G = 0.2f;
            const float B = 0.9f;
            const float A = 0.85f;
            const byte ExpectedR = (byte)(R * 255);
            const byte ExpectedG = (byte)(G * 255);
            const byte ExpectedB = (byte)(B * 255);
            const byte ExpectedA = (byte)(A * 255);
            var c = new Color(R, G, B, A);
            Assert.AreEqual(c.Value, V);
            Assert.AreEqual(c.R, ExpectedR);
            Assert.AreEqual(c.G, ExpectedG);
            Assert.AreEqual(c.B, ExpectedB);
            Assert.AreEqual(c.A, ExpectedA);
        }

        [Test]
        public void ShouldConstructFromArgb()
        {
            var expected = new Color(0x12345678);
            var actual = Color.FromArgb(0x12785634);

            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.R, actual.R);
            Assert.AreEqual(expected.G, actual.G);
            Assert.AreEqual(expected.B, actual.B);
            Assert.AreEqual(expected.A, actual.A);
        }

        [Test]
        public void ShouldConstructFromRgb()
        {
            var expected = new Color(0xFF123456);
            var actual = Color.FromRgb(0x563412);

            Assert.AreEqual(expected.Value, actual.Value);
            Assert.AreEqual(expected.R, actual.R);
            Assert.AreEqual(expected.G, actual.G);
            Assert.AreEqual(expected.B, actual.B);
            Assert.AreEqual(expected.A, actual.A);
        }

        [Test]
        public void ShouldConstructFromHsv()
        {
            var red = Color.FromHsv(0, 100, 100);
            var green = Color.FromHsv(120, 100, 100);
            var blue = Color.FromHsv(240, 100, 100);
            var hotPink = Color.FromHsv(330, 58.8, 100);
            Assert.AreEqual(Color.Red, red);
            Assert.AreEqual(Color.Green, green);
            Assert.AreEqual(Color.Blue, blue);
            Assert.AreEqual(Color.HotPink, hotPink);
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
            const uint B = 0xFFFF00FF;
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

        [Test]
        public void ShouldConvertFromSystemColor()
        {
            var source = SystemColor.FromArgb(5, 10, 15, 20);
            var coloreColor = Color.FromSystemColor(source);

            Assert.AreEqual(source.R, coloreColor.R);
            Assert.AreEqual(source.G, coloreColor.G);
            Assert.AreEqual(source.B, coloreColor.B);
            Assert.AreEqual(source.A, coloreColor.A);
        }

        [Test]
        public void ShouldExplicitCastToSystemColor()
        {
            var coloreColor = new Color(1, 2, 4, 8);
            var systemColor = (SystemColor)coloreColor;

            Assert.AreEqual(coloreColor.R, systemColor.R);
            Assert.AreEqual(coloreColor.G, systemColor.G);
            Assert.AreEqual(coloreColor.B, systemColor.B);
            Assert.AreEqual(coloreColor.A, systemColor.A);
        }

        [Test]
        public void ShouldExplicitCastFromSystemColor()
        {
            var systemColor = SystemColor.FromArgb(5, 10, 15, 20);
            var coloreColor = (Color)systemColor;

            Assert.AreEqual(systemColor.R, coloreColor.R);
            Assert.AreEqual(systemColor.G, coloreColor.G);
            Assert.AreEqual(systemColor.B, coloreColor.B);
            Assert.AreEqual(systemColor.A, coloreColor.A);
        }

        [Test]
        public void ShouldImplicitCastToSystemColor()
        {
            var coloreColor = new Color(1, 2, 4, 8);
            SystemColor systemColor = coloreColor;

            Assert.AreEqual(coloreColor.R, systemColor.R);
            Assert.AreEqual(coloreColor.G, systemColor.G);
            Assert.AreEqual(coloreColor.B, systemColor.B);
            Assert.AreEqual(coloreColor.A, systemColor.A);
        }

        [Test]
        public void ShouldImplicitCastFromSystemColor()
        {
            var systemColor = SystemColor.FromArgb(5, 10, 15, 20);
            Color coloreColor = systemColor;

            Assert.AreEqual(systemColor.R, coloreColor.R);
            Assert.AreEqual(systemColor.G, coloreColor.G);
            Assert.AreEqual(systemColor.B, coloreColor.B);
            Assert.AreEqual(systemColor.A, coloreColor.A);
        }

        [Test]
        public void ShouldEqualSystemColorUsingOverload()
        {
            var coloreColor = new Color(1, 2, 3, 8);
            var systemColor = SystemColor.FromArgb(8, 1, 2, 3);

            Assert.True(coloreColor.Equals(systemColor));
            Assert.AreEqual(coloreColor, systemColor);
        }

        [Test]
        public void ShouldConvertFromWpfColor()
        {
            var wpfColor = WpfColor.FromArgb(5, 10, 15, 20);
            var coloreColor = Color.FromWpfColor(wpfColor);

            Assert.AreEqual(wpfColor.R, coloreColor.R);
            Assert.AreEqual(wpfColor.G, coloreColor.G);
            Assert.AreEqual(wpfColor.B, coloreColor.B);
            Assert.AreEqual(wpfColor.A, coloreColor.A);
        }

        [Test]
        public void ShouldExplicitCastToWpfColor()
        {
            var coloreColor = new Color(1, 2, 4, 8);
            var wpfColor = (WpfColor)coloreColor;

            Assert.AreEqual(coloreColor.R, wpfColor.R);
            Assert.AreEqual(coloreColor.G, wpfColor.G);
            Assert.AreEqual(coloreColor.B, wpfColor.B);
            Assert.AreEqual(coloreColor.A, wpfColor.A);
        }

        [Test]
        public void ShouldExplicitCastFromWpfColor()
        {
            var wpfColor = WpfColor.FromArgb(5, 10, 15, 20);
            var coloreColor = (Color)wpfColor;

            Assert.AreEqual(wpfColor.R, coloreColor.R);
            Assert.AreEqual(wpfColor.G, coloreColor.G);
            Assert.AreEqual(wpfColor.B, coloreColor.B);
            Assert.AreEqual(wpfColor.A, coloreColor.A);
        }

        [Test]
        public void ShouldImplicitCastToWpfColor()
        {
            var coloreColor = new Color(1, 2, 4, 8);
            WpfColor wpfColor = coloreColor;

            Assert.AreEqual(coloreColor.R, wpfColor.R);
            Assert.AreEqual(coloreColor.G, wpfColor.G);
            Assert.AreEqual(coloreColor.B, wpfColor.B);
            Assert.AreEqual(coloreColor.A, wpfColor.A);
        }

        [Test]
        public void ShouldImplicitCastFromWpfColor()
        {
            var wpfColor = WpfColor.FromArgb(5, 10, 15, 20);
            Color coloreColor = wpfColor;

            Assert.AreEqual(wpfColor.R, coloreColor.R);
            Assert.AreEqual(wpfColor.G, coloreColor.G);
            Assert.AreEqual(wpfColor.B, coloreColor.B);
            Assert.AreEqual(wpfColor.A, coloreColor.A);
        }

        [Test]
        public void ShouldEqualWpfColorUsingOverload()
        {
            var coloreColor = new Color(1, 2, 3, 8);
            var wpfColor = WpfColor.FromArgb(8, 1, 2, 3);

            Assert.True(coloreColor.Equals(wpfColor));
            Assert.AreEqual(coloreColor, wpfColor);
        }
    }
}
