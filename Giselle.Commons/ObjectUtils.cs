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

        public static int CompareTo<T>(T o1, object o2) where T : struct, IComparable<T>
        {
            var t = o2 as T?;

            if (t.HasValue == true)
            {
                return CompareTo(o1, t);
            }
            else
            {
                return 1;
            }

        }

        public static bool CompareTo<T>(in T o1, in T o2, Comparison<T> comparison, out int result)
        {
            if (o1 is ValueType)
            {
                result = comparison(o1, o2);
            }
            else if (o1 == null)
            {
                result = o2 == null ? 0 : 1;
            }
            else if (o2 == null)
            {
                result = -1;
            }
            else
            {
                result = comparison(o1, o2);
            }

            return result != 0;
        }

        public static bool CompareTo<T>(in T o1, in T o2, out int result) where T : IComparable<T>
        {
            return CompareTo(o1, o2, (x, y) => x.CompareTo(y), out result);
        }

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

        public static int GetHashCodeSafe<T>(this T obj)
        {
            if (obj is ValueType || obj != null)
            {
                return obj.GetHashCode();
            }

            return 0;
        }

        public static int AccumulateHashCode<T>(this int hash, T value)
        {
            return hash * HashMultiplier + GetHashCodeSafe(value);
        }

        public static int AccumulateHashCode<T>(this int hash, IEnumerable<T> collection)
        {
            foreach (var obj in collection)
            {
                hash = AccumulateHashCode(hash, obj);
            }

            return hash;
        }

        public static int SelectHashCodeParams<T>(this T first, params T[] array)
        {
            var hash = AccumulateHashCode(HashSeed, first);
            return AccumulateHashCode(hash, array);
        }

        public static int SelectHashCode<T>(this IEnumerable<T> collection)
        {
            return AccumulateHashCode(HashSeed, collection);
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

        public static bool EqualsType<T>(this T o1, T o2)
        {
            if (o1 is ValueType)
            {
                if (o2 is ValueType)
                {
                    return o1.GetType().Equals(o2.GetType());
                }
                else
                {
                    return false;
                }

            }
            else if (o2 is ValueType)
            {
                return false;
            }
            else if (o1 == null)
            {
                return o2 == null;
            }
            else if (o2 == null)
            {
                return false;
            }

            return o1.GetType().Equals(o2.GetType());
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

        public static void ExecuteQuietly<T>(this Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {

            }

        }

        public static void ExecuteQuietly<T>(this T obj, Action<T> action)
        {
            try
            {
                action(obj);
            }
            catch (Exception)
            {

            }

        }

        public static void DisposeQuietly(this IDisposable obj)
        {
            ExecuteQuietly(obj, o => o.Dispose());
        }

    }

}
