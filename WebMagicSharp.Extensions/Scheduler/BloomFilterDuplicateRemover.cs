using System;

using WebMagicSharp.Scheduler;

namespace WebMagicSharp.Scheduler
{
    public class BloomFilterDuplicateRemover : IDuplicateRemover
    {

        private int expectedInsertions;

        private double fpp;

        private int counter;

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
