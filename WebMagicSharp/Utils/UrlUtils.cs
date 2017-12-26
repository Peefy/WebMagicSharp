using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WebMagicSharp.Utils
{
    public class UrlUtils
    {
        public static string CanonicalizeUrl(string url, string refer)
        {
            Uri baseUri;
            try
            {
                try
                {
                    baseUri = new Uri(refer);
                }
                catch 
                {
                    return new Uri(refer).ToString();
                }
                if (url.StartsWith("?") == true)
                    url = baseUri.LocalPath + url;
                var abs = new Uri(baseUri, url);
                return abs.ToString();
            }
            catch 
            {
                return "";
            }
        }

        public static string EncodeIllegalCharacterInUrl(string url)
        {
            return url.Replace(" ", "%20");
        }

        public static String FixIllegalCharacterInUrl(String url)
        {
            return url.Replace(" ", "%20").Replace("#+", "#");
        }

        public static string GetHost(String url)
        {
            string host = url;
            int i = url.IndexOf("/", 0, 3);
            if (i > 0)
            {
                host = url.Substring(0, i);
            }
            return host;
        }

        public const string patternForProtoca = "[\\w]+://";

        public static String RemoveProtocol(String url)
        {
            return Regex.Replace(url, patternForProtoca, "");
        }

        public static String GetDomain(String url)
        {
            String domain = RemoveProtocol(url);
            int i = domain.IndexOf("/",0, 1);
            if (i > 0)
            {
                domain = domain.Substring(0, i);
            }
            return RemovePort(domain);
        }

        public static String RemovePort(String domain)
        {
            int portIndex = domain.IndexOf(":");
            if (portIndex != -1)
            {
                return domain.Substring(0, portIndex);
            }
            else
            {
                return domain;
            }
        }

        public static List<Request> ConvertToRequests(IList<String> urls)
        {
            var requestList = new List<Request>();
            foreach (var url in urls)
                requestList.Add(new Request(url));
            return requestList;
        }

        public static List<String> ConvertToUrls(IList<Request> requests)
        {
            var urlList = new List<String>();
            foreach(var request in requests)
            {
                urlList.Add(request.Url);
            }
            return urlList;
        }

        public const string PatternForCharset = "charset\\s*=\\s*['\"]*([^\\s;'\"]*)";

        public static string GetCharset(string contentType)
        {
            var collection = Regex.Matches(contentType, PatternForCharset);
            if (collection.Count >= 1)
            {
                string charset = collection[1].Value;
                return charset;
            }
            return null;
        }

        

    }
}
