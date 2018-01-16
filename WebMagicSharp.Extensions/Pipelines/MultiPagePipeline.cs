using System;
using System.Linq;
using System.Collections.Generic;

using WebMagicSharp.Pipelines;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Pipelines
{
    public class MultiPagePipeline : IPipeline
    {

        private DoubleKeyMap<String, String, Boolean> pageMap = 
            new DoubleKeyMap<String, String, Boolean>();

        private DoubleKeyMap<String, String, IMultiPageModel> objectMap = 
            new DoubleKeyMap<String, String, IMultiPageModel>();

        public void Process(ResultItems resultItems, ITask task)
        {
            var resultItemsAll = resultItems.GetAll();
            foreach(var item in resultItemsAll)
            {
                if(item.Value is IMultiPageModel multiPageModel)
                {
                    pageMap.put(multiPageModel.GetPageKey(),
                                multiPageModel.GetPage(), false);
                    lock (this)
                    {
                        pageMap.put(multiPageModel.GetPageKey(),
                                    multiPageModel.GetPage(), true);
                        var otherPages = multiPageModel.GetOtherPages();
                        if (otherPages?.Count > 0)
                        {
                            foreach (var otherPage in otherPages)
                            {
                                if (pageMap.Get(multiPageModel.GetPageKey(),
                                                         otherPage) == false)
                                {
                                    pageMap.put(multiPageModel.GetPageKey(), otherPage, false);
                                }
                            }
                        }
                        var booleanMap = pageMap.Get(multiPageModel.GetPageKey());
                        objectMap.put(multiPageModel.GetPageKey(),
                                      multiPageModel.GetPage(), multiPageModel);
                        if (booleanMap == null)
                            return;
                        foreach (var stringBooleanEntry in booleanMap)
                        {
                            if (!stringBooleanEntry.Value)
                            {
                                booleanMap.Remove(stringBooleanEntry.Key);
                                return;
                            }
                        }
                        var range = objectMap.Get(multiPageModel.GetPageKey());
                        var entryList = range.ToList();
                        if (entryList.Count > 0)
                        {
                            //entryList.Sort();
                            //pai xu sort
                            var value = entryList[0].Value;
                            for (int i = 1; i < entryList.Count; i++)
                            {
                                value = value.Combine(entryList[i].Value);
                            }
                            resultItemsAll[item.Key] = value;
                        }
                    }                                            
                }
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MultiPagePipeline() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
