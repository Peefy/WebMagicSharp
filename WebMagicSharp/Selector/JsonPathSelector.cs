using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Json path selector.
    /// </summary>
    public class JsonPathSelector : ISelector
    {
        string _jsonPathString;

        public JsonPathSelector(string jsonPathString)
        {
            _jsonPathString = jsonPathString;
        }

        public string Select(string text)
        {
            var jobjs = JObject.Parse(text).SelectTokens(_jsonPathString);
            if (jobjs == null)
            {
                return null;
            }
            return ToJsonString(jobjs.FirstOrDefault());
        }

        public List<string> SelectList(string text)
        {
            var list = new List<String>();
            var jobjs = JObject.Parse(text).SelectTokens(_jsonPathString);
            if (jobjs == null)
            {
                return null;
            }
            foreach(var jobj in jobjs)
            {
                list.Add(ToJsonString(jobj));
            }
            return list;
        }

        public string ToJsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~JsonPathSelector() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
