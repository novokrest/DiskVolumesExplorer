using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IVirtualMachineNamesLoader
    {
        Task<IReadOnlyList<string>> LoadVirtualMachineNamesAsync();
    }

    internal class VirtualMachineNamesLoader : IVirtualMachineNamesLoader
    {
        private readonly IHypervisorServiceProvider _hypervisorServiceProvider;

        public VirtualMachineNamesLoader(IHypervisorServiceProvider hypervisorServiceProvider)
        {
            _hypervisorServiceProvider = hypervisorServiceProvider;
        }

        public Task<IReadOnlyList<string>> LoadVirtualMachineNamesAsync()
        {
            IHypervisorService hypervisorService = _hypervisorServiceProvider.HypervisorService;
            return hypervisorService.GetVirtualMachineNamesAsync();

        }
    }
}
