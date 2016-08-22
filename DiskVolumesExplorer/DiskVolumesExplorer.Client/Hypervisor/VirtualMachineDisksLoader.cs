using DiskVolumesExplorer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IVirtualMachineDisksLoader
    {
        Task<IDiskCollection> LoadVirtualMachineDisks(string virtualMachineName);
    }

    internal sealed class VirtualMachineDisksLoader : IVirtualMachineDisksLoader
    {
        private readonly IAsyncHypervisorServiceProvider _hypervisorServiceProvider;

        public VirtualMachineDisksLoader(IAsyncHypervisorServiceProvider hypervisorServiceProvider)
        {
            _hypervisorServiceProvider = hypervisorServiceProvider;
        }

        public Task<IDiskCollection> LoadVirtualMachineDisks(string virtualMachineName)
        {
            var hypervisorService = _hypervisorServiceProvider.AsyncHypervisorService;
            return hypervisorService.GetDrivesAsync(virtualMachineName);
        }
    }
}
