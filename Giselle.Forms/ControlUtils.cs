using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons.Collections;
using Giselle.Drawing;
using Giselle.Drawing.Drawing;

namespace Giselle.Forms
{
    public static class ControlUtils
    {
        private static readonly MethodInfo SetStyleMethod;
        private static readonly Action<Control, ControlStyles, bool> SetStyleAction;

        private static readonly ContentAlignment anyRight = (ContentAlignment)1092;
        private static readonly ContentAlignment anyBottom = (ContentAlignment)1792;
        private static readonly ContentAlignment anyCenter = (ContentAlignment)546;
        private static readonly ContentAlignment anyMiddle = (ContentAlignment)112;

        public static int DefaultMargin = 10;

        static ControlUtils()
        {
            SetStyleMethod = typeof(Control).GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            SetStyleAction = (Action<Control, ControlStyles, bool>)SetStyleMethod.CreateDelegate(typeof(Action<Control, ControlStyles, bool>));
        }

        public static void FitFontSize(this Control control, float max)
        {
            control.FitFontSize(max, control.Text);
        }

        public static void FitFontSize(this Control control, float max, string text)
        {
            var format = new FontMatchFormat();
            format.Size = max;
            format.ProposedSize = control.Size;
            control.Font = OptimizedControl.DefaultFontManager.FindMatch(text, format);
        }

        public static Rectangle ToLayoutBounds(this Size size, Padding padding)
        {
            return new Rectangle(padding.Left, padding.Top, size.Width - padding.Horizontal, size.Height - padding.Vertical);
        }

        public static Rectangle ToLayoutBounds(this Rectangle bounds, Padding padding)
        {
            return new Rectangle(bounds.Left + padding.Left, bounds.Top + padding.Top, bounds.Width - padding.Horizontal, bounds.Height - padding.Vertical);
        }

        public static int GetAlignmentFactor(HorizontalAlignment align)
        {
            int result = 0;

            switch (align)
            {
                case HorizontalAlignment.Left: result = -1; break;
                case HorizontalAlignment.Center: result = 0; break;
                case HorizontalAlignment.Right: result = 1; break;
            }

            return result;
        }

        public static int Alignment(Size controlSize, Rectangle backBounds, HorizontalAlignment align)
        {
            int alignValue = Alignment(controlSize, backBounds.Size, align);
            alignValue += backBounds.X;

            return alignValue;
        }
        public static int Alignment(Size controlSize, Size backSize, HorizontalAlignment align)
        {
            int alignFactor = GetAlignmentFactor(align);

            int alignValue = (int)((backSize.Width - controlSize.Width) * ((alignFactor + 1) / 2.0D));

            return alignValue;
        }

        public static Rectangle UnionBounds(this Dictionary<Control, Rectangle> map)
        {
            var bounds = new Rectangle();
            var first = true;

            foreach (var pair in map)
            {
                if (first == true)
                {
                    first = false;
                    bounds = pair.Value;
                }
                else
                {
                    bounds = Rectangle.Union(bounds, pair.Value);
                }

            }

            return bounds;
        }

        public static Dictionary<Control, Rectangle> LayoutArray(Rectangle layoutBounds, ContentAlignment anchor, PlaceDirection direction, PlaceLevel level, int margin, IEnumerable<Control> collection)
        {
            return LayoutArray(layoutBounds, anchor, direction, level, margin, collection.Select(c => new KeyValuePair<Control, Size>(c, c.PreferredSize)));
        }

        public static Dictionary<Control, Rectangle> LayoutArray(Rectangle layoutBounds, ContentAlignment anchor, PlaceDirection direction, PlaceLevel level, int margin, IEnumerable<KeyValuePair<Control, Size>> collection)
        {
            var map = new Dictionary<Control, Rectangle>();
            var array = collection.ToArray();

            if (array.Length > 0)
            {
                var maxWidth = array.Max(c => c.Value.Width);
                var maxHeight = array.Max(c => c.Value.Height);
                var maxSize = new Size(maxWidth, maxHeight);

                var first = true;
                var lastBounds = new Rectangle();

                foreach (var pair in array)
                {
                    var control = pair.Key;
                    var size = pair.Value;
                    var newBounds = new Rectangle();

                    if (first == true)
                    {
                        first = false;
                        var directionValue = DrawingUtils.GetDirectionValue(direction) - new Size(1, 1);
                        var s = new Size(directionValue.X != 0 ? size.Width : maxSize.Width, directionValue.Y != 0 ? size.Height : maxSize.Height);
                        var initial = new Rectangle(DrawingUtils.Alignment(s, layoutBounds, anchor), s);
                        newBounds = new Rectangle(DrawingUtils.InnerByDirection(initial, size, direction, level, 0), size);
                    }
                    else
                    {
                        newBounds = new Rectangle(DrawingUtils.PlaceByDirection(lastBounds, size, direction, level, margin), size);
                    }

                    map[control] = newBounds;
                    lastBounds = newBounds;
                }

            }

            return map;
        }

        public static StringFormat StringFormatForAlignment(ContentAlignment align)
        {
            return new StringFormat
            {
                Alignment = TranslateAlignment(align),
                LineAlignment = TranslateLineAlignment(align)
            };
        }

