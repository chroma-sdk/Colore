// ---------------------------------------------------------------------------------------
// <copyright file="MouseStaticTests.cs" company="Corale">
//     Copyright © 2015-2020 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Effects.Mouse.Effects
{
    using Colore.Data;
    using Colore.Effects.Mouse;

    using NUnit.Framework;

    public class MouseStaticTests
    {
        [Test]
        public void ShouldConstructWithAllLedsSetIfNoLed()
        {
            Assert.AreEqual(Led.All, new MouseStatic(Color.Red).Led);
        }

        [Test]
        public void ShouldConstructWithCorrectColor()
        {
            Assert.AreEqual(Color.Red, new MouseStatic(Led.All, Color.Red).Color);
        }

        [Test]
        public void ShouldConstructWithCorrectLed()
        {
            Assert.AreEqual(Led.Backlight, new MouseStatic(Led.Backlight, Color.Black).Led);
        }

        [Test]
        public void ShouldEqualEffectWithSameColorAndLed()
        {
            var a = new MouseStatic(Led.Backlight, Color.Red);
            var b = new MouseStatic(Led.Backlight, Color.Red);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColor()
        {
            var a = new MouseStatic(Led.All, Color.Red);
            var b = new MouseStatic(Led.All, Color.Blue);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentLed()
        {
            var a = new MouseStatic(Led.ScrollWheel, Color.Red);
            var b = new MouseStatic(Led.Strip1, Color.Red);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentLedAndColor()
        {
            var a = new MouseStatic(Led.Strip10, Color.Red);
            var b = new MouseStatic(Led.Strip2, Color.Green);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualEffectWithSameColorUsingEqualOp()
        {
            var a = new MouseStatic(Led.All, Color.Red);
            var b = new MouseStatic(Led.All, Color.Red);
            Assert.True(a == b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorUsingEqualOp()
        {
            var a = new MouseStatic(Led.All, Color.Red);
            var b = new MouseStatic(Led.All, Color.Blue);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldEqualEffectWithSameColorUsingNotEqualOp()
        {
            var a = new MouseStatic(Led.All, Color.Red);
            var b = new MouseStatic(Led.All, Color.Red);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorUsingNotEqualOp()
        {
            var a = new MouseStatic(Led.All, Color.Red);
            var b = new MouseStatic(Led.All, Color.Blue);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = new MouseStatic(Led.All, Color.Red);
            Assert.AreNotEqual(effect, null);
            Assert.False(effect.Equals(null));
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = new MouseStatic(Led.All, Color.Red);
            var obj = new object();
            Assert.False(effect.Equals(obj));
        }

        [Test]
        public void ShouldEqualEffectWithSameLedAndColorCastAsObject()
        {
            var effect = new MouseStatic(Led.Strip3, Color.Red);
            var obj = new MouseStatic(Led.Strip3, Color.Red) as object;
            Assert.True(effect.Equals(obj));
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentLedCastAsObject()
        {
            var effect = new MouseStatic(Led.Strip11, Color.Red);
            var obj = new MouseStatic(Led.Backlight, Color.Red) as object;
            Assert.False(effect.Equals(obj));
        }

        [Test]
        public void ShouldNotEqualEffectWithDifferentColorCastAsObject()
        {
            var effect = new MouseStatic(Led.All, Color.Red);
            var obj = new MouseStatic(Led.All, Color.Blue) as object;
            Assert.False(effect.Equals(obj));
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            var effect = new MouseStatic();
            Assert.Zero(effect.GetHashCode());
        }
    }
}
