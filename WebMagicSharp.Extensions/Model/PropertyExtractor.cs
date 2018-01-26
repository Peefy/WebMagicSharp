using System.Reflection;
using WebMagicSharp.Model.Formatter;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Model
{
    public class PropertyExtractor : Extractor
    {
        protected PropertyInfo propertyInfo;

        public MethodInfo SetterMethod { get; set; }

        public IObjectFormatter<object> ObjectFormatter { get; set; }

        public PropertyExtractor(PropertyInfo property, ISelector selector, Source source, bool notNull, bool multi) : 
            base(selector, source, notNull, multi)
        {
            propertyInfo = property;
        }

        public PropertyInfo Property => propertyInfo;

    }

}
