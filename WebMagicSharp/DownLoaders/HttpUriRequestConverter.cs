using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Proxy;
using WebMagicSharp.Utils;

namespace WebMagicSharp.DownLoaders
{
    public class HttpUriRequestConverter
    {
        public HttpClientRequestContext Convert(Request request, Site site, Proxy.Proxy proxy)
        {
            var httpClientRequestContext = new HttpClientRequestContext
            {
                HttpClient = ConvertHttpClientContext(request, site, proxy),
                HttpRequest = ConvertHttpUriRequest(request, site, proxy)
            };
            return httpClientRequestContext;
        }

        private HttpCoreClient ConvertHttpClientContext(Request request, Site site, Proxy.Proxy proxy)
        {
            var httpContext = new HttpCoreClient();
            httpContext.Items.Url = UrlUtils.FixIllegalCharacterInUrl(request.GetUrl());
            httpContext.Items.Method = request.GetMethod();
            if (site != null)
            {
                httpContext.Items.Timeout = site.TimeOut;
                httpContext.Items.ReadWriteTimeout = site.TimeOut;
            }
            if (site?.Headers != null)
            {
                foreach (var headerEntry in site.Headers)
                {
                    httpContext.Items.Header.Add(headerEntry.Key, headerEntry.Value);
                }
            }
            if (proxy != null && proxy.Username != null)
            {
                httpContext.Items.ProxyIp = $"{proxy.Host}:{proxy.Port}";
                httpContext.Items.ProxyUserName = proxy.Username;
                httpContext.Items.ProxyPwd = proxy.Password;
            }
            if (request.GetCookies() != null && request.GetCookies().Count > 0)
            {
                var cookieStore = new CookieContainer();
                foreach(var cookieEntry in request.GetCookies())
                {
                    var cookie1 = new Cookie(cookieEntry.Key, cookieEntry.Value)
                    {
                        Domain = UrlUtils.RemovePort(UrlUtils.GetDomain(request.Url))
                    };
                    cookieStore.Add(cookie1);
                }
                httpContext.Items.Container = cookieStore;
            }
            if (request.GetHeaders() != null && request.GetHeaders().Count > 0)
            {
                foreach (var header in request.GetHeaders())
                {
                    httpContext.Items.Header.Add(header.Key, header.Value);
                }
            }
            return httpContext;
        }

        private HttpWebRequest ConvertHttpUriRequest(Request request, Site site, Proxy.Proxy proxy)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.
                Create(UrlUtils.FixIllegalCharacterInUrl(request.GetUrl()));
            httpWebRequest.Headers = new WebHeaderCollection();
            if (site.Headers != null)
            {
                foreach(var headerEntry in site.Headers)
                {
                    httpWebRequest.Headers.Add(headerEntry.Key, headerEntry.Value);
                }
            }

            if (site != null)
            {
                httpWebRequest.Timeout = site.TimeOut;
                httpWebRequest.ContinueTimeout = site.TimeOut;
                httpWebRequest.ReadWriteTimeout = site.TimeOut;
            }

            if (proxy != null)
            {
                httpWebRequest.Proxy = new WebProxy(proxy.Host, proxy.Port);
            }
            if (request.GetHeaders() != null && request.GetHeaders().Count > 0)
            {
                foreach(var header in request.GetHeaders())
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }
            return httpWebRequest;
        }

    }
}
