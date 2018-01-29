
using System.Diagnostics;

using WebMagicSharp;
using WebMagicSharp.Handler;

namespace WebMagicSharp.Examples
{
    public class PatternProcessorExample
    {
        public static void Run()
        {
            var githubRepoProcessor = new GithubRepoPatternProcessor("https://github\\.com/[\\w\\-]+/[\\w\\-]+");
            var githubUserProcessor = new GithubUserPatternProcessor("https://github\\.com/[\\w\\-]+");
            var pageProcessor = new CompositePageProcessor(Site.Me.SetDomain("github.com").SetRetryTimes(3));
            var pipeline = new CompositePipeline();
            pageProcessor.SetSubPageProcessors(githubRepoProcessor, githubUserProcessor);
            pipeline.SetSubPipeline(githubRepoProcessor, githubUserProcessor);
            Spider.Create(pageProcessor).AddUrl("https://github.com/code4craft")
                .Thread(5).AddPipeline(pipeline).RunAsync();
        }
    }

    public class GithubRepoPatternProcessor : PatternProcessor
    {
        public GithubRepoPatternProcessor(string pattern) : base(pattern)
        {
        }

        public override MatchOther ProcessPage(Page page)
        {
            page.PutField("reponame", page.GetHtml().
                Xpath("//h1[@class='entry-title public']/strong/a/text()").ToString());
            return MatchOther.Yes;
        }

        public override MatchOther ProcessResult(ResultItems resultItems, ITask task)
        {
            Debug.WriteLine("Extracting from repo" + resultItems.GetRequest());
            Debug.WriteLine("Repo name: " + resultItems.Get("reponame"));
            return MatchOther.Yes;
        }
    }

    public class GithubUserPatternProcessor : PatternProcessor
    {
        public GithubUserPatternProcessor(string pattern) : base(pattern)
        {
        }

        public override MatchOther ProcessPage(Page page)
        {
            Debug.WriteLine("Extracting from " + page.GetUrl());
            page.AddTargetRequests(page.GetHtml().Links().Regex("https://github\\.com/[\\w\\-]+/[\\w\\-]+").All());
            page.AddTargetRequests(page.GetHtml().Links().Regex("https://github\\.com/[\\w\\-]+").All());
            page.PutField("username", page.GetHtml().Xpath("//span[@class='vcard-fullname']/text()").ToString());
            return MatchOther.Yes;
        }

        public override MatchOther ProcessResult(ResultItems resultItems, ITask task)
        {
            Debug.WriteLine("User name: " + resultItems.Get("username"));
            return MatchOther.Yes;
        }
    }

}
