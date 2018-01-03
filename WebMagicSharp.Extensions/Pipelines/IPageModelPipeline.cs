using System;
namespace WebMagicSharp.Pipelines
{
    public interface IPageModelPipeline<T>
    {
        void process(T t, ITask task);
    }
}
