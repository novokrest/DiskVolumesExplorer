using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Mocks;

namespace DiskVolumesExplorer.Client.Hypervisor.Mocks
{
    internal class MockHypervisorService : IAsyncHypervisorService
    {
        public Task<IDiskCollection> GetDrivesAsync(string virtualMachineName)
        {
            var disks = MockVirtualMachinesRepository.GetVirtualMachine(virtualMachineName).Disks;
            return DelayTasks.WithResult(3000, disks);
        }

        public Task<IReadOnlyList<string>> GetVirtualMachinesAsync()
        {
           var virtualMachineNames = Enumerable.Repeat("Virtual Machine", 10).ToList();
            return DelayTasks.WithResult(3000, (IReadOnlyList<string>)virtualMachineNames);
        }
    }
}
