using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface ICleanUpService
    {
        Task CleanUpAsync();
    }

    internal class HypervisorConnectionCloser : ICleanUpService
    {
        public Task CleanUpAsync()
        {
            return Task.Delay(1000);
        }
    }
}
