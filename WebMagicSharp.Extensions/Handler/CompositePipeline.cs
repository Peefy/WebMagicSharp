using System;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Pipelines;

namespace WebMagicSharp.Handler
{
    public class CompositePipeline : IPipeline
    {

        private List<ISubPipeline> subPipelines = new List<ISubPipeline>();

        public void Dispose()
        {
            
        }

        public void Process(ResultItems resultItems, ITask task)
        {
            foreach(var subPipeline in subPipelines)
            {
                if (subPipeline.Match(resultItems.GetRequest()))
                {
                    var matchOtherProcessorProcessor = subPipeline.ProcessResult(resultItems, task);
                    if (matchOtherProcessorProcessor != MatchOther.Yes)
                    {
                        return;
                    }
                }
            }
        }

        public CompositePipeline AddSubPipeline(ISubPipeline subPipeline)
        {
            this.subPipelines.Add(subPipeline);
            return this;
        }

        public CompositePipeline SetSubPipeline(params ISubPipeline[] subPipelines)
        {
            this.subPipelines = new List<ISubPipeline>();
            foreach(var subPipeline in subPipelines)
            {
                this.subPipelines.Add(subPipeline);
            }
            return this;
        }

    }
}
