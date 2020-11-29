using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Giselle.Drawing.Drawing
{
    public static class DrawingUtils
    {
        public static int DefaultMargin { get; set; } = 10;

        public static Point DeriveY(this Point point, int y)
        {
            return new Point(point.X, y);
        }

        public static PointF DeriveY(this PointF point, float y)
        {
            return new PointF(point.X, y);
        }

        public static Point DeriveX(this Point point, int x)
        {
            return new Point(x, point.Y);
        }

        public static PointF DeriveX(this PointF point, float x)
        {
            return new PointF(x, point.Y);
        }

        public static SizeF DeriveWidth(this Size size, float width)
        {
            return new SizeF(width, size.Height);
        }

        public static SizeF DeriveHeight(this Size size, float height)
        {
            return new SizeF(size.Width, height);
        }

        public static Size DeriveWidth(this Size size, int width)
        {
            return new Size(width, size.Height);
        }

        public static Size DeriveHeight(this Size size, int height)
        {
            return new Size(size.Width, height);
        }

        public static Point GetCenter(this Rectangle bounds)
        {
            var point = new Point();
            point.X = bounds.Left + bounds.Width / 2;
            point.Y = bounds.Top + bounds.Height / 2;

            return point;
        }

        public static Point InLeftTop(this Rectangle bounds, int width, int height)
        {
            var x = bounds.InLeft(width);
            var y = bounds.InTop(height);
            return new Point(x, y);
        }

        public static Point InLeftBottom(this Rectangle bounds, int width, int height)
        {
            var x = bounds.InLeft(width);
            var y = bounds.InBottom(height);
            return new Point(x, y);
        }

        public static Point InRightTop(this Rectangle bounds, int width, int height)
        {
            var x = bounds.InRight(width);
            var y = bounds.InTop(height);
            return new Point(x, y);
        }

        public static Point InRightBottom(this Rectangle bounds, int width, int height)
        {
            var x = bounds.InRight(width);
            var y = bounds.InBottom(height);
            return new Point(x, y);
        }

        public static Point InLeftTop(this Rectangle bounds, Size size)
        {
            return InLeftTop(bounds, size.Width, size.Height);
        }

        public static Point InLeftBottom(this Rectangle bounds, Size size)
        {
            return InLeftBottom(bounds, size.Width, size.Height);
        }

        public static Point InRightTop(this Rectangle bounds, Size size)
        {
            return InRightTop(bounds, size.Width, size.Height);
        }

        public static Point InRightBottom(this Rectangle bounds, Size size)
        {
            return InRightBottom(bounds, size.Width, size.Height);
        }

        public static int InRight(this Rectangle bounds, int width)
        {
            return bounds.Right - width;
        }

        public static int InLeft(this Rectangle bounds, int width)
        {
            return bounds.Left + width;
        }

        public static int InBottom(this Rectangle bounds, int height)
        {
            return bounds.Bottom - height;
        }

        public static int InTop(this Rectangle bounds, int height)
        {
            return bounds.Top + height;
        }

        public static Rectangle InLeftBounds(this Rectangle bounds, int width, int offset = 0)
        {
            bounds.X += offset;
            bounds.Width = width;
            return bounds;
        }

        public static Rectangle InRightBounds(this Rectangle bounds, int width, int offset = 0)
        {
            bounds.X = bounds.InRight(width + offset);
            bounds.Width = width;
            return bounds;
        }

        public static Rectangle InTopBounds(this Rectangle bounds, int height, int offset = 0)
        {
            bounds.Y += offset;
            bounds.Height = height;
            return bounds;
        }

        public static Rectangle InBottomBounds(this Rectangle bounds, int height, int offset = 0)
        {
            bounds.Y = bounds.InBottom(height + offset);
            bounds.Height = height;
            return bounds;
        }

        public static int OutRight(this Rectangle bounds, int width)
        {
            return bounds.Right + width;
        }

        public static int OutLeft(this Rectangle bounds, int width)
        {
            return bounds.Left - width;
        }

        public static int OutBottom(this Rectangle bounds, int height)
        {
            return bounds.Bottom + height;
        }

        public static int OutTop(this Rectangle bounds, int height)
        {
            return bounds.Top - height;
        }

        public static Rectangle OutRightBounds(this Rectangle bounds, int width, int offset = 0)
        {
            bounds.X = bounds.Right + offset;
            bounds.Width = width;
            return bounds;
        }

        public static Rectangle OutLeftBounds(this Rectangle bounds, int width, int offset = 0)
        {
            bounds.X = bounds.OutLeft(width + offset);
            bounds.Width = width;
            return bounds;
        }

        public static Rectangle OutBottomBounds(this Rectangle bounds, int height, int offset = 0)
        {
            bounds.Y = bounds.Bottom + offset;
            bounds.Height = height;
            return bounds;
        }

        public static Rectangle OutTopBounds(this Rectangle bounds, int height, int offset = 0)
        {
            bounds.Y = bounds.InTop(height + offset);
            bounds.Height = height;
            return bounds;
        }

        public static Rectangle DeriveLeft(this Rectangle bounds, int left)
        {
            bounds.X = left;
            return bounds;
        }

        public static Rectangle DeriveTop(this Rectangle bounds, int top)
        {
            bounds.Y = top;
            return bounds;
        }

        public static Rectangle DeriveWidth(this Rectangle bounds, int width)
        {
            bounds.Width = width;
            return bounds;
        }

        public static Rectangle DeriveHeight(this Rectangle bounds, int height)
        {
            bounds.Height = height;
            return bounds;
        }

        public static Rectangle DeriveSize(this Rectangle bounds, int width, int height)
        {
            bounds.Width = width;
            bounds.Height = height;
            return bounds;
        }

        public static Rectangle DeriveSize(this Rectangle bounds, Size size)
        {
            bounds.Size = size;
            return bounds;
        }

        public static Rectangle DeriveLocation(this Rectangle bounds, Point location)
        {
            bounds.Location = location;
            return bounds;
        }

        public static Point GetMiddleLocation(Rectangle bounds)
        {
            return new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2);
        }

        public static Point GetDirectionValue(PlaceDirection direction)
        {
            int v = (int)direction;
            int col = v % 3;
            int row = v / 3;

            return new Point(col, row);
        }

        public static double GetLevelValue(PlaceLevel level)
        {
            return ((int)level - 1) / 2.0D;
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction)
        {
            return PlaceByDirection(master, slave, direction, PlaceLevel.Zero);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, int margin)
        {
            return PlaceByDirection(master, slave, direction, PlaceLevel.Zero, margin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level)
        {
            return PlaceByDirection(master, slave, direction, level, DefaultMargin);
        }

        public static Point PlaceByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            var directionValue = GetDirectionValue(direction);
            var xs = new int[3] { master.Left - slave.Width - margin, master.Left, master.Right + margin };
            var ys = new int[3] { master.Top - slave.Height - margin, master.Top, master.Bottom + margin };
            var levelValue = GetLevelValue(level);

            var x = xs[directionValue.X] + (directionValue.X == 1 ? (int)((master.Width - slave.Width) * levelValue) : 0);
            var y = ys[directionValue.Y] + (directionValue.Y == 1 ? (int)((master.Height - slave.Height) * levelValue) : 0);

            return new Point(x, y);
        }

        public static Point InnerByDirection(Rectangle master, Size slave, PlaceDirection direction)
        {
            return InnerByDirection(master, slave, direction, PlaceLevel.Zero);
        }

        public static Point InnerByDirection(Rectangle master, Size slave, PlaceDirection direction, int margin)
        {
            return InnerByDirection(master, slave, direction, PlaceLevel.Zero, margin);
        }

        public static Point InnerByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level)
        {
            return InnerByDirection(master, slave, direction, level, DefaultMargin);
        }

        public static Point InnerByDirection(Rectangle master, Size slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            var directionValue = GetDirectionValue(direction);
            var xs = new int[3] { master.Right - slave.Width - margin, master.Left, master.Left + margin };
            var ys = new int[3] { master.Bottom - slave.Height - margin, master.Top, master.Top + margin };
            var levelValue = GetLevelValue(level);

            var x = xs[directionValue.X] + (directionValue.X == 1 ? (int)((master.Width - slave.Width) * levelValue) : 0);
            var y = ys[directionValue.Y] + (directionValue.Y == 1 ? (int)((master.Height - slave.Height) * levelValue) : 0);

            return new Point(x, y);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, xMargin, yMaster, yDirection, dm);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            int dm = DefaultMargin;
            return PlaceByReference(xMaster, xDirection, dm, yMaster, yDirection, yMargin);
        }

        public static Point PlaceByReference(Rectangle xMaster, PlaceDirection xDirection, int xMargin, Rectangle yMaster, PlaceDirection yDirection, int yMargin)
        {
            int x = 0;
            int y = 0;

            if (xDirection == PlaceDirection.Left)
            {
                x = xMaster.Left - xMargin;
            }
            else if (xDirection == PlaceDirection.Right)
            {
                x = xMaster.Right + xMargin;
            }
            else
            {
                throw new ArgumentException("xDirection must be Left or Right");
            }

            if (yDirection == PlaceDirection.Top)
            {
                y = yMaster.Top - yMargin;
            }
            else if (yDirection == PlaceDirection.Bottom)
            {
                y = yMaster.Bottom + yMargin;
            }
            else
            {
                throw new ArgumentException("yDirection must be Top or Bottom");
            }

            return new Point(x, y);
        }

        public static StringFormat CreateStringFormat(this ContentAlignment align)
        {
            var format = new StringFormat();
            var alignFactor = GetAlignmentFactor(align);
            format.Alignment = (StringAlignment)(alignFactor.X + 1);
            format.LineAlignment = (StringAlignment)(alignFactor.Y + 1);

            return format;
        }

        public static Point GetAlignmentFactor(ContentAlignment align)
        {
            int l2 = (int)Math.Log((int)align, 2);

            int xd = l2 % 4 - 1;
            int yd = l2 / 4 - 1;

            return new Point(xd, yd);
        }

        public static Point Alignment(Size controlSize, Rectangle backBounds, ContentAlignment align)
        {
            return Alignment(controlSize.Width, controlSize.Height, backBounds.Left, backBounds.Top, backBounds.Width, backBounds.Height, align);
        }
        public static Point Alignment(int controlWidth, int controlHeight, int backLeft, int backTop, int backWidth, int backHeight, ContentAlignment align)
        {
            Point point = Alignment(controlWidth, controlHeight, backWidth, backHeight, align);
            point.X += backLeft;
            point.Y += backTop;

            return point;
        }
        public static Point Alignment(Size controlSize, Size backSize, ContentAlignment align)
        {
            return Alignment(controlSize.Width, controlSize.Height, backSize.Width, backSize.Height, align);
        }
        public static Point Alignment(int controlWidth, int controlHeight, int backWidth, int backHeight, ContentAlignment align)
        {
            Point alignFactor = GetAlignmentFactor(align);

            Point point = new Point(0, 0);

            point.X = (int)((backWidth - controlWidth) * ((alignFactor.X + 1) / 2.0D));
            point.Y = (int)((backHeight - controlHeight) * ((alignFactor.Y + 1) / 2.0D));

            return point;
        }

    }

}
