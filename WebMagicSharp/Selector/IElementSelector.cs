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
        String Select(HtmlDocument element);
        /**
         * Extract all results in text.<br>
         *
         * @param element element
         * @return results
         */
        List<String> SelectList(HtmlDocument element);
    }
}
