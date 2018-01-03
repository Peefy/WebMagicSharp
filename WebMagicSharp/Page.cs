using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Selector;
using WebMagicSharp.Utils;

namespace WebMagicSharp
{
    /// <summary>
    /// Page.
    /// </summary>
    public class Page
    {

        private Request _request;

        private ResultItems _resultItems = new ResultItems();

        private Html _html;

        private Json _json;

        private string _rawText;

        private ISelectable _url;

        private Dictionary<string, List<string>> _headers;

        private int _statusCode = HttpConstant.StatusCode.Code200;

        private bool _downloadSuccess = true;

        private byte[] _bytes;

        private List<Request> _targetRequests = new List<Request>();

        private string _charset;

        public Page()
        {
        }

        public static Page Fail()
        {
            Page page = new Page();
            page.SetDownloadSuccess(false);
            return page;
        }

        public Page SetSkip(bool skip)
        {
            _resultItems.SetSkip(skip);
            return this;

        }

        /**
         * store extract results
         *
         * @param key key
         * @param field field
         */
        public void PutField(string key, Object field)
        {
            _resultItems.Put(key, field);
        }

        /**
         * get html content of page
         *
         * @return html
         */
        public Html GetHtml()
        {
            if (_html == null)
            {
                _html = new Html(_rawText);
            }
            return _html;
        }

        /**
         * get json content of page
         *
         * @return json
         * @since 0.5.0
         */
        public Json GetJson()
        {
            if (_json == null)
            {
                _json = new Json(_rawText);
            }
            return _json;
        }

        /**
         * @param html html
         * @deprecated since 0.4.0
         * The html is parse just when first time of calling {@link #getHtml()}, so use {@link #setRawText(String)} instead.
         */
        public void SetHtml(Html html)
        {
            this._html = html;
        }

        public List<Request> GetTargetRequests()
        {
            return _targetRequests;
        }

        /**
         * add urls to fetch
         *
         * @param requests requests
         */
        public void AddTargetRequests(List<string> requests)
        {
            for(var i = 0;i < requests.Count ;++i)
            {
                var s = requests[i];
                if (string.IsNullOrEmpty(s) || s.Equals("#") || s.StartsWith("javascript:"))
                {
                    continue;
                }
                requests[i] = UrlUtils.CanonicalizeUrl(requests[i], _url.ToString());
                _targetRequests.Add(new Request(requests[i]));
            }
        }

        /**
         * add urls to fetch
         *
         * @param requests requests
         * @param priority priority
         */
        public void AddTargetRequests(List<string> requests, long priority)
        {
            for (var i = 0; i < requests.Count; ++i)
            {
                var s = requests[i];
                if (string.IsNullOrEmpty(s) || s.Equals("#") || s.StartsWith("javascript:"))
                {
                    continue;
                }
                requests[i] = UrlUtils.CanonicalizeUrl(requests[i], _url.ToString());
                _targetRequests.Add(new Request(requests[i]).SetPriority(priority));
            }
        }

        /**
         * add url to fetch
         *
         * @param requestString requestString
         */
        public void AddTargetRequest(string requestString)
        {
            if (string.IsNullOrEmpty(requestString) || requestString.Equals("#"))
            {
                return;
            }
            requestString = UrlUtils.CanonicalizeUrl(requestString, _url.ToString());
            _targetRequests.Add(new Request(requestString));
        }

        /**
         * add requests to fetch
         *
         * @param request request
         */
        public void AddTargetRequest(Request request)
        {
            _targetRequests.Add(request);
        }

        /**
         * get url of current page
         *
         * @return url of current page
         */
        public ISelectable GetUrl()
        {
            return _url;
        }

        public void SetUrl(ISelectable url)
        {
            this._url = url;
        }

        /**
         * get request of current page
         *
         * @return request
         */
        public Request GetRequest()
        {
            return _request;
        }

        public void SetRequest(Request request)
        {
            this._request = request;
            this._resultItems.SetRequest(request);
        }

        public ResultItems GetResultItems()
        {
            return _resultItems;
        }

        public int GetStatusCode()
        {
            return _statusCode;
        }

        public void SetStatusCode(int statusCode)
        {
            this._statusCode = statusCode;
        }

        public string GetRawText()
        {
            return _rawText;
        }

        public Page SetRawText(string rawText)
        {
            this._rawText = rawText;
            return this;
        }

        public Dictionary<string, List<string>> GetHeaders()
        {
            return _headers;
        }

        public void SetHeaders(Dictionary<string, List<string>> headers)
        {
            this._headers = headers;
        }

        public bool IsDownloadSuccess()
        {
            return _downloadSuccess;
        }

        public void SetDownloadSuccess(bool downloadSuccess)
        {
            this._downloadSuccess = downloadSuccess;
        }

        public byte[] GetBytes()
        {
            return _bytes;
        }

        public void SetBytes(byte[] bytes)
        {
            this._bytes = bytes;
        }

        public string GetCharset()
        {
            return _charset;
        }

        public void SetCharset(string charset)
        {
            this._charset = charset;
        }

        public override string ToString()
        {
            return "Page{" +
                    "request=" + _request +
                    ", resultItems=" + _resultItems +
                    ", html=" + _html +
                    ", json=" + _json +
                    ", rawText='" + _rawText + '\'' +
                    ", url=" + _url +
                    ", headers=" + _headers +
                    ", statusCode=" + _statusCode +
                    ", downloadSuccess=" + _downloadSuccess +
                    ", targetRequests=" + _targetRequests +
                    ", charset='" + _charset + '\'' +
                    ", bytes=" + ArrayToString(_bytes) +
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
