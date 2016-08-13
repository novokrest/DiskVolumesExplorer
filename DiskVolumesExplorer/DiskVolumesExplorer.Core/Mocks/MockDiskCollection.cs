using System;
using System.Collections.Generic;

namespace DiskVolumesExplorer.Core.Mocks
{
    class MockDiskCollection : MockCollection<MockDisk, IDrive>, IDriveCollection
    {
        public MockDiskCollection(int disksCount = 5)
            : base(disksCount)
        {
        }

        public IDrive this[int index] => Elements[index];
    }

    internal class MockDisk : CountableInstance, IDrive
    {
        public MockDisk()
        {
            Title = $"Disk #{Number}";
            Type = "Basic";
            SizeInBytes = 1024;
            Status = "Online";
            Volumes = new MockVolumeCollection();
        }

        public string Title { get; }

        public ulong SizeInBytes { get; }

        public string Status { get; }

        public string Type { get; }

        public IVolumeCollection Volumes { get; }
    }
}
