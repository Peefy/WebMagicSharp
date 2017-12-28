using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Thread
{
    public class CountableThreadPool : IDisposable
    {
        public int ThreadNum { get; set; }

        public CountableThreadPool(int threadNum)
        {
            this.ThreadNum = threadNum;
        }

        public int getThreadAlive()
        {
            return 0;
        }

        public void Dispose()
        {
            
        }

        public bool isShutdown()
        {
            return true;
        }

        public void shutdown()
        {
            
        }

    }
}
