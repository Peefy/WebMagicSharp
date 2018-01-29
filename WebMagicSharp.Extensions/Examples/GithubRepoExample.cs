
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Extensions;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Examples
{
    [TargetUrl("https://github.com/\\w+/\\w+")]
    [HelpUrl(new string[] { "https://github.com/\\w+\\?tab=repositories", "https://github.com/\\w+", "https://github.com/explore/*" })]
    public class GithubRepoExample
    {
        [ExtractBy(Value = "//h1[@class='public']/strong/a/text()", NotNull = true)]
        public string Name { get; set; }

        [ExtractBy("https://github\\.com/(\\w+)/.*")]
        public string Author { get; set; }

        [ExtractBy("//div[@id='readme']/tidyText()")]
        public string ReadMe { get; set; }

        [ExtractBy(Value = "//div[@class='repository-lang-stats']//li//span[@class='lang']/text()", IsMulti = true)]
        public List<string> Language { get; set; }

        [ExtractBy("//ul[@class='pagehead-actions']/li[1]//a[@class='social-count js-social-count']/text()")]
        public int Star { get; set; }

        [ExtractBy("//ul[@class='pagehead-actions']/li[2]//a[@class='social-count']/text()")]
        public int Fork { get; set; }

        [ExtractByUrl]
        public string Url { get; set; }

        public override string ToString()
        {
            return "GithubRepo{" +
                "name='" + Name + '\'' +
                ", author='" + Author + '\'' +
                ", readme='" + ReadMe + '\'' +
                ", language=" + Language.ToListString() +
                ", star=" + Star +
                ", fork=" + Fork +
                ", url='" + Url + '\'' +
                '}';
        }

        public static void Run()
        {
            OOSpider.Create(Site.Me.SetSleepTime(100),
                new ConsolePageModelPipeline<GithubRepoExample>());
        }
    }

}
