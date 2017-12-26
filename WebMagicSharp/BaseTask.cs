using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp
{
    /// <summary>
    /// Base task.
    /// </summary>
    public class BaseTask : ITask
    {
        Site _site;

        private BaseTask()
        {

        }

        public BaseTask(Site site)
        {

        }

        public string GetGuid()
        {
            string guid = _site.getDomain();
            return guid ?? Guid.NewGuid().ToString();
        }

        public Site GetSite()
        {
            return _site;
        }
    }
}
