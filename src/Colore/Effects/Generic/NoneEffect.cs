// ---------------------------------------------------------------------------------------
// <copyright file="NoneEffect.cs" company="Corale">
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
#pragma warning disable CA1051 // Do not declare visible instance fields

namespace Colore.Effects.Generic
{
    using System;
    using System.Runtime.InteropServices;

    using JetBrains.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Describes the <c>NO_EFFECT</c> effect.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NoneEffect : IEquatable<NoneEffect>
    {
        /// <summary>
        /// The size of the struct.
        /// </summary>
        [PublicAPI]
        public readonly int Size;

        /// <summary>
        /// Additional effect parameter.
        /// </summary>
        [PublicAPI]
        public readonly int Parameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoneEffect" /> struct.
        /// </summary>
        /// <param name="parameter">Additional effect parameter to set.</param>
        public NoneEffect(int parameter = 0)
        {
            Parameter = parameter;
            Size = Marshal.SizeOf<NoneEffect>();
        }

        /// <summary>
        /// Checks an instance of <see cref="NoneEffect" /> for equality with another <see cref="NoneEffect" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(NoneEffect left, NoneEffect right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks an instance of <see cref="NoneEffect" /> for inequality with another <see cref="NoneEffect" /> instance.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns><c>true</c> if the two instances are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(NoneEffect left, NoneEffect right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(NoneEffect other)
        {
            return Size == other.Size && Parameter == other.Parameter;
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            return obj is NoneEffect none && Equals(none);
        }

        /// <inheritdoc />
        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Size * 397) ^ Parameter;
            }
        }
    }
}
