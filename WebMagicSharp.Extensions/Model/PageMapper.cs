
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Model;

namespace WebMagicSharp.Model
{
    public class PageMapper<T>
    {
        private Type type;

        private PageModelExtractor pageModelExtractor;

        public PageMapper(Type type, PageModelExtractor pageModelExtractor)
        {
            this.type = type;
            this.pageModelExtractor = pageModelExtractor;
        }

        public T Get(Page page) => (T)pageModelExtractor.Process(page);

        public List<T> GetAll(Page page) => pageModelExtractor.Process(page) as List<T>;

    }

}
