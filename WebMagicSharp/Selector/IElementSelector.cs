using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;

namespace WebMagicSharp.Selector
{
    public interface IElementSelector
    {
        String select(HtmlDocument element);
        /**
         * Extract all results in text.<br>
         *
         * @param element element
         * @return results
         */
        List<String> selectList(HtmlDocument element);
    }
}
