using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Scheduler
{
    public class QueueScheduler : DuplicateRemovedScheduler
    {
        protected Queue<Request> _queue = new Queue<Request>();

        protected override void PushWhenNoDuplicate(Request request, ITask task)
        {
            _queue.Enqueue(request);
        }

        public override Request Poll(ITask task)
        {
            return _queue.Dequeue();
        }


        public int GetLeftRequestsCount(ITask task)
        {
            return _queue.Count;
        }

        public int GetTotalRequestsCount(ITask task)
        {
            return DuplicatedRemover.GetTotalRequestsCount(task);
        }
    }
}
