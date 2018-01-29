
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Examples
{
    [TargetUrl("http://my.oschina.net/flashsword/blog/\\d+")]
    public class OschinaBlogExample
    {
        [ExtractBy("//title/text()")]
        public string Title { get; set; }

        [ExtractBy(Value = "div.BlogContent", Type = ExtractType.Css)]
        public string Content { get; set; }

        [ExtractBy(Value = "//div[@class='BlogTags']/a/text()", IsMulti = true)]
        public List<string> Tags;

        [ExtractBy("//div[@class='BlogStat']/regex('\\d+-\\d+-\\d+\\s+\\d+:\\d+')")]
        public DateTime DateTime { get; set; }

        public static void Run()
        {
            OOSpider.Create(new JsonFilePageModelPipeline("/data/webmagic"))
                .AddUrl("http://my.oschina.net/flashsword/blog").Run();
        }

    }

}
