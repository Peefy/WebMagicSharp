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

        public int GetThreadAlive()
        {
            return 0;
        }

        public void Dispose()
        {
            
        }

        public bool IsShutdown()
        {
            return true;
        }

        public void Shutdown()
        {
            
        }

    }
}
