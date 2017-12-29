using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Scheduler
{
    public class DuplicateRemovedScheduler : IScheduler
    {

        public IDuplicateRemover DuplicatedRemover { get; set; }

        public DuplicateRemovedScheduler()
        {
            DuplicatedRemover = new HashSetDuplicateRemover();
        }

        public virtual Request Poll(ITask task)
        {
            return null;
        }

        public virtual void Push(Request request, ITask task)
        {
            Debug.WriteLine("get a candidate url {0}", request.GetUrl());
            if (ShouldReserved(request) || NoNeedToRemoveDuplicate(request) || !DuplicatedRemover.IsDuplicate(request, task))
            {
                Debug.WriteLine("push to queue {0}", request.GetUrl());
                PushWhenNoDuplicate(request, task);
            }
        }

        protected virtual bool ShouldReserved(Request request)
        {
            return request.getExtra(Request.CycleTriedTimes) != null;
        }

        protected virtual bool NoNeedToRemoveDuplicate(Request request)
        {
            return HttpConstant.Method.POST.Equals(request.getMethod(), 
                StringComparison.CurrentCultureIgnoreCase);
        }

        protected virtual void PushWhenNoDuplicate(Request request, ITask task)
        {

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
        // ~DuplicateRemovedScheduler() {
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
