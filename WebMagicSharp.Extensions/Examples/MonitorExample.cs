
using WebMagicSharp;
using WebMagicSharp.Examples;

using System;
using System.Collections.Generic;

using WebMagicSharp.Monitor;

namespace WebMagicSharp.Examples
{
    public class MonitorExample
    {
        public static void Run()
        {
            var zhihuSpider = Spider.Create(new ZhihuPageProcessor())
                .AddUrl("http://my.oschina.net/flashsword/blog");
            var githubSpider = Spider.Create(new GithubRepoPageProcessor())
                .AddUrl("https://github.com/code4craft");

            SpiderMonitor.Instance.Register(zhihuSpider);
            SpiderMonitor.Instance.Register(githubSpider);
            zhihuSpider.Start();
            githubSpider.Start();
        }
    }

}
