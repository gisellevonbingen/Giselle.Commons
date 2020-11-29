using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public static class ScreenUtils
    {
        public static Point GetInterpolatedLocation(Rectangle bounds)
        {
            return GetInterpolatedLocation(bounds.Location, bounds.Size);
        }

        public static Point GetInterpolatedLocation(Point point, Size size)
        {
            Rectangle workingArea = Screen.FromPoint(point).WorkingArea;

            if (workingArea.Left > point.X)
            {
                point.X = workingArea.Left;
            }

            if (workingArea.Right < point.X + size.Width)
            {
                point.X = workingArea.Right - size.Width;
            }

            if (workingArea.Top > point.Y)
            {
                point.Y = workingArea.Top;
            }

            if (workingArea.Bottom < point.Y + size.Height)
            {
                point.Y = workingArea.Bottom - size.Height;
            }

            return point;
        }

    }

}
