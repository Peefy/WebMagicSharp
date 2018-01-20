using System;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    public class DoubleFormatter : BaseTypeFormatter<Double>
    {
        public override Type Type => typeof(double);

        protected override double FormatTrimmed(string raw)
        {
            return double.Parse(raw);
        }
    }

}
