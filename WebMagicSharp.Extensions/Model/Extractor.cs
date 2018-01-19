using System;

using WebMagicSharp;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class Extractor
    {
        public ISelector selector { get; set; }

        public Source source { get; set; }

        public bool IsNotNull => notNull;

        public bool IsMulti => multi;

        protected bool notNull;

        protected bool multi;

        public Extractor(ISelector selector, Source source, bool notNull, bool multi)
        {
            this.selector = selector;
            this.source = source;
            this.notNull = notNull;
            this.multi = multi;
        }

    }

    public enum Source
    {
        Html,
        Url,
        RawHtml,
        RawText
    }

}
