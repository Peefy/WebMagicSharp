using System;

namespace WebMagicSharp.Pipelines
{
    public interface IPageModelPipeline<T>
    {
        void Process(T t, ITask task);
    }

}
