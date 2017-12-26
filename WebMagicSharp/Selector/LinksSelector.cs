using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Links selector.
    /// </summary>
    public class LinksSelector : BaseElementSelector
    {
        public override bool hasAttribute()
        {
            return true;
        }

        public override string select(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override HtmlAgilityPack.HtmlNode selectElement(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override List<HtmlAgilityPack.HtmlNode> selectElements(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override List<string> selectList(HtmlDocument element)
        {
            var elements = element.DocumentNode.SelectNodes("a");
            var links = new List<String>();
            foreach(var element0 in elements)
            {
                links.Add(element0.Attributes["href"].ToString());
            }
            return links;
        }
    }
}
