using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public class HtmlNode : AbstractSelectable
    {

        List<HtmlAgilityPack.HtmlNode> elements;

        public HtmlNode(List<HtmlAgilityPack.HtmlNode> elements)
        {
            this.elements = elements;
        }

        public HtmlNode()
        {
            elements = null;
        }

        public virtual List<HtmlAgilityPack.HtmlNode> getElements()
        {
            return elements;
        }

        [Obsolete("Unrealized")]
        public override ISelectable JsonPath(string jsonPath)
        {
            throw new NotImplementedException();
        }

        public override ISelectable Links()
        {
            return selectElements(new LinksSelector());
        }

        public override List<ISelectable> Nodes()
        {
            var selectables = new List<ISelectable>();
            foreach(var element in getElements())
            {
                var childElements = new List<HtmlAgilityPack.HtmlNode>(1);
                childElements.Add(element);
                selectables.Add(new HtmlNode(childElements));
            }
            return selectables;
        }

        public override ISelectable SmartContent()
        {
            var smartContentSelector = Selectors.SmartContent();
            return Select(smartContentSelector, GetSourceTexts());
        }

        public override ISelectable Xpath(string xpath)
        {
            var xpathSelector = Selectors.XPath(xpath);
            return selectElements(xpathSelector);
        }

        protected override List<string> GetSourceTexts()
        {
            var sourceTexts = new List<String>();
            foreach(var element in getElements())
            {
                sourceTexts.Add(element.WriteContentTo());
            }
            return sourceTexts;
        }

        public override ISelectable Select(ISelector selector)
        {
            return SelectList(selector);
        }

        public override ISelectable SelectList(ISelector selector)
        {
            if (selector is BaseElementSelector) {
                return selectElements((BaseElementSelector)selector);
            }
            return SelectList(selector, GetSourceTexts());
        }

        protected ISelectable selectElements(BaseElementSelector elementSelector)
        {
            var elementsTemp = getElements();
            if (!elementSelector.hasAttribute())
            {
                List<HtmlAgilityPack.HtmlNode> resultElements 
                    = new List<HtmlAgilityPack.HtmlNode>();
                foreach(var element in elementsTemp)
                {
                    //var nodes = checkElementAndConvert(element);
                    var selectElementsTemp = 
                        elementSelector.selectElements(element.WriteContentTo());
                    resultElements.AddRange(selectElementsTemp);
                }
                return new HtmlNode(resultElements);
            }
            else
            {
                // has attribute, consider as plaintext
                var resultStrings = new List<String>();
                var document = checkElementAndConvert(elementsTemp);
                foreach (var element in elementsTemp)
                {                  
                    var selectList = elementSelector.selectList(document);
                    resultStrings.AddRange(selectList);
                }
                return new PlainText(resultStrings);

            }
        }

        private HtmlAgilityPack.HtmlDocument checkElementAndConvert(List<HtmlAgilityPack.HtmlNode> nodes)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            foreach(var node in nodes)
            {
                document.DocumentNode.AppendChild(node);
            }
            return document;
        }

    }
}
