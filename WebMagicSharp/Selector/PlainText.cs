using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Plain text.
    /// </summary>
    public class PlainText : AbstractSelectable
    {

        protected List<String> sourceTexts;

        public PlainText(List<String> sourceTexts)
        {
            this.sourceTexts = sourceTexts;
        }

        public PlainText(String text)
        {
            this.sourceTexts = new List<String>();
            sourceTexts.Add(text);
        }

        public static PlainText create(String text)
        {
            return new PlainText(text);
        }

        public override ISelectable Jquery(string selector)
        {
            throw new NotImplementedException();
        }

        public override ISelectable Jquery(string selector, string attrName)
        {
            throw new NotImplementedException();
        }

        public override ISelectable JsonPath(string jsonPath)
        {
            throw new NotImplementedException();
        }

        public override ISelectable Links()
        {
            throw new NotImplementedException();
        }

        public override List<ISelectable> Nodes()
        {
            var nodes = new List<ISelectable>();
            foreach(var str in GetSourceTexts())
            {
                nodes.Add(PlainText.create(str));
            }
            return nodes;
        }

        public override ISelectable SmartContent()
        {
            throw new NotImplementedException();
        }

        public override ISelectable Xpath(string xpath)
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSourceTexts()
        {
            return sourceTexts;
        }
    }
}
