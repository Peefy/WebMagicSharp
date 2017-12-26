using System;

namespace WebMagicSharp.DownLoaders
{
    public interface IDownloader : IDisposable
    {
        /**
     * Downloads web pages and store in Page object.
     *
     * @param request request
     * @param task task
     * @return page
     */
        Page Download(Request request, ITask task);

        /**
         * Tell the downloader how many threads the spider used.
         * @param threadNum number of threads
         */
        void SetThread(int threadNum);
    }
}