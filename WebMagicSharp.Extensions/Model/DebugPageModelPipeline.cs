using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Pipelines;

namespace WebMagicSharp.Model
{
    public class DebugPageModelPipeline : IPageModelPipeline<object>
    {
        public void Process(object t, ITask task)
        {
            Debug.WriteLine(t);
        }
    }
}
