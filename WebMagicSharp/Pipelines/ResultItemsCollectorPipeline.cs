using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public class ResultItemsCollectorPipeline<T> : ICollectorPipeline<T>
    {

        private List<T> _collector = new List<T>();

        public List<T> GetCollector()
        {
            return _collector;
        }

        public void Process(T resultItems, ITask task)
        {
            _collector.Add(resultItems);
        }

    }
}
