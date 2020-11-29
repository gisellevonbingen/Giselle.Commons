using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public abstract class ReadOnlyDictionary<K, V> : IReadOnlyDictionary<K, V>
    {
        protected Dictionary<K, V> Map { get; }

        public ReadOnlyDictionary()
        {
            this.Map = new Dictionary<K, V>();
        }

        public virtual V this[K key] => this.Map[key];

        public virtual IEnumerable<K> Keys => this.Map.Keys;

        public virtual IEnumerable<V> Values => this.Map.Values;

        public virtual int Count => this.Map.Count;

        public virtual bool ContainsKey(K key) => this.Map.ContainsKey(key);

        public virtual IEnumerator<KeyValuePair<K, V>> GetEnumerator() => this.Map.GetEnumerator();

        public virtual bool TryGetValue(K key, out V value) => this.Map.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}
