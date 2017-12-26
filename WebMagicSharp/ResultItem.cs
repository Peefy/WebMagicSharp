using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    public class ResultItems
    {

        private Dictionary<string, object> fields = new Dictionary<string, object>();

        private Request request;

        private bool skip;

        public T Get<T>(String key) where T : class
        {
            if(fields.TryGetValue(key,out object o) == true)
            {
                return o as T;
            }
            return null;
        }

        public Dictionary<String, Object> GetAll()
        {
            return fields;
        }

        public ResultItems Put<T>(string key, T value)
        {
            fields.Add(key, value);
            return this;
        }

        public Request GetRequest()
        {
            return request;
        }

        public ResultItems SetRequest(Request request)
        {
            this.request = request;
            return this;
        }

        /**
         * Whether to skip the result.<br>
         * Result which is skipped will not be processed by Pipeline.
         *
         * @return whether to skip the result
         */
        public bool IsSkip()
        {
            return skip;
        }


        /**
         * Set whether to skip the result.<br>
         * Result which is skipped will not be processed by Pipeline.
         *
         * @param skip whether to skip the result
         * @return this
         */
        public ResultItems SetSkip(bool skip)
        {
            this.skip = skip;
            return this;
        }

        public override String ToString()
        {
            return "ResultItems{" +
                    "fields=" + fields +
                    ", request=" + request +
                    ", skip=" + skip +
                    '}';
        }
    }
}
