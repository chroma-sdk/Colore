// ---------------------------------------------------------------------------------------
// <copyright file="ChromaLinkCustomTests.cs" company="Corale">
//     Copyright © 2015-2021 by Adam Hellberg and Brandon Scott.
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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Colore.Data;
    using Colore.Effects.ChromaLink;

    using NUnit.Framework;

    [TestFixture]
    public class ChromaLinkCustomTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRangeGet()
        {
            var custom = CustomChromaLinkEffect.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = custom[-1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = custom[ChromaLinkConstants.MaxLeds],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(ChromaLinkConstants.MaxLeds));
        }

        [Test]
        public void ShouldThrowWhenOutOfRangeSet()
        {
            var custom = CustomChromaLinkEffect.Create();

            Assert.That(
                () => custom[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => custom[ChromaLinkConstants.MaxLeds] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(ChromaLinkConstants.MaxLeds));
        }

        [Test]
        public void ShouldSetAllColorsWithColorConstructor()
        {
            var effect = new CustomChromaLinkEffect(Color.Red);

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.Red));
        }

        [Test]
        public void ShouldSetBlackColorsWithCreate()
        {
            var effect = CustomChromaLinkEffect.Create();

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.Black));
        }

        [Test]
        public void ShouldThrowOnInvalidArrayCount()
        {
            var colors = new Color[1];

            // ReSharper disable once NotAccessedVariable
            CustomChromaLinkEffect dummy;

            Assert.That(
                () => dummy = new CustomChromaLinkEffect(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldSetCorrectColorsFromList()
        {
            var colors = new Color[ChromaLinkConstants.MaxLeds];
            colors[0] = Color.Red;
            colors[1] = Color.Blue;
            colors[2] = Color.Green;

            var effect = new CustomChromaLinkEffect(colors);

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(colors[i]));
        }

        [Test]
        public void ShouldThrowOnInvalidListCount()
        {
            var colors = new List<Color> { Color.Black };

            CustomChromaLinkEffect dummy;

            Assert.That(
                () => dummy = new CustomChromaLinkEffect(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldSetAllColorsWithSet()
        {
            var effect = CustomChromaLinkEffect.Create();

            effect.Set(Color.Red);

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.Red));
        }

        [Test]
        public void ShouldResetToBlackWithClear()
        {
            var effect = CustomChromaLinkEffect.Create();
            effect.Set(Color.Red);
            effect.Clear();

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                Assert.That(effect[i], Is.EqualTo(Color.Black));
        }

        [Test]
        public void ShouldGetCorrectColor()
        {
            var colors = new Color[ChromaLinkConstants.MaxLeds];
            colors[3] = Color.Red;

            var effect = new CustomChromaLinkEffect(colors);

            Assert.That(effect[3], Is.EqualTo(colors[3]));
        }

        [Test]
        public void ShouldSetCorrectColor()
        {
            var effect = CustomChromaLinkEffect.Create();
            effect[3] = Color.Blue;

            Assert.That(effect[3], Is.EqualTo(Color.Blue));
        }

        [Test]
        public void ShouldEqualIdenticalEffect()
        {
            var a = new CustomChromaLinkEffect(Color.Red);
            var b = new CustomChromaLinkEffect(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentEffect()
        {
            var a = new CustomChromaLinkEffect(Color.Red);
            var b = new CustomChromaLinkEffect(Color.Blue);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentArray()
        {
            var effect = new CustomChromaLinkEffect(Color.Red);
            var array = new Color[ChromaLinkConstants.MaxLeds];

            for (var i = 0; i < ChromaLinkConstants.MaxLeds; i++)
                array[i] = Color.Blue;

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArrayWithInvalidLength()
        {
            var effect = new CustomChromaLinkEffect(Color.Red);
            var array = new[] { Color.Red, Color.Red, Color.Red };

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = CustomChromaLinkEffect.Create();
            var obj = new object();

            Assert.False(effect == obj);
            Assert.True(effect != obj);
            Assert.False(effect.Equals(obj));
            Assert.AreNotEqual(effect, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = default(CustomChromaLinkEffect);

            Assert.False(effect is null);
            Assert.True(effect is not null);
            Assert.False(effect.Equals(null));
            Assert.AreNotEqual(effect, null);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new CustomChromaLinkEffect(Color.Red);
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new CustomChromaLinkEffect(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            var effect = new CustomChromaLinkEffect();
            Assert.Zero(effect.GetHashCode());
        }

        [Test]
        public void ShouldHaveCorrectColorsInProperty()
        {
            var colors = new Color[ChromaLinkConstants.MaxLeds];
            colors[0] = Color.Red;
            var effect = new CustomChromaLinkEffect(colors.ToList());
            Assert.AreEqual(colors, effect.Colors);
        }
    }
}
