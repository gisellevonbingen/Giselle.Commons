using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public class SimpleRegistry<K, V> : ReadOnlyDictionary<K, V>
    {
        public SimpleRegistry()
        {

        }

        protected virtual void OnRegister(K key, V value)
        {

        }

        public V Register(K key, V value)
        {
            if (this.ContainsKey(key) == true)
            {
                throw new ArgumentException("Already Registered Key : " + key);
            }

            this.Map[key] = value;

            this.OnRegister(key, value);

            return value;
        }

    }

}
