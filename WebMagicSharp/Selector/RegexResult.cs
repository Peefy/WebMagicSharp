using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Selector
{
    /// <summary>
    /// Regex result.
    /// </summary>
    public class RegexResult
    {

        private string[] groups;

        private static RegexResult _emptyResult;
        public static RegexResult EmptyResult =>
            _emptyResult ?? (_emptyResult = new RegexResult());

        public RegexResult()
        {

        }

        public RegexResult(string[] groups)
        {
            this.groups = groups;
        }

        public string Get(int groupId)
        {
            if (groups == null)
            {
                return null;
            }
            return groups[groupId];
        }

    }
}
