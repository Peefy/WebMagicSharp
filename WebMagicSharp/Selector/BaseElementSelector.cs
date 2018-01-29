using System;
using System.Collections.Generic;
using System.Text;

using DuGu.Standard.Html;

using WebMagicSharp;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Base element selector.
    /// </summary>
    public abstract class BaseElementSelector : ISelector, IElementSelector
    {

        HtmlDocument _document;

        public BaseElementSelector()
        {
            _document = new HtmlDocument();
        }

        public string Select(string text)
        {
            if (text != null)
            {
                _document.LoadHtml(text);
                return Select(_document);
            }
            return null;
        }

        public List<string> SelectList(string text)
        {
            if (text != null)
            {
                _document.LoadHtml(text);
                return SelectList(_document);
            }
            else
            {
                return new List<string>();
            }
        }

        public virtual DuGu.Standard.Html.HtmlNode SelectElement(string text)
        {
            if (text != null)
            {
                _document.LoadHtml(text);
                return SelectElement(_document);
            }
            return null;
        }

        public virtual List<DuGu.Standard.Html.HtmlNode> SelectElements(string text)
        {
            if (text != null)
            {
                _document.LoadHtml(text);
                return SelectElements(_document);
            }
            else
            {
                return new List<DuGu.Standard.Html.HtmlNode>();
            }
        }

        public abstract string Select(HtmlDocument element);

        public abstract List<string> SelectList(HtmlDocument element);


        public abstract DuGu.Standard.Html.HtmlNode SelectElement(HtmlDocument element);

        public abstract List<DuGu.Standard.Html.HtmlNode> SelectElements(HtmlDocument element);

        public abstract bool HasAttribute();

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
        // ~BaseElementSelector() {
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
