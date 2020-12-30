using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons.Tags
{
    public class SimpleNameTags<T> : SimpleTags<T>
        where T : INameTag
    {
        public T Find(string name)
        {
            return this.Values.FirstOrDefault(t => t.Name.Equals(name));
        }

    }

}
