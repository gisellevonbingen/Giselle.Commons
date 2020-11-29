using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        protected List<T> List { get; }

        public ReadOnlyList()
        {
            this.List = new List<T>();
        }

        public virtual T this[int index] => this.List[index];

        public virtual int Count => this.List.Count();

        public virtual IEnumerator<T> GetEnumerator() => this.List.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}
