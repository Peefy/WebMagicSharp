using System;

using WebMagicSharp;
using WebMagicSharp.Selector;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Model
{
    public class Extractor
    {
        public ISelector Selector { get; set; }

        public Source Source { get; set; }

        public bool IsNotNull => notNull;

        public bool IsMulti => multi;

        protected bool notNull;

        protected bool multi;

        public Extractor(ISelector selector, Source source, bool notNull, bool multi)
        {
            this.Selector = selector;
            this.Source = source;
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
