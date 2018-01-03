using System;
using System.Collections.Generic;

namespace WebMagicSharp.Model
{
    public class HttpRequestBody
    {
        public static class ContentType
        {
            public const string Json = "application/json";

            public const string Xml = "text/xml";

            public const string Form = "application/x-www-form-urlencoded";

            public const string Multipart = "multipart/form-data";
        }

        private byte[] body;

        private string contentType;

        private string encoding;

        public HttpRequestBody()
        {
        }

        public HttpRequestBody(byte[] body, string contentType, string encoding)
        {
            this.body = body;
            this.contentType = contentType;
            this.encoding = encoding;
        }

        public string GetContentType()
        {
            return contentType;
        }

        public string GetEncoding()
        {
            return encoding;
        }

        public void SetBody(byte[] body)
        {
            this.body = body;
        }

        public void SetContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public void SetEncoding(string encoding)
        {
            this.encoding = encoding;
        }

        public static HttpRequestBody Json(string json, string encoding)
        {
            try
            {
                var bytes = System.Text.Encoding.Default.GetBytes(json);
                return new HttpRequestBody(bytes, ContentType.Json, encoding);
            }
            catch (Exception e)
            {
                throw new Exception("illegal encoding " + encoding, e);
            }
        }

        public static HttpRequestBody Xml(string xml, string encoding)
        {
            try
            {
                var bytes = System.Text.Encoding.Default.GetBytes(xml);
                return new HttpRequestBody(bytes, ContentType.Xml, encoding);
            }
            catch (Exception e)
            {
                throw new Exception("illegal encoding " + encoding, e);
            }
        }

        public static HttpRequestBody Custom(byte[] body, string contentType, string encoding)
        {
            return new HttpRequestBody(body, contentType, encoding);
        }

        public static HttpRequestBody Form(Dictionary<string, object> param, string encoding)
        {
            var nameValuePairs = new List<BasicNameValuePair>();
            foreach(var entry in param)
            {
                nameValuePairs.Add(new BasicNameValuePair(entry.Key, entry.ToString()));
            }
            try
            {
                var bytes = System.Text.Encoding.Default.GetBytes("form");
                return new HttpRequestBody(bytes, ContentType.Form, encoding);
            }
            catch (Exception e)
            {
                throw new Exception("illegal encoding " + encoding, e);
            }
        }

        public byte[] GetBody()
        {
            return body;
        }

    }
}
