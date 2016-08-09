using System.Threading;

namespace DiskVolumesExplorer.Core.Mocks
{
    public class MockVirtualMachine : IVirtualMachine
    {
        private static int _instanceCounter;

        public MockVirtualMachine()
        {
            int instanceNumber = Interlocked.Increment(ref _instanceCounter);
            Name = $"Virtual Machine #{instanceNumber}";
            Disks = new MockDiskCollection();
        }

        public string Name { get; }
        public IDiskCollection Disks { get; }
    }
}
