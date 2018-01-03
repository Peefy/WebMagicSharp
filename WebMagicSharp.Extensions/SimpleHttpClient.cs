using System;

using WebMagicSharp.DownLoaders;
using WebMagicSharp.Proxy;
using WebMagicSharp;

namespace WebMagicSharp
{
    public class SimpleHttpClient
    {
        protected readonly HttpClientDownloader httpClientDownloader;

        protected readonly Site site;

        public SimpleHttpClient()
        {
            site = Site.Me();
            this.httpClientDownloader = new HttpClientDownloader();
        }

        public SimpleHttpClient(Site site)
        {
            this.site = site;
            this.httpClientDownloader = new HttpClientDownloader();
        }

        public void setProxyProvider(IProxyProvider proxyProvider)
        {
            this.httpClientDownloader.SetProxyProvider(proxyProvider);
        }

        public Page get(String url)
        {
            return httpClientDownloader.Download(new Request(url), site.toTask());
        }

        public Page get(Request request)
        {
            return httpClientDownloader.Download(request, site.toTask());
        }
    }
}
