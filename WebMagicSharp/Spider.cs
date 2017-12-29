using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

using WebMagicSharp.DownLoaders;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Processor;
using WebMagicSharp.Scheduler;
using WebMagicSharp.Thread;
using WebMagicSharp.Utils;

namespace WebMagicSharp
{
    public class Spider : ITask , IDisposable
    {

        protected IDownloader downloader;

        protected List<IPipeline> pipelines = new List<IPipeline>();

        protected IPageProcessor pageProcessor;

        protected List<Request> startRequests;

        protected Site site;

        protected String guid;

        protected IScheduler scheduler = new QueueScheduler();

        //protected Logger logger = LoggerFactory.getLogger(getClass());

        protected CountableThreadPool threadPool;

        //protected ExecutorService executorService;

        protected int threadNum = 1;

        //protected AtomicInteger stat = new AtomicInteger(STAT_INIT);
        protected SpiderStatus stat;

        protected bool exitWhenComplete = true;

        protected bool spawnUrl = true;

        protected bool destroyWhenExit = true;

        //private ReentrantLock newUrlLock = new ReentrantLock();
        object newUrlLock = new object();

        //private Condition newUrlCondition = newUrlLock.newCondition();

        //private List<SpiderListener> spiderListeners;

        int pageCount = 0;

        //private final AtomicLong pageCount = new AtomicLong(0);

        private List<ISpiderListener> spiderListeners;

        private DateTime startTime;

        private int emptySleepTime = 30000;

        public static Spider create(IPageProcessor pageProcessor)
        {
            return new Spider(pageProcessor);
        }

        /**
         * create a spider with pageProcessor.
         *
         * @param pageProcessor pageProcessor
         */
        public Spider(IPageProcessor pageProcessor)
        {
            this.pageProcessor = pageProcessor;
            this.site = pageProcessor.GetSite();
        }

        protected void checkIfRunning()
        {
            if (stat == SpiderStatus.Running)
            {
                throw new Exception("Spider is already running!");
            }
        }

        public Spider startUrls(List<String> startUrls)
        {
            checkIfRunning();
            this.startRequests = UrlUtils.ConvertToRequests(startUrls);
            return this;
        }

        public Spider startRequest(List<Request> startRequests)
        {
            checkIfRunning();
            this.startRequests = startRequests;
            return this;
        }

        public Spider setUUID(String guid)
        {
            this.guid = guid;
            return this;
        }

        public Spider Scheduler(IScheduler scheduler)
        {
            return setScheduler(scheduler);
        }

        public Spider setScheduler(IScheduler scheduler)
        {
            checkIfRunning();
            IScheduler oldScheduler = this.scheduler;
            this.scheduler = scheduler;
            if (oldScheduler != null)
            {
                Request request;
                while ((request = oldScheduler.Poll(this)) != null)
                {
                    this.scheduler.Push(request, this);
                }
            }
            return this;
        }

        public Spider pipeline(IPipeline pipeline)
        {
            return addPipeline(pipeline);
        }

        public Spider addPipeline(IPipeline pipeline)
        {
            checkIfRunning();
            this.pipelines.Add(pipeline);
            return this;
        }

        public Spider setPipelines(List<IPipeline> pipelines)
        {
            checkIfRunning();
            this.pipelines = pipelines;
            return this;
        }

        public Spider clearPipeline()
        {
            pipelines = new List<IPipeline>();
            return this;
        }

        public Spider Downloader(IDownloader downloader)
        {
            return setDownloader(downloader);
        }

        public Spider setDownloader(IDownloader downloader)
        {
            checkIfRunning();
            this.downloader = downloader;
            return this;
        }

        protected void initComponent()
        {
            if (downloader == null)
            {
                this.downloader = new HttpClientDownloader();
            }
            if (pipelines.Count == 0)
            {
                pipelines.Add(new ConsolePipeline());
            }
            //downloader.setThread(threadNum);
            if (threadPool == null || threadPool.isShutdown())
            {
                threadPool = new CountableThreadPool(threadNum);
            }
            if (startRequests != null)
            {
                foreach (var request in startRequests)
                {
                    addRequest(request);
                }
                startRequests.Clear();
            }
            startTime = DateTime.Now;
        }

