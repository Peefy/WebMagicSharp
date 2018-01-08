using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public abstract class PatternProcessor : PatternRequestMatcher, ISubPipeline, ISubPageProcessor
    {
        public PatternProcessor(string pattern) : base(pattern)
        {

        }

        public abstract MatchOther processPage(Page page);

        public abstract MatchOther processResult(ResultItems resultItems, ITask task);
    }
}
