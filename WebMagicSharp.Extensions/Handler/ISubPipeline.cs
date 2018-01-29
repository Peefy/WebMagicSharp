using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public interface ISubPipeline : IRequestMatcher
    {
        MatchOther ProcessResult(ResultItems resultItems, ITask task);
    }
}
