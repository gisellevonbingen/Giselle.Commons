using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    [Serializable]
    public class WebNetworkException : Exception
    {
        public WebNetworkException() : base()
        {

        }

        public WebNetworkException(string message) : base(message)
        {

        }

        public WebNetworkException(string message, Exception innerException) : base(message, innerException)
        {

        }

        [SecuritySafeCritical]
        protected WebNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override Exception GetBaseException()
        {
            return base.GetBaseException();
        }

    }

}
