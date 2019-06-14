using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public class ToStringBuilder
    {
        public string Name { get; set; }

        private List<string> Names { get; }
        private Dictionary<string, object> Map { get; }

        public ToStringBuilder()
        {
            this.Name = null;
            this.Names = new List<string>();
            this.Map = new Dictionary<string, object>();
        }

        public ToStringBuilder SetName(string name)
        {
            this.Name = name;
            return this;
        }

        public ToStringBuilder Add(string key, object value, bool overwrite = true)
        {
            if (this.Name.Contains(key) == true)
            {
                if (overwrite == true)
                {
                    this.Names.Remove(key);
                    this.Names.Add(key);
                }

            }
            else
            {
                this.Names.Add(key);
            }

            this.Map[key] = value;

            return this;
        }

        public override string ToString()
        {
            return ObjectUtils.ToStringSimple(this.Name, this.Names, this.Map);
        }

    }

}
