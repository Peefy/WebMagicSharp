using System;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    public class Int32Formatter : BaseTypeFormatter<Int32>
    {
        public override Type Type => typeof(int);

        protected override int FormatTrimmed(string raw)
        {
            return int.Parse(raw);
        }

    }

}
