using System;
using System.Threading.Tasks;

namespace EIPWeb.Features.HubT
{
    public interface ITaskAgent
    {
        void RunSync(TimeSpan duration);
        Task RunAsync(TimeSpan duration);
    }
}