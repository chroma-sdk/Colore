using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corale.Colore.Drawing
{
    public class ChromaGraphics
    {
        /// <summary>
        ///     Clear the keyboard to a color.
        /// </summary>
        /// <param name="color">The color to clear to.</param>
        /// <param name="includeLogo">Whether to include the logo.</param>
        public void Clear(ColorF color, bool includeLogo)
        {
            FillRectangle(color, 0, 0, Width, Height);
            if (includeLogo)
            {
                SetLogoColor(color);
            }
        }

        /// <summary>
        ///     Clear the keyboard to a color without the logo.
        /// </summary>
        /// <param name="color">The color to clear to.</param>
        public void Clear(ColorF color)
        {
            Clear(color, false);
        }

        /// <summary>
        ///     Clear the keyboard to black.
        /// </summary>
        /// <param name="includeLogo"></param>
        public void Clear(bool includeLogo)
        {
            Clear(new ColorF(0, 0, 0), includeLogo);
        }

        /// <summary>
        ///     Clear the keyboard to black without the logo.
        /// </summary>
        public void Clear()
        {
            Clear(false);
        }



        /// <summary>
        ///     Average all the pixels in an image to get a color.
        /// </summary>
        /// <param name="image">Image to average</param>
        /// <returns>Average color found.</returns>
        public ColorF GetAverageColor(Image image)
        {
            int size = image.Width * image.Height;
            int a = 0;
            int r = 0;
            int g = 0;
            int b = 0;

            using (Bitmap map = new Bitmap(image))
            {
                for (int x = 0; x < map.Width; x++)
                {
                    for (int y = 0; y < map.Height; y++)
                    {
                        Color color = map.GetPixel(x, y);
                        a += color.A;
                        r += color.R;
                        g += color.G;
                        b += color.B;
                    }
                }
            }

            return new ColorF((a / 255f) / size, (r / 255f) / size, (g / 255f) / size, (b / 255f) / size);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="width">Width of the image to be drawn.</param>
        /// <param name="height">Height of the image to be drawn.</param>
        /// <param name="alpha">Alpha component all colors will be set to.</param>
        public void DrawImage(Image image, int x, int y, int width, int height, float alpha)
        {
            if (width == 0 || height == 0) return;

            using (Bitmap map = new Bitmap(image, width, height))
            {
                for (int xx = 0; xx < map.Width; xx++)
                {
                    for (int yy = 0; yy < map.Width; yy++)
                    {
                        SetColor(xx + x, yy + y, new ColorF(alpha, map.GetPixel(xx, yy)));
                    }
                }
            }
        }

        /// <summary>
        ///     Draws an image onto to the keyboard.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="width">Width of the image to be drawn.</param>
        /// <param name="height">Height of the image to be drawn.</param>
        /// <param name="alpha">Alpha component all colors will be set to.</param>
        public void DrawImage(Image image, float x, float y, float width, float height, float alpha)
        {
            int x1 = (int)Math.Round(x);
            int y1 = (int)Math.Round(y);
            int w = (int)Math.Round(width);
            int h = (int)Math.Round(height);

            DrawImage(image, x1, y1, w, h, alpha);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="width">Width of the image to be drawn.</param>
        /// <param name="height">Height of the image to be drawn.</param>
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            DrawImage(image, x, y, width, height, 1f);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="width">Width of the image to be drawn.</param>
        /// <param name="height">Height of the image to be drawn.</param>
        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            DrawImage(image, x, y, width, height, 1f);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard with original size.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="alpha">Alpha component all the colors will be set to.</param>
        public void DrawImage(Image image, int x, int y, float alpha)
        {
            DrawImage(image, x, y, image.Width, image.Height, alpha);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard with original size.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        /// <param name="alpha">Alpha component all the colors will be set to.</param>
        public void DrawImage(Image image, float x, float y, float alpha)
        {
            DrawImage(image, x, y, image.Width, image.Height, alpha);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard with original size.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        public void DrawImage(Image image, int x, int y)
        {
            DrawImage(image, x, y, 1f);
        }

        /// <summary>
        ///     Draws an image onto to the keyboard with original size.
        /// </summary>
        /// <param name="image">Image to be drawn.</param>
        /// <param name="x">X position of the image.</param>
        /// <param name="y">Y position of the image.</param>
        public void DrawImage(Image image, float x, float y)
        {
            int x1 = (int)Math.Round(x);
            int y1 = (int)Math.Round(y);

            DrawImage(image, x1, y1, 1f);
        }



        /// <summary>
        ///     Draw the outline of a rectangle.
        /// </summary>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public void DrawRectangle(ColorF color, int x, int y, int width, int height)
        {
            for (int xx = 0; xx < width; xx++)
            {
                for (int yy = 0; yy < height; yy++)
                {
                    if ((xx == 0 || xx == width - 1) || (yy == 0 || yy == height - 1))
                    {
                        SetColor(x + xx, y + yy, color);
                    }
                }
            }
        }

        /// <summary>
        ///     Draw the outline of a rectangle.
        /// </summary>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public void DrawRectangle(ColorF color, float x, float y, float width, float height)
        {
            int x1 = (int)Math.Round(x);
            int y1 = (int)Math.Round(y);
            int w = (int)Math.Round(width);
            int h = (int)Math.Round(height);

            this.DrawRectangle(color, x1, y1, w, h);
        }


        /// <summary>
        ///     Draw a filled rectangle.
        /// </summary>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public void FillRectangle(ColorF color, int x, int y, int width, int height)
        {
            for (int xx = 0; xx < width; xx++)
            {
                for (int yy = 0; yy < height; yy++)
                {
                    SetColor(x + xx, y + yy, color);
                }
            }
        }

        /// <summary>
        ///     Draw a filled rectangle.
        /// </summary>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="x">X position of the rectangle</param>
        /// <param name="y">Y position of the rectangle</param>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        public void FillRectangle(ColorF color, float x, float y, float width, float height)
        {
            int x1 = (int)Math.Round(x);
            int y1 = (int)Math.Round(y);
            int w = (int)Math.Round(width);
            int h = (int)Math.Round(height);

            this.FillRectangle(color, x1, y1, w, h);
        }

    }
}
