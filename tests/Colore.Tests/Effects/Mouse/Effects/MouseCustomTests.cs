// ---------------------------------------------------------------------------------------
// <copyright file="MouseCustomTests.cs" company="Corale">
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

namespace Colore.Tests.Effects.Mouse.Effects
{
    using System;

    using Colore.Data;
    using Colore.Effects.Mouse;

    using NUnit.Framework;

    [TestFixture]
    public class MouseCustomTests
    {
        [Test]
        public void ShouldThrowWhenConstructedWithInvalid2DRowCount()
        {
            var colors = new Color[2][];

            Assert.That(
                () => new CustomMouseEffect(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldThrowWhenConstructedWithInvalid2DColumnCount()
        {
            var colors = new Color[MouseConstants.MaxRows][];
            colors[0] = new Color[1];

            Assert.That(
                () => new CustomMouseEffect(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldConstructFrom2DArray()
        {
            var colors = new Color[MouseConstants.MaxRows][];

            for (var row = 0; row < MouseConstants.MaxRows; row++)
                colors[row] = new Color[MouseConstants.MaxColumns];

            colors[1][2] = Color.Red;
            colors[0][4] = Color.Blue;
            colors[3][1] = Color.Green;

            var effect = new CustomMouseEffect(colors);

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = 0; column < MouseConstants.MaxColumns; column++)
                    Assert.AreEqual(colors[row][column], effect[row, column]);
            }
        }

        [Test]
        public void ShouldThrowWhenConstructedWithInvalid1DSize()
        {
            var colors = new Color[1];

            Assert.That(
                () => new CustomMouseEffect(colors),
                Throws.InstanceOf<ArgumentException>().With.Property("ParamName").EqualTo("colors"));
        }

        [Test]
        public void ShouldConstructFrom1DArray()
        {
            var arr = new Color[MouseConstants.MaxLeds];

            arr[2] = Color.Pink;
            arr[4] = Color.Red;
            arr[8] = Color.Blue;

            var grid = new CustomMouseEffect(arr);

            for (var index = 0; index < MouseConstants.MaxLeds; index++)
                Assert.That(grid[index], Is.EqualTo(arr[index]));
        }

        [Test]
        public void ShouldConstructFromColor()
        {
            var effect = new CustomMouseEffect(Color.Red);

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = 0; column < MouseConstants.MaxColumns; column++)
                    Assert.AreEqual(Color.Red, effect[row, column]);
            }
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DGet()
        {
            var effect = CustomMouseEffect.Create();

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
                () => dummy = effect[MouseConstants.MaxRows, 0],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxRows));

            Assert.That(
                () => dummy = effect[0, -1],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => dummy = effect[0, MouseConstants.MaxColumns],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange2DSet()
        {
            var effect = CustomMouseEffect.Create();

            Assert.That(
                () => effect[-1, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => effect[MouseConstants.MaxRows, 0] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("row")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxRows));

            Assert.That(
                () => effect[0, -1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => effect[0, MouseConstants.MaxColumns] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("column")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxColumns));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DGet()
        {
            var grid = CustomMouseEffect.Create();

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
                () => dummy = grid[MouseConstants.MaxLeds],
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxLeds));
        }

        [Test]
        public void ShouldThrowWhenOutOfRange1DSet()
        {
            var grid = CustomMouseEffect.Create();

            Assert.That(
                () => grid[-1] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(-1));

            Assert.That(
                () => grid[MouseConstants.MaxLeds] = Color.Red,
                Throws.InstanceOf<ArgumentOutOfRangeException>()
                      .With.Property("ParamName")
                      .EqualTo("index")
                      .And.Property("ActualValue")
                      .EqualTo(MouseConstants.MaxLeds));
        }

        [Test]
        public void ShouldGetCorrectColorWithGridIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomMouseEffect(Color.Red)[3, 3]);
        }

