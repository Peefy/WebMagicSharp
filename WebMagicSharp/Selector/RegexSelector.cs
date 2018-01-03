using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Regex selector.
    /// </summary>
    public class RegexSelector : ISelector
    {

        private string regexStr;

        private Regex regex;

        private int group = 1;

        public RegexSelector(string regexStr, int group)
        {
            this.CompileRegex(regexStr);
            this.group = group;
        }

        private void CompileRegex(string regexStr)
        {
            if (string.IsNullOrEmpty(regexStr))
            {
                throw new Exception("regex must not be empty");
            }
            try
            {
                this.regex = new Regex(regexStr, RegexOptions.IgnoreCase);
                this.regexStr = regexStr;
            }
            catch
            {
                throw new Exception("invalid regex " + regexStr);
            }
        }

        /**
         * Create a RegexSelector. When there is no capture group, the value is set to 0 else set to 1.
         * @param regexStr
         */
        public RegexSelector(string regexStr)
        {
            this.CompileRegex(regexStr);
            if (regex.Matches("").Count == 0)
            {
                this.group = 0;
            }
            else
            {
                this.group = 1;
            }
        }

        public string Select(string text)
        {
            return SelectGroup(text).Get(group);
        }

        public List<string> SelectList(String text)
        {
            var strings = new List<string>();
            List<RegexResult> results = SelectGroupList(text);
            foreach (var result in results)
            {
                strings.Add(result.Get(group));
            }
            return strings;
        }

        public RegexResult SelectGroup(string text)
        {
            var matcher = regex.Matches(text);
            List<RegexResult> resultList = new List<RegexResult>();
            if(matcher.Count > 0)
            {
                string[] groups = new string[matcher.Count + 1];
                for (int i = 0; i < groups.Length; i++)
                {
                    groups[i] = matcher[i].ToString();
                }
                return new RegexResult(groups);
            }
            return RegexResult.EmptyResult;
        }

        public List<RegexResult> SelectGroupList(string text)
        {
            var matcher = regex.Matches(text);
            List<RegexResult> resultList = new List<RegexResult>();
            string[] groups = new String[matcher.Count + 1];
            for (int i = 0; i < groups.Length; i++)
            {
                groups[i] = matcher[i].ToString();
            }
            resultList.Add(new RegexResult(groups));
            return resultList;
        }

        public override string ToString()
        {
            return regexStr;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RegexSelector() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
