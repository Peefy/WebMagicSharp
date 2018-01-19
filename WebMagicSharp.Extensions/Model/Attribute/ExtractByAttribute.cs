using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Model.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ExtractByAttribute : System.Attribute
    {
        public string Value { get; set; }

        public ExtractType Type { get; set; } = ExtractType.XPath;

        public ExtractSource Source = ExtractSource.SelectedHtml;

        public bool NotNull { get; set; } = false;

        public bool IsMulti { get; set;  } =  false;
    }

    public enum ExtractSource
    {
        SelectedHtml,
        RawHtml,
        RawText
    }

    public enum ExtractType
    {
        XPath,
        Regex,
        Css,
        JsonPath
    }

}
