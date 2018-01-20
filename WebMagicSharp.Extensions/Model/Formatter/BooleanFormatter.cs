using System;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    public class BooleanFormatter : BaseTypeFormatter<Boolean>
    {
        public override Type Type => typeof(bool);

        protected override bool FormatTrimmed(string raw)
        {
            return bool.Parse(raw);
        }
    }

}
