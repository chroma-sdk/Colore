// <copyright file="CustomTests.cs" company="Corale">
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

namespace Corale.Colore.Tests.Razer.Mouse.Effects
{
    using System;

    using Colore.Core;
    using Colore.Razer.Mouse;
    using Colore.Razer.Mouse.Effects;

    using NUnit.Framework;

    [TestFixture]
    public class CustomTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRangeGet()
        {
            var effect = Custom.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = effect[-1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => dummy = effect[Constants.MaxLeds],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxLeds));
        }

        [Test]
        public void ShouldThrowWhenOutOfRangeSet()
        {
            var effect = Custom.Create();

            Assert.That(
                () => effect[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => effect[Constants.MaxLeds] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("led")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxLeds));
        }

        [Test]
        public void ShouldThrowWhenInvalidLedGet()
        {
            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = Custom.Create()[Led.All],
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("led"));
        }

        [Test]
        public void ShouldThrowWhenInvalidLedSet()
        {
            var effect = Custom.Create();

            Assert.That(
                () => effect[Led.All] = Color.Red,
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("led"));
        }

        [Test]
        public void ShouldConstructWithCorrectColor()
        {
            var effect = new Custom(Color.Red);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.AreEqual(Color.Red, effect[i]);
        }

        [Test]
        public void ShouldConstructFromList()
        {
            var colors = new Color[Constants.MaxLeds];
            colors[0] = Color.Red;
            colors[1] = Color.Blue;
            colors[2] = Color.Green;

            var effect = new Custom(colors);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.AreEqual(colors[i], effect[i]);
        }

        [Test]
        public void ShouldThrowWhenInvalidListLength()
        {
            var colors = new Color[1];

            Assert.That(
                () => new Custom(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldGetCorrectColorWithIndexIndexer()
        {
            var effect = new Custom(Color.Red);

            Assert.AreEqual(Color.Red, effect[5]);
        }

        [Test]
        public void ShouldSetCorrectColorWithIndexIndexer()
        {
            var effect = Custom.Create();

            effect[5] = Color.Red;

            Assert.AreEqual(Color.Red, effect[5]);
        }

        [Test]
        public void ShouldGetCorrectColorWithLedIndexer()
        {
            var effect = new Custom(Color.Red);

            Assert.AreEqual(Color.Red, effect[Led.Logo]);
        }

        [Test]
        public void ShouldSetCorrectColorWithLedIndexer()
        {
            var effect = Custom.Create();
            effect[Led.Logo] = Color.Red;

            Assert.AreEqual(Color.Red, effect[Led.Logo]);
        }

        [Test]
        public void ShouldCreateWithBlackColors()
        {
            var effect = Custom.Create();

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.AreEqual(Color.Black, effect[i]);
        }

        [Test]
        public void ShouldSetAllColors()
        {
            var effect = Custom.Create();
            effect.Set(Color.Red);

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.AreEqual(Color.Red, effect[i]);
        }

        [Test]
        public void ShouldClearColorsToBlack()
        {
            var effect = Custom.Create();
            effect.Set(Color.Red);
            effect.Clear();

            for (var i = 0; i < Constants.MaxLeds; i++)
                Assert.AreEqual(Color.Black, effect[i]);
        }

        [Test]
        public void ShouldEqualIdenticalEffect()
        {
            var a = new Custom(Color.Red);
            var b = new Custom(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentEffect()
        {
            var a = new Custom(Color.Red);
            var b = new Custom(Color.Blue);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualIdenticalArray()
        {
            var effect = new Custom(Color.Red);
            var array = new Color[Constants.MaxLeds];

            for (var i = 0; i < Constants.MaxLeds; i++)
                array[i] = Color.Red;

            Assert.True(effect == array);
            Assert.False(effect != array);
            Assert.True(effect.Equals(array));
            Assert.AreEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualDifferentArray()
        {
            var effect = new Custom(Color.Red);
            var array = new Color[Constants.MaxLeds];

            for (var i = 0; i < Constants.MaxLeds; i++)
                array[i] = Color.Blue;

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArrayWithInvalidLength()
        {
            var effect = new Custom(Color.Red);
            var array = new[] { Color.Red, Color.Red, Color.Red };

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = Custom.Create();
            var obj = new object();

            Assert.False(effect == obj);
            Assert.True(effect != obj);
            Assert.False(effect.Equals(obj));
            Assert.AreNotEqual(effect, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = default(Custom);

            Assert.False(effect == null);
            Assert.True(effect != null);
            Assert.False(effect.Equals(null));
            Assert.AreNotEqual(effect, null);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new Custom(Color.Red);
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new Custom(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }
    }
}
