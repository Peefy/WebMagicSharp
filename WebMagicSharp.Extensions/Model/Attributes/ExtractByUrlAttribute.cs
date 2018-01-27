using System;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExtractByUrlAttribute : Attribute
    {
        public string Value { get; set; } = "";
        public bool NotNull { get; set; } = false;
        public bool IsMulti { get; set; } = false;
    }

}
