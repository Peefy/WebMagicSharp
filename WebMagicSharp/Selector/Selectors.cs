using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Selectors.
    /// </summary>
    public static class Selectors
    {
        public static RegexSelector Regex(String expr)
        {
            return new RegexSelector(expr);
        }

        public static RegexSelector Regex(String expr, int group)
        {
            return new RegexSelector(expr, group);
        }

        public static SmartContentSelector SmartContent()
        {
            return new SmartContentSelector();
        }

        public static XPathSelector XPath(String expr)
        {
            return new XPathSelector(expr);
        }

        public static CssSelector Css(String expr) 
        {
            return new CssSelector(expr);
        }

        public static CssSelector Css(String expr, String attrName) 
        {
            return new CssSelector(expr, attrName);
        }

        public static AndSelector And(ISelector[] selectors)
        {
            return new AndSelector(selectors);
        }

        public static OrSelector Or(ISelector[] selectors)
        {
            return new OrSelector(selectors);
        }

    }
}
