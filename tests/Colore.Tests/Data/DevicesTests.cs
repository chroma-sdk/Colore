// ---------------------------------------------------------------------------------------
// <copyright file="DevicesTests.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
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
    using System.Reflection;

    using Colore.Data;

    using NUnit.Framework;

    [TestFixture]
    public class DevicesTests
    {
        [Test]
        public void ShouldValidateAllDevices()
        {
            var type = typeof(Devices);
            var infos = type.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var info in infos)
            {
                var value = info.GetValue(null);
                Assert.NotNull(value);
                var guid = (Guid)value!;
                Assert.True(Devices.IsValidId(guid), $"Device {info.Name} should validate but does not");
            }
        }

        [Test]
        public void ShouldGetNameForValidDevice()
        {
            var name = Devices.GetName(Devices.Blackwidow);
            Assert.NotNull(name);
            Assert.IsNotEmpty(name);
        }

        [Test]
        public void ShouldThrowOnGettingNameForInvalidDevice()
        {
            Assert.Throws<ArgumentException>(() => Devices.GetName(Guid.Empty));
        }

        [Test]
        public void ShouldGetDescriptionForValidDevice()
        {
            var desc = Devices.GetDescription(Devices.Firefly);
            Assert.NotNull(desc);
            Assert.IsNotEmpty(desc);
        }

        [Test]
        public void ShouldThrowOnGettingDescriptionForInvalidDevice()
        {
            Assert.Throws<ArgumentException>(() => Devices.GetDescription(Guid.Empty));
        }
    }
}
