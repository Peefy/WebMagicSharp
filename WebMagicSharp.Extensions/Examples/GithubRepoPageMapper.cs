
using System;
using System.Collections.Generic;

using WebMagicSharp;
using WebMagicSharp.Model;
using WebMagicSharp.Processor;

namespace WebMagicSharp.Examples
{
    public class GithubRepoPageMapper : IPageProcessor
    {
        private Site site = Site.Me.SetRetryTimes(3).SetSleepTime(0);

        private PageMapper<GithubRepoPageMapper> githubRepoPageMapper =
            new PageMapper<GithubRepoPageMapper>();

        public Site GetSite()
        {
            return site;
        }

        public void Process(Page page)
        {
            page.AddTargetRequests(page.GetHtml().Links().Regex("(https://github\\.com/\\w+/\\w+)").All());
            page.AddTargetRequests(page.GetHtml().Links().Regex("(https://github\\.com/\\w+)").All());
            var githubRepo = githubRepoPageMapper.Get(page);
            if (githubRepo == null)
            {
                page.SetSkip(true);
            }
            else
            {
                page.PutField("repo", githubRepo);
            }
        }

        public static void Run()
        {
            Spider.Create(new GithubRepoPageMapper()).AddUrl("https://github.com/code4craft").Thread(5).Run();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~GithubRepoPageMapper() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}
