namespace Colore.Tests.Razer
{
    using System.ComponentModel;

    using Colore.Razer;

    using NUnit.Framework;

    [TestFixture]
    public class NativeCallExceptionTests
    {
        [Test]
        public void ShouldSetCorrectFunctionName()
        {
            Assert.AreEqual("TestFunc", new NativeCallException("TestFunc", Result.RzSuccess).Function);
        }

        [Test]
        public void ShouldSetCorrectResult()
        {
            Assert.AreEqual(Result.RzSuccess, new NativeCallException("TestFunc", Result.RzSuccess).Result);
        }

        [Test]
        public void ShouldSetMessage()
        {
            const string Func = "TestFunc";
            var result = Result.RzSuccess;
            var expected = string.Format("Call to native Chroma SDK function {0} failed with error: {1}", Func, result);

            Assert.AreEqual(expected, new NativeCallException(Func, result).Message);
        }

        [Test]
        public void ShouldSetInnerException()
        {
            var result = Result.RzSuccess;
            var expected = new Win32Exception(result);
            var actual = new NativeCallException("TestFunc", result).InnerException;
            Assert.AreEqual(expected.GetType(), actual.GetType(), "Expected types to be equal.");
            Assert.AreEqual(expected.HResult, actual.HResult, "Expected HResults to be equal.");
            Assert.AreEqual(expected.Message, actual.Message, "Expected message to be equal.");
            Assert.AreEqual(
                expected.NativeErrorCode,
                ((Win32Exception)actual).NativeErrorCode,
                "Expected native error codes to be equal.");
        }
    }
}
