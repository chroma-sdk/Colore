// <copyright file="BreathingTests.cs" company="Corale">
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

namespace Corale.Colore.Tests.Razer.Keypad.Effects
{
    using Corale.Colore.Core;
    using Corale.Colore.Razer.Keypad.Effects;

    using NUnit.Framework;

    [TestFixture]
    public class BreathingTests
    {
        [Test]
        public void ShouldConstructWithCorrectTypeAndColors()
        {
            const BreathingType Type = BreathingType.Two;
            var first = Color.Red;
            var second = Color.Blue;

            var effect = new Breathing(Type, first, second);

            Assert.AreEqual(Type, effect.Type);
            Assert.AreEqual(first, effect.First);
            Assert.AreEqual(second, effect.Second);
        }

        [Test]
        public void ShouldConstructWithRandomType()
        {
            Assert.AreEqual(BreathingType.Random, new Breathing(BreathingType.Random, Color.Black, Color.Black).Type);
        }

        [Test]
        public void ShouldConstructWithCorrectTypeOnTwoColors()
        {
            Assert.AreEqual(BreathingType.Two, new Breathing(Color.Red, Color.Blue).Type);
        }
    }
}
