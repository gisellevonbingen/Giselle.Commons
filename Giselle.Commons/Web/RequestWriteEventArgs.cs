using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class RequestWriteEventArgs : EventArgs
    {
        public Stream Stream { get; }

        public RequestWriteEventArgs(Stream stream)
        {
            this.Stream = stream;
        }

    }

}
