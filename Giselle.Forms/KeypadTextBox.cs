using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;

namespace Giselle.Forms
{
    public class KeypadTextBox : TextBox
    {
        public KeypadSettings Settings { get; set; }
        public KeypadBase LastKeypad { get; private set; }

        public KeypadParseResult LastParseResult { get; private set; }

        public KeypadTextBox()
        {
            this.Settings = new KeypadSettings();
            this.LastKeypad = null;

            this.LastParseResult = new KeypadParseResult();
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);

            if (this.Validate().Validated == false)
            {
                e.Cancel = true;
                this.Focus();
                this.SelectAll();
            }

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (this.Visible == true)
            {
                var type = this.Settings.Type;

                if (type == KeypadType.Number || type == KeypadType.IPAddress)
                {
                    this.TextAlign = HorizontalAlignment.Right;
                }
                else
                {
                    this.TextAlign = HorizontalAlignment.Left;
                }

                if (type == KeypadType.IPAddress)
                {
                    this.MaxLength = 15;
                }
                else
                {
                    this.MaxLength = 0;
                }
            }

        }

        public KeypadParseResult Validate()
        {
            var result = this.Settings.Validate(this.Text);
            this.LastParseResult = result;

            var color = result.Validated == true ? Color.Black : Color.Red;
            this.ForeColor = color;

            return result;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            this.PopupKeypad();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this.DisposeKeypad();
        }

        public void DisposeKeypad()
        {
            this.LastKeypad.DisposeQuietly();
            this.LastKeypad = null;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            this.Validate();
        }

        public void PopupKeypad()
        {
            this.DisposeKeypad();

            if (this.ReadOnly == true)
            {
                return;
            }

            var settings = this.Settings.Clone();
            var type = settings.Type;
            KeypadBase keypad = null;

            if (type == KeypadType.Number)
            {
                var keypadNumber = new KeypadNumber(settings);
                keypadNumber.TextBox.Text = this.Text;
                keypad = keypadNumber;
            }
            else if (type == KeypadType.String)
            {

            }
            else if (type == KeypadType.IPAddress)
            {
                var keypadIPv4 = new KeypadIPv4(settings)
                {
                    Value = IPAddress.TryParse(this.Text, out var ipAddress) ? ipAddress : IPAddress.Any
                };
                keypad = keypadIPv4;
            }

            if (keypad != null)
            {
                keypad.Show(this);
                keypad.Commit += this.OnKeypadCommit;
                keypad.Location = ScreenUtils.GetInterpolatedLocation(this.PointToScreen(new Point(0, this.Height)), keypad.Size);
                this.LastKeypad = keypad;
            }

        }

        private void OnKeypadCommit(object sender, EventArgs e)
        {
            if (this.ReadOnly == true)
            {
                return;
            }

            if (sender is KeypadNumber keypadNumber)
            {
                this.Text = keypadNumber.TextBox.Text;
            }
            else if (sender is KeypadIPv4 keypadIPv4)
            {
                this.Text = keypadIPv4.Value.ToString();
            }

        }

    }

}
