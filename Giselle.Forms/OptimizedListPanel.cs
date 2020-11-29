using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class OptimizedListPanel<T> : Panel where T : Control
    {
        private readonly List<T> _Items;
        private readonly List<T> VisibleItems;

        private int _ItemGap;
        public int ItemGap { get { return this._ItemGap; } set { this._ItemGap = value; this.OnItemGapChanged(); } }

        public OptimizedListPanel()
        {
            this.SuspendLayout();

            this._Items = new List<T>();
            this.VisibleItems = new List<T>();

            this.BackColor = this.BackColor;
            this.Font = OptimizedControl.DefaultFontManager[12.0F, FontStyle.Regular];
            this.VerticalScroll.Enabled = true;
            this.AutoScroll = true;
            this.Padding = new Padding(10, 0, 0, 0);
            this.ItemGap = 0;

            this.ResumeLayout(false);
        }

        public T[] Items { get { return this._Items.ToArray(); } }


        protected virtual void OnItemGapChanged()
        {
            this.LayoutItems();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is T item)
            {
                this._Items.Add(item);
                this.VisibleItems.Add(item);

                this.OnItemAdded(item);
            }

        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (e.Control is T item)
            {
                this._Items.Remove(item);
                this.VisibleItems.Remove(item);

                this.OnItemRemoved(item);
            }

        }

        protected virtual void OnItemAdded(T item)
        {
            this.LayoutItems();
        }

        protected virtual void OnItemRemoved(T item)
        {
            this.LayoutItems();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.LayoutItems();
        }

        public void LayoutItems()
        {
            var items = this.Items;
            var visibles = this.VisibleItems;
            var layoutBounds = this.ClientSize.ToLayoutBounds(this.Padding);
            var gap = this.ItemGap;

            var top = layoutBounds.Top;

            for (var i = 0; i < items.Length; i++)
            {
                var c = items[i];

                if (visibles.Contains(c) == true)
                {
                    c.Size = c.GetPreferredSize(layoutBounds.Size);
                    c.Location = new Point(layoutBounds.Left, top);

                    top = c.Bottom + gap;
                }

            }

        }

    }

}
