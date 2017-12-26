using System;
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
    public class Spider : ITask
    {

        protected IDownloader downloader;

        protected List<IPipeline> pipelines = new List<IPipeline>();

        protected IPageProcessor pageProcessor;

        protected List<Request> startRequests;

        protected Site site;

        protected String uuid;

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
        object taskLocked = new object();

        //private Condition newUrlCondition = newUrlLock.newCondition();

        //private List<SpiderListener> spiderListeners;

        //private final AtomicLong pageCount = new AtomicLong(0);

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
            this.site = pageProcessor.getSite();
        }

        public string GetGuid()
        {
            throw new NotImplementedException();
        }

        public Site GetSite()
        {
            throw new NotImplementedException();
        }
    }
}