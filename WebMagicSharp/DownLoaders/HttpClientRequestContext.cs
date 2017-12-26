using System;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.DownLoaders
{
    public class HttpClientRequestContext
    {
        public HttpCoreClient HttpClient { get; set; }

        public HttpWebRequest HttpRequest { get; set; }

    }
}
