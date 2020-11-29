using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Giselle.Forms
{
    public static class ComboBoxWrapperUtils
    {
        public static ComboBoxItemWrapper<V>[] GetItems<V>(this ComboBox comboBox)
        {
            return comboBox.Items.OfType<ComboBoxItemWrapper<V>>().ToArray();
        }

        public static ComboBoxItemWrapper<V> GetItem<V>(this ComboBox comboBox, V value)
        {
            var items = comboBox.GetItems<V>();

            if (value is IEquatable<V>)
            {
                var equatable = (IEquatable<V>)value;
                var item = items.FirstOrDefault(i => equatable.Equals(i.Value));
                return item;
            }
            else
            {
                var item = items.FirstOrDefault(i => object.Equals(value, i.Value));
                return item;
            }

        }

        public static V GetSelectedItem<V>(this ComboBox comboBox)
        {
            var item = comboBox.SelectedItem;

            if (item != null)
            {
                return (item as ComboBoxItemWrapper<V>).Value;
            }
            else
            {
                return default(V);
            }

        }

        public static void Select<T, V>(this ComboBox comboBox, V value)
        {
            var item = comboBox.GetItem<V>(value);
            comboBox.SelectedItem = item;
        }

    }

}
