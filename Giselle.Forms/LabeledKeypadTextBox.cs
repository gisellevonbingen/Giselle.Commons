using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class LabeledKeypadTextBox : LabeledControl
    {
        public KeypadTextBox TextBox { get; private set; }

        public LabeledKeypadTextBox()
        {
            this.SuspendLayout();

            this.Label.TextChanged += this.OnLabelTextChanged;

            this.TextBox = new KeypadTextBox();
            this.Controls.Add(this.TextBox);

            this.ResumeLayout(false);

            this.UpdateControlsSize();
            this.UpdateTextBoxKeypadTitle();
        }

        public void UpdateTextBoxKeypadTitle()
        {
            this.TextBox.Settings.Title = this.Label.Text;
        }

        private void OnLabelTextChanged(object sender, EventArgs e)
        {
            this.UpdateTextBoxKeypadTitle();
        }

        protected override Control GetValueControl()
        {
            return this.TextBox;
        }

    }

}
