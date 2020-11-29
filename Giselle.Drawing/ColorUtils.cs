using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Drawing
{
    public static class ColorUtils
    {
        public static Color DeriveAlpha(this Color color, double a)
        {
            return FromArgb((byte)(a * 255.0D), color.R, color.G, color.B);
        }

        public static Color DeriveAlpha(this Color color, byte a)
        {
            return FromArgb(a, color.R, color.G, color.B);
        }

        public static Color Blend(this Color dst, Color src)
        {
            var sa = (double)src.A / byte.MaxValue;
            var da = (double)dst.A / byte.MaxValue;
            var ra = 1 - (1 - sa) * (1 - da);

            var r = ((sa * src.R) + ((1 - sa) * da * dst.R)) / ra;
            var g = ((sa * src.G) + ((1 - sa) * da * dst.G)) / ra;
            var b = ((sa * src.B) + ((1 - sa) * da * dst.B)) / ra;

            var result = FromArgb((byte)(ra * 255.0D), (byte)r, (byte)g, (byte)b);
            return result;
        }

        public static Color FromArgb(this uint argb)
        {
            return Color.FromArgb((int)argb);
        }

        public static Color FromArgb(this int argb)
        {
            return Color.FromArgb(argb);
        }

        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return Color.FromArgb(a, r, g, b);
        }

        public static Color FromRgb(this uint rgb)
        {
            return FromArgb(0xFF000000 | rgb);
        }

        public static Color FromRgb(this int rgb)
        {
            return FromArgb(0xFF000000 | (uint)rgb);
        }

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return Color.FromArgb(r, g, b);
        }

    }

}
