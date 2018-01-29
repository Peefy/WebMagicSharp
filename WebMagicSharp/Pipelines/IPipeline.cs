using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public interface IPipeline : IDisposable
    {
        void Process(ResultItems resultItems, ITask task);
    }
}
