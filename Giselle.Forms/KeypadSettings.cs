using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Giselle.Forms
{
    public class KeypadSettings
    {
        public KeypadType Type { get; set; }
        public string Title { get; set; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public int? Dps { get; set; }
        public int? MaxLength { get; set; }
        public event EventHandler<KeypadValidateEventArgs> Validating;

        public KeypadSettings()
        {

        }

        public KeypadSettings(KeypadSettings other)
        {
            this.Type = other.Type;
            this.Title = other.Title;
            this.Min = other.Min;
            this.Max = other.Max;
            this.Dps = other.Dps;
            this.MaxLength = this.MaxLength;
            this.Validating = other.Validating;
        }

        public void SetNumberRange(NumberRange range)
        {
            this.Type = KeypadType.Number;
            this.Min = range.Min;
            this.Max = range.Max;
            this.Dps = range.Dps;
        }

        protected virtual void OnValidate(KeypadValidateEventArgs e)
        {
            var handler = this.Validating;
            if (handler != null) handler(this, e);
        }

        public KeypadParseResult Validate(string text)
        {
            var result = new KeypadParseResult();
            var type = this.Type;

            if (type == KeypadType.Number)
            {
                double value;
                var cause = NumberRange.TryParse(text, this.ToNumberRange(), out value);
                result.NumberErrorCause = cause;
                result.Number = value;
                result.Validated = cause == NumberErrorCause.None;
            }
            else if (type == KeypadType.IPAddress)
            {
                IPAddress value = null;
                var cause = IPAddressErrorCause.None;
                var splits = text.Split('.');

                if (splits.Length == 4)
                {
                    var anyDecimalValidated = splits.All(t => { byte b; return byte.TryParse(t, out b); });

                    if (anyDecimalValidated == true)
                    {
                        if (IPAddress.TryParse(text, out value) == false)
                        {
                            cause = IPAddressErrorCause.ParseError;
                        }

                    }
                    else
                    {
                        cause = IPAddressErrorCause.AnyDecimalInvalid;
                    }

                }
                else
                {
                    cause = IPAddressErrorCause.DecimalCountInvalid;
                }

                result.Validated = cause == IPAddressErrorCause.None;
                result.IPAddressCause = cause;
                result.IPAddress = value;
            }
            else if (type == KeypadType.String)
            {
                result.Validated = true;
                result.String = text;
            }

            var e = new KeypadValidateEventArgs(this, result, text);
            this.OnValidate(e);
            return result;
        }

        public NumberRange ToNumberRange()
        {
            return new NumberRange() { Min = this.Min, Max = this.Max, Dps = this.Dps };
        }

        public KeypadSettings Clone()
        {
            return new KeypadSettings(this);
        }

    }

}
