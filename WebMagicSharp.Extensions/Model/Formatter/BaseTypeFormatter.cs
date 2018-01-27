using System;

namespace WebMagicSharp.Model.Formatter
{
    /// <summary>
    /// abstarct base type formatter class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseTypeFormatter<T> : IObjectFormatter 
    {
        /// <summary>
        /// 
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="raw"></param>
        /// <returns></returns>
        public T Format(string raw)
        {
            if (raw == null)
            {
                return default(T);
            }
            raw = raw.Trim();
            return FormatTrimmed(raw);
        }

        protected abstract T FormatTrimmed(String raw);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="extra"></param>
        public void InitParam(string[] extra)
        {
            
        }

        [Obsolete]
        string IObjectFormatter.Format(string raw)
        {
            return null;
        }
    }

}
