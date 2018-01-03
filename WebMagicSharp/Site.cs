using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp
{
    /// <summary>
    /// Site.
    /// </summary>
    public class Site
    {
        private string _domain;

        private string _userAgent;

        private Dictionary<string, string> _defaultCookies = 
            new Dictionary<string, string>();

        private Dictionary<string, Dictionary<string, string>> _cookies =
            new Dictionary<string, Dictionary<string, string>>();

        private string _charset;

        private int _sleepTime = 5000;

        private int _retryTimes = 0;

        private int _cycleRetryTimes = 0;

        private int _retrySleepTime = 1000;

        private int _timeOut = 5000;

        public static List<int> DefaultStatusCodeSet = new List<int>()
        {
            Utils.HttpConstant.StatusCode.Code200
        };

        private List<int> _acceptStatCode = DefaultStatusCodeSet;

        private Dictionary<string, string> _headers =
            new Dictionary<string, string>();

        private bool _useGzip = true;

        private bool _disableCookieManagement = false;

        public Site()
        {

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
        public Site AddCookie(string name, string value)
        {
            _defaultCookies.Add(name, value);
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
        public Site AddCookie(string domain, string name, string value)
        {
            if (!_cookies.ContainsKey(domain))
            {
                _cookies.Add(domain, new Dictionary<string, string>());
            }
            _cookies[domain].Add(name, value);
            return this;
        }

        /**
         * set user agent
         *
         * @param userAgent userAgent
         * @return this
         */
        public Site SetUserAgent(string userAgent)
        {
            this._userAgent = userAgent;
            return this;
        }

        /**
         * get cookies
         *
         * @return get cookies
         */
        public Dictionary<string, string> GetCookies()
        {
            return _defaultCookies;
        }

        /**
         * get cookies of all domains
         *
         * @return get cookies
         */
        public Dictionary<string, Dictionary<string, string>> GetAllCookies()
        {
            return _cookies;
        }

        /**
         * get user agent
         *
         * @return user agent
         */
        public string UserAgent => _userAgent;

        /**
         * get domain
         *
         * @return get domain
         */
        public string Domain => _domain;

        /**
         * set the domain of site.
         *
         * @param domain domain
         * @return this
         */
        public Site SetDomain(string domain)
        {
            this._domain = domain;
            return this;
        }

        /**
         * Set charset of page manually.<br>
         * When charset is not set or set to null, it can be auto detected by Http header.
         *
         * @param charset charset
         * @return this
         */
        public Site SetCharset(string charset)
        {
            this._charset = charset;
            return this;
        }

        /**
         * get charset set manually
         *
         * @return charset
         */
        public string Charset => _charset;

        public int TimeOut => _timeOut;

        /**
         * set timeout for downloader in ms
         *
         * @param timeOut timeOut
         * @return this
         */
        public Site SetTimeOut(int timeOut)
        {
            this._timeOut = timeOut;
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
        public Site SetAcceptStatCode(List<int> acceptStatCode)
        {
            this._acceptStatCode = acceptStatCode;
            return this;
        }

        /**
         * get acceptStatCode
         *
         * @return acceptStatCode
         */
        public List<int> AcceptStatCode => _acceptStatCode;

        /**
         * Set the interval between the processing of two pages.<br>
         * Time unit is micro seconds.<br>
         *
         * @param sleepTime sleepTime
         * @return this
         */
        public Site SetSleepTime(int sleepTime)
        {
            this._sleepTime = sleepTime;
            return this;
        }

        /**
         * Get the interval between the processing of two pages.<br>
         * Time unit is micro seconds.<br>
         *
         * @return the interval between the processing of two pages,
         */
        public int SleepTime => _sleepTime;

        /**
         * Get retry times immediately when download fail, 0 by default.<br>
         *
         * @return retry times when download fail
         */
        public int RetryTimes => _retryTimes;

        public Dictionary<string, string> Headers => _headers;

        /**
         * Put an Http header for downloader. <br>
         * Use {@link #addCookie(String, String)} for cookie and {@link #setUserAgent(String)} for user-agent. <br>
         *
         * @param key   key of http header, there are some keys constant in {@link HttpConstant.Header}
         * @param value value of header
         * @return this
         */
        public Site AddHeader(string key, string value)
        {
            _headers.Add(key, value);
            return this;
        }

        /**
         * Set retry times when download fail, 0 by default.<br>
         *
         * @param retryTimes retryTimes
         * @return this
         */
        public Site SetRetryTimes(int retryTimes)
        {
            this._retryTimes = retryTimes;
            return this;
        }

        /**
         * When cycleRetryTimes is more than 0, it will add back to scheduler and try download again. <br>
         *
         * @return retry times when download fail
         */
        public int GetCycleRetryTimes()
        {
            return _cycleRetryTimes;
        }

        /**
         * Set cycleRetryTimes times when download fail, 0 by default. <br>
         *
         * @param cycleRetryTimes cycleRetryTimes
         * @return this
         */
        public Site SetCycleRetryTimes(int cycleRetryTimes)
        {
            this._cycleRetryTimes = cycleRetryTimes;
            return this;
        }

        public bool IsUseGzip()
        {
            return _useGzip;
        }

        public int GetRetrySleepTime()
        {
            return _retrySleepTime;
        }

        /**
         * Set retry sleep times when download fail, 1000 by default. <br>
         *
         * @param retrySleepTime retrySleepTime
         * @return this
         */
        public Site SetRetrySleepTime(int retrySleepTime)
        {
            this._retrySleepTime = retrySleepTime;
            return this;
        }

        /**
         * Whether use gzip. <br>
         * Default is true, you can set it to false to disable gzip.
         *
         * @param useGzip useGzip
         * @return this
         */
        public Site SetUseGzip(bool useGzip)
        {
            this._useGzip = useGzip;
            return this;
        }

        public bool IsDisableCookieManagement()
        {
            return _disableCookieManagement;
        }

        /**
         * Downloader is supposed to store response cookie.
         * Disable it to ignore all cookie fields and stay clean.
         * Warning: Set cookie will still NOT work if disableCookieManagement is true.
         * @param disableCookieManagement disableCookieManagement
         * @return this
         */
        public Site SetDisableCookieManagement(bool disableCookieManagement)
        {
            this._disableCookieManagement = disableCookieManagement;
            return this;
        }

        public ITask ToTask()
        {
            return new BaseTask(this);
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null ) 
                return false;

            Site site = (Site)obj;

            if (_cycleRetryTimes != site._cycleRetryTimes) return false;
            if (_retryTimes != site._retryTimes) return false;
            if (_sleepTime != site._sleepTime) return false;
            if (_timeOut != site._timeOut) return false;
            if (_acceptStatCode != null ? !_acceptStatCode.Equals(site._acceptStatCode) : site._acceptStatCode != null)
                return false;
            if (_charset != null ? !_charset.Equals(site._charset) : site._charset != null) return false;
            if (_defaultCookies != null ? !_defaultCookies.Equals(site._defaultCookies) : site._defaultCookies != null)
                return false;
            if (_domain != null ? !_domain.Equals(site._domain) : site._domain != null) return false;
            if (_headers != null ? !_headers.Equals(site._headers) : site._headers != null) return false;
            if (_userAgent != null ? !_userAgent.Equals(site._userAgent) : site._userAgent != null) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = _domain != null ? _domain.GetHashCode() : 0;
            result = 31 * result + (_userAgent != null ? _userAgent.GetHashCode() : 0);
            result = 31 * result + (_defaultCookies != null ? _defaultCookies.GetHashCode() : 0);
            result = 31 * result + (_charset != null ? _charset.GetHashCode() : 0);
            result = 31 * result + _sleepTime;
            result = 31 * result + _retryTimes;
            result = 31 * result + _cycleRetryTimes;
            result = 31 * result + _timeOut;
            result = 31 * result + (_acceptStatCode != null ? _acceptStatCode.GetHashCode() : 0);
            result = 31 * result + (_headers != null ? _headers.GetHashCode() : 0);
            return result;
        }

        public override string ToString()
        {
            return "Site{" +
                    "domain='" + _domain + '\'' +
                    ", userAgent='" + _userAgent + '\'' +
                    ", cookies=" + _defaultCookies +
                    ", charset='" + _charset + '\'' +
                    ", sleepTime=" + _sleepTime +
                    ", retryTimes=" + _retryTimes +
                    ", cycleRetryTimes=" + _cycleRetryTimes +
                    ", timeOut=" + _timeOut +
                    ", acceptStatCode=" + _acceptStatCode +
                    ", headers=" + _headers +
                    '}';
        }

    }
}
