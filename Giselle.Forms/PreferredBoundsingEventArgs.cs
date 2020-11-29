using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class PreferredBoundsingEventArgs : EventArgs
    {
        public Rectangle LayoutBounds { get; private set; }
        public Dictionary<Control, Rectangle> Map { get; private set; }

        public PreferredBoundsingEventArgs(Rectangle layoutBounds, Dictionary<Control, Rectangle> map)
        {
            this.LayoutBounds = layoutBounds;
            this.Map = map;
        }

    }

}
