using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

<<<<<<< HEAD
using StackExchange.Redis;

using WebMagicSharp;
=======
using WebMagicSharp.Scheduler;
>>>>>>> fe0493ec8c1dbdf2b0c98f6d6f050907a7aed103

namespace WebMagicSharp.Scheduler
{
    public class RedisScheduler : DuplicateRemovedScheduler, IMonitorableScheduler, IDuplicateRemover
    {
<<<<<<< HEAD
        

=======
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
>>>>>>> fe0493ec8c1dbdf2b0c98f6d6f050907a7aed103
    }
}
