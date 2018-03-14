// ---------------------------------------------------------------------------------------
// <copyright file="CustomTests.cs" company="Corale">
//     Copyright © 2015-2017 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Effects.Keypad.Effects
{
    using System;

    using Colore.Data;
    using Colore.Effects.Keypad;

    using NUnit.Framework;

    [TestFixture]
    public class CustomTests
    {
        [Test]
        public void ShouldThrowWhenOutOfRange2DGet()
        {
            var grid = KeypadCustom.Create();

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
                () => dummy = grid[KeypadConstants.MaxRows, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxRows));

            Assert.That(
                () => dummy = grid[0, -1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = grid[0, KeypadConstants.MaxColumns],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DSet()
        {
            var grid = KeypadCustom.Create();

            Assert.That(
                () => grid[-1, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[KeypadConstants.MaxRows, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxRows));

            Assert.That(
                () => grid[0, -1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[0, KeypadConstants.MaxColumns] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DGet()
        {
            var grid = KeypadCustom.Create();

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
                () => dummy = grid[KeypadConstants.MaxKeys],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxKeys));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DSet()
        {
            var grid = KeypadCustom.Create();

            Assert.That(
                () => grid[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[KeypadConstants.MaxKeys] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(KeypadConstants.MaxKeys));
        }

        [Test]
        public void ShouldThrowWhenInvalid2DRowCount()
        {
            // We don't need to set up the columns as the code should throw before
            // it reaches the point of iterating rows
            var arr = new Color[2][];

            // ReSharper disable once NotAccessedVariable
            KeypadCustom dummy;

            Assert.That(
                () => dummy = new KeypadCustom(arr),
                Throws.ArgumentException.With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldThrowWhenInvalid2DColumnCount()
        {
            var arr = new Color[KeypadConstants.MaxRows][];

            // We only need to populate one of the rows, as the
            // code shouldn't check further anyway.
            arr[0] = new Color[2];

            // ReSharper disable once NotAccessedVariable
            KeypadCustom dummy;

            Assert.That(
                () => dummy = new KeypadCustom(arr),
                Throws.ArgumentException.With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldThrowWhenInvalid1DSize()
        {
            var arr = new Color[2];

            // ReSharper disable once NotAccessedVariable
            KeypadCustom dummy;

            Assert.That(
                () => dummy = new KeypadCustom(arr),
                Throws.ArgumentException.With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldSetToBlackWithCreate()
        {
            var grid = KeypadCustom.Create();

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                for (var column = 0; column < KeypadConstants.MaxColumns; column++)
                    Assert.That(grid[row, column], Is.EqualTo(Color.Black));
            }
        }

        [Test]
        public void ShouldSetAllColorsWithColorCtor()
        {
            var grid = new KeypadCustom(Color.Red);

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                for (var column = 0; column < KeypadConstants.MaxColumns; column++)
                    Assert.That(grid[row, column], Is.EqualTo(Color.Red));
            }
        }

        [Test]
        public void ShouldSetProperColorsWith2DCtor()
        {
            var arr = new Color[KeypadConstants.MaxRows][];

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
                arr[row] = new Color[KeypadConstants.MaxColumns];

            // Set some arbitrary colors to test
            arr[0][4] = Color.Purple;
            arr[2][3] = Color.Pink;
            arr[3][0] = Color.Blue;

            var grid = new KeypadCustom(arr);

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                for (var col = 0; col < KeypadConstants.MaxColumns; col++)
                    Assert.That(grid[row, col], Is.EqualTo(arr[row][col]));
            }
        }

        [Test]
        public void ShouldSetProperColorsWith1DCtor()
        {
            var arr = new Color[KeypadConstants.MaxKeys];

            arr[2] = Color.Pink;
            arr[4] = Color.Red;
            arr[8] = Color.Blue;

            var grid = new KeypadCustom(arr);

            for (var index = 0; index < KeypadConstants.MaxKeys; index++)
                Assert.That(grid[index], Is.EqualTo(arr[index]));
        }

        [Test]
        public void ShouldSetNewColors()
        {
            var grid = KeypadCustom.Create();

            grid[0, 4] = Color.Red;

            Assert.That(grid[0, 4], Is.EqualTo(Color.Red));
        }

        [Test]
        public void ShouldClearToBlack()
        {
            var grid = new KeypadCustom(Color.Pink);
            grid.Clear();

            Assert.That(grid, Is.EqualTo(KeypadCustom.Create()));
        }

        [Test]
        public void ShouldSetColorsProperly()
        {
            var grid = KeypadCustom.Create();
            grid.Set(Color.Red);

            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                for (var column = 0; column < KeypadConstants.MaxColumns; column++)
                    Assert.AreEqual(Color.Red, grid[row, column]);
            }
        }

        [Test]
        public void ShouldEqualIdenticalGrid()
        {
            var a = new KeypadCustom(Color.Red);
            var b = new KeypadCustom(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentGrid()
        {
            var a = new KeypadCustom(Color.Red);
            var b = new KeypadCustom(Color.Pink);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferent2DArray()
        {
            var grid = new KeypadCustom(Color.Pink);
            var arr = new Color[KeypadConstants.MaxRows][];

            // Populate the 2D array
            for (var row = 0; row < KeypadConstants.MaxRows; row++)
            {
                arr[row] = new Color[KeypadConstants.MaxColumns];
                for (var col = 0; col < KeypadConstants.MaxColumns; col++)
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
            var grid = KeypadCustom.Create();
            var arr = new Color[2][];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidColumnCount()
        {
            var grid = KeypadCustom.Create();
            var arr = new Color[KeypadConstants.MaxRows][];
            arr[0] = new Color[2];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualDifferent1DArray()
        {
            var grid = new KeypadCustom(Color.Pink);
            var arr = new Color[KeypadConstants.MaxKeys];

            for (var index = 0; index < KeypadConstants.MaxKeys; index++)
                arr[index] = Color.Red;

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual1DArrayWithInvalidSize()
        {
            var grid = KeypadCustom.Create();
            var arr = new Color[2];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var grid = KeypadCustom.Create();
            var obj = new object();

            Assert.False(grid == obj);
            Assert.True(grid != obj);
            Assert.False(grid.Equals(obj));
            Assert.AreNotEqual(grid, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var grid = KeypadCustom.Create();

            Assert.False(grid == null);
            Assert.True(grid != null);
            Assert.False(grid.Equals((Color[][])null));
            Assert.False(grid.Equals((Color[])null));
            Assert.AreNotEqual(grid, null);
        }

        [Test]
        public void ShouldGetWithGridIndexer()
        {
            var grid = new KeypadCustom(Color.Red);
            Assert.AreEqual(Color.Red, grid[3, 3]);
        }

        [Test]
        public void ShouldGetWithIndexIndexer()
        {
            var grid = new KeypadCustom(Color.Red);
            Assert.AreEqual(Color.Red, grid[3]);
        }

        [Test]
        public void ShouldSetWithGridIndexer()
        {
            var grid = KeypadCustom.Create();

            grid[3, 4] = Color.Red;

            Assert.AreEqual(Color.Red, grid[3, 4]);
        }

        [Test]
        public void ShouldSetWithIndexIndexer()
        {
            var grid = KeypadCustom.Create();

            grid[5] = Color.Red;

            Assert.AreEqual(Color.Red, grid[5]);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new KeypadCustom(Color.Red);
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new KeypadCustom(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }
    }
}
