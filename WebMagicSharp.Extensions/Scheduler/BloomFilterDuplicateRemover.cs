using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using WebMagicSharp.Processor;
using WebMagicSharp.Selector;
using WebMagicSharp.Scheduler;

namespace WebMagicSharp.Scheduler
{
    [Obsolete("not finished")]
    public class BloomFilterDuplicateRemover : IDuplicateRemover
    {

        private int expectedInsertions;

        public BloomFilterDuplicateRemover(int expectedInsertions)
        {
            this.expectedInsertions = expectedInsertions;
        }
        
        public BloomFilterDuplicateRemover(int expectedInsertions, double fpp)
        {

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
