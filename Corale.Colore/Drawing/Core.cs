// ---------------------------------------------------------------------------------------
// <copyright file="Core.cs" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
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
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Corale.Colore.Drawing
{
    using System;

    using Corale.Colore.Core;
    using Corale.Colore.Razer.Keyboard;
    using Corale.Colore.Razer.Keyboard.Effects;

    /// <summary>
    ///     Class for drawing a complex grid to the keyboard.
    /// </summary>
    public partial class ChromaGraphics
    {

        public const int LogoX = 20;
        public const int LogoY = 0;

        private Color[][] _grid;

        public ChromaGraphics()
        {
            // Create the proper grid.
            _grid = new Color[Constants.MaxRows][];
            for (int i = 0; i < _grid.Length; i++)
            {
                _grid[i] = new Color[Constants.MaxColumns];
                for (int j = 0; j < _grid[i].Length; j++)
                {
                    _grid[i][j] = Color.Black;
                }
            }
        }


        /// <summary>
        ///     Width of the graphics object.
        /// </summary>
        public int Width
        {
            get { return _grid[0].Length; }
        }

        /// <summary>
        ///     Height of the graphics object
        /// </summary>
        public int Height
        {
            get { return _grid.Length; }
        }



        /// <summary>
        ///     Get the color of any key on the keyboard.
        /// </summary>
        /// <param name="x">X position of the key.</param>
        /// <param name="y">Y position of the key</param>
        /// <returns>The color of the key at that position.</returns>
        public ColorF GetColor(int x, int y)
        {
            if ((x >= 0 && x < Width) && (y >= 0 && y < Height))
            {
                Color color = _grid[y][x];
                return new ColorF(color);
            }
            else
            {
                throw new ArgumentException("The x or y component is out of range.");
            }
        }

        /// <summary>
        ///     Set the color of any key. Cannot set the logo color with this method.
        /// </summary>
        /// <param name="x">X position of the key</param>
        /// <param name="y">Y position of the key</param>
        /// <param name="color">Color the key will be set to.</param>
        public void SetColor(int x, int y, ColorF color)
        {
            if ((x >= 0 && x < Width)
                && (y >= 0 && y < Height)
                && !(x == LogoX && y == LogoY)) // Do not override the logo with this method.
            {

                _grid[y][x] = color.ToColor(_grid[y][x]);
            }
        }

        /// <summary>
        ///     Set the color of the logo.
        /// </summary>
        /// <param name="color">Color the logo will be set to.</param>
        public void SetLogoColor(ColorF color)
        {
            _grid[LogoY][LogoX] = color.ToColor(_grid[LogoY][LogoX]);
        }



        /// <summary>
        ///     The grid effect that can be set on any keyboard.
        /// </summary>
        public CustomGrid Effect
        {
            get { return new CustomGrid(_grid); }
        }
    }
}
