using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Processor;

namespace WebMagicSharp.Handler
{
    public class CompositePageProcessor : IPageProcessor
    {

        private Site site;

        private List<ISubPageProcessor> subPageProcessors = new List<ISubPageProcessor>();

        public CompositePageProcessor(Site site) => this.site = site;

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            foreach(var subPageProcessor in subPageProcessors)
            {
                if (subPageProcessor.Match(page.GetRequest()))
                {
                    MatchOther matchOtherProcessorProcessor = subPageProcessor.ProcessPage(page);
                    if (matchOtherProcessorProcessor != MatchOther.Yes)
                    {
                        return;
                    }
                }
            }
        }

        public CompositePageProcessor SetSite(Site site)
        {
            this.site = site;
            return this;
        }

        public CompositePageProcessor AddSubPageProcessor(ISubPageProcessor subPageProcessor)
        {
            this.subPageProcessors.Add(subPageProcessor);
            return this;
        }

        public CompositePageProcessor SetSubPageProcessors(params ISubPageProcessor[] subPageProcessors)
        {
            this.subPageProcessors = new List<ISubPageProcessor>();
            foreach (var subPageProcessor in subPageProcessors)
            {
                this.subPageProcessors.Add(subPageProcessor);
            }
            return this;
        }

    }


}
