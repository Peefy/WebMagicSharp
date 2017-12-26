using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public class PlainText : AbstractSelectable
    {

        protected List<String> sourceTexts;

        public PlainText(List<String> sourceTexts)
        {
            this.sourceTexts = sourceTexts;
        }

        public PlainText(String text)
        {
            this.sourceTexts = new List<String>();
            sourceTexts.Add(text);
        }

        public static PlainText create(String text)
        {
            return new PlainText(text);
        }

        public List<string> All()
        {
            throw new NotImplementedException();
        }

        public ISelectable Css(string selector)
        {
            throw new NotImplementedException();
        }

        public ISelectable Css(string selector, string attrName)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Get()
        {
            throw new NotImplementedException();
        }

        public ISelectable JsonPath(string jsonPath)
        {
            throw new NotImplementedException();
        }

        public ISelectable Links()
        {
            throw new NotImplementedException();
        }

        public bool Match()
        {
            throw new NotImplementedException();
        }

        public List<ISelectable> Nodes()
        {
            throw new NotImplementedException();
        }

        public ISelectable Regex(string regex)
        {
            throw new NotImplementedException();
        }

        public ISelectable Regex(string regex, int group)
        {
            throw new NotImplementedException();
        }

        public ISelectable Replace(string regex, string replacement)
        {
            throw new NotImplementedException();
        }

        public ISelectable Select(ISelector selector)
        {
            throw new NotImplementedException();
        }

        public ISelectable SelectList(ISelector selector)
        {
            throw new NotImplementedException();
        }

        public ISelectable SmartContent()
        {
            throw new NotImplementedException();
        }

        public ISelectable Xpath(string xpath)
        {
            throw new NotImplementedException();
        }

        protected override List<string> GetSourceTexts()
        {
            throw new NotImplementedException();
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
        // ~ReplaceSelector() {
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
