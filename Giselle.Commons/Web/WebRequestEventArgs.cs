using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class WebRequestEventArgs : EventArgs
    {
        public HttpWebRequest Request { get; }
        public WebRequestParameter Parameter { get; }
        public int TryIndex { get; }

        public WebRequestEventArgs(HttpWebRequest request, WebRequestParameter parameter, int tryIndex)
        {
            this.Request = request;
            this.Parameter = parameter;
            this.TryIndex = tryIndex;
        }

    }

}
