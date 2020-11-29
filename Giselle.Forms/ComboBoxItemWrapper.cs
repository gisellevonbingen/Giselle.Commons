using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public interface IComboBoxItemWrapper
    {
        object Value { get; }
        string Text { get; }
    }

    public class ComboBoxItemWrapper : IComboBoxItemWrapper
    {
        public object Value { get; private set; }
        public string Text { get; private set; }

        public ComboBoxItemWrapper(object value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

    }

    public class ComboBoxItemWrapper<T> : IComboBoxItemWrapper
    {
        public T Value { get; private set; }
        public string Text { get; private set; }

        public ComboBoxItemWrapper(T value, string text)
        {
            this.Value = value;
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        object IComboBoxItemWrapper.Value
        {
            get { return this.Value; }
        }

    }

}
