using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class WebRequestParameter
    {
        public string Uri { get; set; } = null;
        public string Method { get; set; } = null;
        public string Referer { get; set; } = null;
        public string ContentType { get; set; } = null;
        public string Accept { get; set; } = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        public string AcceptLanguage { get; set; } = "ko-KR,ko;q=0.8,en-US;q=0.6,en;q=0.4";
        public CookieContainer CookieContainer { get; set; } = null;
        public int Timeout { get; set; } = 60 * 1000;
        public int RetryCount { get; set; } = 2;
        public bool AllowAutoRedirect { get; set; } = false;
        public object WriteParameter { get; set; } = null;
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public WebProxySettings Proxy { get; set; } = null;
        public string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
        public string CacheControl { get; set; } = "max-age=0";
        public string DNT { get; set; } = "1";
        public Version ProtocolVersion { get; set; } = HttpVersion.Version11;
        public ICredentials Credentials { get; set; } = CredentialCache.DefaultCredentials;
        public bool KeepAlive { get; set; } = true;

        public WebRequestParameter()
        {

        }

    }

}
