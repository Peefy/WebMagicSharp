using System;
using System.Reflection;

using WebMagicSharp;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class FieldExtractor : Extractor
    {
        protected FieldInfo fieldInfo;

        protected PropertyInfo propertyInfo;

        protected MethodInfo methodInfo;

        public FieldExtractor(FieldInfo field, ISelector selector, Source source, bool notNull, bool multi) : 
            base(selector, source, notNull, multi)
        {
            fieldInfo = field;
        }
    }

}
