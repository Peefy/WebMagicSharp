using System;

using WebMagicSharp.Pipelines;

namespace WebMagicSharp.Model
{
    public class ConsolePageModelPipeline : IPageModelPipeline<object>
    {
        public void Process(object t, ITask task)
        {
            Console.WriteLine(t);
        }
    }

    public class ConsolePageModelPipeline<T> : IPageModelPipeline<T>
    {
        public void Process(T t, ITask task)
        {
            Console.WriteLine(t);
        }
    }

}
