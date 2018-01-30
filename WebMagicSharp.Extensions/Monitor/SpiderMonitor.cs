
using System;
using System.Collections.Generic;

using WebMagicSharp.Utils;

namespace WebMagicSharp.Monitor
{
    [Experimental]
    public class SpiderMonitor
    {

        object locked = new object();

        private static SpiderMonitor _instance;

        public static SpiderMonitor Instance => _instance ?? (_instance = new SpiderMonitor());

        public SpiderMonitor Register(Spider[] spiders)
        {
            lock(locked)
            {
                foreach (var spider in spiders)
                {
                    var monitorSpiderListener = new MonitorSpiderListener();
                    if (spider.GetSpiderListeners() == null)
                    {
                        spider.SetSpiderListeners(new List<ISpiderListener>
                        {
                            monitorSpiderListener
                        });
                    }
                    else
                        spider.GetSpiderListeners().Add(monitorSpiderListener);
                }
            }
            return this;
        }

        public SpiderMonitor Register(Spider spider)
        {
            return Register(new Spider[] { spider });
        }

    }

}
