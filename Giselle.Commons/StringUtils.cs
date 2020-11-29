using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons.Collections;

namespace Giselle.Commons
{
    public static class StringUtils
    {
        public static string PlaceholderPrefix { get; } = "{=";
        public static string PlaceholderSuffix { get; } = "}";
        public static string NULL { get; } = "$NULL$";

        public static string[] Split(this string str, string separator)
        {
            return str.Split(new string[] { separator }, StringSplitOptions.None);
        }

        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new string[] { separator }, options);
        }

        public static string[] Split(this string str, string separator, int count, StringSplitOptions options)
        {
            return str.Split(new string[] { separator }, count, options);
        }

        public static string RemoveSuffx(this string str, char suffix)
        {
            return RemoveSuffx(str, suffix.ToString());
        }

        public static string RemoveSuffx(this string str, string suffix)
        {
            if (str == null)
            {
                return null;
            }

            if (str.EndsWith(suffix) == true)
            {
                return str.Substring(0, str.Length - suffix.Length);
            }

            return str;
        }

        public static string AddSuffx(this string str, char suffix)
        {
            return AddSuffx(str, suffix.ToString());
        }

        public static string AddSuffx(this string str, string suffix)
        {
            if (str == null)
            {
                return null;
            }

            if (str.EndsWith(suffix) == false)
            {
                return str + suffix;
            }

            return str;
        }

        public static string RemovePrefix(this string str, char prefix)
        {
            return RemovePrefix(str, prefix.ToString());
        }

        public static string RemovePrefix(this string str, string prefix)
        {
            if (str == null)
            {
                return null;
            }

            if (str.StartsWith(prefix) == true)
            {
                return str.Substring(prefix.Length);
            }

            return str;
        }

        public static string AddPrefix(this string str, char prefix)
        {
            return AddPrefix(str, prefix.ToString());
        }

        public static string AddPrefix(this string str, string prefix)
        {
            if (str == null)
            {
                return null;
            }

            if (str.StartsWith(prefix) == false)
            {
                return prefix + str;
            }

            return str;
        }

        public static string ReplacePlaceholder(this string text, string name, object value)
        {
            return ReplacePlaceholder(text, name, value, PlaceholderPrefix, PlaceholderSuffix);
        }

        public static string ReplacePlaceholder(this string text, string name, object value, string prefix, string suffix)
        {
            if (text == null)
            {
                return null;
            }

            string v = "";

            if (value != null)
            {
                v = value.ToString();
            }

            var placeholder = ToPlaceholder(name, prefix, suffix);

            return text.Replace(placeholder, v);
        }

        public static string ToPlaceholder(this string name)
        {
            return ToPlaceholder(name, PlaceholderPrefix, PlaceholderSuffix);
        }

        public static string ToPlaceholder(this string name, string prefix, string suffix)
        {
            return new StringBuilder().Append(prefix).Append(name).Append(suffix).ToString();
        }

        public static string ReplacePlaceholder(this string text, Dictionary<string, object> placeholders)
        {
            return ReplacePlaceholder(text, placeholders, PlaceholderPrefix, PlaceholderSuffix);
        }

        public static bool FindPlaceholderIndex(this string text, out int si, out int ei)
        {
            return FindPlaceholderIndex(text, PlaceholderPrefix, PlaceholderSuffix, out si, out ei);
        }

        public static bool FindPlaceholderIndex(this string text, string prefix, string suffix, out int si, out int ei)
        {
            if (text != null)
            {
                var si2 = text.IndexOf(prefix);
                var ei2 = si2 > -1 ? text.IndexOf(suffix, si2) : -1;

                if (si2 != -1 && ei2 != -1)
                {
                    si = si2 + prefix.Length;
                    ei = ei2;
                    return true;
                }

            }

            si = -1;
            ei = -1;
            return false;
        }

        public static string ReplacePlaceholder(this string text, Dictionary<string, object> placeholders, string prefix, string suffix)
        {
            if (text == null)
            {
                return null;
            }

            if (placeholders != null)
            {
                while (true)
                {
                    var si = text.IndexOf(prefix);
                    var ei = si > -1 ? text.IndexOf(suffix, si) : -1;

                    if (si == -1 || ei == -1)
                    {
                        break;
                    }
                    else
                    {
                        var se = si + prefix.Length;
                        var name = text.Substring(se, ei - se);
                        text = ReplacePlaceholder(text, name, placeholders.GetSafe(name, NULL));
                    }

                }

            }

            return text;
        }

    }

}
