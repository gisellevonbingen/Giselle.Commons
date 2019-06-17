using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class WebResponse : IDisposable
    {
        public HttpWebResponse Impl { get; }

        public WebResponse(HttpWebResponse impl)
        {
            this.Impl = impl;
        }

        ~WebResponse()
        {
            this.Dispose(false);
        }

        public string ReadAsString()
        {
            using (var stream = this.ReadAsStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }

            }

        }

        public Stream ReadAsStream()
        {
            var contentEncoding = this.Impl.ContentEncoding;
            var stream = this.Impl.GetResponseStream();

            if (contentEncoding.Equals("gzip", StringComparison.OrdinalIgnoreCase) == true)
            {
                return new GZipStream(stream, CompressionMode.Decompress);
            }
            else
            {
                return stream;
            }

        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            ObjectUtils.DisposeQuietly(this.Impl);
        }

    }

}