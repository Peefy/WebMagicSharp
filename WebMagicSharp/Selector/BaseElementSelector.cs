using System;
using System.Collections.Generic;
using System.Text;

using HtmlAgilityPack;

using WebMagicSharp;

namespace WebMagicSharp.Selector
{
    public abstract class BaseElementSelector : ISelector, IElementSelector
    {

        HtmlDocument document;

        public BaseElementSelector()
        {
            document = new HtmlDocument();
        }

        public string Select(string text)
        {
            if (text != null)
            {
                document.LoadHtml(text);
                return select(document);
            }
            return null;
        }

        public List<string> SelectList(string text)
        {
            if (text != null)
            {
                document.LoadHtml(text);
                return selectList(document);
            }
            else
            {
                return new List<String>();
            }
        }

        public HtmlAgilityPack.HtmlNode selectElement(String text)
        {
            if (text != null)
            {
                document.LoadHtml(text);
                return selectElement(document);
            }
            return null;
        }

        public List<HtmlAgilityPack.HtmlNode> selectElements(String text)
        {
            if (text != null)
            {
                document.LoadHtml(text);
                return selectElements(document);
            }
            else
            {
                return new List<HtmlAgilityPack.HtmlNode>();
            }
        }

        public abstract string select(HtmlDocument element);

        public abstract List<string> selectList(HtmlDocument element);


        public abstract HtmlAgilityPack.HtmlNode selectElement(HtmlDocument element);

        public abstract List<HtmlAgilityPack.HtmlNode> selectElements(HtmlDocument element);

        public abstract bool hasAttribute();

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
