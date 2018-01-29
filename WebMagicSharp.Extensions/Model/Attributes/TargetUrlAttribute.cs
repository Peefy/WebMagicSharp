using System;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TargetUrlAttribute : Attribute
    {

        public TargetUrlAttribute() { }

        public TargetUrlAttribute(string[] values) => Value = values;

        public TargetUrlAttribute(string value) => Value = new string[] { value };

        public string[] Value { get; set; } = new string[] { "" };
        public string SourceRegion { get; set; } = "";
    }

}
