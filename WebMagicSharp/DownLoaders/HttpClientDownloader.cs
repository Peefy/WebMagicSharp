using System;
using System.Diagnostics;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.IO;

using HttpCode.Core;

using WebMagicSharp.Proxy;
using WebMagicSharp.Selector;
using WebMagicSharp.Utils;

namespace WebMagicSharp.DownLoaders
{
    public class HttpClientDownloader : AbstractDownloader
    {

        private Dictionary<String, HttpCoreClient> httpClients = 
            new Dictionary<String, HttpCoreClient>();

        private HttpClientGenerator httpClientGenerator = new HttpClientGenerator();

        private HttpUriRequestConverter httpUriRequestConverter = new HttpUriRequestConverter();

        private IProxyProvider proxyProvider;

        private bool responseHeader = true;

        private object lockedObj = new object();

        string contentType;

        string charset;

        HttpClient client;

        public void setProxyProvider(IProxyProvider proxyProvider)
        {
            this.proxyProvider = proxyProvider;
        }

        public void setHttpUriRequestConverter(HttpUriRequestConverter httpUriRequestConverter)
        {
            this.httpUriRequestConverter = httpUriRequestConverter;
        }

        private HttpCoreClient GetHttpClient(Site site)
        {
            if (site == null)
            {
                return httpClientGenerator.getClient(null);
            }
            var domain = site.getDomain();
            HttpCoreClient httpClient;
            if (httpClients.TryGetValue(domain, out httpClient))
            {
                return httpClient;
            }
            else
            {
                lock (lockedObj)
                {
                    httpClient = httpClientGenerator.getClient(site);
                    httpClients.Add(domain, httpClient);
                }
            }
            return httpClient;
        }

        public override Page Download(Request request, ITask task)
        {
            if (task == null || task.GetSite() == null)
            {
                throw new ArgumentNullException("task or site can not be null");
            }
            HttpResults httpResponse = null;
            var httpClient = GetHttpClient(task.GetSite());
            var proxy = proxyProvider != null ? proxyProvider.GetProxy(task) : null;
            var requestContext = httpUriRequestConverter.Convert(request, task.GetSite(), proxy);
            Page page = Page.fail();
            try
            {             
                httpResponse = httpClient.GetHtml();
                contentType = httpClient.Items.ContentType;
                charset = httpClient.Items.EncodingStr;
                page = handleResponse(request, request.getCharset() != null ? request.getCharset() : task.GetSite().getCharset(), httpResponse, task);
                OnSuccess(request);
                Debug.WriteLine($"downloading page success {request.GetUrl()}");
                return page;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"download page {request.GetUrl()} error");
                OnError(request);
                return page;
            }
            finally
            {
                if (httpResponse != null)
                {
                    //ensure the connection is released back to pool
                    
                }
                if (proxyProvider != null && proxy != null)
                {
                    proxyProvider.ReturnProxy(proxy, page, task);
                }
            }
        }

        protected Page handleResponse(Request request, String charset, HttpResults httpResponse, ITask task) 
        {
            var bytes = httpResponse.ResultByte;
            Page page = new Page();
            page.setBytes(bytes);
            page.setCharset(charset);
            page.setRawText(Encoding.GetEncoding(charset).GetString(bytes));        
            page.setUrl(new PlainText(request.GetUrl()));
            page.setRequest(request);
            page.setStatusCode(httpResponse.StatusCodeNum);
            page.setDownloadSuccess(true);
            if (responseHeader)
            {
                page.setHeaders(HttpClientUtils.ConvertHeaders(httpResponse.Header));
            }
            return page;
        }

        public override void SetThread(int threadNum)
        {
            throw new NotImplementedException();
        }

        public override void OnSuccess(Request request)
        {
            base.OnSuccess(request);
        }

        public override void OnError(Request request)
        {
            base.OnError(request);
        }

    }
}
