namespace Colore.Tests.Razer
{
    using Colore.Core;
    using Colore.Razer;

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
        public void ShouldEqualIdenticalColor()
        {
            var a = new Color(0);
            var b = new Color(0);
            Assert.AreEqual(a, b);
            Assert.True(a == b);
            Assert.False(a != b);
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
        public void ShouldEqualIdenticalUint()
        {
            var a = new Color(255, 0, 255);
            const uint B = 0x00FF00FF;
            Assert.AreEqual(a, B);
            Assert.True(a == B);
            Assert.False(a != B);
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
        public void UintShouldEqualIdenticalColor()
        {
            const uint A = 0x00FFFFFF;
            var b = new Color(0x00FFFFFF);
            Assert.AreEqual(A, b);
            Assert.True(A == b);
            Assert.False(A != b);
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
        public void ShouldNotEqualArbitraryObject()
        {
            var c = new Color(0);
            var o = new object();
            Assert.AreNotEqual(c, o);
            Assert.False(c == o);
            Assert.True(c != o);
        }

        [Test]
        public void ShouldImplicitCastToUint()
        {
            const uint Expected = 0x00010203;
            uint actual = new Color(0x00010203);
            Assert.AreEqual(Expected, actual);
        }

        [Test]
        public void UintShouldImplicitCastToColor()
        {
            var expected = new Color(0x00123456);
            Color actual = 0x00123456;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldHaveCorrectHashCode()
        {
            const uint V = 0x00123456;
            var expected = V.GetHashCode();
            var actual = new Color(V).GetHashCode();
            Assert.AreEqual(expected, actual);
        }
    }
}
