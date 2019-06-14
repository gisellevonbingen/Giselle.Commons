using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class EnumUtils
    {
        public static long ToLong<E>(E value) where E : struct, IConvertible
        {
            return value.ToInt64(null);
        }

        public static E ToValue<E>(long value) where E : struct, IConvertible
        {
            return (E)Enum.ToObject(typeof(E), value);
        }

        public static E[] GetValues<E>() where E : struct, IConvertible
        {
            return (E[])Enum.GetValues(typeof(E));
        }

        public static E Parse<E>(string name) where E : struct, IConvertible
        {
            return (E)Enum.Parse(typeof(E), name);
        }

        public static E Parse<E>(string name, E defaultValue) where E : struct, IConvertible
        {
            return Parse(name, false, defaultValue);
        }

        public static E Parse<E>(string name, bool ignoreCase, E defaultValue) where E : struct, IConvertible
        {
            E value = defaultValue;

            if (Enum.TryParse<E>(name, ignoreCase, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }

        }

    }

}
