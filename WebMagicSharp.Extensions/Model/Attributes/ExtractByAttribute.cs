using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ExtractByAttribute : Attribute
    {
        public string Value { get; set; } = "";

        public ExtractType Type { get; set; } = ExtractType.XPath;

        public ExtractSource Source { get; set; } = ExtractSource.SelectedHtml;

        public bool NotNull { get; set; } = false;

        public bool IsMulti { get; set;  } =  false;
    }

}
