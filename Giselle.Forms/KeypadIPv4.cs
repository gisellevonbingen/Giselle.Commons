using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;
using Giselle.Commons.Collections;
using Giselle.Drawing;
using Giselle.Drawing.Drawing;

namespace Giselle.Forms
{
    public class KeypadIPv4 : KeypadBase
    {
        public Label RangeLabel { get; private set; }
        public TextBox[] TextBoxs { get; private set; }
        private TextBox LastFocusTextBox = null;
        private TextBox FirstErrorTextBox = null;
        public Label ErrorLabel { get; private set; }
        public OptimizedButton[] NumberButtons { get; private set; }
        public OptimizedButton BackspaceButton { get; private set; }
        public OptimizedButton ClearButton { get; private set; }
        public OptimizedButton DotButton { get; private set; }

        private bool ValueUpdating = false;
        private IPAddress _Value = null;
        public IPAddress Value { get { return this._Value; } set { this._Value = value; this.OnValueChanged(); } }

        private KeypadParseResult _Result = new KeypadParseResult();
        public KeypadParseResult Result { get { return new KeypadParseResult(this._Result); } private set { this._Result = value; this.OnResultChanged(); } }

        public KeypadIPv4(KeypadSettings settings)
            : base(settings)
        {
            this.SuspendLayout();

            var buttonFont = this.FontManager[16.0F, FontStyle.Regular];

            var rangeLabel = this.RangeLabel = new Label();
            rangeLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(rangeLabel);

            var textBoxs = this.TextBoxs = new TextBox[4];
            this.LastFocusTextBox = null;

            for (var i = 0; i < textBoxs.Length; i++)
            {
                var textBox = textBoxs[i] = new TextBox();
                textBox.MaxLength = 3;
                textBox.Font = this.FontManager[16.0F, FontStyle.Regular];
                textBox.TextAlign = HorizontalAlignment.Right;
                textBox.GotFocus += this.OnTextBoxGotFocus;
                textBox.TextChanged += this.OnTextBoxTextChanged;
                textBox.KeyDown += this.OnTextBoxKeyDown;
                this.Controls.Add(textBox);
            }

            var errorLabel = this.ErrorLabel = new Label();
            errorLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(errorLabel);

            this.NumberButtons = new OptimizedButton[10];

            for (int i = 0; i < this.NumberButtons.Length; ++i)
            {
                var button = this.NumberButtons[i] = new OptimizedButton();
                button.Size = new Size(46, 44);
                button.Text = i.ToString();
                button.Tag = i;
                button.Font = buttonFont;
                button.Click += this.OnNumberButtonClick;
                this.Controls.Add(button);
            }

            var backspaceButton = this.BackspaceButton = new OptimizedButton();
            backspaceButton.Text = "<-";
            backspaceButton.Font = buttonFont;
            backspaceButton.Click += this.OnBackspaceButtonClick;
            this.Controls.Add(backspaceButton);

            var clearButton = this.ClearButton = new OptimizedButton();
            clearButton.Text = "C";
            clearButton.Font = buttonFont;
            clearButton.Click += this.OnClearButtonClick;
            this.Controls.Add(clearButton);

            var dotButton = this.DotButton = new OptimizedButton();
            dotButton.Text = ".";
            dotButton.Font = buttonFont;
            dotButton.Click += this.OnDotButtonClick;
            this.Controls.Add(dotButton);

            this.ResumeLayout(false);

            this.ClientSize = this.GetPreferredSize(new Size(300, 300));
        }

        private void OnResultChanged()
        {
            this.UpdateErrorText();
        }

        private void UpdateErrorText()
        {
            var text = this.GetErrorText(this.Result);
            this.ErrorLabel.Text = text;
            this.ErrorLabel.Font = this.FontManager.FindMatch(text, new FontMatchFormat() { Size = 14, ProposedSize = this.ErrorLabel.Size });
        }

        public string GetErrorText(KeypadParseResult result)
        {
            var cause = result.IPAddressCause;

            if (cause == IPAddressErrorCause.DecimalCountInvalid)
            {
                return "값 인식 불가";
            }
            else if (cause == IPAddressErrorCause.AnyDecimalInvalid)
            {
                return "값 인식 불가";
            }
            else if (cause == IPAddressErrorCause.ParseError)
            {
                return "값 인식 불가";
            }
            else if (result.CustomError != null)
            {
                return result.CustomError.ErrorMessage;
            }
            else if (cause == IPAddressErrorCause.None)
            {
                return string.Empty;
            }

            return "알 수 없는 에러";
        }

        private void OnNumberButtonClick(object sender, EventArgs e)
        {
            var textBox = this.LastFocusTextBox;
            textBox.Focus();

            var v = (int)((OptimizedButton)sender).Tag;
            textBox.Appand(v.ToString());
            this.NextFocus(textBox, true);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var last = this.TextBoxs.FirstOrDefault();
            last.Focus();
            last.SelectAll();
        }

        private TextBox GetFirstErrorTextBox()
        {
            return this.TextBoxs.FirstOrDefault(tb => { return byte.TryParse(tb.Text, out byte b) == false; });
        }

        private KeypadParseResult Parse()
        {
            var text = string.Join(".", this.TextBoxs.Select(t => t.Text));
            var result = this.Settings.Validate(text);

            this.Result = result;
            this.FirstErrorTextBox = this.GetFirstErrorTextBox();
            return result;
        }

        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            var text = textBox.Text;

            textBox.ForeColor = byte.TryParse(text, out _) ? Color.Black : Color.Red;

