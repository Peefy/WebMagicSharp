using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Or selector.
    /// </summary>
    public class OrSelector : ISelector
    {
        private List<ISelector> _selectors = new List<ISelector>();

        public OrSelector(ISelector[] selectors)
        {
            foreach (var selector in selectors)
            {
                _selectors.Add(selector);
            }
        }

        public OrSelector(List<ISelector> selectors)
        {
            this._selectors = selectors;
        }

        public string Select(string text)
        {
            foreach (var selector in _selectors)
            {
                var result = selector.Select(text);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public List<string> SelectList(string text)
        {
            var results = new List<string>();
            foreach(var selector in _selectors)
            {
                var strings = selector.SelectList(text);
                results.AddRange(strings);
            }
            return results;
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
