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
        private readonly IAsyncHypervisorServiceProvider _hypervisorServiceProvider;

        public VirtualMachineNamesLoader(IAsyncHypervisorServiceProvider hypervisorServiceProvider)
        {
            _hypervisorServiceProvider = hypervisorServiceProvider;
        }

        public Task<IReadOnlyList<string>> LoadVirtualMachineNamesAsync()
        {
            IAsyncHypervisorService hypervisorService = _hypervisorServiceProvider.AsyncHypervisorService;
            return hypervisorService.GetVirtualMachinesAsync();

        }
    }
}
