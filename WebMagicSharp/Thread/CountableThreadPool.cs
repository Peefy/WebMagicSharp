using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Thread
{
    public class CountableThreadPool : IDisposable
    {
        private int threadNum;

        public CountableThreadPool(int threadNum)
        {
            this.threadNum = threadNum;
        }

        public void Dispose()
        {
            
        }
    }
}
