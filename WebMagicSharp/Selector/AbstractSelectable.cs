using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public abstract class AbstractSelectable : ISelectable
    {
        protected abstract List<String> GetSourceTexts();

        public virtual ISelectable Css(String selector)
        {
            return this;
        }

        public virtual ISelectable Css(String selector, String attrName)
        {
            return this;
        }

        protected virtual ISelectable Select(ISelector selector, List<String> strings)
        {
            List<String> results = new List<String>();
            foreach(var str in strings)
            {
                var result = selector.Select(str);
                if (result != null)
                {
                    results.Add(result);
                }
            }
            return new PlainText(results);
        }

        protected virtual ISelectable SelectList(ISelector selector, List<String> strings)
        {
            List<String> results = new List<String>();
            foreach(var str in strings)
            {
                List<String> result = selector.SelectList(str);
                results.AddRange(result);
            }
            return new PlainText(results);
        }

        public virtual List<String> All()
        {
            return GetSourceTexts();
        }

        public virtual String Get()
        {
            var all = All();
            return all?.Count > 0 ? all[0] : null;
        }

        public virtual ISelectable Select(ISelector selector)
        {
            return Select(selector, GetSourceTexts());
        }

        public virtual ISelectable SelectList(ISelector selector)
        {
            return SelectList(selector, GetSourceTexts());
        }

        public virtual ISelectable Regex(String regex)
        {
            RegexSelector regexSelector = Selectors.Regex(regex);
            return SelectList(regexSelector, GetSourceTexts());
        }

        public virtual ISelectable Regex(String regex, int group)
        {
            RegexSelector regexSelector = Selectors.Regex(regex, group);
            return SelectList(regexSelector, GetSourceTexts());
        }

        public virtual ISelectable Replace(String regex, String replacement)
        {
            ReplaceSelector replaceSelector = new ReplaceSelector(regex, replacement);
            return Select(replaceSelector, GetSourceTexts());
        }

        public virtual String GetFirstSourceText()
        {
            var list = GetSourceTexts();
            if(list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public override String ToString()
        {
            return Get();
        }

        public bool Match()
        {
            return GetSourceTexts() != null && GetSourceTexts().Count > 0;
        }

        public abstract ISelectable Xpath(string xpath);

        public abstract ISelectable SmartContent();

        public abstract ISelectable Links();

        public abstract List<ISelectable> Nodes();

        [Obsolete("Unrealized")]
        public abstract ISelectable JsonPath(String jsonPath);

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
        // ~AbstractSelectable() {
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
