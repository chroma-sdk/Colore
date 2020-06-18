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
            var info = new SdkDeviceInfo(DeviceType.Headset, true);
            Assert.AreEqual(DeviceType.Headset, info.Type);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldConstructWithCorrectState(bool state)
        {
            var info = new SdkDeviceInfo(DeviceType.Speakers, state);
            Assert.AreEqual(state, info.Connected);
        }

        [Test]
        public void ShouldHaveZeroHashCodeOnDefaultInstance()
        {
            Assert.Zero(new SdkDeviceInfo().GetHashCode());
        }

        [Test]
        public void ShouldEqualOtherWithSameData()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, true);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentType()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, true);
            var b = new SdkDeviceInfo(DeviceType.Mouse, true);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentState()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, false);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var info = new SdkDeviceInfo(DeviceType.Keyboard, true);
            Assert.False(info.Equals(null));
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var info = new SdkDeviceInfo(DeviceType.Mouse, false);
            Assert.AreNotEqual(info, new object());
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, true);
            Assert.True(a == b);
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, true);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentTypeUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, true);
            var b = new SdkDeviceInfo(DeviceType.Mouse, true);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentTypeUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Mousepad, true);
            var b = new SdkDeviceInfo(DeviceType.Mouse, true);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentStateUsingEqualOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, false);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentStateUsingInequalOp()
        {
            var a = new SdkDeviceInfo(DeviceType.Keyboard, true);
            var b = new SdkDeviceInfo(DeviceType.Keyboard, false);
            Assert.True(a != b);
        }
    }
}
