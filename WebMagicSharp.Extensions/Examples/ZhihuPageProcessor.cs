using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.Examples
{
    public class ZhihuPageProcessor : IPageProcessor
    {
        private Site site = Site.Me.SetRetryTimes(3).SetSleepTime(1000);

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            page.AddTargetRequests(page.GetHtml().Links().Regex("https://www\\.zhihu\\.com/question/\\d+/answer/\\d+.*").All());
            page.PutField("title", page.GetHtml().Xpath("//h1[@class='QuestionHeader-title']/text()").ToString());
            page.PutField("question", page.GetHtml().Xpath("//div[@class='QuestionRichText']//tidyText()").ToString());
            page.PutField("answer", page.GetHtml().Xpath("//div[@class='QuestionAnswer-content']/tidyText()").ToString());
            if (page.GetResultItems().Get("title") == null)
            {
                //skip this page
                page.SetSkip(true);
            }
        }
    }
}
