using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class StringUtils
    {
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

    }

}
