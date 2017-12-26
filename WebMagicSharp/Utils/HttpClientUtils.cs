using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class HttpClientUtils
    {
        public static Dictionary<String, List<String>> ConvertHeaders(WebHeaderCollection headers)
        {
            Dictionary<String, List<String>> results = 
                new Dictionary<String, List<String>>();
            foreach (var header in headers.AllKeys)
            {
                if(results.TryGetValue(header,out var list) == true)
                {
                    list.Add(headers[header]);
                }
                else
                {
                    list = new List<string>();
                    results.Add(header, list);
                    list.Add(headers[header]);
                }
            }
            return results;
        }
    }
}
