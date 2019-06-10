using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class EncodingUtils
    {
        private static readonly Encoding _UTF8WithoutBOM = new UTF8Encoding(false);
        public static Encoding UTF8WithoutBOM { get { return _UTF8WithoutBOM; } }
    }

}
