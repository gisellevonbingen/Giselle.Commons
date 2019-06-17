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
            return this.ReadAsString(Encoding.UTF8);
        }

        public string ReadAsString(Encoding encoding)
        {
            using (var stream = this.ReadAsStream())
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }

            }

        }

        public Stream ReadAsStream()
        {
            return this.ReadAsStream(false);
        }

        public Stream ReadAsStream(bool ignoreContentEncoding)
        {
            var stream = this.Impl.GetResponseStream();

            if (ignoreContentEncoding == true)
            {
                return stream;
            }

            var contentEncoding = this.Impl.ContentEncoding;

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