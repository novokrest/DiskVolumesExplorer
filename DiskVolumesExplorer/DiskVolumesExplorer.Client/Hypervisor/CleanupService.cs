using System.Threading;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface ICleanUpService
    {
        Task CleanUpAsync();
    }

    internal class HypervisorConnectionCloser : ICleanUpService
    {
        private readonly IHypervisor _hypervisor;

        public HypervisorConnectionCloser(IHypervisor hypervisor)
        {
            _hypervisor = hypervisor;
        }

        public Task CleanUpAsync()
        {
            return _hypervisor.DisconnectAsync();
        }
    }
}
