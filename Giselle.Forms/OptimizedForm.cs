using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;

namespace Giselle.Forms
{
    public class OptimizedForm : Form
    {
        public FontManager FontManager { get; private set; }

        public event EventHandler<PreferredBoundsingEventArgs> PreferredBoundsing;
        public event EventHandler<ControlCancelEventArgs> ChildValidating;
        public event EventHandler<ControlEventArgs> ChildValidated;

        public OptimizedForm()
        {
            this.SuspendLayout();

            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.KeyPreview = true;
            this.FontManager = OptimizedControl.DefaultFontManager;
            this.Font = this.FontManager[11.0F, FontStyle.Regular];

            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.MakeOptimization();

            this.Padding = new Padding(10);

            this.ResumeLayout(false);
        }

        protected virtual void OnChildValidating(object sender, CancelEventArgs e)
        {
            var e2 = new ControlCancelEventArgs(sender as Control, e.Cancel);
            this.ChildValidating?.Invoke(this, e2);
            e.Cancel = e2.Cancel;
        }

        protected virtual void OnChildValidated(object sender, EventArgs e)
        {
            this.ChildValidated?.Invoke(this, new ControlEventArgs(sender as Control));
        }

        private void OnControlAdded(object sender, ControlEventArgs e)
        {
            var list = new List<Control>() { e.Control };
            list.AddRange(e.Control.Controls.OfType<Control>());

            foreach (var control in list)
            {
                control.ControlAdded += this.OnControlAdded;
                control.ControlRemoved += this.OnControlRemoved;
                control.Validating += this.OnChildValidating;
                control.Validated += this.OnChildValidated;
            }

        }

        private void OnControlRemoved(object sender, ControlEventArgs e)
        {
            var list = new List<Control>() { e.Control };
            list.AddRange(e.Control.Controls.OfType<Control>());

            foreach (var control in list)
            {
                control.ControlAdded -= this.OnControlAdded;
                control.ControlRemoved -= this.OnControlRemoved;
                control.Validating -= this.OnChildValidating;
                control.Validated -= this.OnChildValidated;
            }

        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            this.OnControlAdded(this, e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            this.OnControlRemoved(this, e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.OnKeyEscpace();
                return true;
            }
            else if (keyData == Keys.Return)
            {
                this.OnKeyReturn();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void OnKeyEscpace()
        {
            if (this.Owner != null)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }

        protected virtual void OnKeyReturn()
        {

        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            var layoutBounds = this.GetLayoutBounds(proposedSize);
            var map = this.GetPreferredBounds(layoutBounds);
            var width = 0;
            var height = 0;

            foreach (var bounds in map.Values)
            {
                width = Math.Max(width, bounds.Right);
                height = Math.Max(height, bounds.Bottom);
            }

            var offset = this.GetPreferredSizeOffset();
            width += offset.Width;
            height += offset.Height;

            var padding = this.Padding;
            width = Math.Max(width, padding.Horizontal);
            height = Math.Max(height, padding.Vertical);

            return new Size(width, height);
        }

        protected virtual Size GetPreferredSizeOffset()
        {
            var padding = this.Padding;
            return new Size(padding.Right, padding.Bottom);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.UpdateControlsBoundsPreferred();
        }

        protected virtual void UpdateControlsBoundsPreferred()
        {
            this.UpdateControlsBoundsPreferred(this.ClientSize);
        }

        public virtual Dictionary<Control, Rectangle> GetPreferredBounds()
        {
            return this.GetPreferredBounds(this.ClientSize);
        }

        protected virtual Rectangle GetLayoutBounds()
        {
            return this.GetLayoutBounds(this.ClientSize);
        }

        protected virtual void UpdateControlsBoundsPreferred(Size size)
        {
            var map = this.GetPreferredBounds(size);

            foreach (var pair in map)
            {
                var control = pair.Key;
                var bounds = pair.Value;

                if (control != null)
                {
                    control.Bounds = bounds;
                }

            }

        }

        public virtual Dictionary<Control, Rectangle> GetPreferredBounds(Size size)
        {
            var layoutBounds = this.GetLayoutBounds(size);
            var map = this.GetPreferredBounds(layoutBounds);

            return map;
        }

        protected virtual Rectangle GetLayoutBounds(Size size)
        {
            return size.ToLayoutBounds(this.Padding);
        }

        protected virtual Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = new Dictionary<Control, Rectangle>();

            this.OnPreferredBoundsing(new PreferredBoundsingEventArgs(layoutBounds, map));

            return map;
        }

        protected virtual void OnPreferredBoundsing(PreferredBoundsingEventArgs e)
        {
            this.PreferredBoundsing?.Invoke(this, e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                foreach (var control in this.Controls.OfType<Control>().ToArray())
                {
                    control.DisposeQuietly();
                }

                this.Controls.Clear();
            }

        }

    }

}
