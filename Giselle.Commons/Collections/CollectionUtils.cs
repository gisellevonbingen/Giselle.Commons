using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public static class CollectionUtils
    {
        public static void RemoveAll<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var obj in collection.ToArray())
            {
                list.Remove(obj);
            }

        }

        public static T Find<T>(this IEnumerable<T> list, string name) where T : INamed
        {
            return list.FirstOrDefault(v => v.Name.Equals(name));
        }

        public static IEnumerable<T> FindAll<T>(this IEnumerable<T> list, string name) where T : INamed
        {
            return list.Where(v => v.Name.Equals(name));
        }

        public static int Compare<T>(T x, T y, params Comparison<T>[] comparers)
        {
            foreach (var comparer in comparers)
            {
                var result = comparer(x, y);

                if (result != 0)
                {
                    return result;
                }

            }

            return 0;
        }

        public static int Compare<T>(T x, T y, params IComparer<T>[] comparers)
        {
            foreach (var comparer in comparers)
            {
                var result = comparer.Compare(x, y);

                if (result != 0)
                {
                    return result;
                }

            }

            return 0;
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach(array, action);
        }

        public static IEnumerable<T> Join<T>(this IEnumerable<T> collection, T separator)
        {
            using (var tor = collection.GetEnumerator())
            {
                if (tor.MoveNext() == true)
                {
                    yield return tor.Current;
                }

                while (tor.MoveNext() == true)
                {
                    yield return separator;
                    yield return tor.Current;
                }

            }

        }

        public static bool AddIfAbsent<T>(this List<T> list, T item)
        {
            if (list.Contains(item) == false)
            {
                list.Add(item);
                return true;
            }

            return false;
        }

        public static bool EqualsDictionary<K, V>(this Dictionary<K, V> instance, Dictionary<K, V> other)
        {
            return instance.Count == other.Count && instance.Except(other).Any() == false;
        }

        public static IEnumerable<T> ShallowCopy<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return collection;
            }

            return collection.Select(v => v);
        }

        public static T[] ShallowCopy<T>(this T[] array)
        {
            if (array == null)
            {
                return array;
            }

            var clone = new T[array.Length];
            Array.Copy(array, 0, clone, 0, clone.Length);

            return clone;
        }

        public static void Replace<K, V>(this Dictionary<K, V> dictionary, IEnumerable<KeyValuePair<K, V>> values)
        {
            dictionary.Clear();

            if (values == null)
            {
                return;
            }

            dictionary.PutAll(values);
        }

        public static void PutAll<K, V>(this Dictionary<K, V> dictionary, IEnumerable<KeyValuePair<K, V>> values)
        {
            if (values == null)
            {
                return;
            }

            foreach (var pair in values)
            {
                dictionary[pair.Key] = pair.Value;
            }

        }

        public static V PutSafe<K, V>(this IDictionary<K, V> dictionary, K key, Func<V, V> func, V fallback = default)
        {
            if (dictionary.TryGetValue(key, out var prev) == false)
            {
                prev = fallback;
            }

            var value = func(prev);
            dictionary[key] = value;
            return prev;
        }

        public static V GetSafe<K, V>(this IReadOnlyDictionary<K, V> dictionary, K key, V fallback = default)
        {
            if (dictionary.TryGetValue(key, out var value) == true)
            {
                return value;
            }

            return fallback;
        }

        public static IEnumerable<DictionaryEntry> GetEnumerable(IDictionaryEnumerator enumerator)
        {
            for (; enumerator.MoveNext();)
            {
                yield return enumerator.Entry;
            }

        }

        public static void RemoveAll<V>(this ICollection<V> list, IEnumerable<V> enumerable)
        {
            foreach (var item in enumerable)
            {
                list.Remove(item);
            }

        }

        public static void Replace<V>(this IList<V> list, IEnumerable<V> enumerable)
        {
            list.Clear();

            foreach (var item in enumerable)
            {
                list.Add(item);
            }

        }

        public static T Get<T>(this IList<T> list, int index, T fallback = default)
        {
            if (0 <= index && index < list.Count)
            {
                return list[index];
            }

            return fallback;
        }

        public static T Dequeue<T>(this IList<T> list, T fallback = default)
        {
            if (list.Count > 0)
            {
                var element = list.Get(0);
                list.RemoveAt(0);

                return element;
            }
            else
            {
                return fallback;
            }

        }

        public static IEnumerable<T> Dequeue<T>(this IList<T> list, int count, T fallback = default)
        {
            for (var i = 0; i < count; i++)
            {
                yield return list.Dequeue(fallback);
            }

        }

        public static void Shuffle<T>(this IList<T> list, Random rand)
        {
            for (var i = 0; i < list.Count; i++)
            {
                var j = rand.Next(0, i);

                var temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }

        }

        public static IEnumerable<T> Randomly<T>(this IEnumerable<T> enumerable, Random rand)
        {
            var array = enumerable.ToArray();
            array.Shuffle(rand);

            return array;
        }

    }

}
