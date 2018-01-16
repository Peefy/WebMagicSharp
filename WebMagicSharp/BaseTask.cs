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

        public BaseTask()
        {
            _site = new Site();
        }

        public BaseTask(Site site)
        {
            _site = site;
        }

        public string GetGuid()
        {
            var guid = _site.Domain;
            if(guid == null)
            {
                return Guid.NewGuid().ToString();
            }
            return guid;
        }

        public Site GetSite()
        {
            return _site;
        }
    }
}
