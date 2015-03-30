using Corale.Colore.Core;
using Corale.Colore.Razer.Keyboard;
using Corale.Colore.Razer.Keyboard.Effects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corale.Colore.Drawing
{
    public partial class ChromaGraphics
    {

        public const int LogoX = 20;
        public const int LogoY = 0;

        private Color[][] grid;

        public ChromaGraphics()
        {
            // Create the proper grid.
            grid = new Color[Constants.MaxRows][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new Color[Constants.MaxColumns];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    grid[i][j] = Color.Black;
                }
            }
        }


        /// <summary>
        ///     Width of the graphics object.
        /// </summary>
        public int Width
        {
            get { return grid[0].Length; }
        }

        /// <summary>
        ///     Height of the graphics object
        /// </summary>
        public int Height
        {
            get { return grid.Length; }
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
                Color color = grid[y][x];
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

                grid[y][x] = color.ToColor(grid[y][x]);
            }
        }

        /// <summary>
        ///     Set the color of the logo.
        /// </summary>
        /// <param name="color">Color the logo will be set to.</param>
        public void SetLogoColor(ColorF color)
        {
            grid[LogoY][LogoX] = color.ToColor(grid[LogoY][LogoX]);
        }



        /// <summary>
        ///     The grid effect that can be set on any keyboard.
        /// </summary>
        public CustomGrid Effect
        {
            get { return new CustomGrid(grid); }
        }

    }
}
