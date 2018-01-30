using System;
using System.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Text;

using DuGu.Standard.Html;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// XPath selector.
    /// </summary>
    public class XPathSelector : BaseElementSelector
    {
        public XPathExpression XPathString {get;set;}

        public XPathSelector(string xPathStr)
        {
            XPathString = XPathExpression.Compile(xPathStr);

        }

        public override bool HasAttribute() => XPathString.Expression.Contains("@");

        public override string Select(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(XPathString.Expression).ToString();
        }


        public override List<string> SelectList(HtmlDocument element)
        {
            var list = new List<string>();
            var nodes = element.DocumentNode.SelectNodes(XPathString.Expression);
            foreach(var node in nodes)
            {
                list.Add(node.WriteContentTo());
            }
            return list;
        }

        public override DuGu.Standard.Html.HtmlNode SelectElement(HtmlDocument element)
        {
            var elements = SelectElements(element);
            return elements.FirstOrDefault();
        }

        public override List<DuGu.Standard.Html.HtmlNode> SelectElements(HtmlDocument element)
        {
            var list = element.DocumentNode.SelectNodes(XPathString.Expression).ToList();
            return list;
        }
    }
}
