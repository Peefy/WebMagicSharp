using System;
using System.Collections.Generic;
using System.Text;

namespace WebMagicSharp.Monitor
{
    public interface ISpiderStatusMXBean
    {
        string getName();

        string getStatus();

        int getThread();

        int getTotalPageCount();

        int getLeftPageCount();

        int getSuccessPageCount();

        int getErrorPageCount();

        List<String> getErrorPages();

        void start();

        void stop();

        DateTime getStartTime();

        int getPagePerSecond();
    }

}
