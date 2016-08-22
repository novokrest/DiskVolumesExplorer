using System.Threading;

namespace DiskVolumesExplorer.Core.Mocks
{
    public class MockVirtualMachine : IVirtualMachine
    {
        public MockVirtualMachine(string virtualMachineName)
        {
            Name = $"Virtual Machine #{virtualMachineName}";
            Disks = new MockDiskCollection();
        }

        public string Name { get; }
        public IDiskCollection Disks { get; }
    }
}