        internal static StringAlignment TranslateAlignment(ContentAlignment align)
        {
            StringAlignment result;
            if ((align & anyRight) != (System.Drawing.ContentAlignment)0)
            {
                result = StringAlignment.Far;
            }
            else if ((align & anyCenter) != (System.Drawing.ContentAlignment)0)
            {
                result = StringAlignment.Center;
            }
            else
            {
                result = StringAlignment.Near;
            }
            return result;
        }

        internal static TextFormatFlags TranslateAlignmentForGDI(System.Drawing.ContentAlignment align)
        {
            TextFormatFlags result;
            if ((align & anyBottom) != (System.Drawing.ContentAlignment)0)
            {
                result = TextFormatFlags.Bottom;
            }
            else if ((align & anyMiddle) != (System.Drawing.ContentAlignment)0)
            {
                result = TextFormatFlags.VerticalCenter;
            }
            else
            {
                result = TextFormatFlags.Default;
            }
            return result;
        }

        internal static StringAlignment TranslateLineAlignment(System.Drawing.ContentAlignment align)
        {
            StringAlignment result;
            if ((align & anyBottom) != (System.Drawing.ContentAlignment)0)
            {
                result = StringAlignment.Far;
            }
            else if ((align & anyMiddle) != (System.Drawing.ContentAlignment)0)
            {
                result = StringAlignment.Center;
            }
            else
            {
                result = StringAlignment.Near;
            }
            return result;
        }

        internal static TextFormatFlags TranslateLineAlignmentForGDI(System.Drawing.ContentAlignment align)
        {
            TextFormatFlags result;
            if ((align & anyRight) != (System.Drawing.ContentAlignment)0)
            {
                result = TextFormatFlags.Right;
            }
            else if ((align & anyCenter) != (System.Drawing.ContentAlignment)0)
            {
                result = TextFormatFlags.HorizontalCenter;
            }
            else
            {
                result = TextFormatFlags.Default;
            }
            return result;
        }

        internal static StringFormat CreateStringFormat(Control ctl, System.Drawing.ContentAlignment textAlign, bool showEllipsis, bool useMnemonic, bool showKeyboardCues)
        {
            StringFormat stringFormat = StringFormatForAlignment(textAlign);
            if (ctl.RightToLeft == RightToLeft.Yes)
            {
                stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
            }
            if (showEllipsis)
            {
                stringFormat.Trimming = StringTrimming.EllipsisCharacter;
                stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
            }
            if (!useMnemonic)
            {
                stringFormat.HotkeyPrefix = HotkeyPrefix.None;
            }
            else if (showKeyboardCues)
            {
                stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
            }
            else
            {
                stringFormat.HotkeyPrefix = HotkeyPrefix.Hide;
            }
            if (ctl.AutoSize)
            {
                stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
            }
            return stringFormat;
        }

        public static Size Pack(this Dictionary<Control, Rectangle> map)
        {
            int right = 0;
            int bottom = 0;

            foreach (var pair in map)
            {
                var bounds = pair.Value;
                right = Math.Max(right, bounds.Right);
                bottom = Math.Max(bottom, bounds.Bottom);
            }

            return new Size(right, bottom);
        }

        public static void SetStyle(this Control control, ControlStyles flag, bool value)
        {
            SetStyleAction(control, flag, value);
        }

        public static T MakeOptimization<T>(this T control) where T : Control
        {
            SetStyle(control, ControlStyles.UserPaint, true);
            SetStyle(control, ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(control, ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(control, ControlStyles.ResizeRedraw, true);

            if ((control is Form) == false)
            {
                SetStyle(control, ControlStyles.SupportsTransparentBackColor, true);
                SetStyle(control, ControlStyles.StandardDoubleClick, true);
                SetStyle(control, ControlStyles.EnableNotifyMessage, true);
            }

            return control;
        }

        public static Size Pack(this Control control)
        {
            return Pack(control, 0, 0);
        }
        public static Size Pack(this Control control, int marginWidth, int marginHeight)
        {
            Control.ControlCollection controls = control.Controls;

            int width = 0;
            int height = 0;

            for (int i = 0; i < controls.Count; ++i)
            {
                Control c = controls[i];

                if (i == 0)
                {
                    width = c.Right;
                    height = c.Bottom;
                }
                else
                {
                    width = Math.Max(width, c.Right);
                    height = Math.Max(height, c.Bottom);
                }

            }

            return new Size(width + marginWidth, height + marginHeight);
        }

        public static void InvokeFNeeded(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }

        }

        public static void PlaceByDirection(Control master, Control slave, PlaceDirection direction)
        {
            PlaceByDirection(master, slave, direction, PlaceLevel.Zero);
        }

        public static void PlaceByDirection(Control master, Control slave, PlaceDirection direction, int margin)
        {
            PlaceByDirection(master, slave, direction, PlaceLevel.Zero, margin);
        }

        public static void PlaceByDirection(Control master, Control slave, PlaceDirection direction, PlaceLevel level)
        {
            PlaceByDirection(master, slave, direction, level, DrawingUtils.DefaultMargin);
        }

        public static void PlaceByDirection(Control master, Control slave, PlaceDirection direction, PlaceLevel level, int margin)
        {
            slave.Location = DrawingUtils.PlaceByDirection(master.Bounds, slave.Size, direction, level, margin);
        }

    }

}
