namespace Corale.Colore.Tests.Razer
{
    using System.Security.Policy;

    using Colore.Razer;

    using NUnit.Framework;

    [TestFixture]
    public class ResultTests
    {
        [Test]
        public void ShouldEqualIdenticalResult()
        {
            Assert.AreEqual(new Result(0), new Result(0));
        }

        [Test]
        public void ShouldNotEqualDifferentResult()
        {
            Assert.AreNotEqual(new Result(0), new Result(1));
            Assert.True(new Result(0) != new Result(1));
        }

        [Test]
        public void ShouldEqualIdenticalInt()
        {
            var result = new Result(1);
            const int I = 1;
            Assert.AreEqual(result, I);

            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.True(result.Equals((object)I));
        }

        [Test]
        public void ShouldNotEqualDifferentInt()
        {
            Assert.AreNotEqual(new Result(0), 1);
        }

        [Test]
        public void IntShouldEqualResult()
        {
            const int I = 1;
            var result = new Result(1);
            Assert.AreEqual(I, result);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            Assert.AreNotEqual(Result.RzSuccess, null);
            Assert.False(Result.RzSuccess.Equals(null));
        }

        [Test]
        public void ShouldNotEqualUnsupportedType()
        {
            Assert.AreNotEqual(Result.RzSuccess, new object());
            Assert.False(Result.RzSuccess.Equals(new object()));
        }

        [Test]
        public void SuccessShouldEqualTrue()
        {
            Assert.True(Result.RzSuccess);
        }

        [Test]
        public void FailureShouldEqualFalse()
        {
            Assert.False(Result.RzFailed);
        }

        [Test]
        public void SuccessShouldImplicitCastToTrueBool()
        {
            bool test = Result.RzSuccess;
            Assert.True(test);
        }

        [Test]
        public void FailureShouldImplicitCastToFalseBool()
        {
            bool test = Result.RzFailed;
            Assert.False(test);
        }

        [Test]
        public void ShouldImplicitCastToInt()
        {
            var result = new Result(2);
            int i = result;
            Assert.AreEqual(result, i);
        }

        [Test]
        public void IntShouldImplicitCastToResult()
        {
            const int I = 2;
            Result result = I;
            Assert.AreEqual(result, I);
        }

        [Test]
        public void DefinedResultShouldHaveName()
        {
            Assert.AreEqual(Result.RzSuccess.Name, "RzSuccess");
        }

        [Test]
        public void DefinedResultShouldHaveDescription()
        {
            Assert.AreEqual(Result.RzSuccess.Description, "Success.");
        }

        [Test]
        public void UnknownResultShouldHaveUnknownName()
        {
            Assert.AreEqual(new Result(-50).Name, "Unknown");
        }

        [Test]
        public void UnknownResultShouldHaveUnknownDescription()
        {
            Assert.AreEqual(new Result(-50).Description, "Unknown.");
        }

        [Test]
        public void ResultShouldToStringCorrectly()
        {
            Assert.AreEqual(Result.RzSuccess.ToString(), "RzSuccess: Success. (0)");
        }

        [Test]
        public void CompareToEqualShouldReturnZero()
        {
            Assert.AreEqual(new Result(0).CompareTo(new Result(0)), 0);
        }

        [Test]
        public void CompareToEqualIntShouldReturnZero()
        {
            Assert.AreEqual(new Result(0).CompareTo(0), 0);
        }

        [Test]
        public void IntCompareToEqualResultShouldReturnZero()
        {
            Assert.AreEqual(0.CompareTo(new Result(0)), 0);
        }

        [Test]
        public void CompareToLowerShouldReturnPositive()
        {
            Assert.Greater(new Result(1), new Result(0));
        }

        [Test]
        public void CompareToLowerIntShouldReturnPositive()
        {
            Assert.Greater(new Result(1), 0);
        }

        [Test]
        public void IntCompareToLowerResultShouldReturnPositive()
        {
            Assert.Greater(1, new Result(0));
        }

        [Test]
        public void CompareToHigherShouldReturnNegative()
        {
            Assert.Less(new Result(0), new Result(1));
        }

        [Test]
        public void CompareToHigherIntShouldReturnNegative()
        {
            Assert.Less(new Result(0), 1);
        }

        [Test]
        public void IntCompareToHigherResultShouldReturnNegative()
        {
            Assert.Less(0, new Result(1));
        }

