using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client
{
    internal class VolumeViewModel
    {
        private readonly IVolume _volume;

        public VolumeViewModel(IVolume volume)
        {
            _volume = volume;
            Icon = IconType.VolumeIcon;
        }

        public IconType Icon { get; }

        public string Title => _volume.Title;
        public ulong FreeSpaceInBytes => _volume.FreeSpaceInBytes;
        public ulong CapacityInBytes => _volume.CapacityInBytes;
        public string FileSystem => _volume.FileSystem;
        public string Status => _volume.Status;
    }
}
