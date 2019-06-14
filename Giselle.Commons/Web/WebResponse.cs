﻿using System;
using System.Collections.Generic;
using System.IO;
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
            using (var stream = this.Impl.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }

            }

        }

        public Stream ReadAsStream()
        {
            return this.Impl.GetResponseStream();
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