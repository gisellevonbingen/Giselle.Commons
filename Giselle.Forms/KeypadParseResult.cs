using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public class KeypadParseResult
    {
        public KeypadType Type { get; set; }
        public bool Validated { get; set; }
        public IKeypadError CustomError { get; set; }

        public NumberErrorCause NumberErrorCause { get; set; }
        public double Number { get; set; }
        public IPAddressErrorCause IPAddressCause { get; set; }
        public IPAddress IPAddress { get; set; }
        public string String { get; set; }

        public KeypadParseResult()
        {

        }

        public KeypadParseResult(KeypadParseResult other)
        {
            this.Type = other.Type;
            this.Validated = other.Validated;
            this.CustomError = other.CustomError;

            this.NumberErrorCause = other.NumberErrorCause;
            this.Number = other.Number;
            this.IPAddressCause = other.IPAddressCause;
            this.IPAddress = other.IPAddress;
            this.String = other.String;
        }

    }

}
