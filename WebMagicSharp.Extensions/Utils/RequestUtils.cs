using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class RequestUtils
    {
        private static string p4Range = "\\[(\\d+)\\-(\\d+)\\]";

        public static List<Request> From(string exp)
        {
            var matcher = Regex.Match(exp, p4Range);
            if(matcher.NextMatch() != null)
            {
                return new List<Request>
                {
                    new Request(exp)
                };
            }
            int rangeFrom = int.Parse(matcher.Groups[1].ToString());
            int rangeTo = int.Parse(matcher.Groups[2].ToString());
            if (rangeFrom > rangeTo)
            {
                return new List<Request>();
            }
            List<Request> requests = new List<Request>();
            for (int i = rangeFrom; i <= rangeTo; i++)
            {
                requests.Add(new Request(Regex.Replace(exp, p4Range, i.ToString())));
            }
            return requests;
        }

    }
}
