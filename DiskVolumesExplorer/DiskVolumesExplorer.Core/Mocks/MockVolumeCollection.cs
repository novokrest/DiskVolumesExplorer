using System;

namespace DiskVolumesExplorer.Core.Mocks
{
    internal class MockVolume : CountableInstance, IVolume
    {
        public MockVolume()
        {
            Title = $"Volume {Number}";
            FreeSpaceInBytes = 10000;
            CapacityInBytes = 3*FreeSpaceInBytes;
            FileSystem = "NTFS";
            Status = "Healthy";
        }

        public string Title { get; }

        public ulong FreeSpaceInBytes { get; }

        public ulong CapacityInBytes { get; }

        public string FileSystem { get; }

        public string Status { get; }
    }

    internal class MockVolumeCollection : MockCollection<MockVolume, IVolume>, IVolumeCollection
    {
        public MockVolumeCollection(int volumesCount = 5)
            : base(volumesCount)
        {
        }

        public IVolume this[int index] => Elements[index];
    }
}
