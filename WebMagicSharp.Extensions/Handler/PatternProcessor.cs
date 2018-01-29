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

        public abstract MatchOther ProcessPage(Page page);

        public abstract MatchOther ProcessResult(T resultItems, ITask task);
    }
}
