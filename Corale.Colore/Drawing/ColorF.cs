using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corale.Colore.Drawing
{
    public class ColorF
    {

        private float a, r, g, b;

        /// <summary>
        ///     Construct a ARGB color.
        /// </summary>
        /// <param name="a">Alpha component.</param>
        /// <param name="r">Red component.</param>
        /// <param name="g">Green component.</param>
        /// <param name="b">Blue component.</param>
        public ColorF(float a, float r, float g, float b)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
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
            get { return a; }
            set
            {
                a = value;
                if (a > 1)
                {
                    a = 1;
                }
                else if (a < 0)
                {
                    a = 0;
                }
            }
        }

        /// <summary>
        ///     Red component.
        /// </summary>
        public float R
        {
            get { return r; }
            set
            {
                r = value;
                if (r > 1)
                {
                    r = 1;
                }
                else if (r < 0)
                {
                    r = 0;
                }
            }
        }

        /// <summary>
        ///     Green component.
        /// </summary>
        public float G
        {
            get { return g; }
            set
            {
                g = value;
                if (g > 1)
                {
                    g = 1;
                }
                else if (g < 0)
                {
                    g = 0;
                }
            }
        }

        /// <summary>
        ///     Blue component.
        /// </summary>
        public float B
        {
            get { return b; }
            set
            {
                b = value;
                if (b > 1)
                {
                    b = 1;
                }
                else if (b < 0)
                {
                    b = 0;
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

            float a = this.a * invBlend + blendTo.A * blend;
            float r = this.r * invBlend + blendTo.R * blend;
            float g = this.g * invBlend + blendTo.G * blend;
            float b = this.b * invBlend + blendTo.B * blend;

            return new ColorF(a, r, g, b);
        }

        /// <summary>
        ///     Get the inverse of this color.
        /// </summary>
        /// <param name="includeAlpha">Whether the alpha should be inversed.</param>
        /// <returns></returns>
        public ColorF Inverse(bool includeAlpha)
        {
            if(includeAlpha)
            {
                return new ColorF(1 - a, 1- r, 1 - g, 1 - b);
            }
            else
            {
                return new ColorF(a, 1 - r, 1 - g, 1 - b);
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
            return System.Drawing.Color.FromArgb((int) (a * 255), (int) (r * 255), (int) (g * 255), (int) (b * 255));
        }

        /// <summary>
        ///     Constructs a Corale.Colore.Core.Color from the RGB components.
        /// </summary>
        /// <returns>Corale.Colore.Core.Color identical to this color, minus the alpha.</returns>
        public Corale.Colore.Core.Color ToColor()
        {
            return new Corale.Colore.Core.Color(r, g, b);
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
