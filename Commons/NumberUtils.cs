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
        public static double? ToDoubleNullable(string str)
        {
            return double.TryParse(str, out var value) ? value : new double?();
        }

        public static float ToFloat(string str, float fallback = 0.0F)
        {
            return float.TryParse(str, out var value) ? value : fallback;
        }

        public static float? ToFloatNullable(string str)
        {
            return float.TryParse(str, out var value) ? value : new float?();
        }

        public static ulong ToULong(string str, ulong fallback = 0)
        {
            return ulong.TryParse(str, out var value) ? value : fallback;
        }

        public static ulong? ToULongNullable(string str)
        {
            return ulong.TryParse(str, out var value) ? value : new ulong?();
        }

        public static long ToLong(string str, long fallback = 0)
        {
            return long.TryParse(str, out var value) ? value : fallback;
        }

        public static long? ToLongNullable(string str)
        {
            return long.TryParse(str, out var value) ? value : new long?();
        }

        public static uint ToUInt(string str, uint fallback = 0)
        {
            return uint.TryParse(str, out var value) ? value : fallback;
        }

        public static uint? ToUIntNullable(string str)
        {
            return uint.TryParse(str, out var value) ? value : new uint?();
        }

        public static int ToInt(string str, int fallback = 0)
        {
            return int.TryParse(str, out var value) ? value : fallback;
        }

        public static int? ToIntNullable(string str)
        {
            return int.TryParse(str, out var value) ? value : new int?();
        }

        public static ushort ToUShort(string str, ushort fallback = 0)
        {
            return ushort.TryParse(str, out var value) ? value : fallback;
        }

        public static ushort? ToUShortNullable(string str)
        {
            return ushort.TryParse(str, out var value) ? value : new ushort?();
        }

        public static short ToShort(string str, short fallback = 0)
        {
            return short.TryParse(str, out var value) ? value : fallback;
        }

        public static short? ToShortNullable(string str)
        {
            return short.TryParse(str, out var value) ? value : new short?();
        }

        public static char ToChar(string str, char fallback = '\0')
        {
            return char.TryParse(str, out var value) ? value : fallback; 
        }

        public static char? ToCharNullable(string str)
        {
            return char.TryParse(str, out var value) ? value : new char?();
        }

        public static byte ToByte(string str, byte fallback = 0)
        {
            return byte.TryParse(str, out var value) ? value : fallback;
        }

        public static byte? ToByteNullable(string str)
        {
            return byte.TryParse(str, out var value) ? value : new byte?();
        }

        public static sbyte ToSByte(string str, sbyte fallback = 0)
        {
            return sbyte.TryParse(str, out var value) ? value : fallback;
        }

        public static sbyte? ToSByteNullable(string str)
        {
            return sbyte.TryParse(str, out var value) ? value : new sbyte?();
        }

        public static bool ToBool(string str, bool fallback = false)
        {
            return bool.TryParse(str, out var value) ? value : fallback;
        }

        public static bool? ToBoolNullable(string str)
        {
            return bool.TryParse(str, out var value) ? value : new bool?();
        }

    }

}
