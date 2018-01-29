using System;
using System.Diagnostics;
using System.Security;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Utils;


namespace WebMagicSharp.Pipelines
{
    public class FilePipeline : IPipeline
    {

        public string Path { get; set; }

        public void Process(ResultItems resultItems, ITask task)
        {
            var seperator = FilePersistentBase.PathSeparator;
            var path = this.Path + seperator + 
                task.GetGuid() + seperator;
            try
            {
                var fileName = path + MD5.Create(resultItems.GetRequest().GetUrl()) + ".html";
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("url:\t" + resultItems.GetRequest().GetUrl());
                foreach (var entry in resultItems.GetAll())
                {
                    stringBuilder.Append($"{entry.Key}:\t{entry.Value}");
                }
                File.WriteAllText(fileName, stringBuilder.ToString());
            }
            catch (IOException e) {
                Debug.WriteLine($"write file error:{e.Message}");
            }
        }

        private FilePipeline()
        {

        }

        public FilePipeline(string path)
        {
            Path = path;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~FilePipeline() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
