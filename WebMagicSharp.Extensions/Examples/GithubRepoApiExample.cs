
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Extensions;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Examples
{

    public class GithubRepoApiExample : IHasKey
    {
        [ExtractBy(Type = ExtractType.JsonPath, Value = "$.name", Source = ExtractSource.RawText)]
        public string Name { get; set; }

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..owner.login", Source = ExtractSource.RawText)]
        public string Author { get; set; }

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$..language",IsMulti = true, Source = ExtractSource.RawText)]
        public List<string> Language { get; set; }

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$.stargazers_count", Source = ExtractSource.RawText)]
        public int Star { get; set; }

        [ExtractBy(Type = ExtractType.JsonPath, Value = "$.forks_count", Source = ExtractSource.RawText)]
        public int Fork { get; set; }

        [ExtractByUrl]
        public string Url { get; set; }

        public string Key => Author + ":" + Name;

        public static void Run()
        {
            var spider = OOSpider.Create(Site.Me.SetSleepTime(100), new ConsolePageModelPipeline<GithubRepoApiExample>());
            spider.AddUrl("https://api.github.com/repos/code4craft/webmagic").Run();
        }

    }

}
