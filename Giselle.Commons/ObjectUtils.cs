using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons.Collections;

namespace Giselle.Commons
{
    public static class ObjectUtils
    {
        public static int HashSeed { get; } = 17;
        public static int HashMultiplier { get; } = 31;

        public static IEnumerable<T> InsertFirst<T>(this T first, IEnumerable<T> collection)
        {
            yield return first;

            foreach (var e in collection)
            {
                yield return e;
            }

        }

        public static IEnumerable<T> InsertLast<T>(this T first, IEnumerable<T> collection)
        {
            foreach (var e in collection)
            {
                yield return e;
            }

            yield return first;
        }

        public static int GetHashCodeSafe<T>(T obj)
        {
            if (obj is ValueType || obj != null)
            {
                return obj.GetHashCode();
            }

            return 0;
        }

        public static int AccumulateHashCode<T>(int hash, T value)
        {
            return hash * HashMultiplier + GetHashCodeSafe(value);
        }

        public static int AccumulateHashCode<T>(int hash, IEnumerable<T> collection)
        {
            foreach (var obj in collection)
            {
                hash = AccumulateHashCode(hash, obj);
            }

            return hash;
        }

        public static int SelectHashCode<T>(this T first, params T[] array)
        {
            var hash = AccumulateHashCode(HashSeed, first);
            return AccumulateHashCode(hash, array);
        }

        public static int SelectHashCode<T>(this IEnumerable<T> collection)
        {
            return AccumulateHashCode(HashSeed, collection);
        }

        public static bool EqualsTypeStruct<T>(this T o1, T o2) where T : struct
        {
            return o1.GetType().Equals(o2.GetType());
        }

        public static bool EqualsTypeClass<T>(this T o1, T o2) where T : class
        {
            if (o1 == null)
            {
                return o2 == null;
            }
            else if (o2 == null)
            {
                return false;
            }

            return o1.GetType().Equals(o2.GetType());
        }

        public static T ConsumeOwn<T>(this T instance, Action<T> action)
        {
            if (instance is ValueType || instance != null)
            {
                action(instance);
                return instance;
            }
            else
            {
                return default;
            }

        }

        public static V ConsumeSelect<T, V>(this T instance, Func<T, V> action, V fallback = default)
        {
            if (instance is ValueType || instance != null)
            {
                return action(instance);
            }
            else
            {
                return fallback;
            }

        }

        public static bool EqualsType<T>(this T instance, T other) where T : class, IEquatable<T>
        {
            return other != null && instance.GetType().Equals(other.GetType()) == true;
        }

        public static string ToString<T>(T value)
        {
            if (value == null)
            {
                return StringUtils.NULL;
            }
            else if (value is string || value.GetType().IsPrimitive == true || value is Enum)
            {
                return value.ToString();
            }
            else if (value is IDictionary dictionary)
            {
                var list = CollectionUtils.GetEnumerable(dictionary.GetEnumerator());
                return ToString(list.Select(e => $"{ToString(e.Key)}={ToString(e.Value)}").ToArray());
            }
            else if (value is IEnumerable enumerable)
            {
                return $"[{string.Join(", ", enumerable.OfType<object>().Select(o => ToString(o)).ToArray())}]";
            }

            return value.ToString();
        }

        public static void ExecuteQuietly<T>(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {

            }

        }

        public static void ExecuteQuietly<T>(T obj, Action<T> action)
        {
            try
            {
                action(obj);
            }
            catch (Exception)
            {

            }

        }

        public static void DisposeQuietly(IDisposable obj)
        {
            ExecuteQuietly(obj, o => o.Dispose());
        }

    }

}
