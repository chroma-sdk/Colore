// ---------------------------------------------------------------------------------------
// <copyright file="KeyboardCustomTests.cs" company="Corale">
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
    public class KeyboardCustomTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRange2DGet()
        {
            var grid = KeyboardCustom.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = grid[-1, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = grid[KeyboardConstants.MaxRows, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxRows));

            Assert.That(
                () => dummy = grid[0, -1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = grid[0, KeyboardConstants.MaxColumns],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DSet()
        {
            var grid = KeyboardCustom.Create();

            Assert.That(
                () => grid[-1, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[KeyboardConstants.MaxRows, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxRows));

            Assert.That(
                () => grid[0, -1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[0, KeyboardConstants.MaxColumns] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DGet()
        {
            var grid = KeyboardCustom.Create();

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
                () => dummy = grid[KeyboardConstants.MaxKeys],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxKeys));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DSet()
        {
            var grid = KeyboardCustom.Create();

            Assert.That(
                () => grid[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[KeyboardConstants.MaxKeys] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeyboardConstants.MaxKeys));
        }

        [Test]
        public void ShouldSetToBlackWithCreate()
        {
            var grid = KeyboardCustom.Create();

            for (var row = 0; row < KeyboardConstants.MaxRows; row++)
            {
                for (var column = 0; column < KeyboardConstants.MaxColumns; column++)
                    Assert.That(grid[row, column], Is.EqualTo(Color.Black));
            }
        }

        [Test]
        public void ShouldSetAllColorsWithColorCtor()
        {
            var grid = new KeyboardCustom(Color.Red);

            for (var row = 0; row < KeyboardConstants.MaxRows; row++)
            {
                for (var column = 0; column < KeyboardConstants.MaxColumns; column++)
                    Assert.That(grid[row, column], Is.EqualTo(Color.Red));
            }
        }

        [Test]
        public void ShouldSetNewColors()
        {
            var grid = KeyboardCustom.Create();

            grid[0, 5] = Color.Red;

            Assert.That(grid[0, 5], Is.EqualTo(Color.Red));
        }

        [Test]
        public void ShouldClearToBlack()
        {
            var grid = new KeyboardCustom(Color.Pink);
            grid.Clear();

            Assert.That(grid, Is.EqualTo(KeyboardCustom.Create()));
        }

        [Test]
        public void ShouldEqualIdenticalGrid()
        {
            var a = new KeyboardCustom(Color.Red);
            var b = new KeyboardCustom(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentGrid()
        {
            var a = new KeyboardCustom(Color.Red);
            var b = new KeyboardCustom(Color.Pink);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferent2DArray()
        {
            var grid = new KeyboardCustom(Color.Pink);
            var arr = new Color[KeyboardConstants.MaxRows][];

            // Populate the 2D array
            for (var row = 0; row < KeyboardConstants.MaxRows; row++)
            {
                arr[row] = new Color[KeyboardConstants.MaxColumns];
                for (var col = 0; col < KeyboardConstants.MaxColumns; col++)
                    arr[row][col] = Color.Red;
            }

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidRowCount()
        {
            var grid = KeyboardCustom.Create();
            var arr = new Color[2][];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidColumnCount()
        {
            var grid = KeyboardCustom.Create();
            var arr = new Color[KeyboardConstants.MaxRows][];
            arr[0] = new Color[2];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var grid = KeyboardCustom.Create();
            var obj = new object();

            Assert.False(grid == obj);
            Assert.True(grid != obj);
            Assert.False(grid.Equals(obj));
            Assert.AreNotEqual(grid, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var grid = KeyboardCustom.Create();

            Assert.False(grid == null);
            Assert.True(grid != null);
            Assert.AreNotEqual(grid, null);
        }

        [Test]
        public void ShouldGetWithGridIndexer()
        {
            var grid = new KeyboardCustom(Color.Red);
            Assert.AreEqual(Color.Red, grid[3, 3]);
        }

        [Test]
        public void ShouldGetWithIndexIndexer()
        {
            var grid = new KeyboardCustom(Color.Red);
            Assert.AreEqual(Color.Red, grid[3]);
        }

        [Test]
        public void ShouldGetWithKeyIndexer()
        {
            var grid = KeyboardCustom.Create();
            grid[Key.Escape] = Color.Red;
            Assert.AreEqual(Color.Red, grid[Key.Escape]);
        }

        [Test]
        public void ShouldSetWithGridIndexer()
        {
            var grid = KeyboardCustom.Create();

            grid[5, 5] = Color.Red;

            Assert.AreEqual(Color.Red, grid[5, 5]);
        }

        [Test]
        public void ShouldSetWithIndexIndexer()
        {
            var grid = KeyboardCustom.Create();

            grid[5] = Color.Red;

            Assert.AreEqual(Color.Red, grid[5]);
        }

        [Test]
        public void ShouldSetWithKeyIndexer()
        {
            var grid = KeyboardCustom.Create();

            grid[Key.Escape] = Color.Red;

            Assert.AreEqual(Color.Red, grid[Key.Escape]);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new KeyboardCustom(Color.Red)
            {
                [Key.A] = Color.Green,
                [Key.Escape] = Color.Orange,
                [0, 1] = Color.Orange
            };
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new KeyboardCustom(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            var effect = new KeyboardCustom();
            Assert.Zero(effect.GetHashCode());
        }

        [Test]
        public void ShouldConstructProperMultiArray()
        {
            var effect = KeyboardCustom.Create();
            effect[3, 12] = Color.Red;
            effect[Key.Space] = Color.Green;
            var (colors, keys) = effect.ToMultiArrays();
            Assert.AreEqual(Color.Red, colors[3, 12]);
            Assert.AreEqual(Color.Green.AsKeyColor, keys[5, 7]);
        }
    }
}
