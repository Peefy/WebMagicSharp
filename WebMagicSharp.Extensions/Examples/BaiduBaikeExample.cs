
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Examples;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Examples
{
    public class BaiduBaikeExample
    {
        [ExtractBy("//h1[@class=title]/div[@class=lemmaTitleH1]/text()")]
        public string Name { get; set; }

        [ExtractBy("//div[@id='lemmaContent-0']//div[@class='para']/allText()")]
        public string Description { get; set; }

        public override string ToString()
        {
            return "BaiduBaike{" +
                "name='" + Name + '\'' +
                ", description='" + Description + '\'' +
                '}';
        }

        public static void Run()
        {
            var ooSpider = OOSpider.Create<BaiduBaikeExample>();
            //single download
            var urlTemplate = "http://baike.baidu.com/search/word?word=%s&pic=1&sug=1&enc=utf8";
            var baike = ooSpider.Get<BaiduBaikeExample>("http://baike.baidu.com/search/word?word=httpclient&pic=1&sug=1&enc=utf8");
            Console.WriteLine(baike);
            //multidownload
            var list = new List<string>
            {
                string.Format(urlTemplate, "风力发电"),
                string.Format(urlTemplate, "太阳能"),
                string.Format(urlTemplate, "地热发电"),
                string.Format(urlTemplate, "地热发电")
            };
            var resultItemses = ooSpider.GetAll<BaiduBaikeExample>(list);
            foreach(var item in resultItemses)
            {
                Console.WriteLine(item.ToString());
            }
            ooSpider.Close();
        }

    }

}
