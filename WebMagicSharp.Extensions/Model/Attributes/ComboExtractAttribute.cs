using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class ComboExtractAttribute : Attribute
    {

        public ExtractByAttribute[] Value { get; set; }
        /// <summary>
        /// Judge is not null.
        /// </summary>
        public bool NotNull { get; set; } = false;
        /// <summary>
        /// Source.
        /// </summary>
        public ExtractSource Source { get; set; } = ExtractSource.SelectedHtml;
        /// <summary>
        /// Judge is nulti.
        /// </summary>
        public bool IsMulti { get; set; } = false;
        /// <summary>
        /// operator.
        /// </summary>
        public Op Op { get; set; } = Op.And;
    }

}
