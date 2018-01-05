using System;
using System.Collections.Generic;

namespace WebMagicSharp.Pipelines
{
    public class CollectorPageModelPipeline<T> : IPageModelPipeline<T>
    {
        protected List<T> collector;

        public CollectorPageModelPipeline()
        {
            collector = new List<T>();
        }

        public void Process(T t, ITask task)
        {
            collector.Add(t);
        }

        public List<T> GetCollector() => collector;

    }
}
