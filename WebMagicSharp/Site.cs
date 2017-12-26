using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp
{
    public class Site
    {
        private string domain;

        private string userAgent;

        private Dictionary<String, String> defaultCookies = 
            new Dictionary<string, string>();

        private Dictionary<String, Dictionary<String, String>> cookies =
            new Dictionary<string, Dictionary<string, string>>();

        private String charset;

        private int sleepTime = 5000;

        private int retryTimes = 0;

        private int cycleRetryTimes = 0;

        private int retrySleepTime = 1000;

        private int timeOut = 5000;

        private static List<int> DEFAULT_STATUS_CODE_SET = new List<int>();

        private List<int> acceptStatCode = DEFAULT_STATUS_CODE_SET;

        private Dictionary<String, String> headers =
            new Dictionary<string, string>();

        private bool useGzip = true;

        private bool disableCookieManagement = false;

        public Site()
        {
            DEFAULT_STATUS_CODE_SET.Add(Utils.HttpConstant.StatusCode.CODE_200);
        }

        public static Site Me()
        {
            return new Site();
        }

        /**
         * Add a cookie with domain {@link #getDomain()}
         *
         * @param name name
         * @param value value
         * @return this
         */
        public Site AddCookie(String name, String value)
        {
            defaultCookies.Add(name, value);
            return this;
        }

        /**
         * Add a cookie with specific domain.
         *
         * @param domain domain
         * @param name name
         * @param value value
         * @return this
         */
        public Site addCookie(String domain, String name, String value)
        {
            if (!cookies.ContainsKey(domain))
            {
                cookies.Add(domain, new Dictionary<string, string>());
            }
            cookies[domain].Add(name, value);
            return this;
        }

        /**
         * set user agent
         *
         * @param userAgent userAgent
         * @return this
         */
        public Site setUserAgent(String userAgent)
        {
            this.userAgent = userAgent;
            return this;
        }

        /**
         * get cookies
         *
         * @return get cookies
         */
        public Dictionary<String, String> getCookies()
        {
            return defaultCookies;
        }

        /**
         * get cookies of all domains
         *
         * @return get cookies
         */
        public Dictionary<String, Dictionary<String, String>> getAllCookies()
        {
            return cookies;
        }

        /**
         * get user agent
         *
         * @return user agent
         */
        public String getUserAgent()
        {
            return userAgent;
        }

        /**
         * get domain
         *
         * @return get domain
         */
        public String getDomain()
        {
            return domain;
        }

        /**
         * set the domain of site.
         *
         * @param domain domain
         * @return this
         */
        public Site setDomain(String domain)
        {
            this.domain = domain;
            return this;
        }

        /**
         * Set charset of page manually.<br>
         * When charset is not set or set to null, it can be auto detected by Http header.
         *
         * @param charset charset
         * @return this
         */
        public Site setCharset(String charset)
        {
            this.charset = charset;
            return this;
        }

        /**
         * get charset set manually
         *
         * @return charset
         */
        public String getCharset()
        {
            return charset;
        }

        public int getTimeOut()
        {
            return timeOut;
        }

        /**
         * set timeout for downloader in ms
         *
         * @param timeOut timeOut
         * @return this
         */
        public Site setTimeOut(int timeOut)
        {
            this.timeOut = timeOut;
            return this;
        }

        /**
         * Set acceptStatCode.<br>
         * When status code of http response is in acceptStatCodes, it will be processed.<br>
         * {200} by default.<br>
         * It is not necessarily to be set.<br>
         *
         * @param acceptStatCode acceptStatCode
         * @return this
         */
        public Site setAcceptStatCode(List<int> acceptStatCode)
        {
            this.acceptStatCode = acceptStatCode;
            return this;
        }

        /**
         * get acceptStatCode
         *
         * @return acceptStatCode
         */
        public List<int> getAcceptStatCode()
        {
            return acceptStatCode;
        }

        /**
         * Set the interval between the processing of two pages.<br>
         * Time unit is micro seconds.<br>
         *
         * @param sleepTime sleepTime
         * @return this
         */
        public Site setSleepTime(int sleepTime)
        {
            this.sleepTime = sleepTime;
            return this;
        }

        /**
         * Get the interval between the processing of two pages.<br>
         * Time unit is micro seconds.<br>
         *
         * @return the interval between the processing of two pages,
         */
        public int getSleepTime()
        {
            return sleepTime;
        }

        /**
         * Get retry times immediately when download fail, 0 by default.<br>
         *
         * @return retry times when download fail
         */
        public int getRetryTimes()
        {
            return retryTimes;
        }

        public Dictionary<String, String> getHeaders()
        {
            return headers;
        }

        /**
         * Put an Http header for downloader. <br>
         * Use {@link #addCookie(String, String)} for cookie and {@link #setUserAgent(String)} for user-agent. <br>
         *
         * @param key   key of http header, there are some keys constant in {@link HttpConstant.Header}
         * @param value value of header
         * @return this
         */
        public Site addHeader(String key, String value)
        {
            headers.Add(key, value);
            return this;
        }

        /**
         * Set retry times when download fail, 0 by default.<br>
         *
         * @param retryTimes retryTimes
         * @return this
         */
        public Site setRetryTimes(int retryTimes)
        {
            this.retryTimes = retryTimes;
            return this;
        }

        /**
         * When cycleRetryTimes is more than 0, it will add back to scheduler and try download again. <br>
         *
         * @return retry times when download fail
         */
        public int getCycleRetryTimes()
        {
            return cycleRetryTimes;
        }

        /**
         * Set cycleRetryTimes times when download fail, 0 by default. <br>
         *
         * @param cycleRetryTimes cycleRetryTimes
         * @return this
         */
        public Site setCycleRetryTimes(int cycleRetryTimes)
        {
            this.cycleRetryTimes = cycleRetryTimes;
            return this;
        }

        public bool isUseGzip()
        {
            return useGzip;
        }

        public int getRetrySleepTime()
        {
            return retrySleepTime;
        }

        /**
         * Set retry sleep times when download fail, 1000 by default. <br>
         *
         * @param retrySleepTime retrySleepTime
         * @return this
         */
        public Site setRetrySleepTime(int retrySleepTime)
        {
            this.retrySleepTime = retrySleepTime;
            return this;
        }

        /**
         * Whether use gzip. <br>
         * Default is true, you can set it to false to disable gzip.
         *
         * @param useGzip useGzip
         * @return this
         */
        public Site setUseGzip(bool useGzip)
        {
            this.useGzip = useGzip;
            return this;
        }

        public bool isDisableCookieManagement()
        {
            return disableCookieManagement;
        }

        /**
         * Downloader is supposed to store response cookie.
         * Disable it to ignore all cookie fields and stay clean.
         * Warning: Set cookie will still NOT work if disableCookieManagement is true.
         * @param disableCookieManagement disableCookieManagement
         * @return this
         */
        public Site setDisableCookieManagement(bool disableCookieManagement)
        {
            this.disableCookieManagement = disableCookieManagement;
            return this;
        }

        public ITask toTask()
        {
            return new BaseTask(this);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null ) 
                return false;

            Site site = (Site)obj;

            if (cycleRetryTimes != site.cycleRetryTimes) return false;
            if (retryTimes != site.retryTimes) return false;
            if (sleepTime != site.sleepTime) return false;
            if (timeOut != site.timeOut) return false;
            if (acceptStatCode != null ? !acceptStatCode.Equals(site.acceptStatCode) : site.acceptStatCode != null)
                return false;
            if (charset != null ? !charset.Equals(site.charset) : site.charset != null) return false;
            if (defaultCookies != null ? !defaultCookies.Equals(site.defaultCookies) : site.defaultCookies != null)
                return false;
            if (domain != null ? !domain.Equals(site.domain) : site.domain != null) return false;
            if (headers != null ? !headers.Equals(site.headers) : site.headers != null) return false;
            if (userAgent != null ? !userAgent.Equals(site.userAgent) : site.userAgent != null) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = domain != null ? domain.GetHashCode() : 0;
            result = 31 * result + (userAgent != null ? userAgent.GetHashCode() : 0);
            result = 31 * result + (defaultCookies != null ? defaultCookies.GetHashCode() : 0);
            result = 31 * result + (charset != null ? charset.GetHashCode() : 0);
            result = 31 * result + sleepTime;
            result = 31 * result + retryTimes;
            result = 31 * result + cycleRetryTimes;
            result = 31 * result + timeOut;
            result = 31 * result + (acceptStatCode != null ? acceptStatCode.GetHashCode() : 0);
            result = 31 * result + (headers != null ? headers.GetHashCode() : 0);
            return result;
        }

        public override String ToString()
        {
            return "Site{" +
                    "domain='" + domain + '\'' +
                    ", userAgent='" + userAgent + '\'' +
                    ", cookies=" + defaultCookies +
                    ", charset='" + charset + '\'' +
                    ", sleepTime=" + sleepTime +
                    ", retryTimes=" + retryTimes +
                    ", cycleRetryTimes=" + cycleRetryTimes +
                    ", timeOut=" + timeOut +
                    ", acceptStatCode=" + acceptStatCode +
                    ", headers=" + headers +
                    '}';
        }

    }
}
