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

        private string _selectorText;

        private string _attrName;

        public CssSelector(string selectorText)
        {
            this._selectorText = selectorText;
        }

        public CssSelector(string selectorText, string attrName)
        {
            this._selectorText = selectorText;
            this._attrName = attrName;
        }

        private string GetValue(HtmlAgilityPack.HtmlNode element)
        {
            if (_attrName == null)
            {
                return element.OuterHtml;
            }
            else if ("innerHtml".Equals(_attrName.ToLower()))
            {
                return element.InnerHtml;
            }
            else if ("text".Equals(_attrName.ToLower()))
            {
                return element.WriteTo();
            }
            else if ("allText".Equals(_attrName.ToLower()))
            {
                return element.WriteContentTo();
            }
            else
            {
                return element.Attributes[_attrName].Name;
            }
        }

        protected string GetText(HtmlAgilityPack.HtmlNode element)
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

        public override bool HasAttribute()
        {
            return !string.IsNullOrEmpty(_attrName);
        }

        public override string Select(HtmlDocument element)
        {
            var elements = SelectElements(element);
            if (elements?.Count() == 0)
            {
                return null;
            }
            return GetValue(elements[0]);
        }

        public override HtmlAgilityPack.HtmlNode SelectElement(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(_selectorText)?.FirstOrDefault();
        }

        public override List<HtmlAgilityPack.HtmlNode> SelectElements(HtmlDocument element)
        {
            return element.DocumentNode.SelectNodes(_selectorText)?.ToList();
        }

        public override List<string> SelectList(HtmlDocument element)
        {
            var strings = new List<string>();
            var elements = SelectElements(element);
            if (elements != null)
            {
                foreach(var node in elements)
                {
                    string value = GetValue(node);
                    if (value != null)
                    {
                        strings.Add(value);
                    }
                }
            }
            return strings;
        }

        public override HtmlAgilityPack.HtmlNode SelectElement(string text)
        {
            return base.SelectElement(text);
        }

        public override List<HtmlAgilityPack.HtmlNode> SelectElements(string text)
        {
            return base.SelectElements(text);
        }

    }
}
