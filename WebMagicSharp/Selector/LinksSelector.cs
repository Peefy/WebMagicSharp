using System;
using System.Collections.Generic;
using System.Text;
using DuGu.Standard.Html;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Links selector.
    /// </summary>
    public class LinksSelector : BaseElementSelector
    {
        public override bool HasAttribute()
        {
            return true;
        }

        public override string Select(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override DuGu.Standard.Html.HtmlNode SelectElement(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override List<DuGu.Standard.Html.HtmlNode> SelectElements(HtmlDocument element)
        {
            throw new NotImplementedException();
        }

        public override List<string> SelectList(HtmlDocument element)
        {
            var elements = element.DocumentNode.SelectNodes("a");
            var links = new List<string>();
            foreach(var element0 in elements)
            {
                links.Add(element0.Attributes["href"].ToString());
            }
            return links;
        }
    }
}
