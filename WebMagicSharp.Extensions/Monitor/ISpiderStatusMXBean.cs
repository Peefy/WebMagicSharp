using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Monitor
{
    public interface ISpiderStatusMXBean
    {
        string GetName();

        string GetStatus();

        int GetThread();

        int GetTotalPageCount();

        int GetLeftPageCount();

        int GetSuccessPageCount();

        int GetErrorPageCount();

        List<String> GetErrorPages();

        void Start();

        void Stop();

        DateTime GetStartTime();

        int GetPagePerSecond();
    }

}
