using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

using WebMagicSharp.Scheduler;
using WebMagicSharp;

namespace WebMagicSharp.Scheduler
{
    public class FileCacheQueueScheduler : DuplicateRemovedScheduler, IMonitorableScheduler, IDuplicateRemover
    {
        private string filePath = Environment.CurrentDirectory;

        private string fileUrlAllName = ".urls.txt";

        private ITask task;

        private String fileCursor = ".cursor.txt";

        private int cursor;

        private bool inited = false;

        Queue<Request> queue;

        private HashSet<string> urls;

        public FileCacheQueueScheduler(string filePath)
        {
            if (!filePath.EndsWith("/", StringComparison.Ordinal) && 
                !filePath.EndsWith("\\", StringComparison.Ordinal))
            {
                filePath += "/";
            }
            this.filePath = filePath;
            InitDuplicateRemover();
        }

        public void Init(ITask task)
        {
            this.task = task;
            if(!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            ReadFile();
            inited = true;
            Debug.WriteLine("init cache scheduler success");
        }

        private void ReadFile()
        {
            try
            {
                queue = new Queue<Request>();
                urls = new HashSet<string>();
                ReadCursorFile();
                ReadUrlFile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(nameof(FileCacheQueueScheduler) + " exception:" + ex);
            }
        }

        private void ReadUrlFile()
        {
            var lines = File.ReadAllLines(fileUrlAllName);
            var lineReaded = 0;
            foreach(var line in lines)
            {
                urls.Add(line.Trim());
                lineReaded++;
                if (lineReaded > cursor)
                    queue.Enqueue(new Request(line));
            }
        }

        private void ReadCursorFile()
        {
            try
            {
                var lines = File.ReadAllLines(fileCursor);
                foreach (var line in lines)
                {
                    cursor = int.Parse(line);
                }
            }
            catch
            {

            }

        }

        public string GetFileName(string filename)
        {
            return filePath + task.GetGuid() + filename;
        }

        private void InitDuplicateRemover()
        {
            DuplicatedRemover = this;
        }

        protected override void PushWhenNoDuplicate(Request request, ITask task)
        {
            if(!inited)
            {
                Init(task);
            }
            queue.Enqueue(request);
            File.AppendAllLines(fileUrlAllName, new string[] { request.GetUrl() });
        }

        public override Request Poll(ITask task)
        {
            if (!inited)
            {
                Init(task);
            }
            File.WriteAllLines(fileCursor, new string[] { (++cursor).ToString() });
            return queue.Dequeue();
        }

        public int GetLeftRequestsCount(ITask task)
        {
            return queue.Count;
        }

        public int GetDupTotalRequestsCount(ITask task)
        {
            return DuplicatedRemover.GetTotalRequestsCount(task);
        }

        public int GetTotalRequestsCount(ITask task)
        {
            return urls.Count;
        }

        public bool IsDuplicate(Request request, ITask task)
        {
            if (!inited)
            {
                Init(task);
            }
            return !urls.Add(request.GetUrl());
        }

        public void ResetDuplicateCheck(ITask task)
        {
            urls.Clear();
        }

    }
}
