using System.Threading;

namespace DiskVolumesExplorer.Core.Mocks
{
    class MockVirtualMachine : IVirtualMachine
    {
        private static int _instanceCounter;

        public MockVirtualMachine()
        {
            int instanceNumber = Interlocked.Increment(ref _instanceCounter);
            Name = $"Virtual Machine #{instanceNumber}";
        }

        public string Name { get; }
    }
}
