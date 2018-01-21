using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Scheduler;

namespace WebMagicSharp.Scheduler
{
    public class RedisScheduler : DuplicateRemovedScheduler, IMonitorableScheduler, IDuplicateRemover
    {
        public int GetLeftRequestsCount(ITask task)
        {
            throw new NotImplementedException();
        }

        public int GetTotalRequestsCount(ITask task)
        {
            throw new NotImplementedException();
        }

        public bool IsDuplicate(Request request, ITask task)
        {
            throw new NotImplementedException();
        }

        public void ResetDuplicateCheck(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}
