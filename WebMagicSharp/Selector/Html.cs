
using System;
using System.Collections.Generic;
using System.Text;

using DuGu.Standard.Html;

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

        /**
         * @param selector selector
         * @return result
         */
        public String SelectDocument(ISelector selector)
        {
            if (selector is IElementSelector elementSelector)
            {
                return elementSelector.Select(GetDocument());
            }
            else
            {
                return selector.Select(FirstSourceText);
            }
        }

        public List<String> SelectDocumentForList(ISelector selector)
        {
            if (selector is IElementSelector elementSelector)
            {
                return elementSelector.SelectList(GetDocument());
            }
            else
            {
                return selector.SelectList(FirstSourceText);
            }
        }

        public static Html Create(string text)
        {
            return new Html(text);
        }

    }
}
