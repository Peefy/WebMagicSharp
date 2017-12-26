using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Utils
{
    public static class RegexUtil
    {
        /// <summary>
        /// 获取所有的A链接
        /// </summary>
        public const string Alist = "<a[\\s\\S]+?href[=\"\']([\\s\\S]+?)[\"\'\\s+][\\s\\S]+?>([\\s\\S]+?)</a>";
        /// <summary>
        /// 获取所有的Img标签
        /// </summary>
        public const string ImgList = "<img[\\s\\S]*?src=[\"\']([\\s\\S]*?)[\"\'][\\s\\S]*?>([\\s\\S]*?)>";
        /// <summary>
        /// 所有的Nscript
        /// </summary>
        public const string Nscript = "<nscript[\\s\\S]*?>[\\s\\S]*?</nscript>";
        /// <summary>
        /// 所有的Style
        /// </summary>
        public const string Style = "<style[\\s\\S]*?>[\\s\\S]*?</style>";
        /// <summary>
        /// 所有的Script
        /// </summary>
        public const string Script = "<script[\\s\\S]*?>[\\s\\S]*?</script>";
        /// <summary>
        /// 所有的Html
        /// </summary>
        public const string Html = "<[\\s\\S]*?>";
        /// <summary>
        /// 换行符号
        /// </summary>
        public static string NewLine = Environment.NewLine;
        /// <summary>
        ///获取网页编码
        /// </summary>
        public const string Enconding = "<meta[^<]*charset=([^<]*)[\"']";
        /// <summary>
        /// 所有Html
        /// </summary>
        public const string AllHtml = "([\\s\\S]*?)";
        /// <summary>
        /// title
        /// </summary>
        public const string HtmlTitle = "<title>([\\s\\S]*?)</title>";
    }
}
