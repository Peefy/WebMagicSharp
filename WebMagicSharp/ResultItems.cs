using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    /// <summary>
    /// Result items.
    /// </summary>
    public class ResultItems
    {

        private Dictionary<string, object> _fields = new Dictionary<string, object>();

        private Request _request;

        private bool _skip;

        public T Get<T>(string key) where T : class
        {
            if(_fields.TryGetValue(key, out object o) == true)
            {
                return o as T;
            }
            return null;
        }

        public Dictionary<string, object> GetAll()
        {
            return _fields;
        }

        public ResultItems Put<T>(string key, T value)
        {
            _fields.Add(key, value);
            return this;
        }

        public Request GetRequest()
        {
            return _request;
        }

        public ResultItems SetRequest(Request request)
        {
            this._request = request;
            return this;
        }

        public bool IsSkip()
        {
            return _skip;
        }

        public ResultItems SetSkip(bool skip)
        {
            this._skip = skip;
            return this;
        }

        public override String ToString()
        {
            return "ResultItems{" +
                    "fields=" + _fields +
                    ", request=" + _request +
                    ", skip=" + _skip +
                    '}';
        }
    }
}
