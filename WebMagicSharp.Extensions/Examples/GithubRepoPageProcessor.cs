using WebMagicSharp.Processor;

namespace WebMagicSharp.Examples
{
    public class GithubRepoPageProcessor : IPageProcessor
    {

        private Site site = Site.Me.SetRetryTimes(3).SetSleepTime(1000).SetTimeOut(10000);

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            page.AddTargetRequests(page.GetHtml().Links().Regex("(https://github\\.com/[\\w\\-]+/[\\w\\-]+)").All());
            page.AddTargetRequests(page.GetHtml().Links().Regex("(https://github\\.com/[\\w\\-])").All());
            page.PutField("author", page.GetUrl().Regex("https://github\\.com/(\\w+)/.*").ToString());
            page.PutField("name", page.GetHtml().Xpath("//h1[@class='public']/strong/a/text()").ToString());
            if (page.GetResultItems().Get("name") == null)
            {
                //skip this page
                page.SetSkip(true);
            }
            page.PutField("readme", page.GetHtml().Xpath("//div[@id='readme']/tidyText()"));
        }

        public static void Run()
        {
            Spider.Create(new GithubRepoPageProcessor()).AddUrl("https://github.com/code4craft").Thread(5).Run();
        }

    }

}
