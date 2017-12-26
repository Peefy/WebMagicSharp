
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
        private HtmlDocument document;

        public Html(String text, String url)
        {
            try
            {
                this.document = new HtmlDocument();
                this.document.LoadHtml(text);
            }
            catch
            {
                this.document = null;
            }
        }

        public Html(String text)
        {
            try
            {
                this.document = new HtmlDocument();
                this.document.LoadHtml(text);
            }
            catch
            {
                this.document = null;
            }
        }

        public Html(HtmlDocument document)
        {
            this.document = document;
        }

        public HtmlDocument getDocument()
        {
            return document;
        }
    }
}
