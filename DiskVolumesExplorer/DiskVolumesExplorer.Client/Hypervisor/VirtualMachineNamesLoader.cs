using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IVirtualMachineNamesLoader
    {
        Task<IReadOnlyCollection<string>> LoadVirtualMachineNamesAsync();
    }

    internal class VirtualMachineNamesLoader : IVirtualMachineNamesLoader
    {
        private readonly IHypervisorServiceProvider _hypervisorServiceProvider;

        public VirtualMachineNamesLoader(IHypervisorServiceProvider hypervisorServiceProvider)
        {
            _hypervisorServiceProvider = hypervisorServiceProvider;
        }

        public Task<IReadOnlyCollection<string>> LoadVirtualMachineNamesAsync()
        {
            IHypervisor hypervisor = _hypervisorServiceProvider.Hypervisor;
            return hypervisor.GetVirtualMachineNamesAsync();

        }
    }
}
