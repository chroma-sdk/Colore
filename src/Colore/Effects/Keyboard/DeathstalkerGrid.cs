// ---------------------------------------------------------------------------------------
// <copyright file="DeathstalkerGrid.cs" company="Corale">
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

namespace Colore.Effects.Keyboard
{
    using System;
    using System.Runtime.InteropServices;

    using Colore.Data;
    using Colore.Helpers;
    using Colore.Serialization;

    using JetBrains.Annotations;

    using Newtonsoft.Json;

    /// <inheritdoc cref="IEquatable{T}" />
    /// <summary>
    /// Describes a custom grid effect for every key.
    /// </summary>
    /// <remarks>
    /// This effect is only used for compatibility with the Razer Deathstalker Chroma keyboard.
    /// </remarks>
    [JsonConverter(typeof(DeathstalkerGridConverter))]
    [StructLayout(LayoutKind.Sequential)]
    public struct DeathstalkerGrid : IEquatable<DeathstalkerGrid>
    {
        /// <summary>
        /// Contains positions for each zone key.
        /// </summary>
        /// <remarks>
        /// <para>When a "zone key" is set, the entire zone for that key will light
        /// up in that color.</para>
        /// <para>
        /// Shoutout to antonpup at GitHub for posting the Deathstalker keymap
        /// which has since been removed from Razer's documentation.
        /// https://github.com/antonpup/Aurora/issues/286#issuecomment-269695154
        /// </para>
        /// </remarks>
        private static readonly (int Row, int Column)[] Zones =
        {
            (1, 1),
            (1, 4),
            (1, 8),
            (1, 12),
            (1, 15),
            (1, 18)
        };

        /// <summary>
        /// Color definitions for each key on the keyboard.
        /// </summary>
        /// <remarks>
        /// The array is 1-dimensional, but will be passed to code expecting
        /// a 2-dimensional array. Access to this array is done using indices
        /// according to: <c>column + row * KeyboardConstants.MaxColumns</c>
        /// </remarks>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = KeyboardConstants.MaxKeys)]
        private readonly Color[] _colors;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathstalkerGrid" /> struct
        /// with every position set to a specific color.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to set each position to.</param>
        public DeathstalkerGrid(Color color)
        {
            _colors = new Color[KeyboardConstants.MaxKeys];

            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
                _colors[index] = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathstalkerGrid" /> struct
        /// with the colors copied from another struct of the same type.
        /// </summary>
        /// <param name="other">The <see cref="KeyboardCustom" /> struct to copy data from.</param>
        public DeathstalkerGrid(DeathstalkerGrid other)
        {
            _colors = new Color[KeyboardConstants.MaxKeys];

            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                _colors[index] = other._colors[index];
            }
        }

        /// <summary>
        /// Gets or sets a position in the custom grid.
        /// </summary>
        /// <param name="index">The index to access, zero indexed.</param>
        /// <returns>The <see cref="Color" /> at the specified position.</returns>
        [PublicAPI]
        public Color this[int index]
        {
            get
            {
                if (index < 0 || index >= Zones.Length)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        index,
                        "Attempted to access an index that does not exist.");
                }

                var (row, column) = Zones[index];
                return _colors[column + row * KeyboardConstants.MaxColumns];
            }

            set
            {
                if (index < 0 || index >= Zones.Length)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(index),
                        index,
                        "Attempted to access an index that does not exist.");
                }

                var (row, column) = Zones[index];
                _colors[column + row * KeyboardConstants.MaxColumns] = value;
            }
        }

        /// <summary>
        /// Compares an instance of <see cref="DeathstalkerGrid" /> with
        /// another object for equality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="DeathstalkerGrid" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are equal, otherwise <c>false</c>.</returns>
        public static bool operator ==(DeathstalkerGrid left, object right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares an instance of <see cref="DeathstalkerGrid" /> with
        /// another object for inequality.
        /// </summary>
        /// <param name="left">The left operand, an instance of <see cref="DeathstalkerGrid" />.</param>
        /// <param name="right">The right operand, any type of object.</param>
        /// <returns><c>true</c> if the two objects are not equal, otherwise <c>false</c>.</returns>
        public static bool operator !=(DeathstalkerGrid left, object right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Creates a new empty <see cref="DeathstalkerGrid" /> struct.
        /// </summary>
        /// <returns>An instance of <see cref="DeathstalkerGrid" />
        /// filled with the color black.</returns>
        public static DeathstalkerGrid Create()
        {
            return new DeathstalkerGrid(Color.Black);
        }

        /// <summary>
        /// Returns a copy of this struct.
        /// </summary>
        /// <returns>A copy of this struct.</returns>
        [PublicAPI]
        public DeathstalkerGrid Clone()
        {
            return new DeathstalkerGrid(this);
        }

        /// <summary>
        /// Clears the colors from the grid, setting them to <see cref="Color.Black" />.
        /// </summary>
        public void Clear()
        {
            Set(Color.Black);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => _colors?.GetHashCode() ?? 0;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="obj"/> and this instance are of compatible types
        /// and represent the same value; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            return obj is DeathstalkerGrid custom && Equals(custom);
        }

        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">A <see cref="KeyboardCustom" /> to compare with this object.</param>
        public bool Equals(DeathstalkerGrid other)
        {
            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                if (_colors[index] != other._colors[index])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the entire grid to a specific <see cref="Color" />.
        /// </summary>
        /// <param name="color">The <see cref="Color" /> to apply.</param>
        [PublicAPI]
        public void Set(Color color)
        {
            for (var index = 0; index < KeyboardConstants.MaxKeys; index++)
            {
                _colors[index] = color;
            }
        }

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

        /// <summary>
        /// Retrieves the internal backing arrays as multi-dimensional <see cref="Color" /> arrays.
        /// </summary>
        /// <returns>A two-dimensional array of <see cref="Color" /> values.</returns>
        internal Color[,] ToMultiArray()
        {
            var colors = new Color[KeyboardConstants.MaxRows, KeyboardConstants.MaxColumns];

            _colors.CopyToMultidimensional(colors);

            return colors;
        }

#pragma warning restore CA1814 // Prefer jagged arrays over multidimensional
    }
}
