using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Model;

namespace WebMagicSharp
{
    /// <summary>
    /// Request.
    /// </summary>
    public class Request : IComparable
    {
        public const string CycleTriedTimes = "_cycle_tried_times";

        private string url;

        private string method;

        public string Url => url;

        private HttpRequestBody requestBody;

        /**
         * Store additional information in extras.
         */
        private Dictionary<string, Object> extras;

        /**
         * cookies for current url, if not set use Site's cookies
         */
        private Dictionary<string, string> cookies = 
            new Dictionary<string, string>();

        private Dictionary<string, string> headers =
            new Dictionary<string, string>();

        /**
         * Priority of the request.<br>
         * The bigger will be processed earlier. <br>
         * @see us.codecraft.webmagic.scheduler.PriorityScheduler
         */
        private long priority;

        /**
         * When it is set to TRUE, the downloader will not try to parse response body to text.
         *
         */
        private bool binaryContent = false;

        private string charset;

        public Request()
        {
        }

        public Request(string url)
        {
            this.url = url;
        }

        public long GetPriority()
        {
            return priority;
        }

        /**
         * Set the priority of request for sorting.<br>
         * Need a scheduler supporting priority.<br>
         * @see us.codecraft.webmagic.scheduler.PriorityScheduler
         *
         * @param priority priority
         * @return this
         */
        public Request SetPriority(long priority)
        {
            this.priority = priority;
            return this;
        }

        public object GetExtra(string key)
        {
            if (extras == null)
            {
                return null;
            }
            return extras[key];
        }

        public Request PutExtra(string key, object value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }

        public string GetUrl()
        {
            return url;
        }

        public Dictionary<string, object> GetExtras()
        {
            return extras;
        }

        public Request SetExtras(Dictionary<string, Object> extras)
        {
            this.extras = extras;
            return this;
        }

        public Request SetUrl(string url)
        {
            this.url = url;
            return this;
        }

        /**
         * The http method of the request. Get for default.
         * @return httpMethod
         * @see us.codecraft.webmagic.utils.HttpConstant.Method
         * @since 0.5.0
         */
        public string GetMethod()
        {
            return method;
        }

        public Request SetMethod(string method)
        {
            this.method = method;
            return this;
        }

        public override int GetHashCode()
        {
            int result = url != null ? url.GetHashCode() : 0;
            result = 31 * result + (method != null ? method.GetHashCode() : 0);
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;

            Request request = (Request)obj;

            if (url != null ? !url.Equals(request.url) : request.url != null) return false;
            return method != null ? method.Equals(request.method) : request.method == null;
        }

        public Request AddCookie(string name, string value)
        {
            cookies.Add(name, value);
            return this;
        }

        public Request AddHeader(string name, string value)
        {
            headers.Add(name, value);
            return this;
        }

        public Dictionary<string, string> GetCookies()
        {
            return cookies;
        }

        public Dictionary<string, string> GetHeaders()
        {
            return headers;
        }

        public HttpRequestBody GetRequestBody()
        {
            return requestBody;
        }

        public void SetRequestBody(HttpRequestBody requestBody)
        {
            this.requestBody = requestBody;
        }

        public bool IsBinaryContent()
        {
            return binaryContent;
        }

        public Request SetBinaryContent(bool binaryContent)
        {
            this.binaryContent = binaryContent;
            return this;
        }

        public string GetCharset()
        {
            return charset;
        }

        public Request SetCharset(string charset)
        {
            this.charset = charset;
            return this;
        }

        public override string ToString()
        {
            return "Request{" +
                    "url='" + url + '\'' +
                    ", method='" + method + '\'' +
                    ", extras=" + extras +
                    ", priority=" + priority +
                    ", headers=" + headers +
                    ", cookies=" + cookies +
                    '}';
        }

        public int CompareTo(object obj)
        {
            if (obj is null)
                return -1;
            var requestThat = obj as Request;
            return this.GetPriority().CompareTo(requestThat.GetPriority());
        }
    }
}
