// <copyright file="CustomGridTests.cs" company="Corale">
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

    using Corale.Colore.Core;
    using Corale.Colore.Razer.Mouse;
    using Corale.Colore.Razer.Mouse.Effects;

    using NUnit.Framework;

    [TestFixture]
    public class CustomGridTests
    {
        [Test]
        public void ShouldThrowWhenConstructedWithInvalid2DRowCount()
        {
            var colors = new Color[2][];

            Assert.That(
                () => new CustomGrid(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldThrowWhenConstructedWithInvalid2DColumnCount()
        {
            var colors = new Color[Constants.MaxRows][];
            colors[0] = new Color[1];

            Assert.That(
                () => new CustomGrid(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldConstructFrom2DArray()
        {
            var colors = new Color[Constants.MaxRows][];

            for (var row = 0; row < Constants.MaxRows; row++)
                colors[row] = new Color[Constants.MaxColumns];

            colors[1][2] = Color.Red;
            colors[0][4] = Color.Blue;
            colors[3][1] = Color.Green;

            var effect = new CustomGrid(colors);

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    Assert.AreEqual(colors[row][column], effect[row, column]);
            }
        }

        [Test]
        public void ShouldThrowWhenConstructedWithInvalid1DSize()
        {
            var colors = new Color[1];

            Assert.That(
                () => new CustomGrid(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldConstructFrom1DArray()
        {
            var arr = new Color[Constants.MaxGridLeds];

            arr[2] = Color.Pink;
            arr[4] = Color.Red;
            arr[8] = Color.Blue;

            var grid = new CustomGrid(arr);

            for (var index = 0; index < Constants.MaxGridLeds; index++)
                Assert.That(grid[index], Is.EqualTo(arr[index]));
        }

        [Test]
        public void ShouldConstructFromColor()
        {
            var effect = new CustomGrid(Color.Red);

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    Assert.AreEqual(Color.Red, effect[row, column]);
            }
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DGet()
        {
            var effect = CustomGrid.Create();

            // ReSharper disable once NotAccessedVariable
            Color dummy;

            Assert.That(
                () => dummy = effect[-1, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("row")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => dummy = effect[Constants.MaxRows, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("row")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxRows));

            Assert.That(
                () => dummy = effect[0, -1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("column")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => dummy = effect[0, Constants.MaxColumns],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("column")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DSet()
        {
            var effect = CustomGrid.Create();

            Assert.That(
                () => effect[-1, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("row")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => effect[Constants.MaxRows, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("row")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxRows));

            Assert.That(
                () => effect[0, -1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("column")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => effect[0, Constants.MaxColumns] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("column")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DGet()
        {
            var grid = CustomGrid.Create();

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
                () => dummy = grid[Constants.MaxGridLeds],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("index")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxGridLeds));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DSet()
        {
            var grid = CustomGrid.Create();

            Assert.That(
                () => grid[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("index")
                    .And.Property("ActualValue")
                    .EqualTo(-1));

            Assert.That(
                () => grid[Constants.MaxGridLeds] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                    .With.Property("ParamName")
                    .EqualTo("index")
                    .And.Property("ActualValue")
                    .EqualTo(Constants.MaxGridLeds));
        }

        [Test]
        public void ShouldGetCorrectColorWithGridIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomGrid(Color.Red)[3, 3]);
        }

        [Test]
        public void ShouldGetCorrectColorWithIndexIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomGrid(Color.Red)[5]);
        }

        [Test]
        public void ShouldSetCorrectColorWithGridIndexer()
        {
            var effect = CustomGrid.Create();
            effect[5, 5] = Color.Red;

            Assert.AreEqual(Color.Red, effect[5, 5]);
        }

        [Test]
        public void ShouldSetCorrectColorWithIndexIndexer()
        {
            var effect = CustomGrid.Create();
            effect[5] = Color.Red;

            Assert.AreEqual(Color.Red, effect[5]);
        }

        [Test]
        public void ShouldGetCorrectColorWithLedIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomGrid(Color.Red)[GridLed.Logo]);
        }

        [Test]
        public void ShouldSetCorrectColorWithLedIndexer()
        {
            var effect = CustomGrid.Create();
            effect[GridLed.Logo] = Color.Red;

            Assert.AreEqual(Color.Red, effect[GridLed.Logo]);
        }

        [Test]
        public void ShouldCreateWithAllBlackColors()
        {
            var effect = CustomGrid.Create();

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    Assert.AreEqual(Color.Black, effect[row, column]);
            }
        }

        [Test]
        public void ShouldSetCorrectColorWithSet()
        {
            var effect = CustomGrid.Create();
            effect.Set(Color.Red);

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    Assert.AreEqual(Color.Red, effect[row, column]);
            }
        }

        [Test]
        public void ShouldClearToBlack()
        {
            var effect = CustomGrid.Create();
            effect.Set(Color.Red);
            effect.Clear();

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                for (var column = 0; column < Constants.MaxColumns; column++)
                    Assert.AreEqual(Color.Black, effect[row, column]);
            }
        }

        [Test]
        public void ShouldEqualIdenticalEffect()
        {
            var a = new CustomGrid(Color.Red);
            var b = new CustomGrid(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentEffect()
        {
            var a = new CustomGrid(Color.Red);
            var b = new CustomGrid(Color.Blue);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualIdentical2DArray()
        {
            var effect = new CustomGrid(Color.Red);

            var array = new Color[Constants.MaxRows][];

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                array[row] = new Color[Constants.MaxColumns];

                for (var column = 0; column < Constants.MaxColumns; column++)
                    array[row][column] = Color.Red;
            }

            Assert.True(effect == array);
            Assert.False(effect != array);
            Assert.True(effect.Equals(array));
            Assert.AreEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualDifferent2DArray()
        {
            var effect = new CustomGrid(Color.Red);

            var array = new Color[Constants.MaxRows][];

            for (var row = 0; row < Constants.MaxRows; row++)
            {
                array[row] = new Color[Constants.MaxColumns];

                for (var column = 0; column < Constants.MaxColumns; column++)
                    array[row][column] = Color.Blue;
            }

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidRowCount()
        {
            var effect = CustomGrid.Create();

            var array = new Color[1][];

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidColumnCount()
        {
            var effect = CustomGrid.Create();

            var array = new Color[Constants.MaxRows][];
            array[0] = new Color[1];

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldEqualIdentical1DArray()
        {
            var grid = new CustomGrid(Color.Red);
            var arr = new Color[Constants.MaxGridLeds];

            // Populate the 1D array
            for (var index = 0; index < Constants.MaxGridLeds; index++)
                arr[index] = Color.Red;

            Assert.True(grid == arr);
            Assert.False(grid != arr);
            Assert.True(grid.Equals(arr));
            Assert.AreEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualDifferent1DArray()
        {
            var grid = new CustomGrid(Color.Pink);
            var arr = new Color[Constants.MaxGridLeds];

            for (var index = 0; index < Constants.MaxGridLeds; index++)
                arr[index] = Color.Red;

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual1DArrayWithInvalidSize()
        {
            var grid = CustomGrid.Create();
            var arr = new Color[2];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = CustomGrid.Create();
            var obj = new object();

            Assert.False(effect == obj);
            Assert.True(effect != obj);
            Assert.False(effect.Equals(obj));
            Assert.AreNotEqual(effect, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = default(CustomGrid);

            Assert.False(effect == null);
            Assert.True(effect != null);
            Assert.False(effect.Equals((Color[][])null));
            Assert.False(effect.Equals((Color[])null));
            Assert.AreNotEqual(effect, null);
        }
    }
}
