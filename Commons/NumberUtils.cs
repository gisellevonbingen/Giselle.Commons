using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Commons
{
    public static class NumberUtils
    {
        public const int ByteBits = 8;

        public static byte Rotate(byte value, int amount)
        {
            if (amount == 0)
            {
                return value;
            }

            amount = amount % ByteBits;

            if (amount > 0)
            {
                amount = +amount;
                return (byte)((value << amount) | (value >> (ByteBits - amount)));
            }
            else
            {
                amount = -amount;
                return (byte)((value >> amount) | (value << (ByteBits - amount)));
            }

        }

        public static double ToDouble(string str, double fallback = 0.0D)
        {
            return double.TryParse(str, out var value) ? value : fallback;
        }

        public static float ToFloat(string str, float fallback = 0.0F)
        {
            return float.TryParse(str, out var value) ? value : fallback;
        }

        public static ulong ToULong(string str, ulong fallback = 0)
        {
            return ulong.TryParse(str, out var value) ? value : fallback;
        }

        public static long ToLong(string str, long fallback = 0)
        {
            return long.TryParse(str, out var value) ? value : fallback;
        }

        public static uint ToUInt(string str, uint fallback = 0)
        {
            return uint.TryParse(str, out var value) ? value : fallback;
        }

        public static int ToInt(string str, int fallback = 0)
        {
            return int.TryParse(str, out var value) ? value : fallback;
        }

        public static ushort ToUShort(string str, ushort fallback)
        {
            return ushort.TryParse(str, out var value) ? value : fallback;
        }

        public static short ToShort(string str, short fallback = 0)
        {
            return short.TryParse(str, out var value) ? value : fallback;
        }

        public static char ToChar(string str, char fallback = '\0')
        {
            return char.TryParse(str, out var value) ? value : fallback; 
        }

        public static byte ToByte(string str)
        {
            return ToByte(str, 0);
        }
        public static byte ToByte(string str, byte fallback = 0)
        {
            return byte.TryParse(str, out var value) ? value : fallback;
        }

        public static sbyte ToSByte(string str, sbyte fallback = 0)
        {
            return sbyte.TryParse(str, out var value) ? value : fallback;
        }

    }

}
