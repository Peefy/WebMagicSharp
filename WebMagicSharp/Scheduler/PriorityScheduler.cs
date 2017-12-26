using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.Scheduler
{

    public class PriorityScheduler : DuplicateRemovedScheduler, IMonitorableScheduler
    {
        private Queue<Request> noPriorityQueue = new Queue<Request>();
        private SortedSet<Request> priorityQueuePlus = new SortedSet<Request>();
        private SortedSet<Request> priorityQueueMinus = new SortedSet<Request>();

        protected override void PushWhenNoDuplicate(Request request, ITask task)
        {
            if (request.getPriority() == 0)
            {
                noPriorityQueue.Enqueue(request);
            }
            else if (request.getPriority() > 0)
            {
                priorityQueuePlus.Add(request);
            }
            else
            {
                priorityQueueMinus.Add(request);
            }
        }

        public override Request Poll(ITask task)
        {
            Request poll = priorityQueuePlus.FirstOrDefault();
            priorityQueuePlus.Remove(poll);
            if (poll != null)
            {
                return poll;
            }
            poll = noPriorityQueue.Dequeue();
            if (poll != null)
            {
                return poll;
            }
            poll = priorityQueueMinus.FirstOrDefault();
            priorityQueueMinus.Remove(poll);
            return poll;
        }


        public int GetLeftRequestsCount(ITask task)
        {
            return DuplicatedRemover.GetTotalRequestsCount(task);
        }

        public int GetTotalRequestsCount(ITask task)
        {
            return DuplicatedRemover.GetTotalRequestsCount(task);
        }
    }
}
