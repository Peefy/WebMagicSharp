using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public interface ISubPageProcessor : IRequestMatcher
    {
        MatchOther ProcessPage(Page page);
    }
}
