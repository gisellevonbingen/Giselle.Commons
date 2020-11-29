using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public class SimpleNameRegistry<T> : ReadOnlyDictionary<string, T> where T : INamed
    {
        public SimpleNameRegistry()
        {

        }

        public T Register(T value)
        {
            var name = value.Name;

            if (this.ContainsKey(value.Name) == true)
            {
                throw new ArgumentException("Already Registered Name : " + name);
            }

            this.Map[name] = value;
            return value;
        }

    }

}
