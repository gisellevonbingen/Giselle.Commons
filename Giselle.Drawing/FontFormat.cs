using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace Giselle.Drawing
{
    public struct FontFormat : IComparable, IComparable<FontFormat>, IEquatable<FontFormat>
    {
        public float Size { get; private set; }
        public FontStyle Style { get; private set; }

        public FontFormat(float size, FontStyle style)
            : this()
        {
            this.Size = size;
            this.Style = style;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((FontFormat)obj);
        }

        public bool Equals(FontFormat other)
        {
            if (this.Size.Equals(other.Size) == false)
            {
                return false;
            }

            if (this.Style.Equals(other.Style) == false)
            {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hash = ObjectUtils.HashSeed;
            hash = hash.AccumulateHashCode(this.Size);
            hash = hash.AccumulateHashCode(this.Style);
            return hash;
        }

        public int CompareTo(object obj)
        {
            return ObjectUtils.CompareTo(this, obj);
        }

        public int CompareTo(FontFormat other)
        {
            if (ObjectUtils.CompareTo(this.Size, other.Size, out int c)) return c;
            if (ObjectUtils.CompareTo(this.Style, other.Style, (x, y) => x.CompareTo(y), out c)) return c;

            return c;
        }

    }

}