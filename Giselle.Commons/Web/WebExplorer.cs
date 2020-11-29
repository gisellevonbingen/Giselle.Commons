using Giselle.Commons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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

        public delegate void WebRequestWriteEventHandler(object sender, WebRequestWriteEventArgs e);

        public CookieContainer Cookies { get; }

        public event EventHandler<WebRequestEventArgs> Requesting;
        public event EventHandler<WebResponseEventArgs> Responsed;

        public WebExplorer()
        {
            this.Cookies = new CookieContainer();
        }

        protected virtual void OnRequesting(WebRequestEventArgs e)
        {
            this.Requesting?.Invoke(this, e);
        }

        protected virtual void OnResponsed(WebResponseEventArgs e)
        {
            this.Responsed?.Invoke(this, e);
        }

        public virtual HttpWebRequest CreateRequest(WebRequestParameter parameter)
        {
            var uri = new Uri(parameter.Uri);
            var request = (HttpWebRequest)WebRequest.Create(uri);

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

        protected virtual void WriteReuqestParameter(HttpWebRequest request, object writeParameter)
        {
            if (writeParameter == null)
            {
                return;
            }

            using (var requestStream = request.GetRequestStream())
            {
                if (writeParameter is WebRequestWriteEventHandler handler)
                {
                    var e = new WebRequestWriteEventArgs(requestStream);
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

        public virtual WebResponse Request(WebRequestParameter parameter, CancellationTokenSource cancelTokenSource = null)
        {
            Exception lastException = null;

            for (int i = 0; i < parameter.RetryCount + 1; i++)
            {
                try
                {
                    var token = cancelTokenSource?.Token;
                    return this.RequestInTry(parameter, i, token);
                }
                catch (Exception e)
                {
                    lastException = e;
                }

            }

            throw new WebNetworkException("", lastException);
        }

        protected virtual WebResponse RequestInTry(WebRequestParameter parameter, int tryIndex, CancellationToken? cancelToken = null)
        {
            Exception innerException = null;

            try
            {
                var request = this.CreateRequest(parameter);
                this.WriteReuqestParameter(request, parameter.WriteParameter);

                this.OnRequesting(new WebRequestEventArgs(request, parameter, tryIndex));

                HttpWebResponse responseImpl = null;

                try
                {
                    cancelToken?.Register(() => request.Abort());
                    responseImpl = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException e)
                {
                    innerException = e;
                    responseImpl = (HttpWebResponse)e.Response;
                }

                if (responseImpl != null)
                {
                    this.Cookies.Add(responseImpl.Cookies);
                    var response = new WebResponse(responseImpl);

                    try
                    {
                        this.OnResponsed(new WebResponseEventArgs(request, parameter, tryIndex, response));

                        return response;
                    }
                    catch (Exception e)
                    {
                        innerException = e;
                        ObjectUtils.DisposeQuietly(response);
                    }

                }

            }
            catch (Exception e)
            {
                innerException = e;
            }

            throw new WebNetworkException("", innerException);
        }

    }

}
