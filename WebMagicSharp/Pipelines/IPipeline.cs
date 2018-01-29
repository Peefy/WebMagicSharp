using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Pipelines
{
    public interface IPipeline
    {
        void Process(ResultItems resultItems, ITask task);
    }

    public interface IPipeline<T>
    {
        void Process(T resultItems, ITask task);
    }

}
