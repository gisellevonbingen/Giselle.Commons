using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public class EventDrivenList<T> : SimpleList<T>, IList<T>
    {
        public event EventHandler<CollectionEventArgs<T>> Added;
        public event EventHandler<CollectionEventArgs<T>> Removed;

        public EventDrivenList()
        {

        }

        T IList<T>.this[int index]
        {
            get => this.List[index];
            set => throw new NotImplementedException();
        }

        protected virtual void OnAdded(CollectionEventArgs<T> e)
        {
            this.Added?.Invoke(this, e);
        }

        protected virtual void OnRemoved(CollectionEventArgs<T> e)
        {
            this.Removed?.Invoke(this, e);
        }

        public void Insert(int index, T item)
        {
            this.List.Insert(index, item);
            this.OnAdded(new CollectionEventArgs<T>(new T[] { item }));
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            var array = collection.ToArray();
            this.List.InsertRange(index, array);
            this.OnAdded(new CollectionEventArgs<T>(array));
        }

        public override void Add(T item)
        {
            this.List.Add(item);
            this.OnAdded(new CollectionEventArgs<T>(new T[] { item }));
        }

        public void RemoveAt(int index)
        {
            var item = this[index];
            this.Remove(item);
        }

        public void RemoveAll(Func<T, bool> predicate)
        {
            var collection = this.Where(predicate).ToArray();
            this.RemoveAll(collection);
        }

        public override bool Remove(T item)
        {
            if (this.List.Remove(item) == true)
            {
                this.OnRemoved(new CollectionEventArgs<T>(new T[] { item }));
                return true;
            }

            return false;
        }

        public int IndexOf(T item)
        {
            return this.List.IndexOf(item);
        }

    }

}
