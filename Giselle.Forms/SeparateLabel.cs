using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;

namespace Giselle.Forms
{
    public class SeparateLabel : Label
    {
        private bool _LineRender;
        public bool LineRender { get { return this._LineRender; } set { this._LineRender = value; this.OnLineRenderChanged(new EventArgs()); } }
        public event EventHandler LineRenderChanged;

        private Color _LineColor;
        public Color LineColor { get { return this._LineColor; } set { this._LineColor = value; this.OnLineColorChanged(new EventArgs()); } }
        public event EventHandler LineColorChanged;

        private float _LineWidth;
        public float LineWidth { get { return this._LineWidth; } set { this._LineWidth = value; this.OnLineWidthChanged(new EventArgs()); } }
        public event EventHandler LineWidthChanged;

        private Padding _LineMargin;
        public Padding LineMargin { get { return this._LineMargin; } set { this._LineMargin = value; this.OnLineMarginChanged(new EventArgs()); } }
        public event EventHandler LineMarginChanged;

        private Pen _LineCustomPen;
        public Pen LineCustomPen { get { return this._LineCustomPen; } set { this._LineCustomPen = value; this.OnLineCustomPenChanged(new EventArgs()); } }
        public event EventHandler LineCustomPenChanged;

        public SeparateLabel()
        {

        }

        protected virtual void DrawLine(PaintEventArgs e, Region[] regions)
        {
            if (this.LineRender == false)
            {
                return;
            }

            Pen tempPen = null;

            try
            {
                var pen = this.LineCustomPen;

                if (pen == null)
                {
                    pen = tempPen = new Pen(this.LineColor, this.LineWidth);
                }

                var g = e.Graphics;
                var rect = new RectangleF();

                foreach (var r in regions.Select(r => r.GetBounds(g)))
                {
                    rect = RectangleF.Union(rect, r);
                }

                var margin = this.LineMargin;

                var y = rect.Top + rect.Height / 2.0F - pen.Width;
                var left = rect.Left - margin.Left;
                var right = rect.Right + margin.Right;

                g.DrawLine(pen, rect.Left, y, left, y);
                g.DrawLine(pen, right, y, rect.Right, y);
            }
            finally
            {
                tempPen.DisposeQuietly();
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var rectangle = this.ClientRectangle.ToLayoutBounds(this.Padding);

            using (var format = ControlUtils.CreateStringFormat(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic, this.ShowKeyboardCues))
            {
                var regions = e.Graphics.MeasureCharacterRanges(this.Text, this.Font, new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), format);
                this.DrawLine(e, regions);
            }

        }

        private void OnLineRenderChanged(EventArgs e)
        {
            this.LineRenderChanged?.Invoke(this, e);
            this.Invalidate();
        }

        private void OnLineColorChanged(EventArgs e)
        {
            this.LineColorChanged?.Invoke(this, e);
            this.Invalidate();
        }

        private void OnLineWidthChanged(EventArgs e)
        {
            this.LineWidthChanged?.Invoke(this, e);
            this.Invalidate();
        }

        private void OnLineMarginChanged(EventArgs e)
        {
            this.LineMarginChanged?.Invoke(this, e);
            this.Invalidate();
        }

        private void OnLineCustomPenChanged(EventArgs e)
        {
            this.LineCustomPenChanged?.Invoke(this, e);

            this.Invalidate();
        }

    }

}
