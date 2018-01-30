using System;
using System.Collections.Generic;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Handler
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
                var o = item.Value;
                if (o is IMultiPageModel multiPageModel)
                {
                    pageMap.Put(multiPageModel.GetPageKey(), multiPageModel.GetPage(), false);
                    //每个key单独加锁
                    lock (pageMap.Get(multiPageModel.GetPageKey()))
                    {
                        pageMap.Put(multiPageModel.GetPageKey(), multiPageModel.GetPage(), true);
                        //其他需要拼凑的部分
                        if (multiPageModel.GetOtherPages() != null)
                        {
                            foreach (var otherPage in multiPageModel.GetOtherPages())
                            {
                                var aBoolean = pageMap.Get(multiPageModel.GetPageKey(), otherPage);
                                pageMap.Put(multiPageModel.GetPageKey(), otherPage, false);
                            }
                        }
                        //check if all pages are processed
                        var booleanMap = pageMap.Get(multiPageModel.GetPageKey());
                        objectMap.Put(multiPageModel.GetPageKey(), multiPageModel.GetPage(), multiPageModel);
                        if (booleanMap == null)
                        {
                            return;
                        }
                        // /过滤，这次完成的page item中，还未拼凑完整的item，不进入下一个pipeline
                        foreach (var stringBooleanEntry in booleanMap)
                        {
                            if (!stringBooleanEntry.Value)
                            {
                                //item.remove();
                                return;
                            }
                        }
                        var entryList = new List<KeyValuePair<String, IMultiPageModel>>();
                        entryList.AddRange(objectMap.Get(multiPageModel.GetPageKey()));
                        if (entryList.Count != 0)
                        {
                            var comparison = new Comparison<KeyValuePair<string, IMultiPageModel>>
                                ((KeyValuePair<string, IMultiPageModel> d1, KeyValuePair<string, IMultiPageModel> d2) =>
                                {
                                    try
                                    {
                                        int i1 = int.Parse(d1.Key);
                                        int i2 = int.Parse(d2.Key);
                                        return i1 - i2;
                                    }
                                    catch
                                    {
                                        return d1.Key.CompareTo(d2.Key);
                                    }
                                });
                            entryList.Sort(comparison);
                        }
                        // 合并
                        var value = entryList[0].Value;
                        for (int i = 1; i < entryList.Count; i++)
                        {
                            value = value.Combine(entryList[i].Value);
                        }

                    }
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MultiPagePipeline() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }


}
