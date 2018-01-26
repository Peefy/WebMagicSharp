using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.Handler
{
    public interface IRequestMatcher
    {
        bool Match(Request request);

    }

    public enum MatchOther
    {
        Yes,
        No
    }

}
