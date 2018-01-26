using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public abstract class PatternRequestMatcher : IRequestMatcher
    {

        protected String pattern;

        private Regex patternCompiled;

        public PatternRequestMatcher(String pattern)
        {
            this.pattern = pattern;
            this.patternCompiled = new Regex(pattern);
        }

        public bool Match(Request request)
        {
            return patternCompiled.IsMatch(request.GetUrl());
        }
    }
}
