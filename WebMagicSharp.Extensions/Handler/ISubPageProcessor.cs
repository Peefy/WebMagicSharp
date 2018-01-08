using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Handler
{
    public interface ISubPageProcessor : IRequestMatcher
    {
        MatchOther processPage(Page page);
    }
}
