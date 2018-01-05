
using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Html.
    /// </summary>
    public class Html : HtmlNode
    {
        private HtmlDocument _document;

        public Html(string text, string url)
        {
            try
            {
                _document = new HtmlDocument();
                _document.LoadHtml(text);
            }
            catch
            {
                _document = null;
            }
        }

        public Html(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }
            try
            {
                _document = new HtmlDocument();
                _document.LoadHtml(text);
            }
            catch
            {
                _document = null;
            }
        }

        public Html(HtmlDocument document)
        {
            _document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public HtmlDocument GetDocument()
        {
            return _document;
        }

        protected override List<Element> getElements()
        {
            return Collections.< Element > singletonList(getDocument());
        }

        /**
         * @param selector selector
         * @return result
         */
        public String selectDocument(Selector selector)
        {
            if (selector instanceof ElementSelector) {
                ElementSelector elementSelector = (ElementSelector)selector;
                return elementSelector.select(getDocument());
            } else {
                return selector.select(getFirstSourceText());
            }
        }

        public List<String> selectDocumentForList(Selector selector)
        {
            if (selector instanceof ElementSelector) {
                ElementSelector elementSelector = (ElementSelector)selector;
                return elementSelector.selectList(getDocument());
            } else {
                return selector.selectList(getFirstSourceText());
            }
        }

        public static Html Create(string text)
        {
            return new Html(text);
        }

    }
}
