using System;
using System.Collections.Generic;
using System.Text;
using WebMagicSharp.Processor;

namespace WebMagicSharp.Model
{
    public class OOSpider : Spider
    {
        public OOSpider(IPageProcessor pageProcessor) : base(pageProcessor)
        {
        }
    }
}
