using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Drawing
{
    public static class DrawingUtils
    {
        public static int DefaultMargin { get; set; } = 10;

        public static Point GetMiddleLocation(Rectangle bounds)
        {
            return new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }

        public static Point GetDirectionValue(PlaceDirection direction)
        {
            int col = ((int)direction % (int)PlaceDirection.Left);
            int row = ((int)direction / (int)PlaceDirection.Left);
            return new Point(col, row);
        }

        public static double GetLevelValue(PlaceLevel level)
        {
            return (double)(level - PlaceLevel.Zero) / 2.0;
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction)
        {
            return DrawingUtils.PlaceByDirection(master, slave, direction, PlaceLevel.Zero);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, int margin)
        {
            return DrawingUtils.PlaceByDirection(master, slave, direction, PlaceLevel.Zero, margin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level)
        {
            return DrawingUtils.PlaceByDirection(master, slave, direction, level, DrawingUtils.DefaultMargin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            var directionValue = DrawingUtils.GetDirectionValue(direction);
            int[] xs = new int[]
            {
                master.Left - slave.Width - margin,
                master.Left,
                master.Right + margin
            };
            int[] ys = new int[]
            {
                master.Top - slave.Height - margin,
                master.Top,
                master.Bottom + margin
            };
            double levelValue = DrawingUtils.GetLevelValue(level);
            int x = xs[directionValue.X] + ((directionValue.X == 1) ? ((int)((double)(master.Width - slave.Width) * levelValue)) : 0);
            int y = ys[directionValue.Y] + ((directionValue.Y == 1) ? ((int)((double)(master.Height - slave.Height) * levelValue)) : 0);
            return new Point(x, y);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DrawingUtils.DefaultMargin;
            return DrawingUtils.PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DrawingUtils.DefaultMargin;
            return DrawingUtils.PlaceByReference(xMaster, xDirection, xMargin, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            int dm = DrawingUtils.DefaultMargin;
            return DrawingUtils.PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, yMargin);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            bool flag = xDirection == PlaceDirection.Left;
            int x;
            int y;

            if (flag)
            {
                x = xMaster.Left - xMargin;
            }
            else
            {
                bool flag2 = xDirection == PlaceDirection.Right;

                if (!flag2)
                {
                    throw new ArgumentException("xDirection must be Left or Right");
                }

                x = xMaster.Right + xMargin;
            }

            bool flag3 = yDirection == PlaceDirection.Top;

            if (flag3)
            {
                y = yMaster.Top - yMargin;
            }
            else
            {
                bool flag4 = yDirection == PlaceDirection.Bottom;

                if (!flag4)
                {
                    throw new ArgumentException("yDirection must be Top or Bottom");
                }

                y = yMaster.Bottom + yMargin;
            }

            return new Point(x, y);
        }

    }

}
