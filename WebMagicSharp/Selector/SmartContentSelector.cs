using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public class SmartContentSelector : ISelector
    {
        public string Select(string text)
        {
            var html = text;
            html = Regex.Replace(html, "(?is)<!DOCTYPE.*?>", "");
            html = Regex.Replace(html, "(?is)<!DOCTYPE.*?>", "");
            html = Regex.Replace(html, "(?is)<!--.*?-->", "");              // remove html comment
            html = Regex.Replace(html, "(?is)<script.*?>.*?</script>", ""); // remove javascript
            html = Regex.Replace(html, "(?is)<style.*?>.*?</style>", "");   // remove css
            html = Regex.Replace(html, "&.{2,5};|&#.{2,5};", " ");          // remove special char
            html = Regex.Replace(html, "(?is)<.*?>", "");
            List<String> lines;
            int blocksWidth = 3;
            int threshold = 86;
            int start;
            int end;
            StringBuilder strs = new StringBuilder();
            var indexDistribution = new List<int>();

            lines = html.Split('\n').ToList();

            for (int i = 0; i < lines.Count - blocksWidth; i++)
            {
                int wordsNum = 0;
                for (int j = i; j < i + blocksWidth; j++)
                {                 
                    lines[j] = Regex.Replace(lines[j], "\\s+", "");
                    wordsNum += lines[j].Length;
                }
                indexDistribution.Add(wordsNum);
            }

            start = -1; end = -1;
            bool boolstart = false, boolend = false;
            strs.Length = 0;

            for (int i = 0; i < indexDistribution.Count - 1; i++)
            {
                if (indexDistribution[i] > threshold && !boolstart)
                {
                    if (indexDistribution[i + 1] != 0
                            || indexDistribution[i + 2] != 0
                            || indexDistribution[i + 3] != 0)
                    {
                        boolstart = true;
                        start = i;
                        continue;
                    }
                }
                if (boolstart)
                {
                    if (indexDistribution[i] == 0
                            || indexDistribution[i + 1] == 0)
                    {
                        end = i;
                        boolend = true;
                    }
                }
                StringBuilder tmp = new StringBuilder();
                if (boolend)
                {
                    //System.out.println(start+1 + "\t\t" + end+1);
                    for (int ii = start; ii <= end; ii++)
                    {
                        if (lines[ii].Length < 5) continue;
                        tmp.Append(lines[ii] + "\n");
                    }
                    String str = tmp.ToString();
                    //System.out.println(str);
                    if (str.Contains("Copyright")) continue;
                    strs.Append(str);
                    boolstart = boolend = false;
                }
            }
            return strs.ToString();
        }

        [Obsolete("Unrealized")]
        public List<string> SelectList(string text)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~SmartContentSelector() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }


        #endregion
    }
}
