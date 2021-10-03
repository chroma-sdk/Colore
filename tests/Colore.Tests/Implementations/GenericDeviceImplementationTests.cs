// ---------------------------------------------------------------------------------------
// <copyright file="GenericDeviceImplementationTests.cs" company="Corale">
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

namespace Colore.Tests.Implementations
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Colore.Api;
    using Colore.Data;
    using Colore.Effects.Generic;
    using Colore.Implementations;

    using Moq;

    using NUnit.Framework;

    [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
    public class GenericDeviceImplementationTests
    {
        // Safe to suppress null warning here since the `Setup` method acts
        // like a constructor.
        private Mock<IChromaApi> _api = null!;

        [SetUp]
        public void Setup()
        {
            _api = new Mock<IChromaApi>();
        }

        [Test]
        public void ShouldConstructWithCorrectDeviceIdWhenValid()
        {
            var deviceId = Devices.BladeStealth;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            Assert.AreEqual(deviceId, device.DeviceId);
        }

        [Test]
        public void ShouldThrowWhenConstructedWithInvalidDeviceId()
        {
            Assert.Throws<UnsupportedDeviceException>(() => new GenericDeviceImplementation(Guid.Empty, _api.Object));
        }

        [Test]
        public void ShouldThrowWhenConstructedWithNullApi()
        {
            Assert.Throws<ArgumentNullException>(() => new GenericDeviceImplementation(Devices.Core, null!));
        }

        [Test]
        public async Task ShouldCallEffectApiWithCorrectDeviceIdOnClear()
        {
            var deviceId = Devices.BlackwidowTe;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);
            await device.ClearAsync();

            _api.Verify(
                a => a.CreateDeviceEffectAsync(deviceId, It.IsAny<EffectType>(), It.IsAny<NoneEffect>()),
                Times.Once);
        }

        [Test]
        public async Task ShouldCallEffectApiWithCorrectEffectOnClear()
        {
            var deviceId = Devices.BlackwidowXTe;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);
            await device.ClearAsync();

            _api.Verify(a => a.CreateDeviceEffectAsync(It.IsAny<Guid>(), EffectType.None, It.IsAny<NoneEffect>()), Times.Once);
        }

        [Test]
        public async Task ShouldCallEffectApiWithCorrectDataOnClear()
        {
            var deviceId = Devices.Firefly;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);
            await device.ClearAsync();

            _api.Verify(a => a.CreateDeviceEffectAsync(It.IsAny<Guid>(), It.IsAny<EffectType>(), default(NoneEffect)));
        }

        [Test]
        public async Task ShouldReturnEffectIdOnClear()
        {
            var deviceId = Devices.Deathadder;
            var effectId = Guid.NewGuid();
            _api.Setup(a => a.CreateDeviceEffectAsync(deviceId, EffectType.None, default(NoneEffect)))
                .ReturnsAsync(effectId);

            var device = new GenericDeviceImplementation(deviceId, _api.Object);
            var setEffectId = await device.ClearAsync();

            Assert.AreEqual(effectId, setEffectId);
        }

        [Test]
        public void ShouldThrowOnSetAll()
        {
            var device = new GenericDeviceImplementation(Devices.Blade14, _api.Object);
            Assert.ThrowsAsync<NotSupportedException>(() => device.SetAllAsync(Color.Red));
        }

        [Test]
        public async Task ShouldCreateEffectWithCorrectDeviceIdOnSetEffect()
        {
            var deviceId = Devices.Tartarus;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            await device.SetEffectAsync(EffectType.None, default(NoneEffect));

            _api.Verify(a => a.CreateDeviceEffectAsync(deviceId, It.IsAny<EffectType>(), It.IsAny<NoneEffect>()), Times.Once);
        }

        [Test]
        public async Task ShouldCreateEffectWithCorrectEffectOnSetEffect()
        {
            var deviceId = Devices.Tartarus;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            await device.SetEffectAsync(EffectType.None, default(NoneEffect));

            _api.Verify(a => a.CreateDeviceEffectAsync(It.IsAny<Guid>(), EffectType.None, It.IsAny<NoneEffect>()), Times.Once);
        }

        [Test]
        public async Task ShouldCreateEffectWithCorrectDataOnSetEffect()
        {
            var deviceId = Devices.Tartarus;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            await device.SetEffectAsync(EffectType.None, default(NoneEffect));

            _api.Verify(
                a => a.CreateDeviceEffectAsync(It.IsAny<Guid>(), It.IsAny<EffectType>(), default(NoneEffect)),
                Times.Once);
        }

        [Test]
        public async Task ShouldReturnCorrectEffectIdOnSetEffect()
        {
            var deviceId = Devices.Orochi;
            var effectId = Guid.NewGuid();
            var device = new GenericDeviceImplementation(deviceId, _api.Object);
            _api.Setup(a => a.CreateDeviceEffectAsync(deviceId, EffectType.None, default(NoneEffect))).ReturnsAsync(effectId);

            var setEffectId = await device.SetEffectAsync(EffectType.None, default(NoneEffect));

            Assert.AreEqual(effectId, setEffectId);
        }

        [Test]
        public async Task SetEffectOverloadShouldCreateEffectWithZeroData()
        {
            var deviceId = Devices.BlackwidowTe;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            await device.SetEffectAsync(EffectType.None);

            _api.Verify(a => a.CreateDeviceEffectAsync(deviceId, EffectType.None, IntPtr.Zero), Times.Once);
        }

        [Test]
        public async Task SetEffectOverloadShouldCreateEffectWithCorrectEffect()
        {
            var deviceId = Devices.Diamondback;
            var device = new GenericDeviceImplementation(deviceId, _api.Object);

            await device.SetEffectAsync(EffectType.Reserved);

            _api.Verify(a => a.CreateDeviceEffectAsync(deviceId, EffectType.Reserved, IntPtr.Zero), Times.Once);
        }
    }
}
