using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

using HttpCode.Core;

namespace WebMagicSharp.DownLoaders
{
    public class HttpClientGenerator
    {
        string _gzip = "gzip";
        HttpCoreClient _http;

        public HttpClientGenerator()
        {
            _http = new HttpCoreClient();
        }

        public HttpCoreClient GetClient(Site site)
        {
            return GenerateClient(site);
        }

        private HttpCoreClient GenerateClient(Site site)
        {
            if (site.getUserAgent() != null)
            {
                _http.Items.UserAgent = site.getUserAgent();
            }
            else
            {
                _http.Items.UserAgent = "";
            }
            if (site.isUseGzip())
            {
                _http.Items.Header.Add(HttpRequestHeader.AcceptEncoding, _gzip);
            }
            GenerateCookie(site);
            return _http;
        }

        private void GenerateCookie(Site site)
        {
            if (site.isDisableCookieManagement())
            {
                return;
            }
            var cookieStore = new CookieContainer();
            foreach (var cookieEntry in site.getCookies())
            {
                var cookie = new Cookie(cookieEntry.Key, cookieEntry.Value);
                cookie.Domain = site.getDomain();
                cookieStore.Add(cookie);
            }
            foreach (var domainEntry in site.getAllCookies())
            {
                foreach (var cookieEntry in domainEntry.Value)
                {
                    var cookie = new Cookie(cookieEntry.Key, cookieEntry.Value);
                    cookie.Domain = domainEntry.Key;
                    cookieStore.Add(cookie);
                }
            }
            _http.Items.Container = cookieStore;
        }

    }

    public class HttpCoreClient
    {
        public HttpHelpers HttpClient { get; set; }
        public HttpItems Items { get; set; }

        public HttpCoreClient()
        {
            HttpClient = new HttpHelpers();
            Items = new HttpItems();
            Items.IsSetSecurityProtocolType = true;
            Items.SecProtocolTypeEx = SecurityProtocolTypeEx.Ssl3;
            Items.Method = "GET";
        }

        public HttpResults GetHtml()
        {
            return HttpClient.GetHtml(Items);
        }

    }

}
