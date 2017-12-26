using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Css selector.
    /// </summary>
    public class CssSelector : BaseElementSelector
    {

        private String selectorText;

        private String attrName;

        public CssSelector(String selectorText)
        {
            this.selectorText = selectorText;
        }

        public CssSelector(String selectorText, String attrName)
        {
            this.selectorText = selectorText;
            this.attrName = attrName;
        }

        private String getValue(HtmlAgilityPack.HtmlNode element)
        {
            if (attrName == null)
            {
                return element.OuterHtml;
            }
            else if ("innerHtml".Equals(attrName.ToLower()))
            {
                return element.InnerHtml;
            }
            else if ("text".Equals(attrName.ToLower()))
            {
                return element.WriteTo();
            }
            else if ("allText".Equals(attrName.ToLower()))
            {
                return element.WriteContentTo();
            }
            else
            {
                return element.Attributes[attrName].Name;
            }
        }

        protected String getText(HtmlAgilityPack.HtmlNode element)
        {
            StringBuilder accum = new StringBuilder();
            foreach(var node in element.ChildNodes)
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    accum.Append(node.InnerText);
                }
            }      
            return accum.ToString();
        }


        public override bool hasAttribute()
        {
            return !string.IsNullOrEmpty(attrName);
        }

        public override string select(HtmlDocument element)
        {
            var elements = selectElements(element);
            if (elements?.Count() == 0)
            {
                return null;
            }
            return getValue(elements[0]);
        }

        public override HtmlAgilityPack.HtmlNode selectElement(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(selectorText)?.FirstOrDefault();
        }

        public override List<HtmlAgilityPack.HtmlNode> selectElements(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(selectorText)?.ToList();
        }

        public override List<string> selectList(HtmlDocument element)
        {
            var strings = new List<String>();
            var elements = selectElements(element);
            if (elements != null)
            {
                foreach(var node in elements)
                {
                    String value = getValue(node);
                    if (value != null)
                    {
                        strings.Add(value);
                    }
                }
            }
            return strings;
        }

        public override HtmlAgilityPack.HtmlNode selectElement(string text)
        {
            return base.selectElement(text);
        }

        public override List<HtmlAgilityPack.HtmlNode> selectElements(string text)
        {
            return base.selectElements(text);
        }

    }
}
