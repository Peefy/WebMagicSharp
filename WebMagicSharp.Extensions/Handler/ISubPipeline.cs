using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public interface ISubPipeline : IRequestMatcher
    {
        MatchOther processResult(ResultItems resultItems, ITask task);
    }
}
