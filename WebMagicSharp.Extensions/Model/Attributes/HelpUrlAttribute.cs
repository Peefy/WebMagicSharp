using System;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class HelpUrlAttribute : Attribute
    {
        public HelpUrlAttribute() { }

        public HelpUrlAttribute(string[] values) => Value = values;

        public HelpUrlAttribute(string value) => Value = new string[] { value };

        public string[] Value { get; set; } = new string[] { "" };
        public string SourceRegion { get; set; } = "";
    }

}
