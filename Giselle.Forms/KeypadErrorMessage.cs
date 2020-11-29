using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public class KeypadErrorMessage : IKeypadError
    {
        public string ErrorMessage { get; private set; }

        public KeypadErrorMessage(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

    }

}
