using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Collections
{
    public class CollectionEventArgs<T>  : EventArgs
    {
        public CollectionEventArgs(T[] elements)
        {
            this.Elements = elements;
        }

        public T[] Elements { get; }
    }

}
