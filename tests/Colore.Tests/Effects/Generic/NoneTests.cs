// ---------------------------------------------------------------------------------------
// <copyright file="NoneTests.cs" company="Corale">
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

namespace Colore.Tests.Effects.Generic;

using Colore.Effects.Generic;

using NUnit.Framework;

[TestFixture]
public class NoneTests
{
    [Test]
    public void ShouldConstructWithCorrectParameter()
    {
        Assert.That(new NoneEffect(42).Parameter, Is.EqualTo(42));
    }

    [Test]
    public void ShouldEqualOtherWithSameParameter()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(1);
        Assert.AreEqual(a, b);
    }

    [Test]
    public void ShouldNotEqualOtherWithDifferentParameter()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(2);
        Assert.AreNotEqual(a, b);
    }

    [Test]
    public void ShouldEqualOtherWithSameParameterUsingEqualOp()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(1);
        Assert.True(a == b);
    }

    [Test]
    public void ShouldEqualOtherWithSameParameterUsingInequalOp()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(1);
        Assert.False(a != b);
    }

    [Test]
    public void ShouldNotEqualOtherWithDifferentParameterUsingEqualOp()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(2);
        Assert.False(a == b);
    }

    [Test]
    public void ShouldNotEqualOtherWithDifferentParameterUsingInequalOp()
    {
        var a = new NoneEffect(1);
        var b = new NoneEffect(2);
        Assert.True(a != b);
    }

    [Test]
    public void ShouldHaveZeroHashCodeOnDefaultInstance()
    {
        Assert.Zero(new NoneEffect().GetHashCode());
    }

    [Test]
    public void ShouldNotEqualNull()
    {
        Assert.False(new NoneEffect(0).Equals(null));
    }

    [Test]
    public void ShouldNotEqualArbitraryObject()
    {
        Assert.False(new NoneEffect(0).Equals(new object()));
    }
}