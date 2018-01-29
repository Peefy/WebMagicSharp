using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    public interface IPageProcessor
    {
        void Process(Page page);

        Site GetSite();
    }
}
