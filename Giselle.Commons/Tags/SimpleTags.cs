using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Tags
{
    public class SimpleTags<T> where T : ITag
    {
        public List<T> Values { get; private set; }

        public SimpleTags()
        {
            this.Values = new List<T>();
        }

        public virtual T Register(T value)
        {
            this.Values.Add(value);
            return value;
        }

    }

}
