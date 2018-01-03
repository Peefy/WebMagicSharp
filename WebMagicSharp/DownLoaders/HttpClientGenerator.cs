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
            if (site.UserAgent != null)
            {
                _http.Items.UserAgent = site.UserAgent;
            }
            else
            {
                _http.Items.UserAgent = "";
            }
            if (site.IsUseGzip())
            {
                _http.Items.Header.Add(HttpRequestHeader.AcceptEncoding, _gzip);
            }
            GenerateCookie(site);
            return _http;
        }

        private void GenerateCookie(Site site)
        {
            if (site.IsDisableCookieManagement())
            {
                return;
            }
            var cookieStore = new CookieContainer();
            foreach (var cookieEntry in site.GetCookies())
            {
                var cookie = new Cookie(cookieEntry.Key, cookieEntry.Value)
                {
                    Domain = site.Domain
                };
                cookieStore.Add(cookie);
            }
            foreach (var domainEntry in site.GetAllCookies())
            {
                foreach (var cookieEntry in domainEntry.Value)
                {
                    var cookie = new Cookie(cookieEntry.Key, cookieEntry.Value)
                    {
                        Domain = domainEntry.Key
                    };
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
            Items = new HttpItems
            {
                IsSetSecurityProtocolType = true,
                SecProtocolTypeEx = SecurityProtocolTypeEx.Ssl3,
                Method = "GET"
            };
        }

        public HttpResults GetHtml()
        {
            return HttpClient.GetHtml(Items);
        }

    }

}
