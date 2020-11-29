using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;
using Giselle.Drawing;

namespace Giselle.Forms
{
    public class FontManager : IDisposable
    {
        public string FontFamily { get; private set; }
        private readonly Dictionary<FontFormat, Font> Map;

        public Font this[FontFormat format]
        {
            get
            {
                return this.GetFont(format);
            }

        }

        public Font this[float size, FontStyle style]
        {
            get
            {
                return this.GetFont(size, style);
            }

        }

        public FontManager(string fontFamily)
        {
            this.FontFamily = fontFamily;
            this.Map = new Dictionary<FontFormat, Font>();
        }

        ~FontManager()
        {
            this.Dispose(false);
        }

        public Font GetFont(FontFormat format)
        {
            var map = this.Map;

            if (map.ContainsKey(format) == true)
            {
                return map[format];
            }
            else
            {
                var font = new Font(FontFamily, format.Size, format.Style);
                map[format] = font;
                return font;
            }

        }

        public Font GetFont(float size, FontStyle style)
        {
            return this.GetFont(new FontFormat(size, style));
        }

        public Font FindMatch(string text, FontMatchFormat format)
        {
            var proposedSize = format.ProposedSize;
            var size = format.Size;

            while (true)
            {
                if (size <= 1.0F)
                {
                    return this[1.0F, format.Style];
                }

                var font = this[size, format.Style];
                var textSize = TextRenderer.MeasureText(text, font, proposedSize, format.Flags);

                if (textSize.Height <= proposedSize.Height && textSize.Width <= proposedSize.Width)
                {
                    return font;
                }
                else
                {
                    size--;
                }

            }

        }

        public Font DeriveSize(Font font, float size)
        {
            return this[size, font.Style];
        }

        public Font DeriveSizeDelta(Font font, float amount)
        {
            return this[font.Size + amount, font.Style];
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                foreach (var font in this.Map.Values)
                {
                    font.DisposeQuietly();
                }

                this.Map.Clear();
            }

        }

    }

}
