using System;
using System.Collections.Generic;
using System.Text;

using DuGu.Standard.Html;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Element selector.
    /// </summary>
    public interface IElementSelector
    {
        string Select(HtmlDocument element);

        List<string> SelectList(HtmlDocument element);
    }
}
