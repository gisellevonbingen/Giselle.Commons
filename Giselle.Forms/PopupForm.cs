using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class PopupForm : OptimizedForm
    {
        public PopupForm()
        {

        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }

}
