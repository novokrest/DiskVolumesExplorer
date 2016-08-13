using DiskVolumesExplorer.Core;
using System.Collections.Generic;
using System.Linq;

namespace DiskVolumesExplorer.Client.Extensions
{
    internal static class DriveCollectionExtension
    {
        public static IReadOnlyCollection<DriveViewModel> ExtractDriveViewModelCollection(this IDriveCollection drives)
        {
            return drives.ExtractDriveViewModels().ToList();
        }

        private static IEnumerable<DriveViewModel> ExtractDriveViewModels(this IDriveCollection drives)
        {
            foreach(IDrive drive in drives)
            {
                yield return new DriveViewModel(drive);
            }
        }
    }
}
