using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class WebExplorer
    {
        static WebExplorer()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.DefaultConnectionLimit = 9999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
        }

        private static bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public delegate void RequestWriteEventHandler(object sender, RequestWriteEventArgs e);

        public CookieContainer Cookies { get; }

        public WebExplorer()
        {
            this.Cookies = new CookieContainer();
        }

        public HttpWebRequest CreateRequest(RequestParameter parameter)
        {
            string url = parameter.URL;
            var uri = new Uri(url);
            var request = (HttpWebRequest)WebRequest.Create(url);

            var headers = request.Headers;
            request.Credentials = parameter.Credentials;
            request.Method = parameter.Method;
            request.ProtocolVersion = parameter.ProtocolVersion;
            request.Host = uri.Host;
            request.KeepAlive = parameter.KeepAlive;
            headers["Cache-Control"] = parameter.CacheControl;
            request.UserAgent = parameter.UserAgent;
            request.Accept = parameter.Accept;
            headers["DNT"] = parameter.DNT;
            headers["Accept-Language"] = parameter.AcceptLanguage;

            if (parameter.ContentType != null)
            {
                request.ContentType = parameter.ContentType;
            }

            foreach (var pair in parameter.Headers)
            {
                headers[pair.Key] = pair.Value;
            }

            var cookies = new CookieContainer();
            cookies.Add(this.Cookies.GetCookies(uri));

            if (parameter.CookieContainer != null)
            {
                cookies.Add(parameter.CookieContainer.GetCookies(uri));
            }

            if (parameter.Referer != null)
            {
                request.Referer = parameter.Referer;
            }

            request.CookieContainer = cookies;
            request.AllowAutoRedirect = parameter.AllowAutoRedirect;

            var proxy = parameter.Proxy;

            if (proxy != null)
            {
                var proxyImpl = new WebProxy(proxy.Hostname, proxy.Port);
                proxyImpl.BypassProxyOnLocal = false;
                request.Proxy = proxyImpl;
            }

            int timeout = parameter.Timeout;
            request.ReadWriteTimeout = timeout;
            request.Timeout = timeout;
            request.ContinueTimeout = timeout;

            return request;
        }

        private void WriteReuqestParameter(HttpWebRequest request, object writeParameter)
        {
            if (writeParameter == null)
            {
                return;
            }

            using (var requestStream = request.GetRequestStream())
            {
                if (writeParameter is RequestWriteEventHandler handler)
                {
                    var e = new RequestWriteEventArgs(requestStream);
                    handler(this, e);
                }
                else if (writeParameter is byte[] bytes)
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }
                else
                {
                    var value = Encoding.Default.GetBytes(string.Concat(writeParameter));
                    requestStream.Write(value, 0, value.Length);
                }

            }

        }

        public SessionResponse Request(RequestParameter parameter)
        {
            Exception lastException = null;

            for (int i = 0; i < parameter.RetryCount + 1; i++)
            {
                try
                {
                    return this.Request0(parameter);
                }
                catch (Exception e)
                {
                    lastException = e;
                }

            }

            throw new NetworkException("", lastException);
        }

        private SessionResponse Request0(RequestParameter parameter)
        {
            HttpWebResponse response = null;
            Exception innerException = null;

            try
            {
                var request = this.CreateRequest(parameter);
                this.WriteReuqestParameter(request, parameter.WriteParameter);

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                    innerException = e;
                    response = (HttpWebResponse)e.Response;
                }

                if (response != null)
                {
                    this.Cookies.Add(response.Cookies);
                    return new SessionResponse(response);
                }

            }
            catch (Exception e)
            {
                innerException = e;
            }

            throw new NetworkException("", innerException);
        }

    }

}
