using System.Collections.Generic;

namespace DiskVolumesExplorer.Core.Mocks
{
    internal class MockVolume : CountableInstance, IVolume
    {
        public MockVolume()
        {
            Name = $"Volume {Number}";
            FreeSpace = 10000;
            Capacity = 3*FreeSpace;
        }

        public string Name { get; }
        public ulong FreeSpace { get; }
        public ulong Capacity { get; }
    }

    internal class MockVolumeCollection : MockCollection<MockVolume>, IVolumeCollection
    {
        public MockVolumeCollection(int volumesCount = 5)
            : base(volumesCount)
        {
            Volumes = Elements;
        }

        public IReadOnlyCollection<IVolume> Volumes { get; }
    }
}
