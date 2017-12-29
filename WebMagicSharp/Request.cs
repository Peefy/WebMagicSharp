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
        private const long serialVersionUID = 2062192774891352043L;

        public const String CycleTriedTimes = "_cycle_tried_times";

        private string url;

        private string method;

        public string Url => url;

        private HttpRequestBody requestBody;

        /**
         * Store additional information in extras.
         */
        private Dictionary<String, Object> extras;

        /**
         * cookies for current url, if not set use Site's cookies
         */
        private Dictionary<String, String> cookies = 
            new Dictionary<string, string>();

        private Dictionary<String, String> headers =
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

        private String charset;

        public Request()
        {
        }

        public Request(String url)
        {
            this.url = url;
        }

        public long getPriority()
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
        public Request setPriority(long priority)
        {
            this.priority = priority;
            return this;
        }

        public Object getExtra(String key)
        {
            if (extras == null)
            {
                return null;
            }
            return extras[key];
        }

        public Request putExtra(String key, Object value)
        {
            if (extras == null)
            {
                extras = new Dictionary<string, object>();
            }
            extras.Add(key, value);
            return this;
        }

        public String GetUrl()
        {
            return url;
        }

        public Dictionary<String, Object> getExtras()
        {
            return extras;
        }

        public Request setExtras(Dictionary<String, Object> extras)
        {
            this.extras = extras;
            return this;
        }

        public Request setUrl(String url)
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
        public String getMethod()
        {
            return method;
        }

        public Request setMethod(String method)
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

        public Request addCookie(String name, String value)
        {
            cookies.Add(name, value);
            return this;
        }

        public Request addHeader(String name, String value)
        {
            headers.Add(name, value);
            return this;
        }

        public Dictionary<String, String> getCookies()
        {
            return cookies;
        }

        public Dictionary<String, String> getHeaders()
        {
            return headers;
        }

        public HttpRequestBody getRequestBody()
        {
            return requestBody;
        }

        public void setRequestBody(HttpRequestBody requestBody)
        {
            this.requestBody = requestBody;
        }

        public bool isBinaryContent()
        {
            return binaryContent;
        }

        public Request setBinaryContent(bool binaryContent)
        {
            this.binaryContent = binaryContent;
            return this;
        }

        public String getCharset()
        {
            return charset;
        }

        public Request setCharset(String charset)
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
            return this.getPriority().CompareTo(requestThat.getPriority());
        }
    }
}
