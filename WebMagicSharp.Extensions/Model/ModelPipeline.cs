
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Utils;
using WebMagicSharp.Model.Attributes;
using WebMagicSharp.Pipelines;

namespace WebMagicSharp.Model
{
    public class ModelPipeline<T> : IPipeline
    {
        private Dictionary<Type, IPageModelPipeline<T>> pageModelPipelines
            = new Dictionary<Type, IPageModelPipeline<T>>();

        public ModelPipeline()
        {
            
        }

        public ModelPipeline<T> Put(Type type, IPageModelPipeline<T> pageModelPipeline)
        {
            pageModelPipelines.Add(type, pageModelPipeline);
            return this;
        }

        public void Process(WebMagicSharp.T resultItems, ITask task)
        {
            foreach(var keyValuePair in pageModelPipelines)
            {
                var obj = resultItems.Get<object>(keyValuePair.Key.Name);
                if(obj != null)
                {
                    var attr = AttributeUtil.
                        GetAttribute<ExtractByAttribute>(keyValuePair.Key);
                    if (attr != null || !attr.IsMulti)
                        keyValuePair.Value.Process((T)obj, task);
                    else
                    {
                        var list = obj as List<object>;
                        foreach (var o in list)
                            keyValuePair.Value.Process((T)o, task);
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
