using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// And selector.
    /// </summary>
    public class AndSelector : ISelector
    {

        private List<ISelector> _selectors = new List<ISelector>();

        public AndSelector(ISelector[] selectors)
        {
            foreach(var selector in selectors)
            {
                this.Selectors.Add(selector);
            }
        }

        public AndSelector(List<ISelector> selectors)
        {
            this.Selectors = selectors;
        }

        public string Select(string text)
        {
            foreach(var selector in Selectors)
            {
                if (text == null)
                {
                    return null;
                }
                text = selector.Select(text);
            }
            return text;
        }

        public List<string> SelectList(string text)
        {
            var results = new List<String>();
            var first = true;
            foreach(var selector in Selectors)
            {
                if (first)
                {
                    results = selector.SelectList(text);
                    first = false;
                }
                else
                {
                    var resultsTemp = new List<String>();
                    foreach(var result in results)
                    {
                        resultsTemp.AddRange(selector.SelectList(result));
                    }
                    results = resultsTemp;
                    if (results == null || results.Count == 0)
                    {
                        return results;
                    }
                }
            }
            return results;
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        public List<ISelector> Selectors { get => _selectors; set => _selectors = value; }

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
        // ~AndSelector() {
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
