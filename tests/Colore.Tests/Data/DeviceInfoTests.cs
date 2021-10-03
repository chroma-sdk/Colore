// ---------------------------------------------------------------------------------------
// <copyright file="DeviceInfoTests.cs" company="Corale">
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

namespace Colore.Tests.Data
{
    using System;

    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class DeviceInfoTests
    {
        private static readonly SdkDeviceInfo BaseInfo = new SdkDeviceInfo(DeviceType.Keyboard, 1);

        private static readonly SdkDeviceInfo AltBaseInfo = new SdkDeviceInfo(DeviceType.Mouse, 0);

        private static readonly Devices.Metadata Metadata = new Devices.Metadata("Foo", "Bar");

        private static readonly Devices.Metadata AltMetadata = new Devices.Metadata("Alice", "Bob");

        [Test]
        public void ShouldConstructWithCorrectTypeFromBaseInfo()
        {
            var info = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreEqual(BaseInfo.Type, info.Type);
        }

        [Test]
        public void ShouldConstructWithCorrectStateFromBaseInfo()
        {
            var info = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreEqual(BaseInfo.Connected, info.Connected);
        }

        [Test]
        public void ShouldConstructWithCorrectId()
        {
            var id = Guid.NewGuid();
            var info = new DeviceInfo(BaseInfo, id, Metadata);
            Assert.AreEqual(id, info.Id);
        }

        [Test]
        public void ShouldConstructWithCorrectNameFromMetadata()
        {
            var info = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreEqual(Metadata.Name, info.Name);
        }

        [Test]
        public void ShouldConstructWithCorrectDescriptionFromMetadata()
        {
            var info = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreEqual(Metadata.Description, info.Description);
        }

        [Test]
        public void ShouldEqualOtherWithSameData()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentBaseInfo()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(AltBaseInfo, Guid.Empty, Metadata);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentId()
        {
            var a = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentMetadata()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, AltMetadata);
            Assert.AreNotEqual(a, b);
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingEqualOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.True(a == b);
        }

        [Test]
        public void ShouldEqualOtherWithSameDataUsingInequalOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.False(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentBaseInfoUsingEqualOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(AltBaseInfo, Guid.Empty, Metadata);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentBaseInfoUsingInequalOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(AltBaseInfo, Guid.Empty, Metadata);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentIdUsingEqualOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentIdUsingInequalOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentMetadataUsingEqualOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, AltMetadata);
            Assert.False(a == b);
        }

        [Test]
        public void ShouldNotEqualOtherWithDifferentMetadataUsingInequalOp()
        {
            var a = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            var b = new DeviceInfo(BaseInfo, Guid.Empty, AltMetadata);
            Assert.True(a != b);
        }

        [Test]
        public void ShouldNotEqualNull()
        {
            var info = new DeviceInfo(BaseInfo, Guid.NewGuid(), Metadata);
            Assert.False(info.Equals(null));
        }

        [Test]
        public void ShouldNotEqualArbitraryObject()
        {
            var info = new DeviceInfo(BaseInfo, Guid.Empty, Metadata);
            Assert.AreNotEqual(info, new object());
        }

        [Test]
        public void ShouldHaveEqualHashCodeOnDefaultInstances()
        {
            var a = new DeviceInfo();
            var b = new DeviceInfo();
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }
    }
}
