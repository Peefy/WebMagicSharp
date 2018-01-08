using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Processor;

namespace WebMagicSharp.Handler
{
    public class CompositePageProcessor : IPageProcessor
    {

        private Site site;

        private List<ISubPageProcessor> subPageProcessors = new List<ISubPageProcessor>();

        public void Dispose()
        {
            
        }

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            foreach(var subPageProcessor in subPageProcessors)
            {
                if (subPageProcessor.match(page.GetRequest()))
                {
                    MatchOther matchOtherProcessorProcessor = subPageProcessor.processPage(page);
                    if (matchOtherProcessorProcessor != MatchOther.YES)
                    {
                        return;
                    }
                }
            }
        }

        public CompositePageProcessor setSite(Site site)
        {
            this.site = site;
            return this;
        }

        public CompositePageProcessor addSubPageProcessor(ISubPageProcessor subPageProcessor)
        {
            this.subPageProcessors.Add(subPageProcessor);
            return this;
        }

        public CompositePageProcessor setSubPageProcessors(ISubPageProcessor[] subPageProcessors)
        {
            this.subPageProcessors = new List<ISubPageProcessor>();
            foreach(var subPageProcessor in subPageProcessors)
            {
                this.subPageProcessors.Add(subPageProcessor);
            }
            return this;
        }

    }
}
