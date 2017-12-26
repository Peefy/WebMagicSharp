using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using WebMagicSharp.Utils;

namespace WebMagicSharp.Scheduler
{
    public class DuplicateRemovedScheduler : IScheduler
    {

        protected IDuplicateRemover DuplicatedRemover { get; set; }

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
            if (ShouldReserved(request) || NoNeedToRemoveDuplicate(request) || !DuplicatedRemover.isDuplicate(request, task))
            {
                Debug.WriteLine("push to queue {0}", request.GetUrl());
                PushWhenNoDuplicate(request, task);
            }
        }

        protected virtual bool ShouldReserved(Request request)
        {
            return request.getExtra(Request.CYCLE_TRIED_TIMES) != null;
        }

        protected virtual bool NoNeedToRemoveDuplicate(Request request)
        {
            return HttpConstant.Method.POST.Equals(request.getMethod(), 
                StringComparison.CurrentCultureIgnoreCase);
        }

        protected virtual void PushWhenNoDuplicate(Request request, ITask task)
        {

        }

        void IScheduler.Push(Request request, ITask task)
        {
            throw new NotImplementedException();
        }

        Request IScheduler.Poll(ITask task)
        {
            throw new NotImplementedException();
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
