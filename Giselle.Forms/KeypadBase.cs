using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class KeypadBase : PopupForm
    {
        public KeypadSettings Settings { get; private set; }

        public OptimizedButton CommitButton { get; private set; }

        public event EventHandler Commit;

        public KeypadBase(KeypadSettings settings)
        {
            this.Settings = settings;

            this.SuspendLayout();

            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Font = this.FontManager[14.0F, FontStyle.Regular];

            this.Text = settings.Title;

            this.CommitButton = new OptimizedButton();
            this.CommitButton.Text = "＝";
            this.CommitButton.Click += this.OnCommitButtonClick;
            this.Controls.Add(this.CommitButton);

            this.ResumeLayout(false);
        }

        protected virtual void OnCommit(EventArgs e)
        {
            var handler = this.Commit;
            if (handler != null) handler(this, e);
        }

        private void OnCommitButtonClick(object sender, EventArgs e)
        {
            this.TryCommit();
        }

        public bool TryCommit()
        {
            if (this.OnTryCommit() == true)
            {
                this.OnCommit(EventArgs.Empty);
                this.DialogResult = DialogResult.OK;
                this.Close();

                return true;
            }

            return false;
        }

        protected virtual bool OnTryCommit()
        {
            return true;
        }

        protected override void OnKeyEscpace()
        {
            base.OnKeyEscpace();

            this.Close();
        }

        protected override void OnKeyReturn()
        {
            base.OnKeyReturn();

            this.TryCommit();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
        }

        private void OnButtonCloseClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }

}
