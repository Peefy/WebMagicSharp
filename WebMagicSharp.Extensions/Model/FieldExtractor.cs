using System;
using System.Reflection;

using WebMagicSharp;
using WebMagicSharp.Model.Formatter;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class FieldExtractor : Extractor
    {
        protected FieldInfo fieldInfo;

        public MethodInfo SetterMethod { get; set; }

        public IObjectFormatter ObjectFormatter { get; set; }

        public FieldExtractor(FieldInfo field, ISelector selector, Source source, bool notNull, bool multi) : 
            base(selector, source, notNull, multi)
        {
            fieldInfo = field;
        }

        public FieldInfo Field => fieldInfo;

    }

}
