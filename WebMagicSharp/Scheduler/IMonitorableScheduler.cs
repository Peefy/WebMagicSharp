using System;
namespace WebMagicSharp.Scheduler
{
    public interface IMonitorableScheduler
    {
        int GetLeftRequestsCount(ITask task);

        int GetTotalRequestsCount(ITask task);
    }
}
