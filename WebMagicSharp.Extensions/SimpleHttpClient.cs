using System;

using WebMagicSharp.DownLoaders;
using WebMagicSharp.Proxy;
using WebMagicSharp;

namespace WebMagicSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleHttpClient
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly HttpClientDownloader httpClientDownloader;

        /// <summary>
        /// 
        /// </summary>
        protected readonly Site site;

        /// <summary>
        /// 
        /// </summary>
        public SimpleHttpClient()
        {
            site = Site.Me();
            this.httpClientDownloader = new HttpClientDownloader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="site"></param>
        public SimpleHttpClient(Site site)
        {
            this.site = site;
            this.httpClientDownloader = new HttpClientDownloader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxyProvider"></param>
        public void SetProxyProvider(IProxyProvider proxyProvider)
        {
            this.httpClientDownloader.SetProxyProvider(proxyProvider);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Page Get(string url)
        {
            return httpClientDownloader.Download(new Request(url), site.ToTask());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Page Get(Request request)
        {
            return httpClientDownloader.Download(request, site.ToTask());
        }
    }
}
