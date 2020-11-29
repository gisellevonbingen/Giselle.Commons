using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Enums
{
    public interface IEnumCache
    {
        string[] Names { get; }
        object[] Values { get; }

        object Parse(string name);
        object ParseSafe(string name, object fallback);
    }

    public class EnumCache<T> : IEnumCache
    {
        public static EnumCache<T> Create()
        {
            return new EnumCache<T>(typeof(T));
        }

        public Type Type { get; }
        private readonly string[] _Names;
        private readonly Dictionary<string, T> Map;

        public EnumComparer<T> Comparer { get; }

        public EnumCache(Type type)
        {
            this.Type = type;
            this._Names = Enum.GetNames(type);

            var map = this.Map = new Dictionary<string, T>();

            foreach (var name in this.Names)
            {
                map[name] = (T)Enum.Parse(type, name);
            }

            this.Comparer = new EnumComparer<T>();
        }

        object IEnumCache.Parse(string name)
        {
            return this.Parse(name);
        }

        public T Parse(string name)
        {
            if (this.TryParse(name, out var value) == true)
            {
                return value;
            }
            else
            {
                throw new FormatException($"Invalid Enum : {this.Type.Name}.{name}");
            }

        }

        public T ParseSafe(string name, T fallback)
        {
            return this.TryParse(name, out var value) ? value : fallback;
        }

        object IEnumCache.ParseSafe(string name, object fallback)
        {
            return this.ParseSafe(name, (T)fallback);
        }

        public bool TryParse(string name, out T value)
        {
            if (string.IsNullOrEmpty(name) == true)
            {
                value = default;
                return false;
            }
            else
            {
                return this.Map.TryGetValue(name, out value);
            }

        }

        public string[] Names => this._Names.ToArray();

        public T[] Values => this.Map.Values.ToArray();
        object[] IEnumCache.Values => this.Values.OfType<object>().ToArray();
    }

}
