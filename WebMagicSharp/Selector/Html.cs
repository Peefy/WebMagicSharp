
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
    }
}
