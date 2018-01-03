using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp;

namespace WebMagicSharp.Scheduler
{

    public class PriorityScheduler : DuplicateRemovedScheduler, IMonitorableScheduler
    {
        private Queue<Request> _noPriorityQueue = new Queue<Request>();
        private SortedSet<Request> _priorityQueuePlus = new SortedSet<Request>();
        private SortedSet<Request> _priorityQueueMinus = new SortedSet<Request>();

        protected override void PushWhenNoDuplicate(Request request, ITask task)
        {
            if (request.GetPriority() == 0)
            {
                _noPriorityQueue.Enqueue(request);
            }
            else if (request.GetPriority() > 0)
            {
                _priorityQueuePlus.Add(request);
            }
            else
            {
                _priorityQueueMinus.Add(request);
            }
        }

        public override Request Poll(ITask task)
        {
            Request poll = _priorityQueuePlus.FirstOrDefault();
            _priorityQueuePlus.Remove(poll);
            if (poll != null)
            {
                return poll;
            }
            poll = _noPriorityQueue.Dequeue();
            if (poll != null)
            {
                return poll;
            }
            poll = _priorityQueueMinus.FirstOrDefault();
            _priorityQueueMinus.Remove(poll);
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
