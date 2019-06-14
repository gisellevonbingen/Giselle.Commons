using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class DictionaryUtils
    {
        public static void PutAll<K, V>(this Dictionary<K, V> destination, Dictionary<K, V> source)
        {
            if (source != null)
            {
                foreach (var pair in source)
                {
                    destination[pair.Key] = pair.Value;
                }

            }

        }

        public static void ClearAndPutAll<K, V>(this Dictionary<K, V> destination, Dictionary<K, V> source)
        {
            destination.Clear();
            PutAll(destination, source);
        }

        public static V Get<K, V>(this Dictionary<K, V> dictionary, K key)
        {
            return Get(dictionary, key, default(V));
        }

        public static V Get<K, V>(this Dictionary<K, V> dictionary, K key, V fallback)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return fallback;
            }

        }

    }

}
