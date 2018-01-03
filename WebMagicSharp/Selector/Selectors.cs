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
        public static RegexSelector Regex(string expr)
        {
            return new RegexSelector(expr);
        }

        public static RegexSelector Regex(string expr, int group)
        {
            return new RegexSelector(expr, group);
        }

        public static SmartContentSelector SmartContent()
        {
            return new SmartContentSelector();
        }

        public static XPathSelector XPath(string expr)
        {
            return new XPathSelector(expr);
        }

        public static CssSelector Css(string expr) 
        {
            return new CssSelector(expr);
        }

        public static CssSelector Css(string expr, string attrName) 
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