        [Test]
        public void ShouldGetCorrectColorWithIndexIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomMouseEffect(Color.Red)[5]);
        }

        [Test]
        public void ShouldSetCorrectColorWithGridIndexer()
        {
            var effect = CustomMouseEffect.Create();
            effect[5, 5] = Color.Red;

            Assert.AreEqual(Color.Red, effect[5, 5]);
        }

        [Test]
        public void ShouldSetCorrectColorWithIndexIndexer()
        {
            var effect = CustomMouseEffect.Create();
            effect[5] = Color.Red;

            Assert.AreEqual(Color.Red, effect[5]);
        }

        [Test]
        public void ShouldGetCorrectColorWithLedIndexer()
        {
            Assert.AreEqual(Color.Red, new CustomMouseEffect(Color.Red)[GridLed.Logo]);
        }

        [Test]
        public void ShouldSetCorrectColorWithLedIndexer()
        {
            var effect = CustomMouseEffect.Create();
            effect[GridLed.Logo] = Color.Red;

            Assert.AreEqual(Color.Red, effect[GridLed.Logo]);
        }

        [Test]
        public void ShouldCreateWithAllBlackColors()
        {
            var effect = CustomMouseEffect.Create();

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = 0; column < MouseConstants.MaxColumns; column++)
                    Assert.AreEqual(Color.Black, effect[row, column]);
            }
        }

        [Test]
        public void ShouldSetCorrectColorWithSet()
        {
            var effect = CustomMouseEffect.Create();
            effect.Set(Color.Red);

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = 0; column < MouseConstants.MaxColumns; column++)
                    Assert.AreEqual(Color.Red, effect[row, column]);
            }
        }

        [Test]
        public void ShouldClearToBlack()
        {
            var effect = CustomMouseEffect.Create();
            effect.Set(Color.Red);
            effect.Clear();

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                for (var column = 0; column < MouseConstants.MaxColumns; column++)
                    Assert.AreEqual(Color.Black, effect[row, column]);
            }
        }

        [Test]
        public void ShouldEqualIdenticalEffect()
        {
            var a = new CustomMouseEffect(Color.Red);
            var b = new CustomMouseEffect(Color.Red);

            Assert.True(a == b);
            Assert.False(a != b);
            Assert.True(a.Equals(b));
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferentEffect()
        {
            var a = new CustomMouseEffect(Color.Red);
            var b = new CustomMouseEffect(Color.Blue);

            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a.Equals(b));
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualDifferent2DArray()
        {
            var effect = new CustomMouseEffect(Color.Red);

            var array = new Color[MouseConstants.MaxRows][];

            for (var row = 0; row < MouseConstants.MaxRows; row++)
            {
                array[row] = new Color[MouseConstants.MaxColumns];

                for (var column = 0; column < MouseConstants.MaxColumns; column++)
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
            var effect = CustomMouseEffect.Create();

            var array = new Color[1][];

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqual2DArrayWithInvalidColumnCount()
        {
            var effect = CustomMouseEffect.Create();

            var array = new Color[MouseConstants.MaxRows][];
            array[0] = new Color[1];

            Assert.False(effect == array);
            Assert.True(effect != array);
            Assert.False(effect.Equals(array));
            Assert.AreNotEqual(effect, array);
        }

        [Test]
        public void ShouldNotEqualDifferent1DArray()
        {
            var grid = new CustomMouseEffect(Color.Pink);
            var arr = new Color[MouseConstants.MaxLeds];

            for (var index = 0; index < MouseConstants.MaxLeds; index++)
                arr[index] = Color.Red;

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqual1DArrayWithInvalidSize()
        {
            var grid = CustomMouseEffect.Create();
            var arr = new Color[2];

            Assert.False(grid == arr);
            Assert.True(grid != arr);
            Assert.False(grid.Equals(arr));
            Assert.AreNotEqual(grid, arr);
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var effect = CustomMouseEffect.Create();
            var obj = new object();

            Assert.False(effect == obj);
            Assert.True(effect != obj);
            Assert.False(effect.Equals(obj));
            Assert.AreNotEqual(effect, obj);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var effect = default(CustomMouseEffect);

            Assert.False(effect == null);
            Assert.True(effect != null);
            Assert.False(effect.Equals(null));
            Assert.AreNotEqual(effect, null);
        }

        [Test]
        public void ClonedStructShouldBeIdentical()
        {
            var original = new CustomMouseEffect(Color.Red);
            var clone = original.Clone();

            Assert.That(clone, Is.EqualTo(original));
        }

        [Test]
        public void ClonedStructShouldBeIndependent()
        {
            var original = new CustomMouseEffect(Color.Red);
            var clone = original.Clone();

            clone.Set(Color.Blue);

            Assert.That(clone, Is.Not.EqualTo(original));
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            var effect = new CustomMouseEffect();
            Assert.Zero(effect.GetHashCode());
        }

        [Test]
        public void ShouldHaveDifferentHashCodesOnDifferentInstances()
        {
            var a = CustomMouseEffect.Create();
            var b = CustomMouseEffect.Create();
            Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }

        [Test]
        public void ShouldConstructProperMultiArray()
        {
            var effect = CustomMouseEffect.Create();
            effect[2, 2] = Color.Red;
            var array = effect.ToMultiArray();
            Assert.AreEqual(Color.Red, array[2, 2]);
        }
    }
}
