using System;

namespace DuGu.Standard.Http
{
    /// <summary>
    /// 正则表达式静态类
    /// </summary>
    public class RegexString
    {
        /// <summary>
        /// 获取所有的A链接
        /// </summary>
        public static readonly string Alist = "<a[\\s\\S]+?href[=\"\']([\\s\\S]+?)[\"\'\\s+][\\s\\S]+?>([\\s\\S]+?)</a>";
        /// <summary>
        /// 获取所有的Img标签
        /// </summary>
        public static readonly string ImgList = "<img[\\s\\S]*?src=[\"\']([\\s\\S]*?)[\"\'][\\s\\S]*?>([\\s\\S]*?)>";
        /// <summary>
        /// 所有的Nscript
        /// </summary>
        public static readonly string Nscript = "<nscript[\\s\\S]*?>[\\s\\S]*?</nscript>";
        /// <summary>
        /// 所有的Style
        /// </summary>
        public static readonly string Style = "<style[\\s\\S]*?>[\\s\\S]*?</style>";
        /// <summary>
        /// 所有的Script
        /// </summary>
        public static readonly string Script = "<script[\\s\\S]*?>[\\s\\S]*?</script>";
        /// <summary>
        /// 所有的Html
        /// </summary>
        public static readonly string Html = "<[\\s\\S]*?>";
        /// <summary>
        /// 换行符号
        /// </summary>
        public static readonly string NewLine = Environment.NewLine;
        /// <summary>
        ///获取网页编码
        /// </summary>
        public static readonly string Enconding = "<meta[^<]*charset=([^<]*)[\"']";
        /// <summary>
        /// 所有Html
        /// </summary>
        public static readonly string AllHtml = "([\\s\\S]*?)";
        /// <summary>
        /// title
        /// </summary>
        public static readonly string HtmlTitle = "<title>([\\s\\S]*?)</title>";
    }
}
