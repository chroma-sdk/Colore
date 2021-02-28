// ---------------------------------------------------------------------------------------
// <copyright file="SdkDeviceInfoTests.cs" company="Corale">
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

namespace Colore.Tests.Data
{
    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class SdkDeviceInfoTests
    {
        [Test]
        public void ShouldConstructWithCorrectType()
        {
            var info = new SdkDeviceInfo(DeviceType.Headset, 1);
            Assert.AreEqual(DeviceType.Headset, info.Type);
        }

        [TestCase(5U)]
        [TestCase(1U)]
        [TestCase(0U)]
        public void ShouldConstructWithCorrectState(uint state)
        {
            var info = new SdkDeviceInfo(DeviceType.Speakers, state);
            Assert.AreEqual(state, info.ConnectedCount);
        }

        [TestCase(1U)]
        [TestCase(5U)]
        [TestCase(42U)]
        public void ShouldBeConnectedWhenCountPositive(uint count)
        {
            var info = new SdkDeviceInfo(DeviceType.Speakers, count);
            Assert.True(info.Connected);
        }

        [Test]
        public void ShouldNotBeConnectedWhenCountZero()
        {
            var info = new SdkDeviceInfo(DeviceType.Speakers, 0);
            Assert.False(info.Connected);
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            Assert.Zero(new SdkDeviceInfo().GetHashCode());
        }

        [Test]
        public void ShouldEqualOtherWithSameData()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 5);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 5);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentType()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, 1);
            var b = new SdkDeviceInfo(DeviceType.Mouse, 1);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentState()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 5);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 0);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var info = new SdkDeviceInfo(DeviceType.Keyboard, 1);
            Assert.False(info.Equals(null));
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var info = new SdkDeviceInfo(DeviceType.Mouse, 0);
            Assert.AreNotEqual(info, new object());
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 1);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 1);
            Assert.True(a == b);
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 2);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 2);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentTypeUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, 3);
            var b = new SdkDeviceInfo(DeviceType.Mouse, 3);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentTypeUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, 4);
            var b = new SdkDeviceInfo(DeviceType.Mouse, 4);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentStateUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 2);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 0);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentStateUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, 3);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, 0);
            Assert.True(a != b);
        }
    }
}
