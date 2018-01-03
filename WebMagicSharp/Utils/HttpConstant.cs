using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class HttpConstant
    {
        public static class Method
        {

            public const string Get = "GET";

            public const string Head = "HEAD";

            public const string Post = "POST";

            public const string Put = "PUT";

            public const string Delete = "DELETE";

            public const string Trace = "TRACE";

            public const string Connect = "CONNECT";

        }

        public static class StatusCode
        {

            public const int Code200 = 200;

        }

        public static class Header
        {

            public const string Referer = "Referer";

            public const string UserAgent = "User-Agent";
        }
    }
}
