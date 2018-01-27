using System;

using WebMagicSharp.Model.Formatter;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class FormatterAttribute : Attribute
    {
        public static Type DefaultFormatterType => typeof(IObjectFormatter);

        public string[] Value { get; set; } = new string[] { "" };
        public Type SubType { get; set; } = typeof(void);
        public Type Formatter { get; set; } = typeof(IObjectFormatter);
    }

}
