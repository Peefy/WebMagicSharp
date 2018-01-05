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

        public String getFieldName()
        {
            return fieldName;
        }

        public void setFieldName(String fieldName)
        {
            this.fieldName = fieldName;
        }

        public ExpressionType getExpressionType()
        {
            return expressionType;
        }

        public void setExpressionType(ExpressionType expressionType)
        {
            this.expressionType = expressionType;
        }

        public String getExpressionValue()
        {
            return expressionValue;
        }

        public void setExpressionValue(String expressionValue)
        {
            this.expressionValue = expressionValue;
        }

        public String[] getExpressionParams()
        {
            return expressionParams;
        }

        public void setExpressionParams(String[] expressionParams)
        {
            this.expressionParams = expressionParams;
        }

        public bool isMulti()
        {
            return multi;
        }

        public void setMulti(bool multi)
        {
            this.multi = multi;
        }

        public ISelector getSelector()
        {
            if (selector == null)
            {
                lock(this) 
                {
                    if (selector == null)
                    {
                        selector = compileSelector();
                    }
                }
            }
            return selector;
        }

        private ISelector compileSelector()
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

        public void setSelector(ISelector selector)
        {
            this.selector = selector;
        }

        public bool isNotNull()
        {
            return notNull;
        }

        public void setNotNull(bool notNull)
        {
            this.notNull = notNull;
        }
    }
}
