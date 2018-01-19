using System;

using WebMagicSharp.Model.Formatter;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class FormatterAttribute : Attribute
    {
        public string Value { get; set; } = "";
        public Type SubType { get; set; } = typeof(void);
        public Type Formatter { get; set; } = typeof(IObjectFormatter<object>);
    }

}
