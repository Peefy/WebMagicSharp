using System;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class HelpUrlAttribute : Attribute
    {
        public string Value { get; set; } = "";
        public string SourceRegion { get; set; } = "";
    }

}
