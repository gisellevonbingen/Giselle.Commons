using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public abstract class SimpleList<T> : ReadOnlyList<T>, ICollection<T>
    {
        public bool IsReadOnly => false;

        public SimpleList()
        {

        }

        public SimpleList(IEnumerable<T> collection)
        {
            this.AddRange(collection);
        }

        public virtual void Replace(IEnumerable<T> collection)
        {
            this.Clear();
            this.AddRange(collection);
        }

        public abstract void Add(T item);

        public abstract bool Remove(T item);

        public virtual void AddRange(IEnumerable<T> collection)
        {
            foreach (var element in collection)
            {
                this.Add(element);
            }

        }

        public virtual void Clear()
        {
            foreach (var element in this.ToArray())
            {
                this.Remove(element);
            }

        }

        public bool Contains(T item)
        {
            return this.List.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.List.CopyTo(array, arrayIndex);
        }

    }

}
