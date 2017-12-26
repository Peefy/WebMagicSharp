using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public class Json : PlainText
    {
        public Json(List<String> strings) : base(strings)
        {
            
        }

        public Json(String text) : base(text)
        {
            
        }

        /**
         * remove padding for JSONP
         * @param padding padding
         * @return json after padding removed
         */
        public Json removePadding(String padding)
        {
            String text = getFirstSourceText();
            XTokenQueue tokenQueue = new XTokenQueue(text);
            tokenQueue.consumeWhitespace();
            tokenQueue.consume(padding);
            tokenQueue.consumeWhitespace();
            String chompBalanced = tokenQueue.chompBalancedNotInQuotes('(', ')');
            return new Json(chompBalanced);
        }

        public <T> T toObject(Class<T> clazz)
        {
            if (getFirstSourceText() == null)
            {
                return null;
            }
            return JSON.parseObject(getFirstSourceText(), clazz);
        }

        public <T> List<T> toList(Class<T> clazz)
        {
            if (getFirstSourceText() == null)
            {
                return null;
            }
            return JSON.parseArray(getFirstSourceText(), clazz);
        }

        @Override
    public Selectable jsonPath(String jsonPath)
        {
            JsonPathSelector jsonPathSelector = new JsonPathSelector(jsonPath);
            return selectList(jsonPathSelector, GetSourceTexts());
        }
    }
}
