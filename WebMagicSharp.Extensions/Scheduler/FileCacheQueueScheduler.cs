using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

using WebMagicSharp.Scheduler;
using WebMagicSharp;

namespace WebMagicSharp.Scheduler
{
    public class FileCacheQueueScheduler : DuplicateRemovedScheduler, IMonitorableScheduler
    {
        private string filePath = Environment.CurrentDirectory;

        private string fileUrlAllName = ".urls.txt";

        private ITask task;

        private String fileCursor = ".cursor.txt";

        //private PrintWriter fileUrlWriter;

        //private PrintWriter fileCursorWriter;

        //private AtomicInteger cursor = new AtomicInteger();
        int cursor;

        //private AtomicBoolean inited = new AtomicBoolean(false);
        bool inited;

        //private BlockingQueue<Request> queue;
        Queue<Request> queue;

        //private Set<String> urls;
        private List<string> urls;

        //private ScheduledExecutorService flushThreadPool;

        public FileCacheQueueScheduler(string filePath)
        {
            if (!filePath.EndsWith("/", StringComparison.Ordinal) && 
                !filePath.EndsWith("\\", StringComparison.Ordinal))
            {
                filePath += "/";
            }
            this.filePath = filePath;
            initDuplicateRemover();
        }

        private void init(ITask task)
        {
            this.task = task;
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            readFile();
            initWriter();
            initFlushThread();
            inited.set(true);
            logger.info("init cache scheduler success");
        }

        private void initDuplicateRemover()
        {

        }

        public int GetLeftRequestsCount(ITask task)
        {
            
        }

        public int GetTotalRequestsCount(ITask task)
        {
            
        }

        public class DefaultDuplicateRemover : IDuplicateRemover
        {
            public int GetTotalRequestsCount(ITask task)
            {
                if (!inited.get())
                {
                    init(task);
                }
                return !urls.add(request.getUrl());
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


}
