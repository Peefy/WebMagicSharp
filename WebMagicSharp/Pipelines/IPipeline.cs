using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public interface IPipeline : IDisposable
    {
        void Process(T resultItems, ITask task);
    }
}
