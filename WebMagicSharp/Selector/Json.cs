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
        public Json(List<string> strings) : base(strings)
        {
            
        }

        public Json(string text) : base(text)
        {
            
        }

        /**
         * remove padding for JSONP
         * @param padding padding
         * @return json after padding removed
         */
        public Json RemovePadding(string padding)
        {
            var text = FirstSourceText;
            text.Replace(" ","");
            return new Json(text);
        }

        public T ToObject<T>() where T : class
        {
            var str = FirstSourceText;
            if (str == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(str);
        }

        public override ISelectable JsonPath(string jsonPath)
        {
            var jsonPathSelector = new JsonPathSelector(jsonPath);
            return SelectList(jsonPathSelector, SourceTexts);
        }
    }
}
