// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLinkStaticTests.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Effects.ChromaLink
{
    using Colore.Data;
    using Colore.Effects.ChromaLink;

    using NUnit.Framework;

    [TestFixture]
    public class ChromaLinkStaticTests
    {
        [Test]
        public void ShouldConstructWithCorrectColor()
        {
            Assert.AreEqual(Color.Red, new StaticChromaLinkEffect(Color.Red).Color);
        }

        [Test]
        public void ShouldEqualEffectWithSameColor()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Red);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColor()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Blue);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualEffectWithSameColorUsingEqualOp()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Red);
            Assert.True(a == b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorUsingEqualOp()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Blue);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldEqualEffectWithSameColorUsingNotEqualOp()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Red);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorUsingNotEqualOp()
        {
            var a = new StaticChromaLinkEffect(Color.Red);
            var b = new StaticChromaLinkEffect(Color.Blue);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = new StaticChromaLinkEffect(Color.Red);
            Assert.AreNotEqual(effect, null);
            Assert.False(effect.Equals(null));
        }

        [Test]
        public void ShouldHaveSameHashcodeAsColor()
        {
            var color = Color.Red;
            var hashcode = color.GetHashCode();
            var effect = new StaticChromaLinkEffect(color);
            Assert.AreEqual(hashcode, effect.GetHashCode());
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = new StaticChromaLinkEffect(Color.Red);
            var obj = new object();
            Assert.False(effect.Equals(obj));
        }

        [Test]
        public void ShouldEqualEffectWithSameColorCastAsObject()
        {
            var effect = new StaticChromaLinkEffect(Color.Red);
            var obj = new StaticChromaLinkEffect(Color.Red) as object;
            Assert.True(effect.Equals(obj));
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorCastAsObject()
        {
            var effect = new StaticChromaLinkEffect(Color.Red);
            var obj = new StaticChromaLinkEffect(Color.Blue) as object;
            Assert.False(effect.Equals(obj));
        }
    }
}
