using System;
namespace WebMagicSharp.Scheduler
{
    public interface IDuplicateRemover
    {
        bool IsDuplicate(Request request, ITask task);

        /**
         * Reset duplicate check.
         * @param task task
         */
        void ResetDuplicateCheck(ITask task);

        /**
         * Get TotalRequestsCount for monitor.
         * @param task task
         * @return number of total request
         */
        int GetTotalRequestsCount(ITask task);
    }
}
