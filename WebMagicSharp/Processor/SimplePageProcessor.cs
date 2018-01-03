using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;


namespace WebMagicSharp.Processor
{
    /// <summary>
    /// 
    /// </summary>
    public class SimplePageProcessor : IPageProcessor, IDisposable
    {
        private string _urlPattern;

        private Site _site;

        public SimplePageProcessor(string urlPattern)
        {
            this._site = Site.Me();
            //compile "*" expression to regex
            this._urlPattern = "(" + urlPattern.Replace(".", "\\.").
                Replace("*", "[^\"'#]*") + ")";

        }


        public void Process(Page page)
        {
            var requests = page.GetHtml().Links().Regex(_urlPattern).All();
            //add urls to fetch
            page.AddTargetRequests(requests);
            //extract by XPath
            page.PutField("title", page.GetHtml().Xpath("//title"));
            page.PutField("html", page.GetHtml().ToString());
            //extract by Readability
            page.PutField("content", page.GetHtml().SmartContent());
        }

        public Site GetSite()
        {
            //settings
            return _site;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SimplePageProcessor() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
