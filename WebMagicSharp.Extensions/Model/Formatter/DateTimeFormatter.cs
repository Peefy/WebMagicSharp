using System;
using System.Globalization;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// 
    /// </summary>
    public class DateTimeFormatter : IObjectFormatter<DateTime>
    {

        public string DefaultPattern { get; set; } = "yyyy-MM-dd HH:mm";

        public Type Type => typeof(DateTime);

        public DateTime Format(string raw)
        {
            return DateTime.ParseExact(raw, DefaultPattern, CultureInfo.InvariantCulture);
        }

        public void InitParam(string[] extra)
        {
            if (string.IsNullOrEmpty(extra[0]) == false)
            {
                DefaultPattern = extra[0];
            }
        }
    }
}
