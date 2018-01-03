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

        protected List<string> _sourceTexts;

        public PlainText(List<string> sourceTexts)
        {
            _sourceTexts = sourceTexts;
        }

        public PlainText(string text)
        {
            _sourceTexts = new List<string>
            {
                text
            };
        }

        public static PlainText Create(string text)
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
            foreach(var str in SourceTexts)
            {
                nodes.Add(PlainText.Create(str));
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

        protected override List<string> SourceTexts => _sourceTexts;
    }
}
