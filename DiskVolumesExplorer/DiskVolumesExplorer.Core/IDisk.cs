using System.Collections.Generic;

namespace DiskVolumesExplorer.Core
{
    public interface IDisk
    {
        string Title { get; }
        string Type { get; }
        ulong SizeInBytes { get; }
        string Status { get; }

        IVolumeCollection Volumes { get; }
    }

    public interface IDiskCollection : IEnumerable<IDisk>
    {
        IDisk this[int index] { get; }
    }
}
