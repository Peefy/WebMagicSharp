using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    public class RegexSelector : ISelector
    {

        private String regexStr;

        private Regex regex;

        private int group = 1;

        public RegexSelector(String regexStr, int group)
        {
            this.CompileRegex(regexStr);
            this.group = group;
        }

        private void CompileRegex(String regexStr)
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
        public RegexSelector(String regexStr)
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

        public String Select(String text)
        {
            return SelectGroup(text).Get(group);
        }

        public List<String> SelectList(String text)
        {
            List<String> strings = new List<String>();
            List<RegexResult> results = SelectGroupList(text);
            foreach (var result in results)
            {
                strings.Add(result.Get(group));
            }
            return strings;
        }

        public RegexResult SelectGroup(String text)
        {
            var matcher = regex.Matches(text);
            List<RegexResult> resultList = new List<RegexResult>();
            if(matcher.Count > 0)
            {
                String[] groups = new String[matcher.Count + 1];
                for (int i = 0; i < groups.Length; i++)
                {
                    groups[i] = matcher[i].ToString();
                }
                return new RegexResult(groups);
            }
            return RegexResult.EmptyResult;
        }

        public List<RegexResult> SelectGroupList(String text)
        {
            var matcher = regex.Matches(text);
            List<RegexResult> resultList = new List<RegexResult>();
            String[] groups = new String[matcher.Count + 1];
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
    }
}
