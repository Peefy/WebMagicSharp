using System;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    public class CharFormatter : BaseTypeFormatter<Char>
    {
        public override Type Type => typeof(char);

        protected override char FormatTrimmed(string raw)
        {
            return raw[0];
        }
    }

}
