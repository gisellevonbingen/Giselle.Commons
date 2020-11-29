using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Enums
{
    public static class EnumUtils
    {
        private static readonly Dictionary<Type, IEnumCache> Caches = new Dictionary<Type, IEnumCache>();

        public static IEnumCache Cache(Type type)
        {
            if (Caches.TryGetValue(type, out var cached) == true)
            {
                return cached as IEnumCache;
            }
            else
            {
                var gtype = typeof(EnumCache<>).MakeGenericType(type);
                var created = (IEnumCache)gtype.GetMethod("Create").Invoke(gtype, new object[0]);
                Caches[type] = created;

                return created;
            }

        }

        public static EnumCache<T> Cache<T>()
        {
            var type = typeof(T);
            return (EnumCache<T>)Cache(type);
        }

        public static EnumComparer<T> Comparer<T>() => Cache<T>().Comparer;

        public static T ToEnum<T>(this string name) => Cache<T>().Parse(name);
        public static T ToEnum<T>(this string name, T fallback) => Cache<T>().ParseSafe(name, fallback);

        public static T Parse<T>(string name) => Cache<T>().Parse(name);

        public static T ParseSafe<T>(string name, T fallback) => Cache<T>().ParseSafe(name, fallback);

        public static bool TryParse<T>(string name, out T value) => Cache<T>().TryParse(name, out value);

        public static string[] Names<T>() => Cache<T>().Names;

        public static T[] Values<T>() => Cache<T>().Values;

        public static IEnumerable<T?> NullableValues<T>() where T : struct => Values<T>().Select(r => new T?(r));

        public static IEnumerable<T?> NullWithNullableValues<T>() where T : struct => new T?().InsertFirst(NullableValues<T>());
    }

}
