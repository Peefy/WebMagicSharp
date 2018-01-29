using System.Linq;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Model.Attributes;
using WebMagicSharp.Selector;

namespace WebMagicSharp.Utils
{

    public static class ExtractorUtils
    {
        public static ISelector GetSelector(ExtractByAttribute extractBy)
        {
            var value = extractBy.Value;
            ISelector selector = null;
            switch(extractBy.Type)
            {
                case ExtractType.Css:
                    selector = new CssSelector(value);
                    break;
                case ExtractType.Regex:
                    selector = new RegexSelector(value);
                    break;
                case ExtractType.XPath:
                    selector = new XPathSelector(value);
                    break;
                case ExtractType.JsonPath:
                    selector = new JsonPathSelector(value);
                    break;
            }
            return selector;
        }

        public static List<ISelector> GetSelectors(IList<ExtractByAttribute> extractBys)
        {
            var selectors = new List<ISelector>();
            if (extractBys == null)
                return selectors;
            foreach(var extractBy in extractBys)
            {
                selectors.Add(GetSelector(extractBy));
            }
            return selectors;
        }
    }
}
