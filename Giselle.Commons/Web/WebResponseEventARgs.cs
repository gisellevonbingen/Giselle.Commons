using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class WebResponseEventArgs : WebRequestEventArgs
    {
        public WebResponse Response { get; }

        public WebResponseEventArgs(HttpWebRequest request, WebRequestParameter parameter, int tryIndex, WebResponse response) : base(request, parameter, tryIndex)
        {
            this.Response = response;
        }

    }

}
