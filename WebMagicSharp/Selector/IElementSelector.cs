using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;

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
