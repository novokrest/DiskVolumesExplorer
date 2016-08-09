using System.Collections.Generic;

namespace DiskVolumesExplorer.Core
{
    public interface IVolumeCollection
    {
        IReadOnlyCollection<IVolume> Volumes { get; } 
    }

    public interface IVolume
    {
        string Name { get; }
        ulong FreeSpace { get; }
        ulong Capacity { get; }
    }
}
