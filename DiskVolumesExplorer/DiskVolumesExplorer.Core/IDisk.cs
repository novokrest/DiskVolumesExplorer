using System.Collections.Generic;

namespace DiskVolumesExplorer.Core
{
    public interface IDrive
    {
        string Title { get; }
        string Type { get; }
        ulong SizeInBytes { get; }
        string Status { get; }

        IVolumeCollection Volumes { get; }
    }

    public interface IDriveCollection : IEnumerable<IDrive>
    {
        IDrive this[int index] { get; }
    }
}
