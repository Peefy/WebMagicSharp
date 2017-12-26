using System;
namespace WebMagicSharp.Scheduler
{
    public interface IScheduler : IDisposable
    {
        void Push(Request request, ITask task);

        /**
         * get an url to crawl
         *
         * @param task the task of spider
         * @return the url to crawl
         */
        Request Poll(ITask task);
    }
}
