using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using DiskVolumesExplorer.Client.Extensions;

namespace DiskVolumesExplorer.Client
{
    internal class DriveViewModel
    {
        public DriveViewModel(IDrive drive)
        {
            Title = drive.Title;
            Type = drive.Type;
            SizeInBytes = drive.SizeInBytes;
            Status = drive.Status;
            Volumes = drive.Volumes.ExtractVolumeViewModelCollection();
            Icon = IconType.DriveIcon;
        }

        public IconType Icon { get; }

        public string Title { get; }
        public string Type { get; }
        public ulong SizeInBytes { get; }
        public string Status { get; }

        public IReadOnlyCollection<VolumeViewModel> Volumes { get; }
    }
}
