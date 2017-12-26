using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public interface ICollectorPipeline<T> : IPipeline
    {
        List<T> GetCollected();
    }
}
