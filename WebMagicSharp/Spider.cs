using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

using WebMagicSharp.DownLoaders;
using WebMagicSharp.Pipelines;
using WebMagicSharp.Processor;
using WebMagicSharp.Scheduler;
using WebMagicSharp.Thread;
using WebMagicSharp.Utils;

namespace WebMagicSharp
{
    /// <summary>
    /// Finish the Spider
    /// </summary>
    public class Spider : ITask , IDisposable
    {

        protected IDownloader downloader;

        protected List<IPipeline> pipelines = new List<IPipeline>();

        protected IPageProcessor pageProcessor;

        protected List<Request> startRequests;

        protected Site site;

        protected string guid;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageProcessor"></param>
        /// <returns></returns>
        public static Spider Create(IPageProcessor pageProcessor)
        {
            return new Spider(pageProcessor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageProcessor"></param>
        public Spider(IPageProcessor pageProcessor)
        {
            this.pageProcessor = pageProcessor;
            this.site = pageProcessor.GetSite();
        }

        protected void CheckIfRunning()
        {
            if (stat == SpiderStatus.Running)
            {
                throw new Exception("Spider is already running!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startUrls"></param>
        /// <returns></returns>
        public Spider StartUrls(List<string> startUrls)
        {
            CheckIfRunning();
            this.startRequests = UrlUtils.ConvertToRequests(startUrls);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRequests"></param>
        /// <returns></returns>
        public Spider StartRequest(List<Request> startRequests)
        {
            CheckIfRunning();
            this.startRequests = startRequests;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Spider SetGuid(string guid)
        {
            this.guid = guid;
            return this;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public Spider Scheduler(IScheduler scheduler)
        {
            return SetScheduler(scheduler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduler"></param>
        /// <returns></returns>
        public Spider SetScheduler(IScheduler scheduler)
        {
            CheckIfRunning();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public Spider Pipeline(IPipeline pipeline)
        {
            return AddPipeline(pipeline);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public Spider AddPipeline(IPipeline pipeline)
        {
            CheckIfRunning();
            this.pipelines.Add(pipeline);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pipelines"></param>
        /// <returns></returns>
        public Spider SetPipelines(List<IPipeline> pipelines)
        {
            CheckIfRunning();
            this.pipelines = pipelines;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Spider ClearPipeline()
        {
            pipelines = new List<IPipeline>();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloader"></param>
        /// <returns></returns>
        public Spider Downloader(IDownloader downloader)
        {
            return SetDownloader(downloader);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="downloader"></param>
        /// <returns></returns>
        public Spider SetDownloader(IDownloader downloader)
        {
            CheckIfRunning();
            this.downloader = downloader;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void InitComponent()
        {
            if (downloader == null)
            {
                this.downloader = new HttpClientDownloader();
            }
            if (pipelines.Count == 0)
            {
                pipelines.Add(new ConsolePipeline());
            }
            if (threadPool == null || threadPool.IsShutdown())
            {
                threadPool = new CountableThreadPool(threadNum);
            }
            if (startRequests != null)
            {
                foreach (var request in startRequests)
                {
                    AddRequest(request);
                }
                startRequests.Clear();
            }
            startTime = DateTime.Now;
        }

        private void CheckRunningStat()
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

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            CheckRunningStat();
            InitComponent();
            Debug.WriteLine($"Spider {GetGuid()} started!");
            while (!(stat == SpiderStatus.Running))
            {
                var request = scheduler.Poll(this);
                if (request == null)
                {
                    WaitNewUrl();
                }
                else
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            ProcessRequest(request);
                            OnSuccess(request);
                        }
                        catch (Exception e)
                        {
                            OnError(request);
                            Debug.WriteLine($"process request {request} error : {e.Message}");

                        }
                        finally
                        {
                            pageCount++;
                            SignalNewUrl();
                        }
                    });
                }
            }
            stat = SpiderStatus.Stop;
            if (destroyWhenExit == true)
                Close();
            Debug.WriteLine($"Spider {GetGuid()} closed! {pageCount} pages downloaded.");
        }

        protected void OnError(Request request)
        {
            if (spiderListeners?.Count > 0)
            {
                foreach(var spiderListener in spiderListeners)
                {
                    spiderListener.OnError(request);
                }
            }
        }

        protected void OnSuccess(Request request)
        {
            if (spiderListeners?.Count > 0)
            {
                foreach (var spiderListener in spiderListeners)
                {
                    spiderListener.OnSuccess(request);
                }
            }
        }

        public void Test(string[] urls)
        {
            InitComponent();
            if (urls.Length > 0)
            {
                foreach(var url in urls)
                {
                    ProcessRequest(new Request(url));
                }
            }
        }

        private void ProcessRequest(Request request)
        {
            Page page = downloader.Download(request, this);
            if (page.IsDownloadSuccess())
            {
                OnDownloadSuccess(request, page);
            }
            else
            {
                OnDownloaderFail(request);
            }
        }

        private void OnDownloadSuccess(Request request, Page page)
        {
            if (site.AcceptStatCode.Contains(page.GetStatusCode()))
            {
                pageProcessor.Process(page);
                ExtractAndAddRequests(page, spawnUrl);
                if (!page.GetResultItems().IsSkip())
                {
                    foreach(var pipeline in pipelines)
                    {
                        pipeline.Process(page.GetResultItems(), this);
                    }
                }
            }
            else
            {
                Debug.WriteLine($"page status code error, page {request.Url}" +
                                " , code: {page.getStatusCode()}");
            }
            Sleep(site.SleepTime);
            return;
        }

        private void OnDownloaderFail(Request request)
        {
            if (site.GetCycleRetryTimes() == 0)
            {
                Sleep(site.SleepTime);
            }
            else
            {
                // for cycle retry
                DoCycleRetry(request);
            }
        }

        private void DoCycleRetry(Request request)
        {
            Object cycleTriedTimesObject = request.GetExtra(Request.CycleTriedTimes);
            if (cycleTriedTimesObject == null)
            {
                AddRequest(request.PutExtra(Request.CycleTriedTimes, 1));
            }
            else
            {
                int cycleTriedTimes = (int)cycleTriedTimesObject;
                cycleTriedTimes++;
                if (cycleTriedTimes < site.GetCycleRetryTimes())
                {
                    AddRequest(request.PutExtra(Request.CycleTriedTimes, cycleTriedTimes));
                }
            }
            Sleep(site.GetRetrySleepTime());
        }

        protected void Sleep(int time)
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

        protected void ExtractAndAddRequests(Page page, bool spawnUrl)
        {
            if (spawnUrl && page.GetTargetRequests()?.Count > 0)
            {
                foreach(var request in page.GetTargetRequests())
                {
                    AddRequest(request);
                }
            }
        }

        private void AddRequest(Request request)
        {
            if (site.Domain== null && request != null && request.Url != null)
            {
                site.SetDomain(UrlUtils.GetDomain(request.Url));
            }
            scheduler.Push(request, this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RunAsync()
        {
            Task.Run(()=>
            {
                this.Run();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public Spider AddUrl(string[] urls)
        {
            foreach(var url in urls)
            {
                AddRequest(new Request(url));
            }
            SignalNewUrl();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(IList<string> urls)
        {
            destroyWhenExit = false;
            spawnUrl = false;
            if (startRequests != null)
            {
                startRequests.Clear();
            }
            foreach(var request in UrlUtils.ConvertToRequests(urls))
            {
                AddRequest(request);
            }
            var collectorPipeline = GetCollectorPipeline<T>();
            pipelines.Add(collectorPipeline);
            Run();
            spawnUrl = true;
            destroyWhenExit = true;
            return collectorPipeline.GetCollector();
        }

        protected virtual ICollectorPipeline<T> GetCollectorPipeline<T>() => (ICollectorPipeline<T>)
            new ResultItemsCollectorPipeline();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public T Get<T>(string url) where T : class
        {
            var urls = new List<string>
            {
                url
            };
            var resultItemses = GetAll<T>(urls);
            if (resultItemses != null && resultItemses.Count > 0)
            {
                return resultItemses.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
        public Spider AddRequest(Request[] requests)
        {
            foreach(var request in requests)
            {
                AddRequest(request);
            }
            SignalNewUrl();
            return this;
        }

        private void WaitNewUrl()
        {
            lock(newUrlLock)
            {
                if (exitWhenComplete)
                    return;
                System.Threading.Thread.Sleep(emptySleepTime);
            }
        }

        private void SignalNewUrl()
        {
            lock (newUrlLock)
            {

            }
        }

        public void Start()
        {
            RunAsync();
        }

        public void Close()
        {
            Dispose();
        }

        public void Stop()
        {
            if(stat == SpiderStatus.Running)
            {
                stat = SpiderStatus.Stop;
                Debug.WriteLine("Spider " + GetGuid() + " stop success!");
            }
        }

        public Spider Thread(int threadNum)
        {
            CheckIfRunning();
            this.threadNum = threadNum;
            if (threadNum <= 0)
            {
                throw new ArgumentException("threadNum should be more than one!",
                                            nameof(threadNum));
            }
            return this;
        }

        public bool IsExitWhenComplete()
        {
            return exitWhenComplete;
        }

        public Spider SetExitWhenComplete(bool exitWhenComplete)
        {
            this.exitWhenComplete = exitWhenComplete;
            return this;
        }

        public int GetThreadAlive()
        {
            if (threadPool == null)
            {
                return 0;
            }
            return threadPool.GetThreadAlive();
        }

        public bool IsSpawnUrl()
        {
            return spawnUrl;
        }

        public long GetPageCount()
        {
            return pageCount;
        }

        public Spider SetSpawnUrl(bool spawnUrl)
        {
            this.spawnUrl = spawnUrl;
            return this;
        }

        public List<ISpiderListener> GetSpiderListeners()
        {
            return spiderListeners;
        }

        public Spider SetSpiderListeners(List<ISpiderListener> spiderListeners)
        {
            this.spiderListeners = spiderListeners;
            return this;
        }

        public DateTime GetStartTime()
        {
            return startTime;
        }

        public IScheduler GetScheduler()
        {
            return scheduler;
        }

        /**
         * Set wait time when no url is polled.<br><br>
         *
         * @param emptySleepTime In MILLISECONDS.
         */
        public void SetEmptySleepTime(int emptySleepTime)
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
                return site.Domain;
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