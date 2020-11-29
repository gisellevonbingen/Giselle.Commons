using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public class KeypadValidateEventArgs : EventArgs
    {
        public KeypadSettings Settings { get; private set; }
        public KeypadParseResult Processing { get; private set; }
        public string Text { get; private set; }

        public KeypadValidateEventArgs(KeypadSettings settings, KeypadParseResult processing, string text)
        {
            this.Settings = settings;
            this.Processing = processing;
            this.Text = text;
        }

    }

}