        [Test]
        public void GreaterOrEqualShouldReturnTrueWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Result(0) >= new Result(0), "Result >= Result comparison failed.");
            Assert.True(new Result(0) >= 0, "Result >= int comparison failed.");
            Assert.True(0 >= new Result(0), "int >= Result comparison failed.");
        }

        [Test]
        public void LessOrEqualShouldReturnTrueWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Result(0) <= new Result(0), "Result <= Result comparison failed.");
            Assert.True(new Result(0) <= 0, "Result <= int comparison failed.");
            Assert.True(0 <= new Result(0), "int <= Result comparison failed.");
        }

        [Test]
        public void GreaterOrEqualShouldReturnTrueWhenGreater()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Result(1) >= new Result(0), "Result >= Result comparison failed.");
            Assert.True(new Result(1) >= 0, "Result >= int comparison failed.");
            Assert.True(1 >= new Result(0), "int >= Result comparison failed.");
        }

        [Test]
        public void LessOrEqualShouldReturnTrueWhenLess()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.True(new Result(0) <= new Result(1), "Result <= Result comparison failed.");
            Assert.True(new Result(0) <= 1, "Result <= int comparison failed.");
            Assert.True(0 <= new Result(1), "int <= Result comparison failed.");
        }

        [Test]
        public void GreaterOrEqualShouldReturnFalseWhenLess()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Result(0) >= new Result(1), "Result >= Result comparison failed.");
            Assert.False(new Result(0) >= 1, "Result >= int comparison failed.");
            Assert.False(0 >= new Result(1), "int >= Result comparison failed.");
        }

        [Test]
        public void LessOrEqualShouldReturnFalseWhenGreater()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Result(1) <= new Result(0), "Result <= Result comparison failed.");
            Assert.False(new Result(1) <= 0, "Result <= int comparison failed.");
            Assert.False(1 <= new Result(0), "int <= Result comparison failed.");
        }

        [Test]
        public void GreaterThanShouldReturnTrueWhenGreater()
        {
            Assert.True(new Result(1) > new Result(0), "Result > Result comparison failed.");
            Assert.True(new Result(1) > 0, "Result > int comparison failed.");
            Assert.True(1 > new Result(0), "int > Result comparison failed.");
        }

        [Test]
        public void GreaterThanShouldReturnFalseWhenLess()
        {
            Assert.False(new Result(0) > new Result(1), "Result > Result comparison failed.");
            Assert.False(new Result(0) > 1, "Result > int comparison failed.");
            Assert.False(0 > new Result(1), "int > Result comparison failed.");
        }

        [Test]
        public void GreaterThanShouldReturnFalseWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Result(0) > new Result(0), "Result > Result comparison failed.");
            Assert.False(new Result(0) > 0, "Result > int comparison failed.");
            Assert.False(0 > new Result(0), "int > Result comparison failed.");
        }

        [Test]
        public void LessThanShouldReturnTrueWhenLess()
        {
            Assert.True(new Result(0) < new Result(1), "Result < Result comparison failed.");
            Assert.True(new Result(0) < 1, "Result < int comparison failed.");
            Assert.True(0 < new Result(1), "int < Result comparison failed.");
        }

        [Test]
        public void LessThanShouldReturnFalseWhenGreater()
        {
            Assert.False(new Result(1) < new Result(0), "Result < Result comparison failed.");
            Assert.False(new Result(1) < 0, "Result < int comparison failed.");
            Assert.False(1 < new Result(0), "int < Result comparison failed.");
        }

        [Test]
        public void LessThanShouldReturnFalseWhenEqual()
        {
            // ReSharper disable once EqualExpressionComparison
            Assert.False(new Result(0) < new Result(0), "Result < Result comparison failed.");
            Assert.False(new Result(0) < 0, "Result < int comparison failed.");
            Assert.False(0 < new Result(0), "int < Result comparison failed.");
        }

        [Test]
        public void FalseShouldShortCircuitLogicalAnd()
        {
            // ReSharper disable once RedundantLogicalConditionalExpressionOperand
            if (Result.RzFailed && true)
                Assert.Fail("If-check failed");
        }

        [Test]
        public void TrueShouldShortCircuitLogicalOr()
        {
            // ReSharper disable once RedundantLogicalConditionalExpressionOperand
            if (Result.RzSuccess || false)
                Assert.Pass();
            Assert.Fail("If-check failed.");
        }
    }
}
