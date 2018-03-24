// ---------------------------------------------------------------------------------------
// <copyright file="DeathstalkerGridTests.cs" company="Corale">
//     Copyright © 2015-2018 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Effects.Keyboard.Effects
{
    using System;

    using Colore.Data;
    using Colore.Effects.Keyboard;

    using NUnit.Framework;

    [TestFixture]
    public class DeathstalkerGridTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRange1DGet()
        {
            var grid = DeathstalkerGrid.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = grid[-1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = grid[KeyboardConstants.MaxDeathstalkerZones],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxDeathstalkerZones));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DSet()
        {
            var grid = DeathstalkerGrid.Create();

            Assert.That(
                () => grid[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[KeyboardConstants.MaxDeathstalkerZones] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxDeathstalkerZones));
        }

        [Test]
        public void ShouldSetToBlackWithCreate()
        {
            var grid = DeathstalkerGrid.Create();

            for (var index = 0; index < KeyboardConstants.MaxDeathstalkerZones; index++)
            {
                Assert.That(grid[index], Is.EqualTo(Color.Black));
            }
        }

        [Test]
        public void ShouldSetAllColorsWithColorCtor()
        {
            var grid = new DeathstalkerGrid(Color.Red);

            for (var index = 0; index < KeyboardConstants.MaxDeathstalkerZones; index++)
            {
                Assert.That(grid[index], Is.EqualTo(Color.Red));
            }
        }

        [Test]
        public void ShouldSetNewColors()
        {
            var grid = DeathstalkerGrid.Create();

            grid[1] = Color.Red;

            Assert.That(grid[1], Is.EqualTo(Color.Red));
        }

        [Test]
        public void ShouldClearToBlack()
        {
            var grid = new DeathstalkerGrid(Color.Pink);
            grid.Clear();

            Assert.That(grid, Is.EqualTo(DeathstalkerGrid.Create()));
        }

        [Test]
        public void ShouldEqualIdenticalGrid()
        {
            var a = new DeathstalkerGrid(Color.Red);
            var b = new DeathstalkerGrid(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentGrid()
        {
            var a = new DeathstalkerGrid(Color.Red);
            var b = new DeathstalkerGrid(Color.Pink);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var grid = DeathstalkerGrid.Create();
            var obj = new object();

            Assert.False(grid == obj);
            Assert.True(grid != obj);
            Assert.False(grid.Equals(obj));
            Assert.AreNotEqual(grid, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var grid = DeathstalkerGrid.Create();

            Assert.False(grid == null);
            Assert.True(grid != null);
            Assert.AreNotEqual(grid, null);
        }

        [Test]
        public void ShouldGetWithIndexIndexer()
        {
            var grid = new DeathstalkerGrid(Color.Red);
            Assert.AreEqual(Color.Red, grid[3]);
        }

        [Test]
        public void ShouldSetWithIndexIndexer()
        {
            var grid = DeathstalkerGrid.Create();

            grid[2] = Color.Red;

            Assert.AreEqual(Color.Red, grid[2]);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new DeathstalkerGrid(Color.Red)
            {
                [1] = Color.Green,
                [3] = Color.Orange,
                [4] = Color.Orange
            };
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new DeathstalkerGrid(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }
    }
}
