using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Selector;
using WebMagicSharp.Utils;

namespace WebMagicSharp
{
    public class Page
    {

        private Request request;

        private ResultItems resultItems = new ResultItems();

        private Html html;

        private Json json;

        private String rawText;

        private ISelectable url;

        private Dictionary<String, List<String>> headers;

        private int statusCode = HttpConstant.StatusCode.CODE_200;

        private bool downloadSuccess = true;

        private byte[] bytes;

        private List<Request> targetRequests = new List<Request>();

        private String charset;

        public Page()
        {
        }

        public static Page fail()
        {
            Page page = new Page();
            page.setDownloadSuccess(false);
            return page;
        }

        public Page setSkip(bool skip)
        {
            resultItems.SetSkip(skip);
            return this;

        }

        /**
         * store extract results
         *
         * @param key key
         * @param field field
         */
        public void putField(String key, Object field)
        {
            resultItems.Put(key, field);
        }

        /**
         * get html content of page
         *
         * @return html
         */
        public Html getHtml()
        {
            if (html == null)
            {
                html = new Html(rawText);
            }
            return html;
        }

        /**
         * get json content of page
         *
         * @return json
         * @since 0.5.0
         */
        public Json getJson()
        {
            if (json == null)
            {
                json = new Json(rawText);
            }
            return json;
        }

        /**
         * @param html html
         * @deprecated since 0.4.0
         * The html is parse just when first time of calling {@link #getHtml()}, so use {@link #setRawText(String)} instead.
         */
        public void setHtml(Html html)
        {
            this.html = html;
        }

        public List<Request> getTargetRequests()
        {
            return targetRequests;
        }

        /**
         * add urls to fetch
         *
         * @param requests requests
         */
        public void addTargetRequests(List<String> requests)
        {
            for(var i = 0;i < requests.Count ;++i)
            {
                var s = requests[i];
                if (string.IsNullOrEmpty(s) || s.Equals("#") || s.StartsWith("javascript:"))
                {
                    continue;
                }
                requests[i] = UrlUtils.CanonicalizeUrl(requests[i], url.ToString());
                targetRequests.Add(new Request(requests[i]));
            }
        }

        /**
         * add urls to fetch
         *
         * @param requests requests
         * @param priority priority
         */
        public void addTargetRequests(List<String> requests, long priority)
        {
            for (var i = 0; i < requests.Count; ++i)
            {
                var s = requests[i];
                if (string.IsNullOrEmpty(s) || s.Equals("#") || s.StartsWith("javascript:"))
                {
                    continue;
                }
                requests[i] = UrlUtils.CanonicalizeUrl(requests[i], url.ToString());
                targetRequests.Add(new Request(requests[i]).setPriority(priority));
            }
        }

        /**
         * add url to fetch
         *
         * @param requestString requestString
         */
        public void addTargetRequest(String requestString)
        {
            if (string.IsNullOrEmpty(requestString) || requestString.Equals("#"))
            {
                return;
            }
            requestString = UrlUtils.CanonicalizeUrl(requestString, url.ToString());
            targetRequests.Add(new Request(requestString));
        }

        /**
         * add requests to fetch
         *
         * @param request request
         */
        public void addTargetRequest(Request request)
        {
            targetRequests.Add(request);
        }

        /**
         * get url of current page
         *
         * @return url of current page
         */
        public ISelectable getUrl()
        {
            return url;
        }

        public void setUrl(ISelectable url)
        {
            this.url = url;
        }

        /**
         * get request of current page
         *
         * @return request
         */
        public Request getRequest()
        {
            return request;
        }

        public void setRequest(Request request)
        {
            this.request = request;
            this.resultItems.SetRequest(request);
        }

        public ResultItems getResultItems()
        {
            return resultItems;
        }

        public int getStatusCode()
        {
            return statusCode;
        }

        public void setStatusCode(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public String getRawText()
        {
            return rawText;
        }

        public Page setRawText(String rawText)
        {
            this.rawText = rawText;
            return this;
        }

        public Dictionary<String, List<String>> getHeaders()
        {
            return headers;
        }

        public void setHeaders(Dictionary<String, List<String>> headers)
        {
            this.headers = headers;
        }

        public bool isDownloadSuccess()
        {
            return downloadSuccess;
        }

        public void setDownloadSuccess(bool downloadSuccess)
        {
            this.downloadSuccess = downloadSuccess;
        }

        public byte[] getBytes()
        {
            return bytes;
        }

        public void setBytes(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public String getCharset()
        {
            return charset;
        }

        public void setCharset(String charset)
        {
            this.charset = charset;
        }

        public override string ToString()
        {
            return "Page{" +
                    "request=" + request +
                    ", resultItems=" + resultItems +
                    ", html=" + html +
                    ", json=" + json +
                    ", rawText='" + rawText + '\'' +
                    ", url=" + url +
                    ", headers=" + headers +
                    ", statusCode=" + statusCode +
                    ", downloadSuccess=" + downloadSuccess +
                    ", targetRequests=" + targetRequests +
                    ", charset='" + charset + '\'' +
                    ", bytes=" + ArrayToString(bytes) +
                    '}';
        }

        public static String ArrayToString(byte[] a)
        {
            if (a == null)
                return "null";
            int iMax = a.Length - 1;
            if (iMax == -1)
                return "[]";

            StringBuilder b = new StringBuilder();
            b.Append('[');
            for (int i = 0; ; i++)
            {
                b.Append(a[i]);
                if (i == iMax)
                    return b.Append(']').ToString();
                b.Append(", ");
            }
        }

    }
}
