using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class ObjectUtils
    {
        public const string NULL = "NULL";

        public static string ToStringSimple(string name, List<string> names, Dictionary<string, object> map)
        {
            string str = "";

            for (int i = 0; i < names.Count; i++)
            {
                string key = names[i];
                object value = null;
                map.TryGetValue(key, out value);

                str += string.Format("{0}={1}", ObjectUtils.ToString(key), ObjectUtils.ToString(value));

                if (i + 1 < names.Count)
                {
                    str += ",";
                }

            }


            return name + "{" + str + "}";
        }

        public static string ToStringSimple(string name, params object[] objs)
        {
            string str = "";

            if (objs == null)
            {
                str = NULL;
            }
            else
            {
                for (int i = 0; i < objs.Length; i += 2)
                {
                    object obj1 = objs[i];
                    object obj2 = null;

                    if (i + 1 < objs.Length)
                    {
                        obj2 = objs[i + 1];
                    }

                    str += string.Format("{0}={1}", obj1, ObjectUtils.ToString(obj2));

                    if (i + 2 < objs.Length)
                    {
                        str += ",";
                    }

                }

            }

            return name + "{" + str + "}";
        }

        public static string ToString(string name, object obj)
        {
            return ToString(name, obj, NULL);
        }

        public static string ToString(object obj, string nullString)
        {
            return ToString(null, obj, NULL);
        }

        public static string ToString(string name, object obj, string nullString)
        {
            if (obj == null)
            {
                return nullString;
            }

            if (obj is string || obj.GetType().IsPrimitive)
            {
                return obj.ToString();
            }
            else if (obj is IDictionary)
            {
                return EnumerableUtils.ToString(name, (IDictionary)obj);
            }
            else if (obj is IEnumerable)
            {
                return EnumerableUtils.ToString(name, (IEnumerable)obj);
            }

            if (name == null)
            {
                return obj.ToString();
            }
            else
            {
                return WrapName(name, obj.ToString());
            }

        }
        public static string ToString(object obj)
        {
            return ToString(null, obj);
        }

        public static string WrapName(string name, string str)
        {
            return name + "{" + str + "}";
        }

        public static void DisposeQuietly(IDisposable disposable)
        {
            try
            {
                if (disposable != null)
                {
                    disposable.Dispose();
                }

            }
            catch
            {

            }

        }

    }

}
