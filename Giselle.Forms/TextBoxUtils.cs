using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public static class TextBoxUtils
    {
        public static void Appand(this TextBox textBox, string str)
        {
            var s = textBox.SelectionStart;
            var l = textBox.SelectionLength;
            textBox.Append(s, s + l, str);
        }

        public static void Append(this TextBox textBox, int leftEndIndex, int rightStartIndex, string insert)
        {
            var text = textBox.Text;
            var left = text.Substring(0, leftEndIndex);
            var right = text.Substring(rightStartIndex);
            var newText = string.Concat(left, insert, right);

            textBox.Text = newText.Substring(0, Math.Min(newText.Length, textBox.MaxLength));
            textBox.SelectionStart = leftEndIndex + insert.Length;
            textBox.SelectionLength = 0;
            textBox.Focus();
        }

        public static void Insert(this TextBox textBox, int index, string insert)
        {
            var text = textBox.Text;
            var s = textBox.SelectionStart;
            var l = textBox.SelectionLength;
            var left = text.Substring(0, index);
            var right = text.Substring(index);
            var newText = string.Concat(left, insert, right);

            var ns = s;
            var nl = l;

            if (index <= s)
            {
                ns = s + insert.Length;
            }

            if (s <= index && index < s + l)
            {
                nl = l + insert.Length;
            }

            textBox.Text = newText;
            textBox.SelectionStart = ns;
            textBox.SelectionLength = nl;
            textBox.Focus();
        }

        public static void RemoveRange(this TextBox textBox, int index, int length)
        {
            var end = index + length;
            var text = textBox.Text;
            var s = textBox.SelectionStart;
            var l = textBox.SelectionLength;

            var left = text.Substring(0, index);
            var right = text.Substring(end);
            var newText = string.Concat(left, right);

            var ns = index <= s ? Math.Max(0, s - index - length) : s - length;
            var nl = l;

            if (s < end && end <= s + l)
            {
                var rl = Math.Max(index, s);
                nl = l - (end - rl);
            }

            textBox.Text = newText;
            textBox.SelectionStart = ns;
            textBox.SelectionLength = nl;
            textBox.Focus();
        }

        public static void RemoveSelection(this TextBox textBox)
        {
            var s = textBox.SelectionStart;
            var l = textBox.SelectionLength;

            if (l > 0)
            {
                textBox.Append(s, s + l, string.Empty);
            }
            else if (s > 0)
            {
                textBox.Append(s - 1, s, string.Empty);
            }

        }

    }

}
