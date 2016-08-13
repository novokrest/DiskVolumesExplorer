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
        Task<IDriveCollection> LoadVirtualMachineDisks(string virtualMachineName);
    }

    internal sealed class VirtualMachineDisksLoader : IVirtualMachineDisksLoader
    {
        private readonly IHypervisorServiceProvider _hypervisorServiceProvider;

        public VirtualMachineDisksLoader(IHypervisorServiceProvider hypervisorServiceProvider)
        {
            _hypervisorServiceProvider = hypervisorServiceProvider;
        }

        public Task<IDriveCollection> LoadVirtualMachineDisks(string virtualMachineName)
        {
            var hypervisorService = _hypervisorServiceProvider.HypervisorService;
            return hypervisorService.GetVirtualMachineDisksAsync(virtualMachineName);
        }
    }
}
