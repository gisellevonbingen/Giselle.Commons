using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class LabeledTextBox : LabeledControl
    {
        public TextBox TextBox { get; private set; }

        public LabeledTextBox()
        {
            this.SuspendLayout();

            this.TextBox = new TextBox();
            this.Controls.Add(this.TextBox);

            this.ResumeLayout(false);

            this.UpdateControlsSize();
        }

        protected override Control GetValueControl()
        {
            return this.TextBox;
        }

    }

}
