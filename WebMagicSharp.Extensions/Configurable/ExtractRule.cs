using System;

using WebMagicSharp.Selector;

namespace WebMagicSharp.Configurable
{
    public class ExtractRule
    {

        private String fieldName;

        private ExpressionType expressionType;

        private String expressionValue;

        private String[] expressionParams;

        private bool multi = false;

        private volatile ISelector selector;

        private bool notNull = false;

        public String GetFieldName()
        {
            return fieldName;
        }

        public void SetFieldName(String fieldName)
        {
            this.fieldName = fieldName;
        }

        public ExpressionType GetExpressionType()
        {
            return expressionType;
        }

        public void SetExpressionType(ExpressionType expressionType)
        {
            this.expressionType = expressionType;
        }

        public String GetExpressionValue()
        {
            return expressionValue;
        }

        public void SetExpressionValue(String expressionValue)
        {
            this.expressionValue = expressionValue;
        }

        public String[] GetExpressionParams()
        {
            return expressionParams;
        }

        public void SetExpressionParams(String[] expressionParams)
        {
            this.expressionParams = expressionParams;
        }

        public bool IsMulti()
        {
            return multi;
        }

        public void SetMulti(bool multi)
        {
            this.multi = multi;
        }

        public ISelector GetSelector()
        {
            if (selector == null)
            {
                lock(this) 
                {
                    if (selector == null)
                    {
                        selector = CompileSelector();
                    }
                }
            }
            return selector;
        }

        private ISelector CompileSelector()
        {
            switch (expressionType)
            {
                case ExpressionType.Css:
                    if (expressionParams.Length >= 1)
                    {
                        return Selectors.Css(expressionValue, expressionParams[0]);
                    }
                    else
                    {
                        return Selectors.Css(expressionValue);
                    }
                case ExpressionType.XPath:
                    return Selectors.XPath(expressionValue);
                case ExpressionType.Regex:
                    if (expressionParams.Length >= 1)
                    {
                        return Selectors.Regex(expressionValue, 
                                               int.Parse(expressionParams[0]));
                    }
                    else
                    {
                        return Selectors.Regex(expressionValue);
                    }
                case ExpressionType.JsonPath:
                    return new JsonPathSelector(expressionValue);
                default:
                    return Selectors.XPath(expressionValue);
            }
        }

        public void SetSelector(ISelector selector)
        {
            this.selector = selector;
        }

        public bool IsNotNull()
        {
            return notNull;
        }

        public void SetNotNull(bool notNull)
        {
            this.notNull = notNull;
        }
    }
}