            if (this.ValueUpdating == false)
            {
                this.ValueUpdating = true;

                this.Value = this.Parse().IPAddress;

                this.ValueUpdating = false;
            }

            this.NextFocus(textBox, true);

            this.Invalidate();
        }

        private void OnTextBoxGotFocus(object sender, EventArgs e)
        {
            this.LastFocusTextBox = (TextBox)sender;
        }

        private void OnValueChanged()
        {
            if (this.ValueUpdating == true)
            {
                return;
            }

            try
            {
                this.ValueUpdating = true;
                var bytes = this.Value.GetAddressBytes();

                for (var i = 0; i < this.TextBoxs.Length; ++i)
                {
                    var textBox = this.TextBoxs[i];
                    textBox.Text = bytes[i].ToString();
                }

                this.Parse();
            }
            finally
            {
                this.ValueUpdating = false;
            }

        }

        protected override bool OnTryCommit()
        {
            if (base.OnTryCommit() == false)
            {
                return false;
            }
            else if (this.Parse().Validated == false)
            {
                if (this.FirstErrorTextBox != null)
                {
                    this.FirstErrorTextBox.Focus();
                    this.FirstErrorTextBox.SelectAll();
                }

                return false;
            }

            return true;
        }

        private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            var textBox = sender as TextBox;

            if (keyCode == Keys.OemPeriod || keyCode == Keys.Decimal)
            {
                e.SuppressKeyPress = true;

                this.NextFocus(textBox, false);
            }
            else if (keyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;

                this.Backspace(textBox);
            }
            else
            {
                this.NextFocus(textBox, true);
            }

            this.Invalidate();
        }

        public void Backspace(TextBox textBox)
        {
            var s = textBox.SelectionStart;
            textBox.RemoveSelection();

            if (s == 0)
            {
                this.PrevFocus(textBox);
            }

        }

        public void NextFocus(TextBox textBox, bool requireComplete)
        {
            if (requireComplete == true)
            {
                if (textBox.SelectionStart != textBox.MaxLength)
                {
                    textBox.Focus();
                    return;
                }

            }

            int index = Array.IndexOf(this.TextBoxs, textBox);

            if (textBox.Text.Length == 0 || index == (this.TextBoxs.Length - 1))
            {
                textBox.Focus();
            }
            else
            {
                var next = this.TextBoxs[index + 1];
                next.Focus();
                next.SelectionStart = 0;
                next.SelectionLength = next.Text.Length;
            }

        }

        public void PrevFocus(TextBox textBox)
        {
            var index = Array.IndexOf(this.TextBoxs, textBox);

            if (index == 0)
            {
                textBox.Focus();
            }
            else
            {
                var prev = this.TextBoxs[index - 1];
                prev.Focus();
                prev.SelectionStart = prev.Text.Length;
                prev.SelectionLength = 0;
            }

        }

        public void Clear()
        {
            foreach (var textBox in this.TextBoxs)
            {
                textBox.Text = string.Empty;
            }

            this.TextBoxs[0].Focus();
        }

        private void OnClearButtonClick(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void OnBackspaceButtonClick(object sender, EventArgs e)
        {
            this.Backspace(this.LastFocusTextBox);
        }

        private void OnDotButtonClick(object sender, EventArgs e)
        {
            this.NextFocus(this.LastFocusTextBox, false);
        }

        protected override Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = base.GetPreferredBounds(layoutBounds);

            map[this.RangeLabel] = new Rectangle(layoutBounds.Left, layoutBounds.Top, layoutBounds.Width, 22);

            var textBoxWidth = (layoutBounds.Width - (10 * (this.TextBoxs.Length - 1))) / this.TextBoxs.Length;
            var sizes = this.TextBoxs.ToDictionary(k => (Control)k, k => new Size(textBoxWidth, k.PreferredHeight));
            map.PutAll(ControlUtils.LayoutArray(map[this.RangeLabel].OutBottomBounds(this.TextBoxs[0].Height), ContentAlignment.MiddleLeft, PlaceDirection.Right, PlaceLevel.Zero, 10, sizes));
            map[this.ErrorLabel] = map[this.TextBoxs[0]].DeriveWidth(map[this.RangeLabel].Width).ConsumeSelect(o => o.OutBottomBounds(o.Height));

            var buttons = new List<OptimizedButton>
            {
                this.NumberButtons[7],
                this.NumberButtons[8],
                this.NumberButtons[9],
                this.BackspaceButton,
                this.NumberButtons[4],
                this.NumberButtons[5],
                this.NumberButtons[6],
                this.ClearButton,
                this.NumberButtons[1],
                this.NumberButtons[2],
                this.NumberButtons[3],
                this.CommitButton,
                this.NumberButtons[0],
                this.DotButton
            };

            var b = map[this.ErrorLabel];
            var l = b.Left;
            var t = b.Bottom;
            var columns = 4;
            var margin = 10;
            var bl = (b.Width - (margin * (columns - 1))) / columns;
            var buttonSize = new Size(bl, bl);

            for (var i = 0; i < buttons.Count; i++)
            {
                var button = buttons[i];
                var xi = i % columns;
                var yi = i / columns;

                map[button] = new Rectangle(new Point(l + xi * (buttonSize.Width + margin), t + yi * (buttonSize.Height + margin)), buttonSize);
            }

            map[this.CommitButton] = map[this.CommitButton].DeriveHeight(map[this.DotButton].Bottom - map[this.NumberButtons[3]].Top);

            return map;
        }

    }

}
