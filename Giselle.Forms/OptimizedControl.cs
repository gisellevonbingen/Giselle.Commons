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
    public class OptimizedControl : Control
    {
        private readonly static FontManager _DefaultFontManager;
        public static FontManager DefaultFontManager { get; set; }

        public FontManager FontManager { get; private set; }

        public event EventHandler<PreferredBoundsingEventArgs> PreferredBoundsing;
        public event EventHandler<ControlCancelEventArgs> ChildValidating;
        public event EventHandler<ControlEventArgs> ChildValidated;

        static OptimizedControl()
        {
            _DefaultFontManager = new FontManager("맑은 고딕");
            DefaultFontManager = _DefaultFontManager;
        }

        public OptimizedControl()
        {
            this.SuspendLayout();

            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.MakeOptimization();
            this.FontManager = DefaultFontManager;
            this.Font = this.FontManager[11.0F, FontStyle.Regular];

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
            var layoutBounds = this.GetLayoutBounds();
            this.UpdateControlsBoundsPreferred(layoutBounds);
        }

        protected virtual void UpdateControlsBoundsPreferred(Rectangle layoutBounds)
        {
            var map = this.GetPreferredBounds(layoutBounds);

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

        protected virtual Rectangle GetLayoutBounds()
        {
            return this.GetLayoutBounds(this.ClientSize);
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

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.Focus();
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg == (int)WindowsMessage.WM_ERASEBKGND)
            {
                return;
            }

            base.OnNotifyMessage(m);
        }

    }

}