using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;


namespace WebMagicSharp.Processor
{
    /// <summary>
    /// 
    /// </summary>
    public class SimplePageProcessor : IPageProcessor
    {
        private string _urlPattern;

        private Site _site;

        public SimplePageProcessor(string urlPattern)
        {
            this._site = Site.Me;
            //compile "*" expression to regex
            this._urlPattern = "(" + urlPattern.Replace(".", "\\.").
                Replace("*", "[^\"'#]*") + ")";

        }

        public void Process(Page page)
        {
            var requests = page.GetHtml().Links().Regex(_urlPattern).All();
            //add urls to fetch
            page.AddTargetRequests(requests);
            //extract by XPath
            page.PutField("title", page.GetHtml().Xpath("//title"));
            page.PutField("html", page.GetHtml().ToString());
            //extract by Readability
            page.PutField("content", page.GetHtml().SmartContent());
        }

        public Site GetSite()
        {
            //settings
            return _site;
        }

    }
}
