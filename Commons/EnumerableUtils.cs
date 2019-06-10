using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class EnumerableUtils
    {
        public static T[] CreateArray<T>(int count, Func<int, T> func)
        {
            T[] array = new T[count];

            for (int i = 0; i < count; ++i)
            {
                array[i] = func(i);
            }

            return array;
        }

        public static string Join(this IEnumerable collection, string separator)
        {
            return Join(collection, separator, (obj) => { return ObjectUtils.ToString(obj); });
        }

        public static string Join(this IEnumerable collection, string separator, Func<object, string> func)
        {
            string str = "";
            var enumerator = collection.GetEnumerator();
            bool passFirst = false;


            foreach (object obj in collection)
            {
                if (passFirst)
                {
                    str += separator;
                }
                else
                {
                    passFirst = true;
                }

                str += func.Invoke(obj);
            }

            return str;
        }

        public static string Join<T>(this IEnumerable<T> collection, string separator)
        {
            return Join(collection, separator, (obj) => { return ObjectUtils.ToString(obj); });
        }

        public static string Join<T>(this IEnumerable<T> collection, string separator, Func<T, string> func)
        {
            string str = "";
            IEnumerator enumerator = collection.GetEnumerator();
            bool passFirst = false;

            foreach (T obj in collection)
            {
                if (passFirst)
                {
                    str += separator;
                }
                else
                {
                    passFirst = true;
                }

                str += func.Invoke(obj);
            }

            return str;
        }

        public static string ToString(string name, IDictionary dictionary)
        {
            string str = "";

            if (dictionary == null)
            {
                str = ObjectUtils.NULL;
            }
            else
            {
                var list = dictionary.OfType<DictionaryEntry>().ToArray();
                str = Join(list, ", ", entry => ObjectUtils.ToString(entry.Key) + "=" + ObjectUtils.ToString(entry.Value));
            }

            return ObjectUtils.WrapName(name, str);
        }
        public static string ToString(string name, IEnumerable collection)
        {
            string str = "";

            if (collection == null)
            {
                str = ObjectUtils.NULL;
            }
            else
            {
                str = Join(collection, ", ");
            }

            return ObjectUtils.WrapName(name, str);
        }
        public static string ToString(IEnumerable collection)
        {
            return ToString("", collection);
        }

        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var c in collection)
            {
                action(c);
            }

        }
        public static void Foreach(this IEnumerable collection, Action<object> action)
        {
            foreach (var c in collection)
            {
                action(c);
            }

        }
        public static void Foreach(this IEnumerable collection, Action<object, int> action)
        {
            int index = 0;

            foreach (var obj in collection)
            {
                action(obj, index);
                index++;
            }
        }
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T, int> action)
        {
            int index = 0;

            foreach (T obj in collection)
            {
                action(obj, index);
                index++;
            }

        }

        public static Dictionary<int, object> Where(this IEnumerable collection, Predicate<object> predicate)
        {
            int index = 0;
            Dictionary<int, object> map = new Dictionary<int, object>();

            foreach (object obj in collection)
            {
                if (predicate(obj))
                {
                    map[index] = obj;
                }

                index++;
            }

            return map;
        }
        public static Dictionary<int, T> Where<T>(this IEnumerable collection, Predicate<T> predicate)
        {
            int index = 0;
            Dictionary<int, T> map = new Dictionary<int, T>();

            foreach (object obj in collection)
            {
                T t = (T)obj;

                if (predicate(t))
                {
                    map[index] = t;
                }

                index++;
            }

            return map;
        }

    }

}
