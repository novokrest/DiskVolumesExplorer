using System.Collections.Generic;

namespace DiskVolumesExplorer.Core
{
    public interface IVolume
    {
        string Title { get; }
        ulong FreeSpaceInBytes { get; }
        ulong CapacityInBytes { get; }
        string FileSystem { get; }
        string Status { get; }
    }

    public interface IVolumeCollection : IEnumerable<IVolume>
    {
        IVolume this[int index] { get; }
    }
}
