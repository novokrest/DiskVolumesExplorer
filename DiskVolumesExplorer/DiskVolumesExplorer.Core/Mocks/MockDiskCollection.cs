using System.Collections.Generic;

namespace DiskVolumesExplorer.Core.Mocks
{
    class MockDiskCollection : MockCollection<MockDisk>, IDiskCollection
    {
        public MockDiskCollection(int disksCount = 5)
            : base(disksCount)
        {
            Disks = Elements;
        }

        public IReadOnlyCollection<IDisk> Disks { get; }
    }

    internal class MockDisk : CountableInstance, IDisk
    {
        public MockDisk()
        {
            Name = $"Disk #{Number}";
            Volumes = new MockVolumeCollection();
        }

        public string Name { get; }

        public IVolumeCollection Volumes { get; }
    }
}
