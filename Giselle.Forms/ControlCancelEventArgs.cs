using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public class ControlCancelEventArgs : CancelEventArgs
    {
        public Control Control { get; private set; }

        public ControlCancelEventArgs(Control control, bool cancel)
        {
            this.Control = control;
            this.Cancel = cancel;
        }

    }

}
