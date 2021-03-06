﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DuGu.Standard.Http;

using WebMagicSharp.Proxy;
using WebMagicSharp.Selector;
using WebMagicSharp.Utils;

namespace WebMagicSharp.DownLoaders
{
    public class HttpClientDownloader : AbstractDownloader
    {

        private Dictionary<string, HttpCoreClient> _httpClients = 
            new Dictionary<string, HttpCoreClient>();

        private HttpClientGenerator _httpClientGenerator = new HttpClientGenerator();

        private HttpUriRequestConverter _httpUriRequestConverter = new HttpUriRequestConverter();

        private IProxyProvider _proxyProvider;

        private bool _responseHeader = true;

        private object _lockedObj = new object();

        string _contentType;

        string _charset;

        public void SetProxyProvider(IProxyProvider proxyProvider)
        {
            this._proxyProvider = proxyProvider;
        }

        public void SetHttpUriRequestConverter(HttpUriRequestConverter httpUriRequestConverter)
        {
            this._httpUriRequestConverter = httpUriRequestConverter;
        }

        private HttpCoreClient GetHttpClient(Site site)
        {
            if (site == null)
            {
                return _httpClientGenerator.GetClient(null);
            }
            var domain = site.Domain;
            if (_httpClients.TryGetValue(domain, out var httpClient))
            {
                return httpClient;
            }
            else
            {
                lock (_lockedObj)
                {
                    httpClient = _httpClientGenerator.GetClient(site);
                    _httpClients.Add(domain, httpClient);
                }
            }
            return httpClient;
        }

        public override Page Download(Request request, ITask task)
        {
            if (task == null || task.GetSite() == null)
            {
                throw new ArgumentNullException("task", "task or site can not be null");
            }
            HttpResults httpResponse = null;
            var httpClient = GetHttpClient(task.GetSite());
            var proxy = _proxyProvider?.GetProxy(task);
            var requestContext = _httpUriRequestConverter.Convert(request, task.GetSite(), proxy);
            Page page = Page.Fail();
            try
            {             
                httpResponse = httpClient.GetHtml();
                _contentType = httpClient.Items.ContentType;
                _charset = httpClient.Items.EncodingStr;
                page = HandleResponse(request, request.GetCharset() ?? task.GetSite().Charset, httpResponse, task);
                OnSuccess(request);
                Debug.WriteLine($"downloading page success {request.GetUrl()}");
                return page;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"download page {request.GetUrl()} error : {e.Message}");
                OnError(request);
                return page;
            }
            finally
            {
                if (httpResponse != null)
                {
                    //ensure the connection is released back to pool        
                }
                if (_proxyProvider != null && proxy != null)
                {
                    _proxyProvider.ReturnProxy(proxy, page, task);
                }
            }
        }

        protected Page HandleResponse(Request request, string charset, HttpResults httpResponse, ITask task) 
        {
            var bytes = httpResponse.ResultByte;
            Page page = new Page();
            page.SetBytes(bytes);
            page.SetCharset(charset);
            page.SetRawText(Encoding.GetEncoding(charset).GetString(bytes));        
            page.SetUrl(new PlainText(request.GetUrl()));
            page.SetRequest(request);
            page.SetStatusCode(httpResponse.StatusCodeNum);
            page.SetDownloadSuccess(true);
            if (_responseHeader)
            {
                page.SetHeaders(HttpClientUtils.ConvertHeaders(httpResponse.Header));
            }
            return page;
        }

        public override void SetThread(int threadNum)
        {
            //throw new NotImplementedException();
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
