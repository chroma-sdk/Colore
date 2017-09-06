// ---------------------------------------------------------------------------------------
// <copyright file="ResultTests.cs" company="Corale">
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

namespace Corale.Colore.Tests.Razer
{
    using Corale.Colore.Razer;

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

        [Test]
        public void SuccessPropertyShouldBeTrueOnSuccess()
        {
            Assert.True(Result.RzSuccess.Success);
        }

        [Test]
        public void SuccessPropertyShouldBeFalseOnFailure()
        {
            Assert.False(Result.RzFailed.Success);
        }

        [Test]
        public void FailedPropertyShouldBeTrueOnFailure()
        {
            Assert.True(Result.RzFailed.Failed);
        }

        [Test]
        public void FailedPropertyShouldBeFalseOnSuccess()
        {
            Assert.False(Result.RzSuccess.Failed);
        }
    }
}
