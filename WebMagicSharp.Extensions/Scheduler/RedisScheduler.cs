using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.Scheduler;

namespace WebMagicSharp.Scheduler
{
    [Obsolete("not finished")]
    public class RedisScheduler : DuplicateRemovedScheduler, IMonitorableScheduler, IDuplicateRemover
    {
        [Obsolete("not finished")]
        public int GetLeftRequestsCount(ITask task)
        {
            throw new NotImplementedException();
        }

        [Obsolete("not finished")]
        public int GetTotalRequestsCount(ITask task)
        {
            throw new NotImplementedException();
        }

        [Obsolete("not finished")]
        public bool IsDuplicate(Request request, ITask task)
        {
            throw new NotImplementedException();
        }

        [Obsolete("not finished")]
        public void ResetDuplicateCheck(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}
