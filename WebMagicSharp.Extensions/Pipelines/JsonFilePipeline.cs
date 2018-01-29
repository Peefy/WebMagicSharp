using System;
using System.Security;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

using Newtonsoft.Json;

using WebMagicSharp.Utils;


namespace WebMagicSharp.Pipelines
{
    public class JsonFilePipeline : FilePersistentBase, IPipeline
    {
        public JsonFilePipeline()
        {
            this.path = "/data/webmagic";
        }

        public JsonFilePipeline(String path)
        {
            this.path = path;
        }

        public virtual void Process(T resultItems, ITask task)
        {
            var totalPath = this.path + PathSeparator + task.GetGuid() + PathSeparator;
            try
            {
                var md5HexString = MD5.Create(resultItems.GetRequest().GetUrl());
                var filePath = totalPath + md5HexString + ".json";
                var jsonString = JsonConvert.SerializeObject(resultItems.GetAll());
                File.WriteAllText(filePath,jsonString);
            }
            catch (IOException e)
            {
                Debug.WriteLine("write file error:" + e);
            }
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
        // ~JsonFilePipeline() {
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
