﻿using System;
using System.Collections.Generic;

using WebMagicSharp.Processor;

namespace WebMagicSharp.Configurable
{
    public class ConfigurablePageProcessor : IPageProcessor
    {
        private Site site;

        private List<ExtractRule> extractRules;

        private ConfigurablePageProcessor()
        {
        }

        public ConfigurablePageProcessor(Site site, List<ExtractRule> extractRules)
        {
            this.site = site;
            this.extractRules = extractRules;
        }

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            foreach(var extractRule in extractRules)
            {
                if (extractRule.isMulti())
                {
                    List<String> results = page.GetHtml().SelectDocumentForList(extractRule.getSelector());
                    if (extractRule.isNotNull() && results.Count == 0)
                    {
                        page.SetSkip(true);
                    }
                    else
                    {
                        page.GetResultItems().Put(extractRule.getFieldName(), results);
                    }
                }
                else
                {
                    String result = page.GetHtml().SelectDocument(extractRule.getSelector());
                    if (extractRule.isNotNull() && result == null)
                    {
                        page.SetSkip(true);
                    }
                    else
                    {
                        page.GetResultItems().Put(extractRule.getFieldName(), result);
                    }
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConfigurablePageProcessor() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}