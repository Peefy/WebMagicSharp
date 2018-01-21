using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DuGu.Standard.Http
{
    /// <summary>
    /// WinInet的方式请求数据
    /// </summary>
    public class Wininet
    {


        #region 字段/属性
        /// <summary>
        /// 默认UserAgent
        /// </summary>
        public string UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; 125LA; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";

        private int _WininetTimeOut = 0;
        /// <summary>
        /// Wininet超时时间 默认0 不设置超时,由于是自行实现(微软没修复超时的bug) 所以如果设置后,每次请求都会暂停.
        /// </summary>
        public int WininetTimeOut
        {
            get { return _WininetTimeOut; }
            set { _WininetTimeOut = value; }
        }

        #endregion


        /// <summary>
        /// 自动解析编码
        /// </summary>
        /// <param name="ms">结果流</param>
        /// <returns>异常时返回null</returns>
        private string EncodingPack(MemoryStream ms)
        {
            Match meta = Regex.Match(Encoding.Default.GetString(ms.ToArray()), "<meta([^<]*)charset=([^<]*)[\"']", RegexOptions.IgnoreCase);
            string c = (meta.Groups.Count > 1) ? meta.Groups[2].Value.ToUpper().Trim() : string.Empty;
            if (c.IndexOf("\"") > 0)
            {
                c = c.Split('\"')[0];
            }
            if (c.Length > 2)
            {
                if (c.IndexOf("UTF-8") != -1)
                {
                    return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
                }
            }
            return Encoding.GetEncoding("GBK").GetString(ms.ToArray());
        }
        /// <summary>
        /// 将内存流转换为字符串
        /// </summary>
        /// <param name="mstream">需要转换的流</param>
        /// <returns></returns>
        public string GetDataPro(MemoryStream mstream)
        {
            using (MemoryStream ms = mstream)
            {
                if (ms != null)
                {
                    //无视编码
                    return EncodingPack(ms);
                }
                else
                {
                    return null;
                }
            }
        }

        #region Cookie操作方法
        /// <summary>
        /// 遍历CookieContainer 转换为Cookie集合对象
        /// </summary>
        /// <param name="cc"></param>
        /// <returns>Cookie集合对象</returns>
        public List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;

        }
        /// <summary>
        /// 将String转CookieContainer
        /// </summary>
        /// <param name="Domain">Cookie对应的Domain</param>
        /// <param name="cookie">具体值</param>
        /// <returns>转换后的Container对象</returns>
        public CookieContainer StringToCookie(string Domain, string cookie)
        {
            string[] arrCookie = cookie.Split(';');
            CookieContainer cookie_container = new CookieContainer();    //加载Cookie
            foreach (string sCookie in arrCookie)
            {
                if (!string.IsNullOrEmpty(sCookie))
                {
                    Cookie ck = new Cookie
                    {
                        Name = sCookie.Split('=')[0].Trim(),
                        Value = sCookie.Split('=')[1].Trim(),
                        Domain = Domain
                    };
                    try
                    {
                        cookie_container.Add(ck);
                    }
                    catch
                    {
                        continue;
                    }

                }
            }
            return cookie_container;
        }
        /// <summary>
        /// 将CookieContainer转换为string类型
        /// </summary>
        /// <param name="cc">需要转换的Container对象</param>
        /// <returns>字符串结果</returns>
        public string CookieToString(CookieContainer cc)
        {
            System.Collections.Generic.List<Cookie> lstCookies = new System.Collections.Generic.List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            StringBuilder sb = new StringBuilder();
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies)
                    {
                        sb.Append(c.Name).Append("=").Append(c.Value).Append(";");
                    }
            }
            return sb.ToString();
        }
        #endregion
    }

}
