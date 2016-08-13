using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal interface IHypervisorService
    {
        Task<IReadOnlyList<string>> GetVirtualMachineNamesAsync();
        Task<IDriveCollection> GetVirtualMachineDisksAsync(string virtualMachineName);

    }
}
