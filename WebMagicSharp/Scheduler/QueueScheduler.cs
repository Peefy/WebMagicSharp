using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Scheduler
{
    public class QueueScheduler : DuplicateRemovedScheduler
    {
        private Queue<Request> queue = new Queue<Request>();

        protected override void PushWhenNoDuplicate(Request request, ITask task)
        {
            queue.Enqueue(request);
        }

        public override Request Poll(ITask task)
        {
            return queue.Dequeue();
        }


        public int GetLeftRequestsCount(ITask task)
        {
            return queue.Count;
        }

        public int GetTotalRequestsCount(ITask task)
        {
            return DuplicatedRemover.GetTotalRequestsCount(task);
        }
    }
}
