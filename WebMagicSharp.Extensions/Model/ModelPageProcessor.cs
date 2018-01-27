
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Processor;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class ModelPageProcessor : IPageProcessor
    {
        private List<PageModelExtractor> pageModelExtractorList = 
            new List<PageModelExtractor>();

        private Site site;

        private bool IsExtractLinks { get; set; } = true;

        public static ModelPageProcessor Create(Site site, Type[] types)
        {
            var modelPageProcessor = new ModelPageProcessor(site);
            foreach(var type in types)
            {
                modelPageProcessor.AddPageModel(type);
            }
            return modelPageProcessor;
        }

        public ModelPageProcessor AddPageModel(Type type)
        {
            PageModelExtractor pageModelExtractor = PageModelExtractor.Create(type);
            pageModelExtractorList.Add(pageModelExtractor);
            return this;
        }

        private ModelPageProcessor(Site site)
        {
            this.site = site;
        }

        public void Process(Page page)
        {
            foreach(var pageModelExtractor in pageModelExtractorList)
            {
                if(IsExtractLinks == true)
                {
                    ExtractLinks(page, pageModelExtractor.HelpUrlRegionSelector,
                        pageModelExtractor.HelpUrlRegexs);
                    ExtractLinks(page, pageModelExtractor.TargetUrlRegionSelector,
                        pageModelExtractor.TargetUrlRegexs);
                }
                var process = pageModelExtractor.Process(page);
                if (process == null || process is IList && ((IList)process).Count == 0)
                    continue;
                PostProcessPageModel(pageModelExtractor.Type, process);
                page.PutField(pageModelExtractor.Type.Name, process);
            }
            if (page.GetResultItems()?.GetAll()?.Count == 0)
                page.GetResultItems().SetSkip(true);
        }

        private void ExtractLinks(Page page, ISelector urlRegionSelector, List<Regex> urlRegexs)
        {
            List<string> links;
            if(urlRegionSelector == null)
            {
                links = page.GetHtml().Links().All();
            }
            else
            {
                links = page.GetHtml().SelectList(urlRegionSelector).Links().All();
            }
            if (links == null)
                return;
            foreach(var link in links)
            {
                foreach(var regex in urlRegexs)
                {
                    var match = regex.Match(link);
                    if(match.Success == true)
                    {
                        page.AddTargetRequest(new Request(match.Groups[0].Value));
                    }
                }
            }
        }

        protected virtual void PostProcessPageModel(Type type, object obj)
        {

        }

        public Site GetSite()
        {
            return site;
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {            
            Dispose(true);
        }
        #endregion
    }

}
