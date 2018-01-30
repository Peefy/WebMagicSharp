using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public class ResultItemsCollectorPipeline<T> : ICollectorPipeline<ResultItems>
    {
        private List<ResultItems> collector = new List<ResultItems>();

        public List<ResultItems> GetCollector()
        {
            return collector;
        }

        public void Process(ResultItems resultItems, ITask task)
        {
            collector.Add(resultItems);
        }
    }
}
