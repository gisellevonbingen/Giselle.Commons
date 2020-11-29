using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class OptimizedGroupBox : OptimizedControl
    {
        public OptimizedGroupBox()
        {
            this.SuspendLayout();

            this.Padding = new Padding(10, 22, 10, 10);

            this.ResumeLayout(false);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            var size = base.GetPreferredSize(proposedSize);
            var padding = this.Padding;
            var textSize = TextRenderer.MeasureText(this.Text, this.Font);

            size.Width = Math.Max(size.Width, padding.Horizontal + textSize.Width);
            size.Height = Math.Max(size.Height, padding.Vertical + textSize.Height);

            return size;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var text = this.Text;
            var font = this.Font;
            var size = this.ClientSize;
            var g = e.Graphics;
            var textSize = g.MeasureString(text, font);
            var textLocation = new PointF(10, 0);
            var textBounds = new RectangleF(textLocation, textSize);

            using (var brush = new SolidBrush(Color.Black))
            {
                g.DrawString(text, font, brush, textBounds);
            }

            using (var pen = new Pen(Color.Black, 1))
            {
                int lo = 1;
                var lineTop = textSize.Height / 2;
                var lineOutBounds = RectangleF.FromLTRB(0, lineTop, size.Width - lo, size.Height - lo);

                var points = new List<PointF>();
                points.Add(new PointF(textBounds.Left, lineOutBounds.Top));
                points.Add(new PointF(lineOutBounds.Left, lineOutBounds.Top));
                points.Add(new PointF(lineOutBounds.Left, lineOutBounds.Bottom));
                points.Add(new PointF(lineOutBounds.Right, lineOutBounds.Bottom));
                points.Add(new PointF(lineOutBounds.Right, lineOutBounds.Top));
                points.Add(new PointF(textBounds.Right, lineOutBounds.Top));
                g.DrawLines(pen, points.ToArray());
            }

        }

    }

}
