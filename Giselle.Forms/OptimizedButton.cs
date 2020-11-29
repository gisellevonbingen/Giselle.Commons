using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Drawing;
using Giselle.Drawing.Drawing;

namespace Giselle.Forms
{
    public class OptimizedButton : OptimizedControl
    {
        public bool MouseDowned { get; private set; }

        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        public ContentAlignment TextAlign { get { return this._TextAlign; } set { this._TextAlign = value; this.OnTextAlignChanged(new EventArgs()); } }
        public event EventHandler TextAlignChanged;

        public OptimizedButton()
        {
            this.SuspendLayout();

            this.ResumeLayout(false);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            this.OnClick(e);
        }

        protected virtual void OnTextAlignChanged(EventArgs e)
        {
            this.TextAlignChanged?.Invoke(this, e);
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.MouseDowned = true;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.MouseDowned = false;
            this.Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            var bounds = this.GetLayoutBounds();
            var foreColor = this.ForeColor;
            var renderColor = this.Enabled ? foreColor : ColorUtils.Blend(foreColor, this.Parent.BackColor.DeriveAlpha(0.5D));

            using (var brush = new SolidBrush(renderColor))
            {
                var text = this.Text;
                var font = this.Font;
                var textSize = g.MeasureString(text, font);

                var renderFont = this.MouseDowned ? this.FontManager.DeriveSizeDelta(font, 1) : font;

                using (var format = this.TextAlign.CreateStringFormat())
                {
                    g.DrawString(text, renderFont, brush, bounds, format);
                }

            }

            var penWidth = this.MouseDowned ? 3 : this.Focused ? 2 : 1;

            using (var pen = new Pen(renderColor, penWidth))
            {
                pen.Alignment = PenAlignment.Inset;
                var lo = pen.Width % 2;
                g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - lo, bounds.Height - lo);
            }

        }

    }

}
