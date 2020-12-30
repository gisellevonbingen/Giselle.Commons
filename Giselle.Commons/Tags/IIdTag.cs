using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Tags
{
    public interface IIdTag<T> : ITag where T : IEquatable<T>
    {
        T Id { get; }
    }

}
