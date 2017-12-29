using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Abstract selectable.
    /// </summary>
    public abstract class AbstractSelectable : ISelectable
    {
        protected abstract List<String> SourceTexts { get; }

        public virtual ISelectable Css(String selector) => Jquery(selector);

        public virtual ISelectable Css(String selector, String attrName) => Jquery(selector, attrName);

        protected virtual ISelectable Select(ISelector selector, List<String> strings)
        {
            foreach (var str in strings)
            {
                if (selector.Select(text: str) != null)
                {
                    new List<string>().Add(selector.Select(str));
                }
            }
            return new PlainText(new List<string>());
        }

        protected virtual ISelectable SelectList(ISelector selector, List<String> strings)
        {
            foreach (var str in strings)
            {
                new List<string>().AddRange(selector.SelectList(str));
            }
            return new PlainText(new List<string>());
        }

        public virtual List<String> All() => SourceTexts;

        public virtual String Get => All()?.Count > 0 ? All()[0] : null;

        public virtual ISelectable Select(ISelector selector) => 
            Select(selector, SourceTexts);

        public virtual ISelectable SelectList(ISelector selector) => 
            SelectList(selector, SourceTexts);

        public virtual ISelectable Regex(String regex) => 
            SelectList(Selectors.Regex(regex), SourceTexts);

        public virtual ISelectable Regex(String regex, int group)
        {
            RegexSelector regexSelector = Selectors.Regex(regex, group);
            return SelectList(regexSelector, SourceTexts);
        }

        public virtual ISelectable Replace(String regex, String replacement)
        {
            ReplaceSelector replaceSelector = new ReplaceSelector(regex, replacement);
            return Select(replaceSelector, SourceTexts);
        }

        public virtual String FirstSourceText
        {
            get
            {
                if (SourceTexts != null && SourceTexts.Count > 0)
                {
                    return SourceTexts[0];
                }
                return null;
            }
        }

        public override String ToString()
        {
            return Get;
        }

        public bool Match() => SourceTexts != null && SourceTexts.Count > 0;

        public abstract ISelectable Xpath(string xpath);

        public abstract ISelectable SmartContent();

        public abstract ISelectable Links();

        public abstract List<ISelectable> Nodes();

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

        public abstract ISelectable Jquery(string selector);
        public abstract ISelectable Jquery(string selector, string attrName);
        #endregion
    }
}
