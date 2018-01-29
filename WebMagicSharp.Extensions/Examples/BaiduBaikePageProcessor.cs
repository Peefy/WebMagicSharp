
using System;
using System.Collections.Generic;
using WebMagicSharp.Extensions;
using WebMagicSharp.Processor;

namespace WebMagicSharp.Examples
{
    public class BaiduBaikePageProcessor : IPageProcessor
    {
        private Site site = Site.Me            .SetRetryTimes(3).SetSleepTime(1000).SetUseGzip(true);

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            page.PutField("name", page.GetHtml().Css("dl.lemmaWgt-lemmaTitle h1", "text").ToString());
            page.PutField("description", page.GetHtml().Xpath("//div[@class='lemma-summary']/allText()"));
        }

        public static void Run()
        {
            //single download
            var spider = Spider.Create(new BaiduBaikePageProcessor()).Thread(2);
            String urlTemplate = "http://baike.baidu.com/search/word?word=%s&pic=1&sug=1&enc=utf8";
            ResultItems resultItems = spider.Get<ResultItems>(string.Format(urlTemplate, "水力发电"));
            Console.WriteLine(resultItems);

            //multidownload
            var list = new List<String>
            {
                String.Format(urlTemplate, "风力发电"),
                String.Format(urlTemplate, "太阳能"),
                String.Format(urlTemplate, "地热发电"),
                String.Format(urlTemplate, "地热发电")
            };
            var resultItemses = spider.GetAll<ResultItems>(list);
            foreach(var item in resultItemses)
            {
                Console.WriteLine(item.GetAll().ToKeyValuePairString());
            }
            spider.Close();
        }

    }

}
