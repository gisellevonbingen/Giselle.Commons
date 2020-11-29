using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giselle.Commons;

namespace Giselle.Forms
{
    public class NumberRange
    {
        public double? Min { get; set; }
        public double? Max { get; set; }
        public int? Dps { get; set; }

        public NumberRange()
        {

        }

        public NumberRange(NumberRange other)
        {
            this.Min = other.Min;
            this.Max = other.Max;
            this.Dps = other.Dps;
        }

        public NumberRange Clone()
        {
            return new NumberRange(this);
        }

        public static NumberErrorCause TryParse(string str, NumberRange range, out double value)
        {
            var cause = NumberErrorCause.None;
            var parse = str.ToDoubleNullableGeneal();
            value = parse ?? 0.0D;

            if (parse.HasValue == false)
            {
                cause = NumberErrorCause.Invalid;
            }
            else if (range != null)
            {
                int dotIndex = str.IndexOf(".");

                if (dotIndex > -1 && str.Substring(dotIndex + 1).Length > range.Dps)
                {
                    cause = NumberErrorCause.DpsOver;
                }
                else if (CheckMin(value, range) == false)
                {
                    cause = NumberErrorCause.MinOver;
                }
                else if (CheckMax(value, range) == false)
                {
                    cause = NumberErrorCause.MaxOver;
                }

            }

            return cause;
        }

        public static bool CheckMin(double value, double? min)
        {
            if (min.HasValue == true)
            {
                return min.Value <= value;
            }

            return true;
        }

        public static bool CheckMin(double value, NumberRange range)
        {
            return CheckMin(value, range.Min);
        }

        public static bool CheckMax(double value, double? max)
        {
            if (max.HasValue == true)
            {
                return value <= max.Value;
            }

            return true;
        }

        public static bool CheckMax(double value, NumberRange range)
        {
            return CheckMax(value, range.Max);
        }

    }

}
