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
    public class NetworkException : Exception
    {
        public NetworkException() : base()
        {

        }

        public NetworkException(string message) : base(message)
        {

        }

        public NetworkException(string message, Exception innerException) : base(message, innerException)
        {

        }

        [SecuritySafeCritical]
        protected NetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public override Exception GetBaseException()
        {
            return base.GetBaseException();
        }

    }

}
