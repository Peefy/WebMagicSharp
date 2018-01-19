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

}