        private void checkRunningStat()
        {
            while (true)
            {
                var statNow = stat;
                if (statNow == SpiderStatus.Running)
                {
                    throw new Exception("Spider is already running!");
                }
                if(stat == statNow)
                {
                    stat = SpiderStatus.Running;
                    break;
                }             
            }
        }

        public void run()
        {
            checkRunningStat();
            initComponent();
            Debug.WriteLine($"Spider {GetGuid()} started!");
            while (!(stat == SpiderStatus.Running))
            {
                var request = scheduler.Poll(this);
                if (request == null)
                {
                    waitNewUrl();
                }
                else
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            processRequest(request);
                            onSuccess(request);
                        }
                        catch (Exception e)
                        {
                            onError(request);
                            Debug.WriteLine($"process request {request} error : {e.Message}");

                        }
                        finally
                        {
                            pageCount++;
                            signalNewUrl();
                        }
                    });
                }
            }
            stat = SpiderStatus.Stop;
            if (destroyWhenExit == true)
                close();
            Debug.WriteLine($"Spider {GetGuid()} closed! {pageCount} pages downloaded.");
        }

        protected void onError(Request request)
        {
            if (spiderListeners?.Count > 0)
            {
                foreach(var spiderListener in spiderListeners)
                {
                    spiderListener.onError(request);
                }
            }
        }

        protected void onSuccess(Request request)
        {
            if (spiderListeners?.Count > 0)
            {
                foreach (var spiderListener in spiderListeners)
                {
                    spiderListener.onSuccess(request);
                }
            }
        }

        public void test(string[] urls)
        {
            initComponent();
            if (urls.Length > 0)
            {
                foreach(var url in urls)
                {
                    processRequest(new Request(url));
                }
            }
        }

        private void processRequest(Request request)
        {
            Page page = downloader.Download(request, this);
            if (page.isDownloadSuccess())
            {
                onDownloadSuccess(request, page);
            }
            else
            {
                onDownloaderFail(request);
            }
        }

        private void onDownloadSuccess(Request request, Page page)
        {
            if (site.getAcceptStatCode().Contains(page.getStatusCode()))
            {
                pageProcessor.Process(page);
                extractAndAddRequests(page, spawnUrl);
                if (!page.getResultItems().IsSkip())
                {
                    foreach(var pipeline in pipelines)
                    {
                        pipeline.Process(page.getResultItems(), this);
                    }
                }
            }
            else
            {
                Debug.WriteLine($"page status code error, page {request.Url}" +
                                " , code: {page.getStatusCode()}");
            }
            sleep(site.getSleepTime());
            return;
        }

        private void onDownloaderFail(Request request)
        {
            if (site.getCycleRetryTimes() == 0)
            {
                sleep(site.getSleepTime());
            }
            else
            {
                // for cycle retry
                doCycleRetry(request);
            }
        }

        private void doCycleRetry(Request request)
        {
            Object cycleTriedTimesObject = request.getExtra(Request.CycleTriedTimes);
            if (cycleTriedTimesObject == null)
            {
                addRequest(request.putExtra(Request.CycleTriedTimes, 1));
            }
            else
            {
                int cycleTriedTimes = (int)cycleTriedTimesObject;
                cycleTriedTimes++;
                if (cycleTriedTimes < site.getCycleRetryTimes())
                {
                    addRequest(request.putExtra(Request.CycleTriedTimes, cycleTriedTimes));
                }
            }
            sleep(site.getRetrySleepTime());
        }

        protected void sleep(int time)
        {
            try
            {
                System.Threading.Thread.Sleep(time);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Thread interrupted when sleep: {e.Message}");
            }
        }

        protected void extractAndAddRequests(Page page, bool spawnUrl)
        {
            if (spawnUrl && page.getTargetRequests()?.Count > 0)
            {
                foreach(var request in page.getTargetRequests())
                {
                    addRequest(request);
                }
            }
        }

        private void addRequest(Request request)
        {
            if (site.getDomain() == null && request != null && request.Url != null)
            {
                site.setDomain(UrlUtils.GetDomain(request.Url));
            }
            scheduler.Push(request, this);
        }

        public void RunAsync()
        {
            Task.Run(()=>
            {
                this.run();
            });
        }

        public Spider addUrl(String[] urls)
        {
            foreach(var url in urls)
            {
                addRequest(new Request(url));
            }
            signalNewUrl();
            return this;
        }

        public List<ResultItems> getAll(IList<String> urls)
        {
            destroyWhenExit = false;
            spawnUrl = false;
            if (startRequests != null)
            {
                startRequests.Clear();
            }
            foreach(var request in UrlUtils.ConvertToRequests(urls))
            {
                addRequest(request);
            }
            var collectorPipeline = getCollectorPipeline();
            pipelines.Add(collectorPipeline);
            run();
            spawnUrl = true;
            destroyWhenExit = true;
            return collectorPipeline.GetCollected();
        }

        protected virtual ICollectorPipeline<ResultItems> getCollectorPipeline() => new ResultItemsCollectorPipeline();

        public ResultItems get(String url) 
        {
            var urls = new List<string>
            {
                url
            };
            var resultItemses = getAll(urls);
            if (resultItemses != null && resultItemses.Count > 0)
            {
                return resultItemses[0];
            }
            else
            {
                return null;
            }
        }

        public Spider addRequest(Request[] requests)
        {
            foreach(var request in requests)
            {
                addRequest(request);
            }
            signalNewUrl();
            return this;
        }

        private void waitNewUrl()
        {
            lock(newUrlLock)
            {
                if (exitWhenComplete)
                    return;
                System.Threading.Thread.Sleep(emptySleepTime);
            }
        }

        private void signalNewUrl()
        {
            lock (newUrlLock)
            {

            }
        }

        public void start()
        {
            RunAsync();
        }

        public void close()
        {
            Dispose();
        }

        public void stop()
        {
            if(stat == SpiderStatus.Running)
            {
                stat = SpiderStatus.Stop;
                Debug.WriteLine("Spider " + GetGuid() + " stop success!");
            }
        }

        public Spider thread(int threadNum)
        {
            checkIfRunning();
            this.threadNum = threadNum;
            if (threadNum <= 0)
            {
                throw new ArgumentException("threadNum should be more than one!",
                                            nameof(threadNum));
            }
            return this;
        }

        public bool isExitWhenComplete()
        {
            return exitWhenComplete;
        }

        public Spider setExitWhenComplete(bool exitWhenComplete)
        {
            this.exitWhenComplete = exitWhenComplete;
            return this;
        }

        public int getThreadAlive()
        {
            if (threadPool == null)
            {
                return 0;
            }
            return threadPool.getThreadAlive();
        }

        public bool isSpawnUrl()
        {
            return spawnUrl;
        }

        public long getPageCount()
        {
            return pageCount;
        }

        public Spider setSpawnUrl(bool spawnUrl)
        {
            this.spawnUrl = spawnUrl;
            return this;
        }

        public List<ISpiderListener> getSpiderListeners()
        {
            return spiderListeners;
        }

        public Spider setSpiderListeners(List<ISpiderListener> spiderListeners)
        {
            this.spiderListeners = spiderListeners;
            return this;
        }

        public DateTime getStartTime()
        {
            return startTime;
        }

        public IScheduler getScheduler()
        {
            return scheduler;
        }

        /**
         * Set wait time when no url is polled.<br><br>
         *
         * @param emptySleepTime In MILLISECONDS.
         */
        public void setEmptySleepTime(int emptySleepTime)
        {
            this.emptySleepTime = emptySleepTime;
        }

        public string GetGuid()
        {
            if (guid != null)
            {
                return guid;
            }
            if (site != null)
            {
                return site.getDomain();
            }
            guid = Guid.NewGuid().ToString();
            return guid;
        }

        public Site GetSite()
        {
            return site;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Spider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion



    }
}