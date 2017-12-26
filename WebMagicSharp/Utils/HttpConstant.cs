using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public class HttpConstant
    {
        public static class Method
        {

            public const String GET = "GET";

            public const String HEAD = "HEAD";

            public const String POST = "POST";

            public const String PUT = "PUT";

            public const String DELETE = "DELETE";

            public const String TRACE = "TRACE";

            public const String CONNECT = "CONNECT";

        }

        public static class StatusCode
        {

            public const int CODE_200 = 200;

        }

        public static class Header
        {

            public const String REFERER = "Referer";

            public const String USER_AGENT = "User-Agent";
        }
    }
}
