using System.Collections.Generic;

namespace DiskVolumesExplorer.Core
{
    public interface IDiskCollection
    {
        IReadOnlyCollection<IDisk> Disks { get; } 
    }

    public interface IDisk
    {
        string Name { get; }
        IVolumeCollection Volumes { get; }
    }
}
