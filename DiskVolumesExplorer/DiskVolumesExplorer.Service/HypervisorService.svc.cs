using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Core.Mocks;

namespace DiskVolumesExplorer.Service
{
    public class MockHypervisorService : IHypervisorService
    {
        private static readonly string[] VirtualMachineNames = new[]
        {
            "Virtual Machine #1",
            "Virtual Machine #2",
            "Virtual Machine #3",
            "Virtual Machine #4"
        };

        public string[] GetVirtualMachineNames()
        {
            return VirtualMachineNames;
        }

        public IVirtualMachine GetVirtualMachine(string name)
        {
            return new MockVirtualMachine();
        }
    }
}
