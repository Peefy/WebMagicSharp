
using System;
using System.Reflection;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Utils;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Model;
using WebMagicSharp.Model.Attributes;

namespace WebMagicSharp.Model
{
    public class PageModelCollectorPipeline<T> : ICollectorPipeline<T>
    {

        private readonly CollectorPageModelPipeline<T> typePipeline =
            new CollectorPageModelPipeline<T>();

        private readonly Type type;

        public PageModelCollectorPipeline(Type type)
        {
            this.type = type;
        }

        public List<T> GetCollector()
        {
            return typePipeline.GetCollector();
        }

        public void Process(ResultItems resultItems, ITask task)
        {
            var obj = resultItems.Get<object>(type.Name);
            if(obj != null)
            {
                var attr = AttributeUtil.GetAttribute<ExtractByAttribute>(type);
                if(attr == null || !attr.IsMulti)
                {
                    typePipeline.Process((T)obj, task);
                }
                else
                {
                    var list = obj as List<object>;
                    if(list?.Count > 0)
                    {
                        foreach (var o in list)
                            typePipeline.Process((T)o, task);
                    }
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }

}
