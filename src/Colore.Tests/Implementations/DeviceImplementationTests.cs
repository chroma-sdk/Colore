// ---------------------------------------------------------------------------------------
// <copyright file="DeviceImplementationTests.cs" company="Corale">
//     Copyright © 2015-2019 by Adam Hellberg and Brandon Scott.
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

namespace Colore.Tests.Implementations
{
    using System;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Implementations;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class DeviceImplementationTests
    {
        private Mock<IChromaApi> _api;

        private Mock<DeviceImplementation> _deviceMock;

        private DeviceImplementation _device;

        [SetUp]
        public void Setup()
        {
            _api = new Mock<IChromaApi>();
            _deviceMock = new Mock<DeviceImplementation>(MockBehavior.Strict, _api.Object);
            _device = _deviceMock.Object;
        }

        [Test]
        public async Task ShouldSetCurrentEffectIdOnSetEffect()
        {
            var guid = Guid.NewGuid();

            await _device.SetEffectAsync(guid);

            Assert.AreEqual(guid, _device.CurrentEffectId);
        }

        [Test]
        public async Task ShouldCallSetEffectApiOnSetEffect()
        {
            var guid = Guid.NewGuid();

            await _device.SetEffectAsync(guid);

            _api.Verify(a => a.SetEffectAsync(guid), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallDeleteEffectOnFirstSetEffect()
        {
            await _device.SetEffectAsync(Guid.Empty);
            _api.Verify(a => a.DeleteEffectAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public async Task ShouldCallDeleteEffectIfPreviousEffectSet()
        {
            var guid = Guid.NewGuid();
            await _device.SetEffectAsync(guid);
            await _device.SetEffectAsync(Guid.NewGuid());

            _api.Verify(a => a.DeleteEffectAsync(guid), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallDeleteEffectOnDeleteWithNoEffect()
        {
            await _device.DeleteCurrentEffectAsync();
            _api.Verify(a => a.DeleteEffectAsync(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public async Task ShouldCallDeleteEffectWithCorrectIdOnDelete()
        {
            var guid = Guid.NewGuid();
            await _device.SetEffectAsync(guid);
            await _device.DeleteCurrentEffectAsync();

            _api.Verify(a => a.DeleteEffectAsync(guid), Times.Once);
        }

        [Test]
        public async Task ShouldResetEffectIdOnDelete()
        {
            await _device.SetEffectAsync(Guid.NewGuid());
            await _device.DeleteCurrentEffectAsync();

            Assert.AreEqual(Guid.Empty, _device.CurrentEffectId);
        }

        [Test]
        public async Task ShouldSetNewEffectIdIfExistingEffect()
        {
            await _device.SetEffectAsync(Guid.NewGuid());
            var guid = Guid.NewGuid();
            await _device.SetEffectAsync(guid);

            Assert.AreEqual(guid, _device.CurrentEffectId);
        }
    }
}
