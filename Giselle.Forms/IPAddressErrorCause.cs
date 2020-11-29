using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public enum IPAddressErrorCause : byte
    {
        None = 0,
        DecimalCountInvalid = 1,
        AnyDecimalInvalid = 2,
        ParseError = 3,
    }

}
