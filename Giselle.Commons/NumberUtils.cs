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

        public static bool IsGeneral(this float value)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
            {
                return false;
            }

            return true;
        }

        public static string ToNumberString(this IFormattable formattable)
        {
            return formattable.ToString("#,##0", null);
        }

        public static double ToDouble(this string str, double fallback = 0.0D)
        {
            return double.TryParse(str, out var value) ? value : fallback;
        }
        public static double? ToDoubleNullable(this string str)
        {
            return double.TryParse(str, out var value) ? value : new double?();
        }

        public static float ToFloat(this string str, float fallback = 0.0F)
        {
            return float.TryParse(str, out var value) ? value : fallback;
        }

        public static float? ToFloatNullable(this string str)
        {
            return float.TryParse(str, out var value) ? value : new float?();
        }

        public static ulong ToULong(this string str, ulong fallback = 0)
        {
            return ulong.TryParse(str, out var value) ? value : fallback;
        }

        public static ulong? ToULongNullable(this string str)
        {
            return ulong.TryParse(str, out var value) ? value : new ulong?();
        }

        public static long ToLong(this string str, long fallback = 0)
        {
            return long.TryParse(str, out var value) ? value : fallback;
        }

        public static long? ToLongNullable(this string str)
        {
            return long.TryParse(str, out var value) ? value : new long?();
        }

        public static uint ToUInt(this string str, uint fallback = 0)
        {
            return uint.TryParse(str, out var value) ? value : fallback;
        }

        public static uint? ToUIntNullable(this string str)
        {
            return uint.TryParse(str, out var value) ? value : new uint?();
        }

        public static int ToInt(this string str, int fallback = 0)
        {
            return int.TryParse(str, out var value) ? value : fallback;
        }

        public static int? ToIntNullable(this string str)
        {
            return int.TryParse(str, out var value) ? value : new int?();
        }

        public static ushort ToUShort(this string str, ushort fallback = 0)
        {
            return ushort.TryParse(str, out var value) ? value : fallback;
        }

        public static ushort? ToUShortNullable(this string str)
        {
            return ushort.TryParse(str, out var value) ? value : new ushort?();
        }

        public static short ToShort(this string str, short fallback = 0)
        {
            return short.TryParse(str, out var value) ? value : fallback;
        }

        public static short? ToShortNullable(this string str)
        {
            return short.TryParse(str, out var value) ? value : new short?();
        }

        public static char ToChar(this string str, char fallback = '\0')
        {
            return char.TryParse(str, out var value) ? value : fallback;
        }

        public static char? ToCharNullable(this string str)
        {
            return char.TryParse(str, out var value) ? value : new char?();
        }

        public static byte ToByte(this string str, byte fallback = 0)
        {
            return byte.TryParse(str, out var value) ? value : fallback;
        }

        public static byte? ToByteNullable(this string str)
        {
            return byte.TryParse(str, out var value) ? value : new byte?();
        }

        public static sbyte ToSByte(this string str, sbyte fallback = 0)
        {
            return sbyte.TryParse(str, out var value) ? value : fallback;
        }

        public static sbyte? ToSByteNullable(this string str)
        {
            return sbyte.TryParse(str, out var value) ? value : new sbyte?();
        }

        public static bool ToBool(this string str, bool fallback = false)
        {
            return bool.TryParse(str, out var value) ? value : fallback;
        }

        public static bool? ToBoolNullable(this string str)
        {
            return bool.TryParse(str, out var value) ? value : new bool?();
        }

        public static bool GetBit(this int value, int index)
        {
            var mask = 1 << index;
            return value.GetMask(mask);
        }

        public static bool GetMask(this int value, int mask)
        {
            return (value & mask) == mask;
        }

        public static int SetBit(this int value, int index, bool flag)
        {
            var mask = 1 << index;
            return value.SetMask(mask, flag);
        }

        public static int SetMask(this int value, int mask, bool flag)
        {
            if (flag == true)
            {
                return value | mask;
            }
            else
            {
                return value & ~mask;
            }

        }

        public static bool GetBit(this long value, int index)
        {
            var mask = 1L << index;
            return value.GetMask(mask);
        }

        public static bool GetMask(this long value, long mask)
        {
            return (value & mask) == mask;
        }

        public static long SetBit(this long value, int index, bool flag)
        {
            var mask = 1L << index;
            return value.SetMask(mask, flag);
        }

        public static long SetMask(this long value, long mask, bool flag)
        {
            if (flag == true)
            {
                return value | mask;
            }
            else
            {
                return value & ~mask;
            }

        }

    }

}
