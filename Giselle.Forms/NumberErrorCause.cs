using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public enum NumberErrorCause : byte
    {
        None = 0,
        Invalid = 1,
        DpsOver = 2,
        MaxOver = 3,
        MinOver = 4,
    }

}
