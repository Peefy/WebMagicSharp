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
            var httpClientRequestContext = new HttpClientRequestContext();
            httpClientRequestContext.HttpClient = convertHttpClientContext(request, site, proxy);
            httpClientRequestContext.HttpRequest = convertHttpUriRequest(request, site, proxy);
            return httpClientRequestContext;
        }

        private HttpCoreClient convertHttpClientContext(Request request, Site site, Proxy.Proxy proxy)
        {
            var httpContext = new HttpCoreClient();
            httpContext.Items.Url = UrlUtils.FixIllegalCharacterInUrl(request.GetUrl());
            httpContext.Items.Method = request.getMethod();
            if (site != null)
            {
                httpContext.Items.Timeout = site.getTimeOut();
                httpContext.Items.ReadWriteTimeout = site.getTimeOut();
            }
            if (site?.getHeaders() != null)
            {
                foreach (var headerEntry in site.getHeaders())
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
            if (request.getCookies() != null && request.getCookies().Count > 0)
            {
                var cookieStore = new CookieContainer();
                foreach(var cookieEntry in request.getCookies())
                {
                    var cookie1 = new Cookie(cookieEntry.Key, cookieEntry.Value);
                    cookie1.Domain = UrlUtils.RemovePort(UrlUtils.GetDomain(request.Url));
                    cookieStore.Add(cookie1);
                }
                httpContext.Items.Container = cookieStore;
            }
            if (request.getHeaders() != null && request.getHeaders().Count > 0)
            {
                foreach (var header in request.getHeaders())
                {
                    httpContext.Items.Header.Add(header.Key, header.Value);
                }
            }
            return httpContext;
        }

        private HttpWebRequest convertHttpUriRequest(Request request, Site site, Proxy.Proxy proxy)
        {
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.
                Create(UrlUtils.FixIllegalCharacterInUrl(request.GetUrl()));
            httpWebRequest.Headers = new WebHeaderCollection();
            if (site.getHeaders() != null)
            {
                foreach(var headerEntry in site.getHeaders())
                {
                    httpWebRequest.Headers.Add(headerEntry.Key, headerEntry.Value);
                }
            }

            if (site != null)
            {
                httpWebRequest.Timeout = site.getTimeOut();
                httpWebRequest.ContinueTimeout = site.getTimeOut();
                httpWebRequest.ReadWriteTimeout = site.getTimeOut();
            }

            if (proxy != null)
            {
                httpWebRequest.Proxy = new WebProxy(proxy.Host, proxy.Port);
            }
            if (request.getHeaders() != null && request.getHeaders().Count > 0)
            {
                foreach(var header in request.getHeaders())
                {
                    httpWebRequest.Headers.Add(header.Key, header.Value);
                }
            }
            return httpWebRequest;
        }

    }
}
