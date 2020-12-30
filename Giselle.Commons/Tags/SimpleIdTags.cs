using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Tags
{
    public class SimpleIdTags<T, V> : SimpleTags<T>
        where T : IIdTag<V>
        where V : IEquatable<V>
    {
        public T Find(V id)
        {
            return this.Values.FirstOrDefault(t => EqualityComparer<V>.Default.Equals(t.Id, id));
        }

    }

}
