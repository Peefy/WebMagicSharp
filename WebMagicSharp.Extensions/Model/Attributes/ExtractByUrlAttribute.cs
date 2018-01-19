using System;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ExtractByUrlAttribute : Attribute
    {
        public string Value { get; set; } = "";
        public bool NotNull { get; set; } = false;
        public bool IsMulti { get; set; } = false;
    }

}
