using System;
using System.Linq;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace WebMagicSharp.Selector
{
    public class XPathSelector : BaseElementSelector
    {
        public XPathExpression XPathString {get;set;}

        public XPathSelector(string xPathStr)
        {
            XPathString = XPathExpression.Compile(xPathStr);
        }

        [Obsolete("Not Finish")]
        public override bool hasAttribute()
        {
            return true;
        }

        public override string select(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(XPathString.Expression).ToString();
        }


        public override List<string> selectList(HtmlDocument element)
        {
            var list = new List<string>();
            var nodes = element.DocumentNode.SelectNodes(XPathString.Expression);
            foreach(var node in nodes)
            {
                list.Add(node.WriteContentTo());
            }
            return list;
        }

        public override HtmlAgilityPack.HtmlNode selectElement(HtmlDocument element)
        {
            var elements = selectElements(element);
            return elements.FirstOrDefault();
        }

        public override List<HtmlAgilityPack.HtmlNode> selectElements(HtmlDocument element)
        {
            var list = element.DocumentNode.SelectNodes(XPathString.Expression).ToList();
            return list;
        }
    }
}
