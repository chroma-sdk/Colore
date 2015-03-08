namespace Colore.Tests.Razer
{
    using Colore.Core;
    using Colore.Razer;

    using NUnit.Framework;

#if WIN64

    using size_t = System.UInt64;

#elif WIN32

    using size_t = System.UInt32;

#else

#error Unsupported build configuration.

#endif

    [TestFixture]
    public class SizeTests
    {
        private const size_t ZeroSizeType = 0;

        private const size_t OneSizeType = 1;

        [Test]
        public void ShouldConstructWithCorrectValue()
        {
            Assert.AreEqual(new Size(0), ZeroSizeType);
        }

        [Test]
        public void ShouldEqualIdenticalSize()
        {
            var a = new Size(0);
            var b = new Size(0);
            Assert.AreEqual(a, b, "AreEqual failed.");
            Assert.True(a == b, "Equality op check failed.");
            Assert.False(a != b, "Inequality op check failed.");
        }

        [Test]
        public void ShouldNotEqualDifferentSize()
        {
            var a = new Size(0);
            var b = new Size(1);
            Assert.AreNotEqual(a, b, "AreNotEqual failed.");
            Assert.False(a == b, "Equality op check failed.");
            Assert.True(a != b, "Inequality op check failed.");
        }

        [Test]
        public void ShouldEqualIdenticalSizeType()
        {
            var a = new Size(0);
            const size_t B = 0;
            Assert.AreEqual(a, B, "AreEqual failed.");
            Assert.True(a == B, "Equality op check failed.");
            Assert.False(a != B, "Inequality op check failed.");
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.True(a.Equals((object)B));
        }

        [Test]
        public void ShouldNotEqualDifferentSizeType()
        {
            var a = new Size(0);
            const size_t B = 1;
            Assert.AreNotEqual(a, B, "AreNotEqual failed.");
            Assert.False(a == B, "Equality op check failed.");
            Assert.True(a != B, "Inequality op check failed.");
        }

        [Test]
        public void SizeTypeShouldEqualIdenticalSize()
        {
            const size_t A = 0;
            var b = new Size(0);
            Assert.AreEqual(A, b, "AreEqual failed.");
            Assert.True(A == b, "Equality op check failed.");
            Assert.False(A != b, "Inequality op check failed.");
        }

        [Test]
        public void SizeTypeShouldNotEqualDifferentSize()
        {
            const size_t A = 0;
            var b = new Size(1);
            Assert.AreNotEqual(A, b, "AreNotEqual failed.");
            Assert.False(A == b, "Equality op check failed.");
            Assert.True(A != b, "Inequality op check failed.");
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            Assert.AreNotEqual(new Size(0), null);
            Assert.False(new Size(0).Equals(null));
        }

        [Test]
        public void ShouldNotEqualUnsupportedType()
        {
            Assert.AreNotEqual(new Size(0), new object());
            Assert.False(new Size(0).Equals(new object()));
        }

        [Test]
        public void GreaterThanShouldReturnTrueWhenGreater()
        {
            Assert.True(new Size(1) > new Size(0));
            Assert.True(new Size(1) > ZeroSizeType);
            Assert.True(OneSizeType > new Size(0));
        }

        [Test]
        public void GreaterThanShouldReturnFalseWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Size(0) > new Size(0));
            Assert.False(new Size(0) > ZeroSizeType);
            Assert.False(ZeroSizeType > new Size(0));
        }

        [Test]
        public void GreaterThanShouldReturnFalseWhenLess()
        {
            Assert.False(new Size(0) > new Size(1));
            Assert.False(new Size(0) > OneSizeType);
            Assert.False(ZeroSizeType > new Size(1));
        }

        [Test]
        public void LessThanShouldReturnTrueWhenLess()
        {
            Assert.True(new Size(0) < new Size(1));
            Assert.True(new Size(0) < OneSizeType);
            Assert.True(ZeroSizeType < new Size(1));
        }

        [Test]
        public void LessThanShouldReturnFalseWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Size(0) < new Size(0));
            Assert.False(new Size(0) < ZeroSizeType);
            Assert.False(ZeroSizeType < new Size(0));
        }

        [Test]
        public void LessThanShouldReturnFalseWhenGreater()
        {
            Assert.False(new Size(1) < new Size(0));
            Assert.False(new Size(1) < ZeroSizeType);
            Assert.False(OneSizeType < new Size(0));
        }

        [Test]
        public void GreaterOrEqualShouldReturnTrueWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Size(0) >= new Size(0));
            Assert.True(new Size(0) >= ZeroSizeType);
            Assert.True(ZeroSizeType >= new Size(0));
        }

        [Test]
        public void GreaterOrEqualShouldReturnTrueWhenGreater()
        {
            Assert.True(new Size(1) >= new Size(0));
            Assert.True(new Size(1) >= ZeroSizeType);
            Assert.True(OneSizeType >= new Size(0));
        }

        [Test]
        public void GreaterOrEqualShouldReturnFalseWhenLess()
        {
            Assert.False(new Size(0) >= new Size(1));
            Assert.False(new Size(0) >= OneSizeType);
            Assert.False(ZeroSizeType >= new Size(1));
        }

        [Test]
        public void LessOrEqualShouldReturnTrueWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Size(0) <= new Size(0));
            Assert.True(new Size(0) <= ZeroSizeType);
            Assert.True(ZeroSizeType <= new Size(0));
        }

        [Test]
        public void LessOrEqualShouldReturnFalseWhenGreater()
        {
            Assert.False(new Size(1) <= new Size(0));
            Assert.False(new Size(1) <= ZeroSizeType);
            Assert.False(OneSizeType <= new Size(0));
        }

        [Test]
        public void LessOrEqualShouldReturnTrueWhenLess()
        {
            Assert.True(new Size(0) <= new Size(1));
            Assert.True(new Size(0) <= OneSizeType);
            Assert.True(ZeroSizeType <= new Size(1));
        }

        [Test]
        public void CompareToEqualShouldReturnZero()
        {
            Assert.AreEqual(new Size(0).CompareTo(new Size(0)), 0);
        }

        [Test]
        public void CompareToEqualSizeTypeShouldReturnZero()
        {
            Assert.AreEqual(new Size(0).CompareTo(ZeroSizeType), 0);
        }

        [Test]
        public void SizeTypeCompareToEqualSizeShouldReturnZero()
        {
            Assert.AreEqual(ZeroSizeType.CompareTo(new Size(0)), 0);
        }

        [Test]
        public void CompareToLowerShouldReturnPositive()
        {
            Assert.Greater(new Size(OneSizeType), new Size(0));
        }

        [Test]
        public void CompareToLowerSizeTypeShouldReturnPositive()
        {
            Assert.Greater(new Size(1), ZeroSizeType);
        }

        [Test]
        public void SizeTypeCompareToLowerSizeShouldReturnPositive()
        {
            Assert.Greater(OneSizeType, new Size(0));
        }

        [Test]
        public void CompareToHigherShouldReturnNegative()
        {
            Assert.Less(new Size(0), new Size(1));
        }

        [Test]
        public void CompareToHigherSizeTypeShouldReturnNegative()
        {
            Assert.Less(new Size(0), OneSizeType);
        }

        [Test]
        public void SizeTypeCompareToHigherSizeShouldReturnNegative()
        {
            Assert.Less(ZeroSizeType, new Size(1));
        }

        [Test]
        public void ShouldImplicitCastToSizeType()
        {
            var size = new Size(5);
            size_t sizet = size;
            Assert.AreEqual(size, sizet);
        }

        [Test]
        public void SizeTypeShouldImplicitCastToSize()
        {
            const size_t Sizet = 5;
            Size size = Sizet;
            Assert.AreEqual(Sizet, size);
        }

        [Test]
        public void ShouldToStringToValue()
        {
            Assert.AreEqual(new Size(23).ToString(), "23");
        }

        [Test]
        public void ShouldHaveSameHashCodeAsValue()
        {
            const size_t Size = 6;
            Assert.AreEqual(new Size(Size).GetHashCode(), Size.GetHashCode());
        }
    }
}
