using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Web
{
    public class ProxySettings
    {
        public static char PortDelimiter { get; } = ':';

        public static bool TryParse(string s, out ProxySettings settings)
        {
            if (s == null)
            {
                settings = null;
                return false;
            }

            string[] splited = s.Split(PortDelimiter);

            if (splited.Length == 2 && ushort.TryParse(splited[1], out var port) == true)
            {
                settings = new ProxySettings(splited[0], port);
                return true;
            }
            else
            {
                settings = null;
                return false;
            }

        }

        public string Hostname { get; set; } = null;
        public ushort Port { get; set; } = 0;

        public ProxySettings()
        {

        }

        public ProxySettings(string hostname, ushort port)
        {
            this.Hostname = hostname;
            this.Port = port;
        }

        public override string ToString()
        {
            return $"{this.Hostname}{PortDelimiter}{this.Port}";
        }

    }

}
