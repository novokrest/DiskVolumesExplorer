using DiskVolumesExplorer.Core;

namespace DiskVolumesExplorer.Client
{
    internal class DiskViewModel
    {
        public DiskViewModel(IDisk disk, SelectedVolumeViewModelNotifier selectedVolumeObserver)
        {
            Title = disk.Title;
            Type = disk.Type;
            SizeInBytes = disk.SizeInBytes;
            Status = disk.Status;
            Volumes = new VolumeViewModelCollection(disk.Volumes, selectedVolumeObserver);
            Icon = IconType.DriveIcon;
        }

        public IconType Icon { get; }

        public string Title { get; }
        public string Type { get; }
        public ulong SizeInBytes { get; }
        public string Status { get; }

        public VolumeViewModelCollection Volumes { get; }
    }
}
