using System;

namespace EIPWeb.Features.HubT
{
    public interface ITaskScheduler
    {
        void AssignMeShortRunningTask(TimeSpan duration);
        void AssignMeLongRunningTask(TimeSpan duration);
    }
}