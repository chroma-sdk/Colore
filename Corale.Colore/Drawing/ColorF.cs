// ---------------------------------------------------------------------------------------
// <copyright file="ColorF.cs" company="Corale">
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

    /// <summary>
    ///     Color system for a easier and more mathimatical way of interacting with color.
    ///     This color system includes the alpha component (transparency).
    /// </summary>
    public struct ColorF
    {

        private float _a;
        private float _r;
        private float _g;
        private float _b;

        /// <summary>
        ///     Construct a ARGB color.
        /// </summary>
        /// <param name="a">Alpha component.</param>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        public ColorF(float a, float r, float g, float b)
        {
            this._a = a;
            this._r = r;
            this._g = g;
            this._b = b;
        }

        /// <summary>
        ///     Construct a RGB color where A = 1.
        /// </summary>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        public ColorF(float r, float g, float b)
            : this(1f, r, g, b)
        {
        }

        /// <summary>
        ///     Construct a ARGB color from another ColorF with a different alpha.
        ///     NOTE: This was created for chaining.
        /// </summary>
        /// <param name="a">Alpha component</param>
        /// <param name="color"></param>
        public ColorF(float a, ColorF color)
            : this(a, color.R, color.G, color.B)
        {
        }

        /// <summary>
        ///     Construct from System.Drawing.Color with specific alpha.
        /// </summary>
        /// <param name="a">Alpha component.</param>
        /// <param name="color"></param>
        public ColorF(float a, System.Drawing.Color color)
            : this(a, color.R / 255f, color.G / 255f, color.B / 255f)
        {
        }

        /// <summary>
        ///     Construct from System.Drawing.Color
        /// </summary>
        /// <param name="color"></param>
        public ColorF(System.Drawing.Color color)
            : this(color.A / 255f, color)
        {
        }

        /// <summary>
        ///     Construct from Corale.Colore.Core.Color with a specific alpha.
        /// </summary>
        /// <param name="a">Alpha component</param>
        /// <param name="color"></param>
        public ColorF(float a, Corale.Colore.Core.Color color)
            : this(a, color.R / 255f, color.G / 255f, color.B / 255f)
        {
        }

        /// <summary>
        ///     Construct from Corale.Colore.Core.Color.
        /// </summary>
        /// <param name="color"></param>
        public ColorF(Corale.Colore.Core.Color color)
            : this(1, color)
        {
        }

        /// <summary>
        ///     Alpha component.
        /// </summary>
        public float A
        {
            get { return _a; }
            set
            {
                _a = value;
                if (_a > 1)
                {
                    _a = 1;
                }
                else if (_a < 0)
                {
                    _a = 0;
                }
            }
        }

        /// <summary>
        ///     Red component.
        /// </summary>
        public float R
        {
            get { return _r; }
            set
            {
                _r = value;
                if (_r > 1)
                {
                    _r = 1;
                }
                else if (_r < 0)
                {
                    _r = 0;
                }
            }
        }

        /// <summary>
        ///     Green component.
        /// </summary>
        public float G
        {
            get { return _g; }
            set
            {
                _g = value;
                if (_g > 1)
                {
                    _g = 1;
                }
                else if (_g < 0)
                {
                    _g = 0;
                }
            }
        }

        /// <summary>
        ///     Blue component.
        /// </summary>
        public float B
        {
            get { return _b; }
            set
            {
                _b = value;
                if (_b > 1)
                {
                    _b = 1;
                }
                else if (_b < 0)
                {
                    _b = 0;
                }
            }
        }


        /// <summary>
        ///     Blend a color into this color by a factor of blend.
        /// </summary>
        /// <param name="blendTo">The color to blend with.</param>
        /// <param name="blend">A normalized value that tells the blender how much of the other color there is.</param>
        public ColorF Blend(ColorF blendTo, float blend)
        {
            float invBlend = 1 - blend;

            float a = this._a * invBlend + blendTo.A * blend;
            float r = this._r * invBlend + blendTo.R * blend;
            float g = this._g * invBlend + blendTo.G * blend;
            float b = this._b * invBlend + blendTo.B * blend;

            return new ColorF(a, r, g, b);
        }

        /// <summary>
        ///     Get the inverse of this color.
        /// </summary>
        /// <param name="includeAlpha">Whether the alpha should be inversed.</param>
        /// <returns></returns>
        public ColorF Inverse(bool includeAlpha)
        {
            if (includeAlpha)
            {
                return new ColorF(1 - _a, 1 - _r, 1 - _g, 1 - _b);
            }
            else
            {
                return new ColorF(_a, 1 - _r, 1 - _g, 1 - _b);
            }
        }

        /// <summary>
        ///     Get the inverse of the color. Alpha stays the same.
        /// </summary>
        /// <returns></returns>
        public ColorF Inverse()
        {
            return Inverse(false);
        }


        /// <summary>
        ///     Constructs a System.Drawing.Color from the ARGB components.
        /// </summary>
        /// <returns>System.Drawing.Color identical to this color.</returns>
        public System.Drawing.Color ToSystemColor()
        {
            return System.Drawing.Color.FromArgb((int)(_a * 255), (int)(_r * 255), (int)(_g * 255), (int)(_b * 255));
        }

        /// <summary>
        ///     Constructs a Corale.Colore.Core.Color from the RGB components.
        /// </summary>
        /// <returns>Corale.Colore.Core.Color identical to this color, minus the alpha.</returns>
        public Corale.Colore.Core.Color ToColor()
        {
            return new Corale.Colore.Core.Color(_r, _g, _b);
        }

        /// <summary>
        ///     Constructs a Corale.Colore.Core.Color from the RGB components
        ///     and blends it to the blendTo color with the alpha.
        /// </summary>
        /// <param name="blendTo">The color to blend with.</param>
        /// <returns>Corale.Colore.Core.Color identical to this color, blended with alpha.</returns>
        public Corale.Colore.Core.Color ToColor(Corale.Colore.Core.Color blendTo)
        {
            return new ColorF(blendTo).Blend(this, this.A).ToColor();
        }
    }
}
