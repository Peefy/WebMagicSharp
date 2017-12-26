using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Processor
{
    public interface IPageProcessor : IDisposable
    {
        void Process(Page page);

        Site GetSite();
    }
}
