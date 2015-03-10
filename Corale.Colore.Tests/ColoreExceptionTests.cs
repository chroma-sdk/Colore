namespace Corale.Colore.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class ColoreExceptionTests
    {
        [Test]
        public void ShouldSetMessage()
        {
            const string Expected = "Test message.";
            Assert.AreEqual(Expected, new ColoreException("Test message.").Message);
        }

        [Test]
        public void ShouldSetInnerException()
        {
            var expected = new Exception("Expected.");
            var actual = new ColoreException(null, new Exception("Expected.")).InnerException;
            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Message, actual.Message);
        }
    }
}
