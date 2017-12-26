using System;
using System.Linq;
using System.Collections.Generic;

using WebMagicSharp.Utils;

namespace WebMagicSharp.Scheduler
{
    /// <summary>
    /// Hash set duplicate remover.
    /// </summary>
    public class HashSetDuplicateRemover : IDuplicateRemover
    {
        private HashSet<String> urls = 
            WMCollection<string>.NewHashSet(new HashSet<String>().ToArray());

        public HashSetDuplicateRemover()
        {
        }

        public int GetTotalRequestsCount(ITask task)
        {
            throw new NotImplementedException();
        }

        public bool isDuplicate(Request request, ITask task)
        {
            throw new NotImplementedException();
        }

        public void ResetDuplicateCheck(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}
