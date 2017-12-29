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
        private HashSet<String> _urls = 
            WMCollection<string>.NewHashSet(new HashSet<String>().ToArray());

        public HashSetDuplicateRemover()
        {
        }

        protected String GetUrl(Request request)
        {
            return request.GetUrl();
        }

        public int GetTotalRequestsCount(ITask task)
        {
            return _urls.Count;
        }

        public bool IsDuplicate(Request request, ITask task)
        {
            return !_urls.Add(GetUrl(request));
        }

        public void ResetDuplicateCheck(ITask task)
        {
            _urls.Clear();
        }
    }
}
