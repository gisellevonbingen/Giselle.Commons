using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Giselle.Commons;
using Giselle.Drawing.Drawing;

namespace Giselle.Forms
{
    public class KeypadNumber : KeypadBase
    {
        public Label RangeLabel { get; private set; }
        public TextBox TextBox { get; private set; }
        public Label ErrorLabel { get; private set; }
        public OptimizedButton[] NumberButtons { get; private set; }
        public OptimizedButton BackspaceButton { get; private set; }
        public OptimizedButton ClearButton { get; private set; }
        public OptimizedButton DotButton { get; private set; }
        public OptimizedButton InvertButton { get; private set; }

        private bool ValueUpdating = false;
        private double _Value = 0.0D;
        public double Value { get { return this._Value; } set { this._Value = value; this.OnValueChanged(); } }

        private KeypadParseResult _Result = new KeypadParseResult();
        public KeypadParseResult Result { get { return new KeypadParseResult(this._Result); } private set { this._Result = value; this.OnResultChanged(); } }

        public KeypadNumber(KeypadSettings settings)
            : base(settings)
        {
            this.SuspendLayout();

            var buttonFont = this.FontManager[16.0F, FontStyle.Regular];

            var rangeLabel = this.RangeLabel = new Label();
            rangeLabel.TextAlign = ContentAlignment.MiddleRight;
            this.Controls.Add(rangeLabel);

            var textBox = this.TextBox = new TextBox();
            textBox.Font = this.FontManager[16.0F, FontStyle.Regular];
            textBox.TextAlign = HorizontalAlignment.Right;
            textBox.TextChanged += this.OnTextBoxTextChanged;
            this.Controls.Add(textBox);

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

            var invertButton = this.InvertButton = new OptimizedButton();
            invertButton.Text = "±";
            invertButton.Font = buttonFont;
            invertButton.Click += this.OnInvertButtonClick;
            this.Controls.Add(invertButton);

            this.ResumeLayout(false);

            this.ClientSize = this.GetPreferredSize(new Size(300, 300));
        }

        private void OnResultChanged()
        {
            var result = this.Result;

            var color = result.Validated ? Color.Black : Color.Red;
            this.TextBox.ForeColor = color;

            var text = this.GetErrorText(result);
            this.ErrorLabel.Text = text;
            this.ErrorLabel.Font = this.FontManager.FindMatch(text, new FontMatchFormat() { Size = 14, ProposedSize = this.ErrorLabel.Size });
        }

        public string GetErrorText(KeypadParseResult result)
        {
            var cause = result.NumberErrorCause;

            if (cause == NumberErrorCause.Invalid)
            {
                return "숫자가 아님";
            }
            else if (cause == NumberErrorCause.DpsOver)
            {
                return "소숫점 제한 초과함";
            }
            else if (cause == NumberErrorCause.MinOver)
            {
                return "하한 값 넘음";
            }
            else if (cause == NumberErrorCause.MaxOver)
            {
                return "상한 값 넘음";
            }
            else if (result.CustomError != null)
            {
                return result.CustomError.ErrorMessage;
            }
            else if (cause == NumberErrorCause.None)
            {
                return string.Empty;
            }

            return "알 수 없는 에러";
        }

        public string ToStringRange(KeypadSettings settings)
        {
            var min = settings.Min;
            var max = settings.Max;
            var dps = settings.Dps ?? 0;
            var dpsFormat = "F" + dps;
            var builder = new StringBuilder();

            if (min.HasValue == true || max.HasValue == true)
            {
                if (min.HasValue == true)
                {
                    builder.Append(settings.Min.Value.ToString(dpsFormat)).Append(" ≤ ");
                }

                builder.Append("값");

                if (max.HasValue == true)
                {
                    builder.Append(" ≤ ").Append(settings.Max.Value.ToString(dpsFormat));
                }

            }

            return builder.ToString();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var settings = this.Settings;
            this.DotButton.Visible = settings.Dps > 0;
            this.RangeLabel.Text = this.ToStringRange(settings);
            this.TextBox.Focus();
            this.TextBox.SelectAll();
            this.UpdateResult();
        }

        private void OnValueChanged()
        {
            var value = this.Value;
            var range = this.Settings;
            var textBox = this.TextBox;

            if (this.ValueUpdating == false)
            {
                this.ValueUpdating = true;

                string text;

                if (range != null)
                {
                    text = value.ToString("F" + range.Dps);
                }
                else
                {
                    text = value.ToString();
                }

                textBox.Text = text;
                textBox.SelectionStart = text.Length;
                textBox.SelectionLength = 0;
                textBox.Focus();

                this.ValueUpdating = false;
                this.Invalidate();
            }

            this.UpdateResult();
        }

        private void UpdateResult()
        {
            this.Result = this.Settings.Validate(this.TextBox.Text);
        }

        private void OnTextBoxTextChanged(object sender, EventArgs e)
        {
            var textBox = this.TextBox;
            string text = textBox.Text;

            if (this.ValueUpdating == false)
            {
                this.ValueUpdating = true;

                this.Value = text.ToDoubleGeneral();

                this.ValueUpdating = false;
                this.Invalidate();
            }

        }

        private void OnNumberButtonClick(object sender, EventArgs e)
        {
            this.TextBox.Focus();

            var v = (int)((OptimizedButton)sender).Tag;
            this.TextBox.Appand(v.ToString());
        }

        protected override bool OnTryCommit()
        {
            if (base.OnTryCommit() == false)
            {
                return false;
            }

            return this.Result.Validated;
        }

        private void OnDotButtonClick(object sender, EventArgs e)
        {
            this.ApplyPrecision();
        }

        private void OnInvertButtonClick(object sender, EventArgs e)
        {
            this.Invert();
        }

        private void OnClearButtonClick(object sender, EventArgs e)
        {
            this.TextBox.Text = string.Empty;
            this.TextBox.Focus();
        }

        private void OnBackspaceButtonClick(object sender, EventArgs e)
        {
            this.TextBox.RemoveSelection();
        }

        public void ApplyPrecision()
        {
            var textBox = this.TextBox;
            var text = textBox.Text;

            if (text.Contains(".") == false)
            {
                textBox.Appand(".");
            }

            textBox.Focus();
        }

        public void Invert()
        {
            var textBox = this.TextBox;
            var text = textBox.Text;

            if (text.StartsWith("-") == false)
            {
                textBox.Insert(0, "-");
            }
            else
            {
                textBox.RemoveRange(0, 1);
            }

            textBox.Focus();
        }

        protected override Dictionary<Control, Rectangle> GetPreferredBounds(Rectangle layoutBounds)
        {
            var map = base.GetPreferredBounds(layoutBounds);

            map[this.RangeLabel] = new Rectangle(layoutBounds.Left, layoutBounds.Top, layoutBounds.Width, 22);
            map[this.TextBox] = map[this.RangeLabel].OutBottomBounds(this.TextBox.Height);
            map[this.ErrorLabel] = map[this.TextBox].OutBottomBounds(this.TextBox.Height);

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
                this.DotButton,
                this.InvertButton
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

            map[this.CommitButton] = map[this.CommitButton].DeriveHeight(map[this.InvertButton].Bottom - map[this.NumberButtons[3]].Top);

            return map;
        }

    }

}
