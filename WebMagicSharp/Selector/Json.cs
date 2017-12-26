using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Json.
    /// </summary>
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
            String text = GetFirstSourceText();
            text.Replace(" ","");
            return new Json(text);
        }

        public T toObject<T>() where T : class
        {
            var str = GetFirstSourceText();
            if (str == null)
            {
                return null;
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        public override ISelectable JsonPath(String jsonPath)
        {
            var jsonPathSelector = new JsonPathSelector(jsonPath);
            return SelectList(jsonPathSelector, GetSourceTexts());
        }
    }
}
